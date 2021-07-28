using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Services;

namespace TASK.Business.StaticThread
{
    public class NotifyOverTimeThreadManagement : ThreadBase
    {
        public object _locker = new object();
        public NotifyOverTimeThreadManagement()
            : base()
        {
        }
        protected override int DUETIME
        {
            get
            {
                return 5000;
            }
        }
        protected override void DoWork()
        {
            try
            {
                NotifyOverTimeService.UpdateOverTime();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "AsyncHermesService", "TransitionThread");
                Log.WriteLog(ex, "UpdateOverTime.txt");
            }
        
        }
        public override bool WakeupCondition
        {

            get
            {
                return NotifyOverTimeService.CheckOverTime();
            }
        }
    }
}
