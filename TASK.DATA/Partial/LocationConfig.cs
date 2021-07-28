using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class LocationConfig
    {
        public static LocationConfig GetCheckPoint(int floor)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.LocationConfigs.SingleOrDefault(c => c.Floor==floor);
            }
        }
    }
}
