using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.ViewModel
{
    public class data_Issue
    {
        public int size { set; get; }
        public int start { set; get; }
        public int limit { set; get; }
        public bool isLastPage { set; get; }
        public object _links { set; get; }
        public List<issue> values { set; get; }

    }
}
