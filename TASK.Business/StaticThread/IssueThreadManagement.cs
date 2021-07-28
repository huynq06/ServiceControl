using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

using TASK.Settings;

namespace TASK.Business.StaticThread
{
    public class IssueThreadManagement : ThreadBase
    {
        public object _locker = new object();
        public IssueThreadManagement()
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
                Services.CheckProcessAwbService.CheckConfirmAwb();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "Issue", "Issue");
                Log.WriteLog(ex, "Issue");
            }
        }
        
    }
}
