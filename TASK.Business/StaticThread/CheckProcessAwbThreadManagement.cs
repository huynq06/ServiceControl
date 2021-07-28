using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

namespace TASK.Business.StaticThread
{
    public class CheckProcessAwbThreadManagement :  ThreadBase
    {
        public object _locker = new object();
        public CheckProcessAwbThreadManagement()
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
                Services.CheckProcessAwbService.CheckProcessAwb();
              
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "AsyncHermesService", "TransitionThread");
                Log.WriteLog(ex, "CheckProcessAwb.txt");
            }
        }
    }
}
