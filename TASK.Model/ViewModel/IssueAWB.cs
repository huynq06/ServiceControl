using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.ViewModel
{
    public class IssueAWB
    {
        public int issueId { set; get; }
        public string issueKey { set; get; }
        public string AWB { set; get; }
        public int status { set; get; }
    }
}
