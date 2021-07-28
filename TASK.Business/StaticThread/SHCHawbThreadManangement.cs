using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

namespace TASK.Business.StaticThread
{
    public class SHCHawbThreadManangement : ThreadBase
    {
        public object _locker = new object();
        public SHCHawbThreadManangement()
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
                Services.SHCHawbService.CheckHawbSHC();
            }
            catch (Exception ex)
            {
                //Log.InsertLog(ex, "Issue", "Issue");
               // Log.WriteLog(ex, "Issue");
            }
        }
    }
}
