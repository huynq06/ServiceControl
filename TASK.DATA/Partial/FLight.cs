using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class Flight
    {
        public static void Insert(List<Flight> listFlight)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                if (listFlight.Count > 0)
                {
                    dbConext.Flights.InsertAllOnSubmit(listFlight);
                }
                dbConext.SubmitChanges();
            }
        }
        public static void Update(List<Flight> listFlight)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                foreach (var flight in listFlight)
                {
                    var flightDb = dbConext.Flights.FirstOrDefault(c => c.ID == flight.ID);
                    flightDb.LandedDate = flight.LandedDate;
                    flightDb.FLUI_LANDED_DATE = flight.FLUI_LANDED_DATE;
                    flightDb.FLUI_LANDED_TIME = flight.FLUI_LANDED_TIME;
                    flightDb.Status = flight.Status;

                }
                dbConext.SubmitChanges();
            }

        }
        public static void CloseFlight(Flight flight)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
               
                    var flightDb = dbConext.Flights.FirstOrDefault(c => c.ID == flight.ID);
                flightDb.Status = flight.Status;
                flightDb.FinishTime = flight.FinishTime;
                
                dbConext.SubmitChanges();
            }

        }
        public static void UpdateLandedTime(Flight flight)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {

                var flightDb = dbConext.Flights.FirstOrDefault(c => c.ID == flight.ID);
                flightDb.LandedDate = flight.LandedDate;
                dbConext.SubmitChanges();
            }

        }
        public static Flight GetFlightByCondition(string flightNumber,int scheduleDate, int scheduleTime)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {

                var flightDb = dbConext.Flights.FirstOrDefault(c => c.FlightNumber==flightNumber && c.FLUI_SCHEDULE_DATE == scheduleDate && c.FLUI_SCHEDULE_TIME == scheduleTime);
                return flightDb;
            }
        }
        public static List<Flight> GetAll()
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return dbConext.Flights.Where(c=>c.Created > DateTime.Now.AddDays(-2)).ToList();
            }
        }
        public static List<Flight> GetAllBQL()
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return dbConext.Flights.Where(c => c.Created > DateTime.Now.AddDays(-2) && c.Status == false).ToList();
            }
        }
        public static List<Flight> GetAllSHC()
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return dbConext.Flights.Where(c => c.Created > DateTime.Now.AddDays(-2) && c.Status ==false).ToList();
            }
        }
        public static Flight GetByGuid(Guid id)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return dbConext.Flights.SingleOrDefault(c => c.FlightID == id);
            }
        }
    }
}
