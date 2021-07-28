using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

namespace TASK.Business.StaticThread
{
    public class CheckConditionALSWManagement :  ThreadBase
    {
        public object _locker = new object();
        public CheckConditionALSWManagement()
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
                Services.CheckProcessAwbService.CheckConfirmAwbExpand();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "Issue", "Issue");
                Log.WriteLog(ex, "CheckConditionALSWManagement");
            }
        }
    }
}
