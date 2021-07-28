using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Settings;

namespace TASK.Services
{
    public static class Issue_DetailService
    {
        public static DateTime GetDateTimeTranformFromAPI(string issueKey)
        {
            DateTime dt = new DateTime();
            string response = "";
            try
            {
                HttpRequest rq = new HttpRequest();
                rq.Credentials = new Credentials()
                {
                    UserName = AppSetting.USER_NAME,
                    Password = AppSetting.PASSWORD
                };
                string url = "http://support.als.com.vn:8882/rest/api/2/issue/" + issueKey + "?expand=changelog";
                rq.Url = url;
                bool check = false;
                response = rq.Execute(null, "GET", "", false, issueKey + " GetDateTimeTranformFromAPI",ref check);
                //data_Issue data_Issue = JsonConvert.DeserializeObject<data_Issue>(rp);
                var jOject = Newtonsoft.Json.Linq.JObject.Parse(response);
                if(jOject["changelog"]["histories"].Count()>1)
                {
                    dt = Convert.ToDateTime(Convert.ToString(jOject["changelog"]["histories"][1]["created"]));
                }
                
            }
            catch (Exception ex)
            {

                Log.LogToText(ex.ToString() + response + "------" + issueKey, "GetDateTimeTranformFromAPI");
            }
            return dt;
        }
        public static Issue_detail GetIssueDetailFromAPI(string issueKey)
        {

            Issue_detail issue_detail = new Issue_detail();
            string rp = "";

            try
            {
                HttpRequest rq = new HttpRequest();
                rq.Credentials = new Credentials()
                {
                    UserName = AppSetting.USER_NAME,
                    Password = AppSetting.PASSWORD
                };
                string url = "http://support.als.com.vn:8882/rest/api/2/issue/" + issueKey;
                rq.Url = url;
                bool check = false;
                rp = rq.Execute(null, "GET", "", false, issueKey + " GetIssueDetailFromAPI",ref check);
                //data_Issue data_Issue = JsonConvert.DeserializeObject<data_Issue>(rp);
                var jOject = Newtonsoft.Json.Linq.JObject.Parse(rp);
                issue_detail.id = int.Parse(Convert.ToString(jOject["id"]));
                issue_detail.key = Convert.ToString(jOject["key"]);
                issue_detail.self = Convert.ToString(jOject["self"]);
                issue_detail.fields_fixVersions_name = "";
                var fields_lastViewed = jOject["fields"]["lastViewed"].ToString();
                if (!string.IsNullOrWhiteSpace(fields_lastViewed))
                {
                    issue_detail.fields_lastViewed = Convert.ToDateTime(jOject["fields"]["lastViewed"]);
                }
                else
                {
                    issue_detail.fields_lastViewed = null;
                }
                if (!string.IsNullOrWhiteSpace(jOject["fields"]["priority"].ToString()))
                {
                    issue_detail.fields_priority_name = Convert.ToString(jOject["fields"]["priority"]["name"]);
                    issue_detail.fields_priority_id = int.Parse(Convert.ToString(jOject["fields"]["priority"]["id"]));
                }

                //issue_detail.fields_assignee_emailAddress = string.IsNullOrWhiteSpace(jOject["fields"]["assignee"].ToString()) ? "" : Convert.ToString(jOject["fields"]["assignee"]["emailAddress"]);
                //issue_detail.fields_assignee_displayName = string.IsNullOrWhiteSpace(jOject["fields"]["assignee"].ToString()) ? "" : Convert.ToString(jOject["fields"]["assignee"]["displayName"]);
                issue_detail.fields_status_name = Convert.ToString(jOject["fields"]["status"]["name"]);
                issue_detail.fields_status_id = int.Parse(Convert.ToString(jOject["fields"]["status"]["id"]));
                issue_detail.fields_status_statusCategory_id = int.Parse(Convert.ToString(jOject["fields"]["status"]["statusCategory"]["id"]));
                issue_detail.fields_status_statusCategory_key = Convert.ToString(jOject["fields"]["status"]["statusCategory"]["key"]);
                issue_detail.fields_status_statusCategory_name = Convert.ToString(jOject["fields"]["status"]["statusCategory"]["name"]);
                issue_detail.fields_status_statusCategory_colorName = Convert.ToString(jOject["fields"]["status"]["statusCategory"]["colorName"]);
                //issue_detail.fields_creator_name = Convert.ToString(jOject["fields"]["creator"]["name"]);
                //issue_detail.fields_creator_displayName = Convert.ToString(jOject["fields"]["creator"]["displayName"]);
                //issue_detail.fields_reporter_name = Convert.ToString(jOject["fields"]["reporter"]["name"]);
                //issue_detail.fields_reporter_name = Convert.ToString(jOject["fields"]["reporter"]["name"]);
                //issue_detail.fields_reporter_displayName = Convert.ToString(jOject["fields"]["reporter"]["displayName"]);
                //issue_detail.fields_created = Convert.ToDateTime(jOject["fields"]["created"]);
                issue_detail.AWB = Convert.ToString(jOject["fields"]["customfield_11900"]);
                issue_detail.Booking = string.IsNullOrWhiteSpace(jOject["fields"]["customfield_11805"].ToString()) ? "" : Convert.ToString(jOject["fields"]["customfield_11805"]);
                issue_detail.Dest = string.IsNullOrWhiteSpace(jOject["fields"]["customfield_11806"].ToString()) ? "" : Convert.ToString(jOject["fields"]["customfield_11806"]);
                issue_detail.fields_summary = Convert.ToString(jOject["fields"]["summary"]);
                issue_detail.weight = Convert.ToString(jOject["fields"]["customfield_11803"]);
                issue_detail.pieces = Convert.ToString(jOject["fields"]["customfield_11804"]);
                issue_detail.fields_duedate = null;
                issue_detail.Created = DateTime.Now;
                issue_detail.SortValue = int.Parse(Convert.ToString(jOject["fields"]["customfield_12008"]));
                issue_detail.CutoffTime = string.IsNullOrWhiteSpace(jOject["fields"]["customfield_12003"].ToString()) ? issue_detail.CutoffTime : Convert.ToDateTime(Convert.ToString(jOject["fields"]["customfield_12003"]));
                issue_detail.FlightType = string.IsNullOrWhiteSpace(jOject["fields"]["customfield_12010"].ToString()) ? "" : Convert.ToString(jOject["fields"]["customfield_12010"]);
                issue_detail.TimeTransition = DateTime.Now;
            }
            catch (Exception ex)
            {

                Log.LogToText(ex.ToString() + rp + "-------------" +issueKey, "GetIssueDetailFromAPI");
            }

            //var dataCondition = (JArray)Newtonsoft.Json.Linq.JObject.Parse(Convert.ToString(jOject["fields"]))["customfield_11800"];
            //var resultCondition = dataCondition.ToArray();
            //for (int i = 0; i < resultCondition.Length; i++)
            //{
            //    Condition condition = new Condition();
            //    condition.id_condittion = int.Parse(Convert.ToString(resultCondition[i]["id"]));
            //    condition.name = Convert.ToString(resultCondition[i]["name"]);
            //    condition.@checked = Convert.ToBoolean(resultCondition[i]["checked"]);
            //    condition.mandatory = Convert.ToBoolean(resultCondition[i]["mandatory"]);
            //    condition.option = Convert.ToBoolean(resultCondition[i]["option"]);
            //    condition.rank = int.Parse(Convert.ToString(resultCondition[i]["rank"]));
            //    condition.issue_id = issue_detail.id;
            //    condition.issue_key = issue_detail.key;
            //    listCondition.Add(condition);
            //}
            //issue_detail.fields_reporter_displayName = int.Parse(Convert.ToString(jOject["id"]));
            return issue_detail;
        }
        public static string GetCommnetIssueFromAPI(string issueKey)
        {
            string commnet = "";
            try
            {
                HttpRequest rq = new HttpRequest();
                rq.Credentials = new Credentials()
                {
                    UserName = AppSetting.USER_NAME,
                    Password = AppSetting.PASSWORD
                };
                string url = "http://support.als.com.vn:8882/rest/api/2/issue/" + issueKey + "/comment";
                rq.Url = url;
                bool check = false;
                string rp = rq.Execute(null, "GET", "", false, issueKey + " GetCommnetIssueFromAPI",ref check);
                //data_Issue data_Issue = JsonConvert.DeserializeObject<data_Issue>(rp);
                var jOject = Newtonsoft.Json.Linq.JObject.Parse(rp);
                //var customField = jOject["comments"].ToString();
                //IList<string> keys = jOject.Properties().Select(p => p.Name).ToList();
                if (jOject["comments"].HasValues)
                {
                    commnet = jOject["comments"][0]["body"].ToString();
                }
            }
            catch (Exception ex)
            {

                Log.WriteLog(ex, "GetIssueDetailFromAPI");
            }

            return commnet;
        }
        public static void Test(string issueKey)
        {
            Issue_detail issue_detail = new Issue_detail();
            HttpRequest rq = new HttpRequest();
            rq.Credentials = new Credentials()
            {
                UserName = AppSetting.USER_NAME,
                Password = AppSetting.PASSWORD
            };
            string url = "http://support.als.com.vn:8882/rest/api/2/issue/" + issueKey;
            rq.Url = url;
            bool check = false;
            string rp = rq.Execute(null, "GET", "", false, issueKey + " Test",ref check);
            //data_Issue data_Issue = JsonConvert.DeserializeObject<data_Issue>(rp);
            var jOject = Newtonsoft.Json.Linq.JObject.Parse(rp);
            issue_detail.id = int.Parse(Convert.ToString(jOject["id"]));
            issue_detail.key = Convert.ToString(jOject["key"]);
            issue_detail.self = Convert.ToString(jOject["self"]);
            issue_detail.fields_fixVersions_name = "";
            var customField = jOject["fields"]["customfield_11808"].ToString();
            GetCarNumber(customField);


        }
        public static void GetCarNumber(string jsonInput)
        {
            jsonInput = jsonInput.Replace(@"\", "");
            var jOject = Newtonsoft.Json.Linq.JObject.Parse(jsonInput);
            IList<string> keys = jOject.Properties().Select(p => p.Name).ToList();
            foreach (var key in keys)
            {
                string carnumber = Convert.ToString(jOject[key]["fields"][0]["value"]);
            }
            //foreach (JObject child in jOject.Children())
            //{
            //    if (child.HasValues)
            //    {
            //        //
            //        // Code to get the keys here
            //        //
            //        string outPut = child.ToString();
            //    }
            //}
        }
    }
}
