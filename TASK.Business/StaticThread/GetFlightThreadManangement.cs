using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Services;

namespace TASK.Business.StaticThread
{
    public class GetFlightThreadManangement : ThreadBase
    {
        public GetFlightThreadManangement()
            : base()
        {
        }
        protected override int DUETIME
        {
            get
            {
                return 1000 * 60 * 60 *1;
            }
        }
        protected override void DoWork()
        {
            try
            {

                GetFLightDiService.GetData();


            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.ToString(), "GetFlightDiThreadManagement");
            }
        }
    }
}
