using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class Condition
    {
        public static void Insert(List<Condition> listCondition)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                if (listCondition.Count > 0)
                {
                    dbConext.Conditions.InsertAllOnSubmit(listCondition);
                }
                dbConext.SubmitChanges();
            }
        }
        public static void Update(List<Condition> listCondition)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                foreach(var condition in listCondition)
                {
                    var conditionDb = dbConext.Conditions.FirstOrDefault(p => p.id_condittion == condition.id_condittion && p.issue_id == condition.issue_id);
                    conditionDb.@checked = condition.@checked;
                }
                   
                dbConext.SubmitChanges();
            }
        }
        public static List<Condition> GetAll()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.Conditions.ToList();
            }
        }
        public static Condition GetByID(int id,int issueId)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.Conditions.FirstOrDefault(p => p.id_condittion == id && p.issue_id == issueId);
            }
        }
        public static bool CheckChange(Condition conditionGetFromAPI, Condition conditionDb)
        {
            if (conditionGetFromAPI.@checked != conditionDb.@checked)
                return true;
            return false;
        }
    }
}
