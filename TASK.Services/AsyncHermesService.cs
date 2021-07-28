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
    public static class AsyncHermesService
    {
        //get danh sach AWB đang ở trang thái đủ điều kiện
        public static void CheckProcessAWB(List<Issue> listExistIssues, List<Issue> listProcessIssues)
        {
            LABSAccess labs = new LABSAccess();
            //lay danh sach AWB du dieu kien kiem tra xem duoc xy ly chua
            //lay danh sach đang chờ tiếp nhận kra xem đc cân chưa
            List<Issue_detail> listAWBEnoughCondition = Issue_detail.GetAll().Where(c => c.fields_status_id == 10901).ToList();
            //lấy danh sách đang tiếp nhận kiểm tra xem lô hàng hoàn thành chưa
           

            if (listExistIssues.Count > 0)
            {
                foreach (var issue in listExistIssues)
                {
                    var awb = Issue_DetailService.GetIssueDetailFromAPI(issue.key);
                    if (labs.CheckProcessingAWB(awb.AWB.Trim().Replace("-","")))
                    {
                        bool check = IssueService.TransformIssue(awb.key, "111");
                        if (check)
                        {
                            //issue.fields_status_id = 10902;
                            //issue.fields_status_name = "Bắt đầu phục vụ";
                            //issue.fields_status_statusCategory_id = 4;
                            //issue.fields_status_statusCategory_name = "In Progress";
                            //Issue.UpdateStatus(issue);
                            //awb.fields_status_id = 10902;
                            //awb.fields_status_name = "Bắt đầu phục vụ";
                            //awb.fields_status_statusCategory_id = 4;
                            //awb.fields_status_statusCategory_name = "In Progress";
                            //Issue_detail.UpdateStatus(awb);
                            Log.WriteLog("Tranform Issue: " + issue.key + " To Bắt đầu phục vụ");
                        }
                    }
                }
            }
            List<Issue_detail> listAWBProcesing = Issue_detail.GetAll().Where(c => c.fields_status_id == 10902).ToList();
            if (listProcessIssues.Count > 0)
            {
                foreach (var issue in listProcessIssues)
                {
                    var awb = Issue_DetailService.GetIssueDetailFromAPI(issue.key);
                    if (labs.CheckCompleteAWB(awb.AWB.Trim().Replace("-", "")))
                    {
                        bool check = IssueService.TransformIssue(awb.key, "71");
                        if (check)
                        {
                            //issue.fields_status_id = 6;
                            //issue.fields_status_name = "Hoàn thành";
                            //issue.fields_status_statusCategory_id = 3;
                            //issue.fields_status_statusCategory_name = "Done";
                            //Issue.UpdateStatus(issue);
                            //awb.fields_status_id = 6;
                            //awb.fields_status_name = "Hoàn thành";
                            //awb.fields_status_statusCategory_id = 3;
                            //awb.fields_status_statusCategory_name = "Done";
                            //Issue_detail.UpdateStatus(awb);
                            Log.WriteLog("Tranform Issue: " + issue.key + " To Hoàn thành");
                        }
                       
                    }
                }
            }

        }
        public static List<IssueAWB> GetListIssueAWB()
        {
            List<IssueAWB> listAWB = (from iss in Issue.GetAll()
                                      join frm in form.GetAll() 
                                      on iss.key
                                      equals frm.issue_key
                                      select new IssueAWB
                                      {
                                          issueId = iss.id,
                                          issueKey = iss.key,
                                          AWB = frm.AWB,
                                          status = iss.fields_status_id.Value
                                      }).ToList();
            return listAWB;
        }
    }
}
