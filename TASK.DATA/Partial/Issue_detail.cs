using StorageManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class Issue_detail
    {
        //public int id { set; get; }
        //public string key { set; get; }
        //public string self { set; get; }
        //public string fields_fixVersions_name { set; get; }
        //public DateTime fields_lastViewed { set; get; }
        //public string fields_priority_name { set; get; }
        //public int fields_priority_id { set; get; }
        //public string fields_assignee_emailAddress { set; get; }
        //public string fields_assignee_displayName { set; get; }
        //public string fields_status_name { set; get; }
        //public int fields_status_statusCategory_id { set; get; }
        //public string fields_status_statusCategory_key { set; get; }
        //public string fields_status_statusCategory_name { set; get; }
        //public string fields_status_statusCategory_colorName { set; get; }
        //public string fields_creator_name { set; get; }
        //public string fields_creator_displayName { set; get; }
        //public string fields_reporter_name { set; get; }
        //public string fields_reporter_displayName { set; get; }
        //public DateTime fields_created { set; get; }
        //public string fields_summary { set; get; }
        //public DateTime fields_duedate { set; get; }
        public static void Insert(List<Issue_detail> listIssue_detail)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                if (listIssue_detail.Count > 0)
                {
                    dbConext.Issue_details.InsertAllOnSubmit(listIssue_detail);
                }
                dbConext.SubmitChanges();
            }
        }
        public static void Update(List<Issue_detail> listIssue_detail)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                foreach (var issueDetail in listIssue_detail)
                {
                    var issueDetailDB = dbConext.Issue_details.FirstOrDefault(c => c.id == issueDetail.id);
                    issueDetailDB.fields_priority_id = issueDetail.fields_priority_id;
                    issueDetailDB.fields_priority_name = issueDetail.fields_priority_name;
                    issueDetailDB.fields_status_id = issueDetail.fields_status_id;
                    issueDetailDB.fields_status_name = issueDetail.fields_status_name;
                    issueDetailDB.fields_status_statusCategory_colorName = issueDetail.fields_status_statusCategory_colorName;
                    issueDetailDB.fields_status_statusCategory_id = issueDetail.fields_status_statusCategory_id;
                    issueDetailDB.fields_status_statusCategory_key = issueDetail.fields_status_statusCategory_key;
                    issueDetailDB.fields_status_statusCategory_name = issueDetail.fields_status_statusCategory_name;
                    issueDetailDB.Comment = issueDetail.Comment;
                    if (!issueDetail.TimeTransition.Equals(DateTime.MinValue))
                    {
                        issueDetailDB.TimeTransition = issueDetail.TimeTransition;
                    }
                  
                }
                dbConext.SubmitChanges();
            }
        }
        public static void UpdateSortValue(List<Issue_detail> listIssue_detail)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                foreach (var issueDetail in listIssue_detail)
                {
                    var issueDetailDB = dbConext.Issue_details.FirstOrDefault(c => c.id == issueDetail.id);
                    issueDetailDB.SortValue = issueDetail.SortValue;

                }
                dbConext.SubmitChanges();
            }
        }
        public static void UpdateStatus(Issue_detail issueDetail)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
               
                    var issueDetailDB = dbConext.Issue_details.FirstOrDefault(c => c.id == issueDetail.id);
                    
                    issueDetailDB.fields_status_id = issueDetail.fields_status_id;
                    issueDetailDB.fields_status_name = issueDetail.fields_status_name;
                    
                    issueDetailDB.fields_status_statusCategory_id = issueDetail.fields_status_statusCategory_id;
                    issueDetailDB.fields_status_statusCategory_key = issueDetail.fields_status_statusCategory_key;
                    issueDetailDB.fields_status_statusCategory_name = issueDetail.fields_status_statusCategory_name;
                    dbConext.SubmitChanges();
            }
        }
        public static void UpdateCutOffTime(List<Issue_detail> listIssue_detail)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                foreach (var issueDetail in listIssue_detail)
                {
                    var issueDetailDB = dbConext.Issue_details.FirstOrDefault(c => c.id == issueDetail.id);
                    issueDetailDB.CutoffTime = issueDetail.CutoffTime;
                }
                dbConext.SubmitChanges();
            }
        }
        public static List<Issue_detail> GetAll()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.Issue_details.ToList();
            }
        }
        public static Issue_detail GetByID(int id)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.Issue_details.FirstOrDefault(p => p.id == id);
            }
        }
        public static bool CheckExist(int id)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.Issue_details.Any(c => c.id == id);
            }
        }
        public static List<Issue_detail> GetListAWBToProcessCutOffTime()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                //return ct.forms.Where(p => p.CutoffTime != null && p.createdDate > DateTime.Today).ToList();
                return ct.Issue_details.Where(p => p.CutoffTime == null && p.Booking != null && p.Booking != "").ToList();
            }
        }
        public static DictionaryStorage<string, DateTime> LoadFromList(List<Issue_detail> listIssueDetail)
        {
            DictionaryStorage<string, DateTime> dictData = new DictionaryStorage<string, DateTime>();
            foreach (var issueDetail in listIssueDetail)
            {
                DateTime dt = new DateTime();
               
                    dictData.EnqueueElementData(issueDetail.Booking, dt);
                
              
            }
            return dictData;

        }
    }
}
