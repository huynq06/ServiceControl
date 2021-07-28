using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.ViewModel
{
    public class PXKHermesViewModel
    {
        public string PXKNo { set; get; }
        public string VCTNo { set; get; }
        public string AWB { set; get; }
        public string Hawb { set; get; }
        public int quantity { set; get; }
        public int GroupNumber { set; get; }
        public string weight { set; get; }
        public string Location { set; get; }
        public DateTime Created { set; get; }
    }
}
