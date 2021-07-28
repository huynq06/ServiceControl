using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class AWBByULD
    {
        public static void Insert(List<AWBByULD> awbs)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                if (awbs.Count > 0)
                {
                    dbConext.AWBByULDs.InsertAllOnSubmit(awbs);
                }
                dbConext.SubmitChanges();
            }
        }
        //public static List<AWBByULD> GetAll()
        //{
        //    using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
        //    {
        //        return dbConext.AWBByULDs.ToList();
        //    }
        //}
        public static List<AWBByULD> GetAllCheck(List<Flight> flights)
        {
            List<AWBByULD> listAwb = new List<AWBByULD>();
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                foreach (var flight in flights)
                {
                    List<AWBByULD> listHawbByFlight = dbConext.AWBByULDs.Where(c => c.Flight_ID == flight.FlightID && c.CheckValue == 1 && c.Process == 1).ToList();
                    if (listHawbByFlight.Count > 0)
                    {
                        listAwb.AddRange(listHawbByFlight);
                    }
                }
                return listAwb;
            }
        }
        public static AWBByULD GetByID(Guid ID)
        {

            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return dbConext.AWBByULDs.SingleOrDefault(c => c.AWB_ID == ID);
            }
        }
        public static List<AWBByULD> GetAllSHC(List<Flight> flights)
        {
            List<AWBByULD> listAwb = new List<AWBByULD>();
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                foreach (var flight in flights)
                {
                    List<AWBByULD> listHawbByFlight = dbConext.AWBByULDs.Where(c => c.Flight_ID == flight.FlightID && c.CheckValue == 1 && c.Process == 0).ToList();
                    if (listHawbByFlight.Count > 0)
                    {
                        listAwb.AddRange(listHawbByFlight);
                    }
                }
                return listAwb;
            }
           
        }
        public static void UpdateAWBStatus(AWBByULD awb)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {

                var awbDB = dbConext.AWBByULDs.FirstOrDefault(c => c.ID == awb.ID);
                awbDB.Process = awb.Process;
                dbConext.SubmitChanges();
            }
        }
    }
}
