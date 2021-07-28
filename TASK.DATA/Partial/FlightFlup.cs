using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class FLightFlup
    {
        public static void Insert(List<FLightFlup> listFlight)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                if (listFlight.Count > 0)
                {
                    dbConext.FLightFlups.InsertAllOnSubmit(listFlight);
                }
                dbConext.SubmitChanges();
            }
        }
        public static void Update(List<FLightFlup> flights)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                foreach (var item in flights)
                {
                    var flightDb = dbConext.FLightFlups.Where(c => c.ID == item.ID).First();
                   // flightDb.TotalULD = item.TotalULD;
                    flightDb.FlightStatus = item.FlightStatus;
                    flightDb.UldLoaded = item.UldLoaded;
                    flightDb.STD = item.STD;
                    flightDb.ETD = item.ETD;
                }

                dbConext.SubmitChanges();
            }
        }
        public static void UpdateDeleted(List<FLightFlup> flights)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                foreach (var item in flights)
                {
                    var flightDb = dbConext.FLightFlups.SingleOrDefault(c => c.ID == item.ID);
                    flightDb.FlightDeleted = 1;
                }

                dbConext.SubmitChanges();
            }
        }
        public static List<string> GetAllToDay()
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return dbConext.FLightFlups.Select(c=>c.FlightID).ToList();
            }
        }
        public static List<FLightFlup> GetAllOpen()
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return dbConext.FLightFlups.Where(c=>c.FlightStatus==0 && c. FlightDeleted == 0).ToList();
            }
        }
        public static FLightFlup GetByID(string id)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return dbConext.FLightFlups.FirstOrDefault(c => c.FlightID == id);
            }
        }
        public static FLightFlup GetByCode(string code)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return dbConext.FLightFlups.FirstOrDefault(c => c.BookingFlight == code);
            }
        }
    }
}
