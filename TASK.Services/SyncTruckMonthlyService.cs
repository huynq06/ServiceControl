using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TASK.DATA;
using TASK.Settings;
using Utils;
namespace TASK.Services
{
    public static class SyncTruckMonthlyService
    {
        public static List<tblDangKyGoiXe> _listTruckToUpdate;
        public static void DoWork()
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
                Log.WriteSeriviceLog(ex.ToString(), "TrackAndTraceTruckMonthlyExceptionLog.text");
            }
        }
        public static void ProcessData()
        {
            //lay danh sach nhung xe truckstatus = 1 va chua co synid de dong bo
            DateTime dateCheck = DateTime.Now.AddHours(-1);
            List<tblDangKyGoiXe> listTruckToTrack = tblDangKyGoiXe.GetListTruckMonthlyForTrackandTrace(dateCheck);
            List<tblTicketStatus> ListTruckMonthlyCheckInOut = tblTicketStatus.GetListMonthlyAroundOneHour(dateCheck);
            try
            {
                foreach (var item in listTruckToTrack)
                {
                    var truckCheckInOut = ListTruckMonthlyCheckInOut.Where(c => c.BienSoXe == item.BienSoXe).ToList();
                    //List<tblTicketStatus> listCheckInOut = tblTicketStatus.GetBySynID(item.SynID.Value);
                    if (truckCheckInOut.Count > 0)
                    {
                        var findTruck = truckCheckInOut.OrderByDescending(c => c.ID).FirstOrDefault();
                        if (findTruck != null && findTruck.ActionCode == "CHECK_OUT" && findTruck.ActionDateTime > item.GioGoi)
                        {
                            item.TruckStatus = TruckConstants.TruckCheckOut;
                        }
                        if (findTruck != null &&  findTruck.ActionCode == "CHECK_IN" && findTruck.ActionDateTime > item.GioGoi)
                        {
                            item.TruckStatus = TruckConstants.TruckCheckIn;
                        }
                        
                        _listTruckToUpdate.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

                Log.WriteSeriviceLog(ex.ToString(), "TrackAndTraceTruckMonthlyExceptionLog.text");
            }
        }
    }
}
