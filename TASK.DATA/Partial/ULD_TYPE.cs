using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class ULD_TYPE
    {
        public static int GetThresholdByID(int id)
        {
            using (FlightControlDbContextDataContext ct = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return ct.ULD_TYPEs.FirstOrDefault(p => p.ID == id).TimeNotify.Value;
            }
        }
        public static int GetOverTimeByID(int id)
        {
            using (FlightControlDbContextDataContext ct = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return ct.ULD_TYPEs.FirstOrDefault(p => p.ID == id).TimeOperation;
            }
        }
    }
}
