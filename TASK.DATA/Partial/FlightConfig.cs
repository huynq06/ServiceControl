using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class FlightConfig
    {
        public static List<FlightConfig> GetAll()
        {
            using (FlightControlDbContextDataContext dbConext = new FlightControlDbContextDataContext(AppSetting.ConnectionStringFlightControl))
            {
                return dbConext.FlightConfigs.ToList();
            }
        }
    }
}
