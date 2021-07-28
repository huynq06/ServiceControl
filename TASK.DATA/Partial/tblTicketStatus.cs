using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class tblTicketStatus
    {
        public static List<tblTicketStatus> GetAllToDay()
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                List <tblTicketStatus> listTicket =  dbConext.tblTicketStatus.Where(c => c.ActionDateTime > DateTime.Now.AddHours(-12) && c.ActionStatus == 1).ToList();
                return listTicket.Select(c => new tblTicketStatus()
                {
                    TicketUID = c.TicketUID,
                    ActionCode = c.ActionCode,
                    ActionValue = c.ActionValue,
                    BienSoXe = c.BienSoXe,
                    TicketType = c.TicketType,
                    ActionDateTime = c.ActionDateTime
                }).Distinct().ToList();
            }
        }
        public static List<tblTicketStatus> GetBySynID(Guid id)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblTicketStatus.Where(c => c.TicketUID==id).ToList();
            }
        }
        public static tblTicketStatus CheckCloseTruck(Guid id)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblTicketStatus.FirstOrDefault(c => c.TicketUID == id);
            }
        }
        public static List<tblTicketStatus> GetListMonthlyAroundOneHour(DateTime dateCheck)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblTicketStatus.Where(c => c.ActionDateTime > dateCheck && c.ActionStatus == 1 && c.ActionMessage == "VÉ THÁNG HỢP LỆ").ToList();
            }
        }
    }
}
