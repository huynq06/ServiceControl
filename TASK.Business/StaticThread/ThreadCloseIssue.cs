using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Services;

namespace TASK.Business.StaticThread
{
    public class ThreadCloseIssue : ThreadBase
    {
        public ThreadCloseIssue(WakeupTimer wakeup)
            : base(wakeup)
        {
        }
        protected override void DoWork()
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                if (WakeupCondition)
                {
                    CloseIssueService.CloseIssue();
                }
               
            }
            catch
            { }
        }
        public override bool WakeupCondition
        {

            get
            {
                DateTime currentTime = DateTime.Now;
                return ((DateTime.Now.Hour == 05 &&
    DateTime.Now.Minute == 05 &&
    DateTime.Now.Second == 0) || (DateTime.Now.Hour == 05 &&
    DateTime.Now.Minute == 07 &&
    DateTime.Now.Second == 0) || (DateTime.Now.Hour == 05 &&
    DateTime.Now.Minute == 09 &&
    DateTime.Now.Second == 0) ||(DateTime.Now.Hour == 05 &&
    DateTime.Now.Minute == 11 &&
    DateTime.Now.Second == 0) ||(DateTime.Now.Hour == 05 &&
    DateTime.Now.Minute == 13 &&
    DateTime.Now.Second == 0)|| (DateTime.Now.Hour == 05 &&
    DateTime.Now.Minute == 15 &&
    DateTime.Now.Second == 0)|| (DateTime.Now.Hour == 05 &&
    DateTime.Now.Minute == 17));
            }
        }
    }
}
