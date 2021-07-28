using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

using TASK.Settings;

namespace TASK.Business.StaticThread
{
    public class CreateFlightThreadManagement : ThreadBase
    {
        public object _locker = new object();
        public CreateFlightThreadManagement()
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
                Services.FlightControlService.GetFlight();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "Issue", "Issue");
                Log.WriteLog(ex, "Issue");
            }
        }
    }
}
