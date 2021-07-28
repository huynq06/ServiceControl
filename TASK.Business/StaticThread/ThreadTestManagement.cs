using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Business.StaticThread
{
    public class ThreadTestManagement : ThreadBase
    {
        public ThreadTestManagement()
            : base()
        {
        }

        protected override int DUETIME
        {
            get
            {
                return 3000;
            }
        }
        protected override void DoWork()
        {
            try
            {
                Services.ServiceTest.ProgramTest();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "Issue", "Issue");
                DATA.Log.WriteLog(ex, "Issue");
            }
        }
    }
}
