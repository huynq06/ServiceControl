using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class ULDByFlight
    {
        public static void Insert(List<ULDByFlight> ulds)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                if (ulds.Count > 0)
                {
                    dbConext.ULDByFlights.InsertAllOnSubmit(ulds);
                }
                dbConext.SubmitChanges();
            }
        }
        public static void Update(List<ULDByFlight> ulds)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                foreach (var uld in ulds)
                {
                    var uldDb = dbConext.ULDByFlights.FirstOrDefault(c => c.ID == uld.ID);

                }
                dbConext.SubmitChanges();
            }

        }
        public static void UpdateStatus(ULDByFlight uld)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {

                var uldDB = dbConext.ULDByFlights.FirstOrDefault(c => c.ID == uld.ID);
                uldDB.NotifyID = uld.NotifyID;
                uldDB.NotifyMessage = uld.NotifyMessage;
                dbConext.SubmitChanges();
            }
        }
        public static List<ULDByFlight> GetAll()
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return dbConext.ULDByFlights.ToList();
            }
        }
        public static List<ULDByFlight> GetByFlight(Guid id)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return dbConext.ULDByFlights.Where(c=>c.Flight_ID==id).ToList();
            }
        }
        public static List<ULDByFlight> GetULDProcessing()
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return dbConext.ULDByFlights.Where(p=>p.Status==1).ToList();
            }
        }

    }
}
