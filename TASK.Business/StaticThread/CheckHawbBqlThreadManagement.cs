using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

namespace TASK.Business.StaticThread
{
    public class CheckHawbBqlThreadManagement : ThreadBase
    {
        public object _locker = new object();
        public CheckHawbBqlThreadManagement()
            : base()
        {
        }

        protected override int DUETIME
        {
            get
            {
                return 60000;
            }
        }
        protected override void DoWork()
        {
            try
            {
                Services.CheckHawbBQLService.CheckHawbBqlOpenFlight();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "Issue", "Issue");
                Log.WriteLog(ex, "Issue");
            }
        }
    }
}
