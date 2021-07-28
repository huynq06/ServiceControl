using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class EXP_AWB
    {
        public static List<EXP_AWB> GetAllToday(DateTime dt)
        {
            using (ConnectAlsContextDataContext ct = new ConnectAlsContextDataContext(AppSetting.ConnectionStringConnectALS))
            {
                return ct.EXP_AWBs.Where(c => c.CREATED_AT > dt && !c.UPDATED_BOOKING.HasValue).ToList();
            }
        }
        public static void UpdateBookingTime(List<EXP_AWB> awbs)
        {
            using (ConnectAlsContextDataContext dbConext = new ConnectAlsContextDataContext(AppSetting.ConnectionStringConnectALS))
            {
                foreach (var item in awbs)
                {
                    var awbDB = dbConext.EXP_AWBs.SingleOrDefault(c => c.AWBID == item.AWBID);
                    awbDB.UPDATED_BOOKING = item.UPDATED_BOOKING;
                    awbDB.SHC = item.SHC;
                    awbDB.USER_ACTION = item.USER_ACTION;
                }

                dbConext.SubmitChanges();
            }
        }
    }
}
