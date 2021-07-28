using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TASK.DATA;
using TASK.Model.ViewModel;
using TASK.Settings;
using System.Transactions;

namespace TASK.Services
{
    public static class IssueService
    {
        //public static List<Issue> _listIssueToInsert;
        //public static List<Issue> _listIssueToUpdate;
        //public static List<Issue_detail> _listIssueDetailToInsert;
        //public static List<Issue_detail> _listIssueDetailToUpdate;
        //public static List<Condition> _listIssueConditionToInsert;
        //public static List<Condition> _listIssueConditionToUpdate;
        //public static List<form> _listformToInsert;
        //Get danh sach Issue
        public static void ProcessListIssue()
        {
            //_listIssueToInsert = new List<Issue> { };
            //_listIssueToUpdate = new List<Issue> { };
            //_listIssueDetailToInsert = new List<Issue_detail> { };
            //_listIssueDetailToUpdate = new List<Issue_detail> { };
            //_listIssueConditionToInsert = new List<Condition> { };
            //_listIssueConditionToUpdate = new List<Condition> { };
            //_listformToInsert = new List<form> { };
            List<Issue> listAwaiConfirmIssue = new List<Issue>();
            List<Issue> listExistIssues = new List<Issue>();
            List<Issue> listProcessIssues = new List<Issue>();
            ProcessData(ref listAwaiConfirmIssue,ref listExistIssues, ref listProcessIssues);
            //try
            //{
            //    using (TransactionScope scope = AppSetting.GetTransactionScope())
            //    {
            //        if (_listIssueToInsert.Count > 0)
            //        {
            //            Issue.Insert(_listIssueToInsert);
            //        }
            //        if (_listIssueToUpdate.Count > 0)
            //        {
            //            Issue.Update(_listIssueToUpdate);
            //        }
            //        if (_listIssueDetailToInsert.Count > 0)
            //        {
            //            Issue_detail.Insert(_listIssueDetailToInsert);
            //        }
            //        if (_listIssueDetailToUpdate.Count > 0)
            //        {
            //            Issue_detail.Update(_listIssueDetailToUpdate);
            //        }
            //        //if (_listIssueConditionToInsert.Count > 0)
            //        //{
            //        //    Condition.Insert(_listIssueConditionToInsert);
            //        //}
            //        ////if (_listIssueConditionToUpdate.Count > 0)
            //        //{
            //        //    Condition.Update(_listIssueConditionToUpdate);
            //        //}
            //        //if(_listformToInsert.Count>0)
            //        //{
            //        //    form.Insert(_listformToInsert);
            //        //}

            //        scope.Complete();
            //    }
            //}
           
            //catch (Exception ex)
            //{
            //    Log.WriteLog(ex.ToString());
            //}
           
            AsyncHermesService.CheckProcessAWB(listExistIssues, listProcessIssues);

        }
        public static void ProcessData(ref List<Issue> listAwaiConfirmIssue, ref List<Issue> listExistIssues, ref List<Issue> listProcessIssues)
        {
            try
            {
               listAwaiConfirmIssue = GetListIssueFromAPI("297");
                 listExistIssues = GetListIssueFromAPI("298");
                listProcessIssues = GetListIssueFromAPI("299");
                //List<Issue> listDbIssues = Issue.GetAll();
                //if (listAwaiConfirmIssue.Count > 0)
                //{
                //    foreach (var issue in listAwaiConfirmIssue)
                //    {
                //        if (!Issue.CheckExist(issue.id))
                //        {
                //            _listIssueToInsert.Add(issue);
                //            var issueDetail = Issue_DetailService.GetIssueDetailFromAPI(issue.key);
                //            _listIssueDetailToInsert.Add(issueDetail);
                //        }
                //        else
                //        {
                //            var issue_DetailDb = Issue_detail.GetByID(issue.id);
                //            var issueDetail = Issue_DetailService.GetIssueDetailFromAPI(issue.key);
                //            if (issue_DetailDb.fields_priority_id != issueDetail.fields_priority_id)
                //            {
                //                _listIssueDetailToUpdate.Add(issueDetail);
                //            }
                //        }
                //        //var issueDetailDb = Issue_detail.GetByID(newIssue.id);
                //        //string comment = Issue_DetailService.GetCommnetIssueFromAPI(newIssue.key);
                //        //if (issueDetailDb.Comment != comment)
                //        //{
                //        //    issueDetailDb.Comment = comment;
                //        //    _listIssueDetailToUpdate.Add(issueDetailDb);
                //        //}
                //    }
                //}
                //if (listExistIssues.Count > 0)
                //{
                //    foreach (var issue in listExistIssues)
                //    {
                //        if (!Issue.CheckExist(issue.id))
                //        {
                //            _listIssueToInsert.Add(issue);
                //            var issueDetail = Issue_DetailService.GetIssueDetailFromAPI(issue.key);
                //            issueDetail.TimeTransition = DateTime.Now;
                //            _listIssueDetailToInsert.Add(issueDetail);
                //        }
                //        else
                //        {
                //            var issueDb = Issue.GetByID(issue.id);
                //            if (issueDb.fields_status_id != issue.fields_status_id)
                //            {
                //                List<Condition> listCondition = new List<Condition>() { };
                //                _listIssueToUpdate.Add(issue);
                //                var issueDetail = Issue_DetailService.GetIssueDetailFromAPI(issueDb.key);
                //                //get comment
                //                string comment = Issue_DetailService.GetCommnetIssueFromAPI(issueDb.key);
                //                DateTime dt = Issue_DetailService.GetDateTimeTranformFromAPI(issueDb.key);
                //                issueDetail.Comment = comment;
                //                issueDetail.TimeTransition = dt;
                //                _listIssueDetailToUpdate.Add(issueDetail);

                //            }
                //            else
                //            {
                //                var issueDetailDb = Issue_detail.GetByID(issueDb.id);
                //                string comment = Issue_DetailService.GetCommnetIssueFromAPI(issueDb.key);
                //                var issueDetail = Issue_DetailService.GetIssueDetailFromAPI(issue.key);
                //                if (issueDetailDb.fields_priority_id != issueDetail.fields_priority_id)
                //                {
                //                    issueDetailDb.fields_priority_id = issueDetail.fields_priority_id;
                //                    issueDetailDb.fields_priority_name = issueDetail.fields_priority_name;
                //                    issueDetailDb.Comment = comment;
                //                    _listIssueDetailToUpdate.Add(issueDetailDb);
                //                }
                //            }
                //        }
                     
                //    }
                //}
                //if (listProcessIssues.Count > 0)
                //{
                //    foreach (var issue in listProcessIssues)
                //    {
                //        if (!Issue.CheckExist(issue.id))
                //        {
                //            _listIssueToInsert.Add(issue);
                //            var issueDetail = Issue_DetailService.GetIssueDetailFromAPI(issue.key);
                //            issueDetail.TimeTransition = DateTime.Now;
                //            _listIssueDetailToInsert.Add(issueDetail);
                //        }
                //        else
                //        {
                //            var issueDb = Issue.GetByID(issue.id);
                //            if (issueDb.fields_status_id != issue.fields_status_id)
                //            {
                //                List<Condition> listCondition = new List<Condition>() { };
                //                _listIssueToUpdate.Add(issue);
                //                var issueDetail = Issue_DetailService.GetIssueDetailFromAPI(issueDb.key);
                //                //get comment
                //                _listIssueDetailToUpdate.Add(issueDetail);

                //            }
                //        }
                       
                //    }
                //}


            }
            catch (Exception ex)
            {

                Log.InsertLog(ex,"Process Data Issue","Process Data");
                Log.WriteLog(ex.ToString(),"Process_Issue");
            }
            
        }
        public static void Test()
        {
            Issue_DetailService.Test("ALSCKT-35");
        }
        public static List<Issue> GetListIssueFromAPI(string queue)
        {
            string response = "";
            List<Issue> listIssueResult = new List<Issue> { };
            try
            {
                HttpRequest rq = new HttpRequest();
                rq.Credentials = new Credentials()
                {
                    UserName = AppSetting.USER_NAME,
                    Password = AppSetting.PASSWORD
                };
                string url = "http://support.als.com.vn:8882/rest/servicedeskapi/servicedesk/21/queue/" + queue + "/issue";
                rq.Url = url;
                bool check = false;
                string rp = rq.Execute(null, "GET", "", true,queue + " GetListIssueFromAPI",ref check);
                //data_Issue data_Issue = JsonConvert.DeserializeObject<data_Issue>(rp);
                var data = (JArray)Newtonsoft.Json.Linq.JObject.Parse(rp)["values"];
                var results = data.ToArray();
                if (results.Length > 0)
                {
                    for (int i = 0; i < results.Length; i++)
                    {
                        Issue issue = new Issue();
                        issue.id = int.Parse(Convert.ToString(results[i]["id"]));
                        issue.key = Convert.ToString(results[i]["key"]);
                        issue.self = Convert.ToString(results[i]["self"]);
                        issue.fields_summary = Convert.ToString(results[i]["fields"]["summary"]);
                       
                        issue.fields_created = Convert.ToDateTime(results[i]["fields"]["created"]);
                        //issue.fields_reporter_displayName = Convert.ToString(results[i]["fields"]["reporter"]["displayName"]);
                        //issue.fields_issuetype_description = Convert.ToString(results[i]["fields"]["issuetype"]["description"]);
                        //issue.fields_issuetype_name = Convert.ToString(results[i]["fields"]["issuetype"]["name"]);
                        issue.fields_status_name = Convert.ToString(results[i]["fields"]["status"]["name"]);
                        issue.fields_status_id = int.Parse(Convert.ToString(results[i]["fields"]["status"]["id"]));
                        issue.fields_status_statusCategory_name = Convert.ToString(results[i]["fields"]["status"]["statusCategory"]["name"]);
                        issue.fields_status_statusCategory_id = int.Parse(Convert.ToString(results[i]["fields"]["status"]["statusCategory"]["id"]));
                        listIssueResult.Add(issue);
                    }
                }
                return listIssueResult;
            }
            catch (Exception ex)
            {
                Log.LogToText(ex.ToString() + " " + response + "---" + queue, "GetListIssueFromAPI");
                return listIssueResult;
            }
            
        }

