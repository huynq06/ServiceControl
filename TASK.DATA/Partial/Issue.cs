using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class Issue
    {
       
        public static void Insert(List<Issue> listIssue)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                if (listIssue.Count > 0)
                {
                    dbConext.Issues.InsertAllOnSubmit(listIssue);
                }
                dbConext.SubmitChanges();
            }
        }
        public static void Update(List<Issue> listIssue)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                foreach(var issue in listIssue)
                {
                    var issueDb = ct.Issues.FirstOrDefault(c => c.id == issue.id);
                    issueDb.fields_status_id = issue.fields_status_id;
                    issueDb.fields_status_name = issue.fields_status_name;
                    issueDb.fields_status_statusCategory_id = issue.fields_status_statusCategory_id;
                    issueDb.fields_status_statusCategory_name = issue.fields_status_statusCategory_name;
                }
                ct.SubmitChanges();
            }
          
        }
        public static void UpdateStatus(Issue issue)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                    var issueDb = ct.Issues.FirstOrDefault(c => c.id == issue.id);
                    issueDb.fields_status_id = issue.fields_status_id;
                    issueDb.fields_status_name = issue.fields_status_name;
                    issueDb.fields_status_statusCategory_id = issue.fields_status_statusCategory_id;
                    issueDb.fields_status_statusCategory_name = issue.fields_status_statusCategory_name;
                
                ct.SubmitChanges();
            }

        }
        public static List<Issue> GetAll()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.Issues.ToList();
            }
        }
        public static Issue GetByID(int id)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.Issues.FirstOrDefault(p => p.id == id);
            }
        }
        public static bool CheckExist(int id)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.Issues.Any(c => c.id == id);
            }
        }
        public static List<Issue> GetListIssueByCondition(int statusId)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.Issues.Where(c=>c.fields_status_id==statusId).ToList();
            }
        }

    }
}
