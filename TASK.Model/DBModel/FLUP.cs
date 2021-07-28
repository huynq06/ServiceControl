using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.DBModel
{
    public class FLUP
    {
        public string FLIGHT_ID { set; get; }
        public string FLIGHT_NO { set; get; }
        public string FLIGHT_TYPE { set; get; }
        public string FlightAirCraffType { set; get; }
        public int TotalULD { set; get; }
        public int LoadedULD { set; get; }
        public DateTime? STD { set; get; }
        public DateTime? ETD { set; get; }
        public int FlightStatus { set; get; }
    }
}
