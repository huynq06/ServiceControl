using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Settings;

namespace TASK.Business.StaticThread
{
    public class LoggerThreadManage : ThreadBase
    {
        public LoggerThreadManage(WakeupTimer wakeup)
            : base(wakeup)
        {
        }

        protected override int DUETIME
        {
            get
            {
                return AppSetting.UpdateDataInterval;
            }
        }

        protected override void DoWork()
        {
            try
            {
                Log.BulkInsert();
            }
            catch
            { }
        }
        public override bool WakeupCondition
        {
            
            get
            {
                int count = Log.QueueCount();
                return (Log.QueueCount() > 0);
            }
        }
    }
}
