using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.DBModel
{
    public class LABS
    {
        public string LABS_IDENT_NO { set; get; }
        public string AWB { set; get; }
        public double LABS_QUANTITY_DEL { set; get; }
        public double LABS_QUANTITY_BOOKED { set; get; }
        public string Scale_Status { set; get; }
        public string GetIn_Status { set; get; }
    }
}
