using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.ViewModel
{
    public class issue
    {
        public int id { set; get; }
        public string self { set; get; }
        public string key { set; get; }
        public fields fields { set; get; }
    }
}
