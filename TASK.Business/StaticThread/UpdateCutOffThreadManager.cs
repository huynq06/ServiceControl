using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

namespace TASK.Business.StaticThread
{
    class UpdateCutOffThreadManager : ThreadBase
    {
        public object _locker = new object();
        public UpdateCutOffThreadManager()
            : base()
        {
        }

        protected override int DUETIME
        {
            get
            {
                return 20000;
            }
        }
        protected override void DoWork()
        {
            try
            {
                Services.CutOffTimeService.ProcessCutOffTime();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "Update CutOfftime", "Issue");
                Log.WriteLog(ex, "CutOfftime.txt");
            }
        }
   
    }
}
