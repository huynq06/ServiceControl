using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.DBModel
{
    public class VCTHermes
    {
        public string LABS_IDENT_NO { set; get; }
        public string LABS_AWB { set; get; }
        public int LABS_QUANTITY_BOOKED { set; get; }
        public double LABS_WEIGHT_BOOKED { set; get; }
        public string AGENT_NAME { set; get; }
        public string BOOKING_FLIGHT { set; get; }
        public string FLUP_TYPE { set; get; }
        public DateTime LABS_CREATED_AT { set; get; }
        public DateTime? CutoffTime { set; get; }
        public DateTime VCT_DATE { set; get; }
        public string FLUP_ID { set; get; }

    }
}