        public static bool TransformIssue(string issueKey,string toStatus)
        {
            bool check = false;
            string response = "";
            try
            {
                ID id = new ID();
                id.id = toStatus;
                Transition tran = new Transition();
                tran.transition = id;
                HttpRequest rq = new HttpRequest();
                rq.Credentials = new Credentials()
                {
                    UserName = AppSetting.USER_NAME,
                    Password = AppSetting.PASSWORD
                };
                string url = "http://support.als.com.vn:8882/rest/api/2/issue/" + issueKey + "/transitions";
                rq.Url = url;
                response = rq.Execute(tran, "POST", "", false, issueKey + " TransformIssue" + "----" + toStatus,ref check);
            }
            catch (Exception ex)
            {
                check = false;
                //Log.InsertLog(ex + response, "Transition Issue From API", "Call API");
                Log.LogToText(ex.ToString() + response + "------------" + issueKey, "Transition");
            }
            return check;
           
        }
        public static void CloseIssue(string issueKey,string IdValue)
        {
            string response = "";
            try
            {
                ID id = new ID();
                id.id = IdValue;
                Transition tran = new Transition();
                tran.transition = id;
                HttpRequest rq = new HttpRequest();
                rq.Credentials = new Credentials()
                {
                    UserName = AppSetting.USER_NAME,
                    Password = AppSetting.PASSWORD
                };
                string url = "http://support.als.com.vn:8882/rest/api/2/issue/" + issueKey + "/transitions";
                rq.Url = url;
                bool check = false;
                response = rq.Execute(tran, "POST", "", false, issueKey + " CloseIssue",ref check);
               

            }
            catch (Exception ex)
            {

                Log.LogToText(ex.ToString() + response + "-----------" + issueKey.ToUpper(), "CloseIssue");
            }

        }
        

    }
}