using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;


namespace TASK.DATA
{
    public partial class CallTruck
    {
        public static void Insert(List<CallTruck> listTruck)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                if (listTruck.Count > 0)
                {
                    dbConext.ExecuteCommand("TRUNCATE TABLE CallTruck");
                    //dbConext.CallTrucks.DeleteAllOnSubmit(dbConext.CallTrucks);
                    dbConext.CallTrucks.InsertAllOnSubmit(listTruck);
                }
                dbConext.SubmitChanges();
            }
        }
    }
}
