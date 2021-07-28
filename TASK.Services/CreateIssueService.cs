using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Model.DBModel;
using DATAACCESS;
using TASK.DATA;
using TASK.Model.ViewModel;
using TASK.Settings;
using Newtonsoft.Json;

namespace TASK.Services
{
    public static class CreateIssueService
    {
        public static List<VCT> _listVCTToInsert;
        // lay danh sach VCT cua nhung lo hang chua can de tao Issue
        public static void GetListVCT()
        {
            string rp = "";
            try
            {
                _listVCTToInsert = new List<VCT> { };
                List<VCTHermes> listVCTToCreatIssue = new List<VCTHermes>();
                List<VCTHermes> listVCTHermes = new VCTAccess().GetData();
                List<VCT> listVCTDB = VCT.GetAllToday();
                foreach (var vctHermes in listVCTHermes)
                {
                    if (listVCTDB.All(c => c.LABS_IDENT_NO != vctHermes.LABS_IDENT_NO))
                    {
                        listVCTToCreatIssue.Add(vctHermes);
                    }
                }
                List<Issue> listIussues = Issue.GetAll().Where(c => c.fields_status_id == 10904).ToList();

                foreach (var issue in listVCTToCreatIssue)
                {
                    PriorityID priorityID = new PriorityID();
                    priorityID.id = "10102";
                    ProjectID projectID = new ProjectID();
                    projectID.id = "11011";
                    IssueTypeID issueTypeID = new IssueTypeID();
                    issueTypeID.id = "10913";
                    fieldValue fieldValue = new fieldValue();
                    fieldValue.issuetype = issueTypeID;
                    fieldValue.project = projectID;
                    fieldValue.priority = priorityID;
                    fieldValue.customfield_11900 = issue.LABS_AWB;
                    fieldValue.customfield_11809 = "";
                    fieldValue.customfield_11804 = issue.LABS_QUANTITY_BOOKED;
                    fieldValue.customfield_11803 = issue.LABS_WEIGHT_BOOKED;
                    fieldValue.customfield_11805 = issue.BOOKING_FLIGHT;
                    if (issue.CutoffTime.HasValue)
                    {
                        fieldValue.customfield_12003 = issue.CutoffTime.ToString();
                        int timeSpanToCutoff = (int)Math.Round((issue.CutoffTime.Value - DateTime.Now).TotalMinutes);
                        if (!string.IsNullOrEmpty(issue.FLUP_TYPE) && issue.FLUP_TYPE == "CARGO")
                        {
                            if (timeSpanToCutoff < 240 && timeSpanToCutoff > 0)
                            {
                                fieldValue.customfield_12008 = 1;
                            }
                        }
                        if (!string.IsNullOrEmpty(issue.FLUP_TYPE) && issue.FLUP_TYPE == "PASSENGER")
                        {
                            if (timeSpanToCutoff < 120 && timeSpanToCutoff > 0)
                            {
                                fieldValue.customfield_12008 = 2;
                            }
                        }
                    }
                    fieldValue.customfield_12010 = issue.FLUP_TYPE;
                    fieldValue.summary = issue.LABS_AWB + "/" + issue.BOOKING_FLIGHT + "/" + issue.LABS_QUANTITY_BOOKED + "/ " + issue.LABS_AWB.Substring(issue.LABS_AWB.Length - 4);
                    fieldJson fields = new fieldJson();
                    fields.fields = fieldValue;
                    string jsonResult = JsonConvert.SerializeObject(fields);
                    HttpRequest rq = new HttpRequest();
                    rq.Credentials = new Credentials()
                    {
                        UserName = AppSetting.USER_NAME,
                        Password = AppSetting.PASSWORD
                    };
                    string url = "http://support.als.com.vn:8882/rest/api/2/issue";
                    rq.Url = url;
                    bool check = false;
                    rp = rq.Execute(fields, "POST", "", false, issue.LABS_AWB + " GetListVCT", ref check);
                    if (check)
                    {
                        VCT vct = new VCT();
                        vct.LABS_IDENT_NO = issue.LABS_IDENT_NO;
                        vct.LABS_AWB = issue.LABS_AWB;
                        vct.LABS_QUANTITY_BOOKED = issue.LABS_QUANTITY_BOOKED;
                        vct.LABS_WEIGHT_BOOKED = issue.LABS_WEIGHT_BOOKED;
                        vct.BOOKING_FLIGHT = issue.BOOKING_FLIGHT;
                        vct.LABS_CREATED_AT = issue.LABS_CREATED_AT;
                        vct.AWB_STATUS = 0;
                        vct.LABS_RECIEVED = 0;
                        vct.ISSUE_STATUS = 0;
                        vct.SortValue = 0;
                        if (issue.CutoffTime.HasValue)
                        {
                            vct.CutOffTime = issue.CutoffTime;
                            int timeSpanToCutoff = (int)Math.Round((issue.CutoffTime.Value - DateTime.Now).TotalMinutes);
                            if (!string.IsNullOrEmpty(issue.FLUP_TYPE) && issue.FLUP_TYPE == "CARGO")
                            {
                                if (timeSpanToCutoff < 240 && timeSpanToCutoff > 0)
                                {
                                     vct.SortValue = 1;
                                }
                            }
                            if (!string.IsNullOrEmpty(issue.FLUP_TYPE) && issue.FLUP_TYPE == "PASSENGER")
                            {
                                if (timeSpanToCutoff < 120 && timeSpanToCutoff > 0)
                                {
                                    vct.SortValue = 2;
                                }
                            }

                        }
                        vct.CargoType = issue.FLUP_TYPE;
                        VCT.Insert(vct);
                    }
                }
            }
            catch (Exception ex)
            {

                Log.LogToText(ex.ToString() + " response: " + "----------", "Create Issue");
            }
            
        }
        public static void GetListVCTVer2()
        {
            try
            {
                _listVCTToInsert = new List<VCT> { };
                List<VCTHermes> listVCTToCreatIssue = new List<VCTHermes>();
                List<VCTHermes> listVCTHermes = new VCTAccess().GetData();
                List<VCT> listVCTDB = VCT.GetAllToday();
                foreach (var vctHermes in listVCTHermes)
                {
                    
                        if (listVCTDB.All(c => c.LABS_IDENT_NO != vctHermes.LABS_IDENT_NO))
                        {
                            listVCTToCreatIssue.Add(vctHermes);
                        }
                        //else
                        //{
                        //    var vctDB = listVCTDB.SingleOrDefault(c => c.LABS_IDENT_NO == vctHermes.LABS_IDENT_NO);
                        //    if (vctDB.VCT_DATE != vctHermes.VCT_DATE)
                        //    {
                        //        VCT.UpdateVCT(vctDB.ID);
                        //    }
                        //}
                    
                  
                }
                foreach (var issue in listVCTToCreatIssue)
                {
                    VCT vct = new VCT();
                    vct.LABS_IDENT_NO = issue.LABS_IDENT_NO;
                    vct.LABS_AWB = issue.LABS_AWB;
                    vct.LABS_QUANTITY_BOOKED = issue.LABS_QUANTITY_BOOKED;
                    vct.LABS_WEIGHT_BOOKED = issue.LABS_WEIGHT_BOOKED;
                    vct.BOOKING_FLIGHT = issue.BOOKING_FLIGHT;
                    vct.LABS_CREATED_AT = DateTime.Now;
                    vct.AWB_STATUS = 0;
                    vct.LABS_RECIEVED = 0;
                    vct.ISSUE_STATUS = 0;
                    vct.SortValue = 0;
                    vct.VCT_DATE = DateTime.Now.Date;
                    vct.ConfirmStatus = 0;
                    vct.AGENT_NAME = issue.AGENT_NAME;
                    if(!string.IsNullOrEmpty(issue.FLUP_ID))
                    {
                        var flup = FLightFlup.GetByID(issue.FLUP_ID);
                        if(flup != null)
                        {
                            if (flup.LAT.HasValue)
                            {
                                vct.CutOffTime = flup.LAT;
                                int timeSpanToCutoff = (int)Math.Round((flup.LAT.Value - DateTime.Now).TotalMinutes);
                                if (!string.IsNullOrEmpty(issue.FLUP_TYPE) && issue.FLUP_TYPE == "CARGO")
                                {
                                    if (timeSpanToCutoff < 240 && timeSpanToCutoff > 0)
                                    {
                                        vct.SortValue = 1;
                                    }
                                }
                                if (!string.IsNullOrEmpty(issue.FLUP_TYPE) && issue.FLUP_TYPE == "PASSENGER")
                                {
                                    if (timeSpanToCutoff < 120 && timeSpanToCutoff > 0)
                                    {
                                        vct.SortValue = 2;
                                    }
                                }

                            }
                        }
                       
                    }
                   
                    vct.CargoType = issue.FLUP_TYPE;
                    VCT.Insert(vct);
                }
            }
            catch (Exception ex)
            {

                Log.LogToText(ex.ToString() + " response: " + "----------", "Create Issue");
            }
          
        }

    }
}
