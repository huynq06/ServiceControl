using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using DATAACCESS;

namespace TASK.Services
{
    public class CheckCloseFlightService
    {
        public static void CheckCloseFlight()
        {
            //lay danh sach flight đang open
            List<Flight> flights = Flight.GetAll().Where(c=>c.Status==false).ToList();
            if (flights.Count > 0)
            {
                foreach (var flight in flights)
                {
                    bool check = new CheckCloseFlightAccess().CheckCloseFlight(flight);
                    if (check)
                    {
                        Flight.CloseFlight(flight);
                    }
                }
            }
          


        }
    }
}
