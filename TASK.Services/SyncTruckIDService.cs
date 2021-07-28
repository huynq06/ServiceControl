using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TASK.DATA;
using TASK.Settings;

namespace TASK.Services
{
    public static class SyncTruckIDService
    {
        public static List<tblDangKyGoiXe> _listTruckToUpdate;
        public static void DoWord()
        {
            _listTruckToUpdate = new List<tblDangKyGoiXe>();
            ProcessData();
            try
            {
                using (TransactionScope scope = AppSetting.GetTransactionScope())
                {
                    if (_listTruckToUpdate.Count > 0)
                    {
                        tblDangKyGoiXe.Update(_listTruckToUpdate);
                    }
                    scope.Complete();
                }
            }

            catch (Exception ex)
            {
                Log.WriteSeriviceLog(ex.ToString(),"SynIDExceptionLog.text");
            }
        }
        public static void ProcessData()
        {
            try
            {
                DateTime dateCheck = DateTime.Now.AddMinutes(-90);
                //lay ra danh sach cac xe trong vong 1h
                List<tblDangKyGoiXe> listTruckForSync = tblDangKyGoiXe.GetListTruckForSync(dateCheck);
                List<tblDangKyVaoRa> listTruckBuyTickets = tblDangKyVaoRa.GetTruckBuyTicket(dateCheck);
                foreach (var item in listTruckForSync)
                {
                    var truck = listTruckBuyTickets.FirstOrDefault(c => c.BienSoXe.Trim() == item.BienSoXe.Trim());
                    if (truck != null)
                    {
                        //tiep tuc ktra xem xe do da close hay chua
                        var CheckTruckClose = tblTicketStatus.CheckCloseTruck(Guid.Parse(truck.SyncID.Trim()));
                        if(CheckTruckClose == null)
                        {
                            item.SynID = Guid.Parse(truck.SyncID.Trim());
                            item.TruckStatus = Utils.TruckConstants.TruckTicket;
                            _listTruckToUpdate.Add(item);
                        }
                      
                    }
                }
            }
            catch (Exception ex)
            {

                Log.WriteSeriviceLog(ex.ToString(), "ProcessData-SynIDExceptionLog.text");
            }
          
        }
    }
}
