using DATAACCESS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TASK.DATA;
using TASK.Model.ViewModel;
using TASK.Settings;

namespace TASK.Services
{
    public class PXKService
    {
        public static List<tblPXK> _listPXKToUpdate;
        public static void CheckProcessPXK()
        {
            _listPXKToUpdate = new List<tblPXK>();
            ProcessData();
            ProcessDataPhase2();
        }
        public static void ProcessData()
        {
            List<tblPXK> listPXKCheckLagi = tblPXK.GetAll();
            if (listPXKCheckLagi.Count > 0)
            {
                try
                {
                    List<PXKHermesViewModel> listPXKHermesCheckLagi = new PXKLagiAccess().GetPXKHermes(listPXKCheckLagi);
                    List<PXKHermesViewModel> listPXKViewModel = new List<PXKHermesViewModel>();
                    if (listPXKHermesCheckLagi.Count > 0)
                    {
                        foreach (var pxk in listPXKHermesCheckLagi)
                        {
                            if (!listPXKViewModel.Any(c => c.PXKNo == pxk.PXKNo))
                            {
                                pxk.GroupNumber = 1;
                                listPXKViewModel.Add(pxk);
                            }
                            else
                            {
                                var pxkObject = listPXKViewModel.SingleOrDefault(c => c.PXKNo == pxk.PXKNo);
                                pxkObject.Location += ", " + pxk.Location;
                                pxkObject.GroupNumber += 1;
                            }
                        }
                        foreach (var pxk in listPXKViewModel)
                        {
                            List<tblPXK> items = tblPXK.GetByPXK(pxk.PXKNo.Trim());
                            if (items.Count > 0)
                            {
                                foreach (var item in items)
                                {
                                    if (pxk.Location.Contains("IDA") || pxk.Location.Contains("CUS") || pxk.Location.Contains("IDR"))
                                    {
                                        item.Status = 3;
                                        item.PXK = pxk.PXKNo;
                                        item.Hawb = pxk.Hawb;
                                        item.AWB = pxk.AWB;
                                        item.Pieces = pxk.quantity;
                                        item.Weight = double.Parse(pxk.weight);
                                        item.GroupNumer = pxk.GroupNumber;
                                        item.Finish = DateTime.Now;
                                    }
                                    else
                                    {
                                        if (item.Status == 0)
                                        {
                                            item.Status = 1;
                                            item.PXK = pxk.PXKNo;
                                            item.Hawb = pxk.Hawb;
                                            item.AWB = pxk.AWB;
                                            item.Pieces = pxk.quantity;
                                            item.Weight = double.Parse(pxk.weight);
                                            item.GroupNumer = pxk.GroupNumber;
                                        }
                                    }
                                    tblPXK.UpdatePXK(item);
                                }
                            }
                        }
                    }
                    List<tblPXK> listPXKCheckVCT = tblPXK.GetListCheck();
                    if (listPXKCheckVCT.Count > 0)
                    {
                        List<PXKHermesViewModel> listPXKHermes = new PXKAccess().GetPXKHermes(listPXKCheckVCT);
                        foreach (var pxk in listPXKHermes)
                        {
                            if (!string.IsNullOrEmpty(pxk.VCTNo.Trim()))
                            {
                                List<tblPXK> items = tblPXK.GetByPXK(pxk.PXKNo.Trim());
                                if (items.Count > 0)
                                {
                                    foreach (var item in items)
                                    {
                                        if (item.Status != 3)
                                        {
                                            item.Status = 2;
                                            item.PXK = pxk.PXKNo;
                                            item.VCT = pxk.VCTNo;
                                        }
                                        else
                                        {
                                            item.PXK = pxk.PXKNo;
                                            item.VCT = pxk.VCTNo;
                                        }
                                        tblPXK.UpdatePXK(item);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    Log.WriteLog(ex.ToString(), "CheckPxk");
                }
                
            }
        }
        public static void ProcessDataPhase2()
        {
            
        }
    }
}
