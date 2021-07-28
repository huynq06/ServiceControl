using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class tblDangKyVaoRa
    {
        public static List<tblDangKyVaoRa> GetTruckSecondFloor()
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblDangKyVaoRas.Where(c=>c.NgayGioVaoThuc.Value > DateTime.Now.AddHours(-4) && c.NgayGioRa==null && c.Floor==2).ToList();
            }
        }
        public static List<tblDangKyVaoRa> GetTruckFirstFloor()
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblDangKyVaoRas.Where(c => c.NgayGioVaoThuc.Value > DateTime.Now.AddHours(-4) && c.NgayGioRa == null && c.Floor == 1).ToList();
            }
        }
        public static List<tblDangKyVaoRa> GetTruckBuyTicket(DateTime dateCheck)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblDangKyVaoRas.Where(c=>c.NgayGioVao> dateCheck).ToList();
            }
        }
    }
}
