using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Services;

namespace TASK.Business.StaticThread
{
    public class CreatFlupThreadManagement : ThreadBase
    {
        public CreatFlupThreadManagement()
            : base()
        {
        }
        protected override int DUETIME
        {
            get
            {
                return 1000*60;
            }
        }
        protected override void DoWork()
        {
            try
            {
                DateTime currentTime = DateTime.Now;
               
                
                    CreatFlupService.GetFlight();
                

            }
            catch
            { }
        }
        public override bool WakeupCondition
        {

            get
            {
                DateTime currentTime = DateTime.Now;
                return (DateTime.Now.Hour == 08 &&
    DateTime.Now.Minute == 30 &&
    DateTime.Now.Second == 0);
            }
        }
    }
}
