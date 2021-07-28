using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Services;


namespace TASK.Business.StaticThread
{
    public class GetFlightDenThreadManagement : ThreadBase
    {
        public GetFlightDenThreadManagement()
            : base()
        {
        }
        protected override int DUETIME
        {
            get
            {
                return 1000 * 60 * 60*1;
            }
        }
        protected override void DoWork()
        {
            try
            {
                DateTime currentTime = DateTime.Now;


                GetFlightDenService.GetData();


            }
            catch(Exception ex)
            {
                Log.WriteLog(ex.ToString(), "GetFlightDenThreadManagement");
            }
        }
    }
}
