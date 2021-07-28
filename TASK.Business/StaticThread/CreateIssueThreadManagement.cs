using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

using TASK.Settings;


namespace TASK.Business.StaticThread
{
    public class CreateIssueThreadManagement : ThreadBase
    {
        public object _locker = new object();
        public CreateIssueThreadManagement()
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
                Services.CreateIssueService.GetListVCTVer2();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "Issue", "Issue");
                Log.WriteLog(ex, "Issue");
            }
        }
    }
}
