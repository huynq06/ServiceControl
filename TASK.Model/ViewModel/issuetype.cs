using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.ViewModel
{
    public class issuetype
    {
        public string self { set; get; }
        public int id { set; get; }
        public string description { set; get; }
        public string iconUrl { set; get; }
        public string name { set; get; }
        public bool subtask { set; get; }
        public string avatarId { set; get; }

    }
}
