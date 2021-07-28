using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;
using TASK.Model.DBModel;

namespace TASK.DATA
{
    public partial class LogCapSo
    {
        public static void UpdateAwbRemain(LogCapSo item)
        {
            using (LogCapSoDbDataContext dbConext = new LogCapSoDbDataContext(AppSetting.ConnectionStringLogCapso))
            {

                var objectDb = dbConext.LogCapSos.FirstOrDefault(c => c.QNO == item.QNO && c.MAWB==item.MAWB && c.HAWB==item.HAWB && c.Created > DateTime.Now.Date);
                objectDb.AWBRemain = item.AWBRemain;
                dbConext.SubmitChanges();
            }

        }
        public static List<LogCapSo> GetListCaplaiAWB(string queue)
        {
            using (LogCapSoDbDataContext dbConext = new LogCapSoDbDataContext(AppSetting.ConnectionStringLogCapso))
            {

                return dbConext.LogCapSos.Where(c => c.QNO == queue && c.Created > DateTime.Now.Date).ToList();
            }

        }
        public static List<LogCapSo> GetListAllHawb()
        {
            using (LogCapSoDbDataContext dbConext = new LogCapSoDbDataContext(AppSetting.ConnectionStringLogCapso))
            {

                return dbConext.LogCapSos.Where(c => c.HAWB == "ALLHAWB" && c.Created > DateTime.Now.AddDays(-1).Date).ToList();
            }

        }
        public static List<LogCapSo> GetListCaplai()
        {
            using (LogCapSoDbDataContext dbConext = new LogCapSoDbDataContext(AppSetting.ConnectionStringLogCapso))
            {

                return dbConext.LogCapSos.Where(c => c.HAWB.ToUpper().Contains("CAP") && c.Created > DateTime.Now.Date).ToList();
            }

        }
        public static void UpdateStatus(List<CapSo> listCapso)
        {
            using (LogCapSoDbDataContext dbConext = new LogCapSoDbDataContext(AppSetting.ConnectionStringLogCapso))
            {
                foreach(var item in listCapso)
                {
                    var capsoDB = dbConext.LogCapSos.FirstOrDefault(c => c.QNO == item.QUEUE && c.MAWB.Trim() == item.MAWB.Trim() && c.HAWB.Trim() == item.HAWB.Trim() && c.Created > DateTime.Now.Date);
                    capsoDB.AWBRemain = item.AWBRemain;
                }


                dbConext.SubmitChanges();
            }

        }
        public static void UpdateStatusLogCapSo(List<LogCapSo> listCapso)
        {
            using (LogCapSoDbDataContext dbConext = new LogCapSoDbDataContext(AppSetting.ConnectionStringLogCapso))
            {
                foreach (var item in listCapso)
                {
                    var capsoDB = dbConext.LogCapSos.FirstOrDefault(c => c.ID == item.ID);
                    capsoDB.AWBRemain = item.AWBRemain;
                }


                dbConext.SubmitChanges();
            }

        }
    }
}
