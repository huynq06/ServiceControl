using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Model.ViewModel;
using Newtonsoft.Json;
using TASK.Settings;
using DATAACCESS;
namespace TASK.Services
{
    public static class UpdateAwbAlsxImformationService
    {
        public static void UpdateProcess()
        {
            try
            {
                DateTime dt = DateTime.Now.AddDays(-1);
                List<EXP_AWB> ListAwbCheck = EXP_AWB.GetAllToday(dt);
                var listAwb4Update = new List<EXP_AWB>();
                foreach(var item in ListAwbCheck)
                {
                    string user = "";
                    item.UPDATED_BOOKING = new GetBookingTimeAccess().GetBookingTime(item.AWBID.ToString(),ref user);
                    if(item.UPDATED_BOOKING.HasValue && item.UPDATED_BOOKING != DateTime.MinValue)
                    {
                        item.USER_ACTION = user;
                        listAwb4Update.Add(item);
                    }
                       
                }

                if (listAwb4Update.Count > 0)
                {
                    EXP_AWB.UpdateBookingTime(listAwb4Update);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
