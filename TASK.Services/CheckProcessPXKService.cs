using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATAACCESS;
using TASK.DATA;
using TASK.Model.DBModel;
using TASK.Settings;
using System.Transactions;
namespace TASK.Services
{
    public static class CheckProcessPXKService
    {

        //lay ra danh sach van don can check
        public static void Process()
        {
             List<CapSo> listCapso = new CheckPXKAccess().GetAll(DateTime.Now);
          //  List<LogCapSo> listCapso = LogCapSo.GetListAllHawb();
            foreach (var item in listCapso)
            {
                if(item.HAWB == "ALLHAWB")
                {
                    int total = 0;
                    List<string> listIdents = new CheckAwbReceiveAccess().GetAllHawbLagiIdent(item.MAWB);
                    if(listIdents.Count > 0)
                    {
                         total = listIdents.Count;
                        foreach(var obj in listIdents)
                        {
                            if(new CheckAwbReceiveAccess().CheckProcessPXKByLagiIdent(obj))
                            {
                                total -= 1;
                            }
                        }
                    }
                    item.AWBRemain = total;
                    //Get tat ca HAWB cua MAWB
                }
                else if(item.HAWB.ToUpper().Contains("CAP"))
                {
                    List<LogCapSo> listCaplai = LogCapSo.GetListCaplaiAWB(item.HAWB.Trim().Substring(item.HAWB.Trim().Length - 4));
                    int total = listCaplai.Count();
                    foreach(var itemCheck in listCaplai)
                    {
                        if (new CheckAwbReceiveAccess().CheckProcessPXK(itemCheck.MAWB, itemCheck.HAWB))
                        {
                            total  -= 1;
                        }
                    }
                    item.AWBRemain = total;
                }
                else if(string.IsNullOrEmpty(item.HAWB.Trim()))
                {
                    if(new CheckAwbReceiveAccess().CheckProcessPXKNoHawb(item.MAWB))
                    {
                        item.AWBRemain = 0;
                    }
                    else
                    {
                        item.AWBRemain = 1;
                    }
                }
                else
                {
                    if (new CheckAwbReceiveAccess().CheckProcessPXK(item.MAWB,item.HAWB))
                    {
                        item.AWBRemain = 0;
                    }
                    else
                    {
                        item.AWBRemain = 1;
                    }
                }
            }
            LogCapSo.UpdateStatus(listCapso);
        }
    }
}
