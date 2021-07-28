using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

namespace TASK.Business.StaticThread
{
    public class PXKThreadManagement : ThreadBase
    {
        public object _locker = new object();
        public PXKThreadManagement()
            : base()
        {
        }

        protected override int DUETIME
        {
            get
            {
                return 10000;
            }
        }
        protected override void DoWork()
        {
            try
            {
                Services.PXKService.CheckProcessPXK();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "Update CutOfftime", "Issue");
                Log.WriteLog(ex, "SHC.txt");
            }
        }
    }
}
