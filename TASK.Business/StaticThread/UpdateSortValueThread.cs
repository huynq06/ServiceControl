using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

namespace TASK.Business.StaticThread
{
    public class UpdateSortValueThread : ThreadBase
    {
        public object _locker = new object();
        public UpdateSortValueThread()
            : base()
        {
        }

        protected override int DUETIME
        {
            get
            {
                return 600000;
            }
        }
        protected override void DoWork()
        {
            try
            {
                Services.UpdateSortValueService.UpdateSortValueVer2();
                Services.UpdateSortValueService.UpdateSortValueCallTruck();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "AsyncHermesService", "TransitionThread");
                Log.WriteLog(ex, "UpdateSortValueThread.txt");
            }
        }
    }
}
