using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.ViewModel
{
    public class fields
    {
        public string summary { set; get; }
        public issuetype issuetype { set; get; }
        public DateTime? duedate { set; get; }
        public DateTime? created { set; get; }
        public object reporter { set; get; }
        public object customfield_10120 { set; get; }
        public status status { set; get; }
    }
}
