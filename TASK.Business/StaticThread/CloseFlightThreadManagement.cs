using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Business.StaticThread
{
    public class CloseFlightThreadManagement : ThreadBase
    {
        public object _locker = new object();
        public CloseFlightThreadManagement()
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
                Services.CheckCloseFlightService.CheckCloseFlight();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "Issue", "Issue");
                DATA.Log.WriteLog(ex, "Issue");
            }
        }
    }
}
