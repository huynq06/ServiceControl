using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

namespace TASK.Business.StaticThread
{
    public class TransitionsIssueThreadManager : ThreadBase
    {
        public object _locker = new object();
        public TransitionsIssueThreadManager()
            : base()
        {
        }

        protected override int DUETIME
        {
            get
            {
                return 60000;
            }
        }
        protected override void DoWork()
        {
            try
            {
                //Services.AsyncHermesService.CheckProcessAWB();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "AsyncHermesService", "TransitionThread");
                Log.WriteLog(ex, "AsyncHermesService.txt");
            }
        }
    
    }
}
