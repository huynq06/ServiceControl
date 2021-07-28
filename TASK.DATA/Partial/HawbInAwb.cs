using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class HawbInAwb
    {
        public static void Insert(List<HawbInAwb> hawbs)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                if (hawbs.Count > 0)
                {
                    dbConext.HawbInAwbs.InsertAllOnSubmit(hawbs);
                }
                dbConext.SubmitChanges();
            }
        }
        public static List<HawbInAwb> GetAll()
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return dbConext.HawbInAwbs.ToList();
            }
        }
        public static List<HawbInAwb> GetAllBQL(List<Flight> flights)
        {
            List<HawbInAwb> listHawb = new List<HawbInAwb>();
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                foreach (var flight in flights)
                {
                    List<HawbInAwb> listHawbByFlight = dbConext.HawbInAwbs.Where(c => c.FlightID == flight.FlightID && c.Bql == true).ToList();
                    if (listHawbByFlight.Count > 0)
                    {
                        listHawb.AddRange(listHawbByFlight);
                    }
                }
                return listHawb;
            }
        }
        public static List<HawbInAwb> GetSHC()
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return dbConext.HawbInAwbs.ToList();
            }
        }
        public static List<HawbInAwb> GetAllSHC(List<Flight> flights)
        {
            List<HawbInAwb> listHawb = new List<HawbInAwb>();
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                foreach(var flight in flights)
                {
                    List<HawbInAwb>  listHawbByFlight = dbConext.HawbInAwbs.Where(c => c.FlightID == flight.FlightID && c.CheckValue == 1 && c.Process == 0).ToList();
                    if(listHawbByFlight.Count > 0)
                    {
                        listHawb.AddRange(listHawbByFlight);
                    }
                }
                return listHawb;
            }
        }
        public static List<HawbInAwb> GetAllChecked(List<Flight> flights)
        {
            List<HawbInAwb> listHawb = new List<HawbInAwb>();
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                foreach (var flight in flights)
                {
                    List<HawbInAwb> listHawbByFlight = dbConext.HawbInAwbs.Where(c => c.FlightID == flight.FlightID && c.CheckValue == 1 && c.Process == 1).ToList();
                    if (listHawbByFlight.Count > 0)
                    {
                        listHawb.AddRange(listHawbByFlight);
                    }
                }
                return listHawb;
            }
        }
        public static void UpdateBql(HawbInAwb hawb)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {

                var hawDb = dbConext.HawbInAwbs.FirstOrDefault(c => c.ID == hawb.ID);
                hawDb.CheckValue = 1;
                dbConext.SubmitChanges();
            }
        }
        public static void UpdateHAWBStatus(HawbInAwb hawb)
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {

                var hawbDB = dbConext.HawbInAwbs.FirstOrDefault(c => c.ID == hawb.ID);
                hawbDB.Process = hawb.Process;
                dbConext.SubmitChanges();
            }
        }
    }
}
