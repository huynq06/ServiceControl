using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Model.ViewModel;
using DATAACCESS;
using TASK.Services;
namespace TASK.Services
{
    public static class CloseIssueService
    {
        public static void CloseIssue()
        {
            //get danh sach Issue ở trạng thái chưa đủ điều kiện
            //Chuyển Issue sang trạng thái dừng phục vụ
            //Update lại vào DB
            List<Issue> listIssueToClose = IssueService.GetListIssueFromAPI("297");
            if (listIssueToClose.Count > 0)
            {
                foreach (var awb in listIssueToClose)
                {
                     IssueService.CloseIssue(awb.key,"101");
                        //var issue = Issue.GetByID(awb.id);
                        //issue.fields_status_id = 10903;
                        //issue.fields_status_name = "Tạm dừng phục vụ";
                        //issue.fields_status_statusCategory_id = 3;
                        //issue.fields_status_statusCategory_name = "Done";
                        //Issue.UpdateStatus(issue);
                        //awb.fields_status_id = 10903;
                        //awb.fields_status_name = "Tạm dừng phục vụ";
                        //awb.fields_status_statusCategory_id = 3;
                        //awb.fields_status_statusCategory_name = "Done";
                      //  Issue_detail.UpdateStatus(awb);
                                      
                }
            }
            //lay danh sach Issue ở trạng thái chờ tiếp nhận
            List<Issue> listIssueWaitingToClose = IssueService.GetListIssueFromAPI("298");
            if (listIssueWaitingToClose.Count > 0)
            {
                foreach (var awb in listIssueWaitingToClose)
                {
                    IssueService.CloseIssue(awb.key, "61");
                    //var issue = Issue.GetByID(awb.id);
                    //issue.fields_status_id = 10903;
                    //issue.fields_status_name = "Tạm dừng phục vụ";
                    //issue.fields_status_statusCategory_id = 3;
                    //issue.fields_status_statusCategory_name = "Done";
                    //Issue.UpdateStatus(issue);
                    //awb.fields_status_id = 10903;
                    //awb.fields_status_name = "Tạm dừng phục vụ";
                    //awb.fields_status_statusCategory_id = 3;
                    //awb.fields_status_statusCategory_name = "Done";
                    //Issue_detail.UpdateStatus(awb);

                }
            }
            //lay danh sach Issue ở trạng thái đang xử lý
            List<Issue> listIssueProcessToClose = IssueService.GetListIssueFromAPI("299");
            if (listIssueProcessToClose.Count > 0)
            {
                foreach (var awb in listIssueProcessToClose)
                {
                    IssueService.CloseIssue(awb.key, "51");
                    //var issue = Issue.GetByID(awb.id);
                    //issue.fields_status_id = 10903;
                    //issue.fields_status_name = "Tạm dừng phục vụ";
                    //issue.fields_status_statusCategory_id = 3;
                    //issue.fields_status_statusCategory_name = "Done";
                    //Issue.UpdateStatus(issue);
                    //awb.fields_status_id = 10903;
                    //awb.fields_status_name = "Tạm dừng phục vụ";
                    //awb.fields_status_statusCategory_id = 3;
                    //awb.fields_status_statusCategory_name = "Done";
                    //Issue_detail.UpdateStatus(awb);

                }
            }
        }
    }
}
