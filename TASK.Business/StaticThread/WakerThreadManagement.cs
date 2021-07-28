using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Settings;

namespace TASK.Business.StaticThread
{
    class WakerThreadManagement : BaseTask
    {
        protected override int INFINITE_NEXT_TIME
        {
            get
            {
                return AppSetting.WakeUpInterval;
            }
        }

        protected override void DoWork()
        {
            try
            {
                //if (StaticThreadManage.Logger.Sleeping && Log.QueueCount() > 0)
                //{
                //    StaticThreadManage.Logger.WakeUp();
                //}
            }
            catch (Exception ex)
            {
                HandleException(ex, AppSetting.WarningType.application_error);
            }
        }
    }
}
