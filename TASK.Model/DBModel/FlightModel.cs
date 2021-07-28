using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.DBModel
{
    public class FlightModel
    {
        public string FlightNumber { set; get; }
        public DateTime Schedule { set; get; }
        public string Status { set; get; }
        public DateTime Created { set; get; }
        public bool FlightStatus { set; get; }
        public DateTime? FFM { set; get; }
        public DateTime? LandedTime { set; get; }
        public int? FLUI_SCHEDULE_DATE { set; get; }
        public int? FLUI_SCHEDULE_TIME { set; get; }
        public int? FLUI_LANDED_DATE { set; get; }
        public int? FLUI_LANDED_TIME { set; get; }
        public string FlightLetterCode { set; get; }
        public string FlightType { set; get; }
        public string FlightAirCraftType { set; get; }
    }
}
