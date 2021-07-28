using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Services;

namespace TASK.Business.StaticThread
{
    public class NotifycationThresholdThreadManagement : ThreadBase
    {
        public object _locker = new object();
        public NotifycationThresholdThreadManagement()
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
                NotifyThresholdService.UpdateThreshold();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "AsyncHermesService", "TransitionThread");
                Log.WriteLog(ex, "UpdateThreshold.txt");
            }
          
           
        }
      
    }
}
