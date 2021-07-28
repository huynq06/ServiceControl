using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Business.StaticThread
{
    public class ThreadTrackTruckStatusManagement : ThreadBase
    {
        public ThreadTrackTruckStatusManagement()
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
                Services.TrackStatusCallTruckService.DoWork();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "Issue", "Issue");
                DATA.Log.WriteSeriviceLog(ex.ToString(), "TrackTruckExceptionLog.txt");
            }
        }
    }
}
