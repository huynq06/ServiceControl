using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Model.ViewModel;
using Newtonsoft.Json;
using TASK.Settings;
using System.Transactions;

namespace TASK.Services
{
    public class UpdateSortValueService
    {
      //  public static List<Issue_detail> _listIssueDetailToUpdate;
        public static void UpdateSortValue()
        {
            string rp = "";
         //   _listIssueDetailToUpdate = new List<Issue_detail> { };
            try
            {
                //List<Issue_detail> listIssueDb = Issue_detail.GetAll().Where(c => c.fields_status_id == 10904 || c.fields_status_id == 10901).ToList();
                var listAwaiConfirmIssue = IssueService.GetListIssueFromAPI("297");
                if (listAwaiConfirmIssue.Count > 0)
                {

                    foreach (var issueObject in listAwaiConfirmIssue)
                    {
                        var issue = Issue_DetailService.GetIssueDetailFromAPI(issueObject.key);
                        int oldSortValue = issue.SortValue.Value;
                        if (issue.CutoffTime.HasValue)
                        {
                            int timeSpanToCutoff = (int)Math.Round((issue.CutoffTime.Value - DateTime.Now).TotalMinutes);
                            if (!string.IsNullOrEmpty(issue.FlightType) && issue.FlightType == "PASSENGER")
                            {
                                if (timeSpanToCutoff < 120 && timeSpanToCutoff > 0)
                                {
                                    issue.SortValue = 2;
                                }
                            }
                            if (!string.IsNullOrEmpty(issue.FlightType) && issue.FlightType == "CARGO")
                            {
                                if (timeSpanToCutoff < 240 && timeSpanToCutoff > 0)
                                {
                                    issue.SortValue = 1;
                                }
                            }

                        }
                        if (oldSortValue != issue.SortValue.Value)
                        {
                            customfield11803 cus = new customfield11803();
                            cus.customfield_12008 = issue.SortValue.Value;
                            fieldsSortvalue fields = new fieldsSortvalue();
                            fields.fields = cus;
                            string jsonResult = JsonConvert.SerializeObject(fields);
                            HttpRequest rq = new HttpRequest();
                            rq.Credentials = new Credentials()
                            {
                                UserName = AppSetting.USER_NAME,
                                Password = AppSetting.PASSWORD
                            };
                            string url = "http://support.als.com.vn:8882/rest/api/2/issue/" + issue.key;
                            rq.Url = url;
                            bool check = false;
                            rp = rq.Execute(fields, "PUT", "", false, issue.key + " UpdateSortValue",ref check);
                          //  _listIssueDetailToUpdate.Add(issue);
                        }
                        
                    }
                }
                //using (TransactionScope scope = AppSetting.GetTransactionScope())
                //{

                //    if (_listIssueDetailToUpdate.Count > 0)
                //    {
                //        Issue_detail.UpdateSortValue(_listIssueDetailToUpdate);
                //    }
                //    scope.Complete();
                //}
            }
            catch (Exception ex)
            {

                Log.LogToText(ex.ToString() + "---" + rp, "UpdateSortValue");
            }
            //Get danh sach Issue Detail can update sort value
           


        }

        public static void UpdateSortValueVer2()
        {
            try
            {
                var listVCTReadyForUpdate = new List<VCT>();
                var listVCTUpdate = VCT.GetListUpdateSortValue();
                if(listVCTUpdate.Count > 0)
                {
                    foreach(var vct in listVCTUpdate)
                    {
                        int oldSortValue = vct.SortValue.Value;
                        int timeSpanToCutoff = (int)Math.Round((vct.CutOffTime.Value - DateTime.Now).TotalMinutes);
                        if (!string.IsNullOrEmpty(vct.CargoType) && vct.CargoType == "PASSENGER")
                        {
                            if (timeSpanToCutoff < 120 && timeSpanToCutoff > 0)
                            {
                                vct.SortValue = 2;
                            }
                        }
                        if (!string.IsNullOrEmpty(vct.CargoType) && vct.CargoType == "CARGO")
                        {
                            if (timeSpanToCutoff < 240 && timeSpanToCutoff > 0)
                            {
                                vct.SortValue = 1;
                            }
                        }
                        if (oldSortValue != vct.SortValue.Value)
                        {
                            listVCTReadyForUpdate.Add(vct);
                        }

                    }
                    VCT.Update(listVCTReadyForUpdate);
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public static void UpdateSortValueCallTruck()
        {
            //lay ra danh sách xe cần update Sort value
            //try
            //{
            //    var listTruckForUpdate = new List<tblDangKyGoiXe>();
            //    var listTruckUpdate = tblDangKyGoiXe.GetListTruckUpdateSortValue();
            //    if (listTruckUpdate.Count > 0)
            //    {
            //        foreach (var truck in listTruckUpdate)
            //        {
            //            int oldSortValue = truck.SortValue.HasValue? truck.SortValue.Value : 0;

                       
            //            var flup = FLightFlup.GetByCode(truck.Remark.Trim());
            //            int timeSpanToCutoff = (int)Math.Round((flup.LAT.Value - DateTime.Now).TotalMinutes);
            //            if (flup.FlightType == "PASSENGER")
            //            {
            //                if (timeSpanToCutoff < 120 && timeSpanToCutoff > 0)
            //                {
            //                    truck.SortValue = 2;
            //                }
            //            }
            //            if (flup.FlightType == "CARGO")
            //            {
            //                if (timeSpanToCutoff < 240 && timeSpanToCutoff > 0)
            //                {
            //                    truck.SortValue = 1;
            //                }
            //            }
            //            if (oldSortValue != truck.SortValue.Value)
            //            {
            //                listTruckForUpdate.Add(truck);
            //            }

            //        }
            //        tblDangKyGoiXe.Update(listTruckForUpdate);
            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

        }
    }
}
