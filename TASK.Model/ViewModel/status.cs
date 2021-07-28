using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.ViewModel
{
    public class status
    {
        public string self { set; get; }
        public string description { set; get; }
        public string iconUrl { set; get; }
        public string name { set; get; }
        public int id { set; get; }
        public statusCategory statusCategory { set; get; }

    }
}
