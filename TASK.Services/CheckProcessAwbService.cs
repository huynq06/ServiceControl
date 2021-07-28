using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATAACCESS;
using TASK.DATA;
using TASK.Model.ViewModel;
using TASK.Settings;
using System.Transactions;

namespace TASK.Services
{
    public class CheckProcessAwbService
    {
        public static void CheckProcessAwb()
        {
            List<VCT> listAwbActive = VCT.GetAllActiveToday();
            List<VCT> listVCT4Update = new List<VCT>();
            if(listAwbActive.Count > 0)
            {
                List<AwbReceivedViewModel> listAwbReceived = new CheckAwbReceiveAccess().CheckReceive(listAwbActive);
                foreach(var awbReceived in listAwbReceived)
                {
                    List<VCT> items = VCT.GetByLagiIden(awbReceived.Lagi_Ident);
                    if(items.Count > 0)
                    {
                        foreach(var item in items)
                        {
                            if(awbReceived.Received > 0)
                            {
                                item.LABS_RECIEVED = awbReceived.Received;
                                if (awbReceived.Received >= item.LABS_QUANTITY_BOOKED)
                                {
                                    item.AWB_STATUS = 3;

                                }
                                listVCT4Update.Add(item);
                            }
                           
                        }
                    }
                }
                if(listVCT4Update.Count > 0)
                {
                    VCT.Update(listVCT4Update);
                }
                //foreach (var item in listAwbActive)
                //{
                //    item.LABS_RECIEVED = new CheckAwbReceiveAccess().CheckReceive(item.LABS_IDENT_NO);
                //    if(item.LABS_RECIEVED == item.LABS_QUANTITY_BOOKED)
                //    {
                //        item.AWB_STATUS = 3;
                //        item.LAST_GROUP = new CheckAwbReceiveAccess().GetLastGroup(item.LABS_IDENT_NO);
                //    }
                //}
               
            }
           
        }
        public static void CheckConfirmAwb()
        {
            List<VCT> listAwbToUpdate = new List<VCT>();
            List<VCT> listAwbActive = VCT.GetAllNotConfirmToday();
            if (listAwbActive.Count > 0)
            {
                foreach (var item in listAwbActive)
                {
                    if (new LABSAccess().CheckCompleteAWB(item.LABS_AWB.Trim().Replace("-", "")))
                    {
                        listAwbToUpdate.Add(item);
                    }
                }
                VCT.UpdateConfirm(listAwbToUpdate);
            }

        }
        public static void CheckConfirmAwbExpand()
        {
            List<VCT> listAwbToUpdate = new List<VCT>();
            List<VCT> listAwbActive = VCT.GetNotConfirmALSWToday();
            if (listAwbActive.Count > 0)
            {
                foreach (var item in listAwbActive)
                {

                    //kiểm tra xem group có 1 kiện ko?
                    if (new LABSAccess().CheckGroupAWB(item.LABS_IDENT_NO))
                    {
                        //kiểm tra group đã đc move đến ESL chưa
                        if(new LABSAccess().CheckGroupMoveAWB(item.LABS_IDENT_NO))
                        {
                            listAwbToUpdate.Add(item);
                        }
                        
                    }
                }
                VCT.UpdateConfirm(listAwbToUpdate);
            }

        }

    }
}
