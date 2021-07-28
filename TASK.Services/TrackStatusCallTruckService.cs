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
    public static class TrackStatusCallTruckService
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
                Log.WriteSeriviceLog(ex.ToString(), "TrackAndTraceTruckExceptionLog.text");
            }
        }
        public static void ProcessData()
        {
            List<tblDangKyGoiXe> listTruckToTrack = tblDangKyGoiXe.GetListTruckForTrackandTrace();
            try
            {
                foreach (var item in listTruckToTrack)
                {
                    List<tblTicketStatus> listCheckInOut = tblTicketStatus.GetBySynID(item.SynID.Value);
                    if (listCheckInOut.Count > 0)
                    {
                        if (listCheckInOut.Count == 1 && listCheckInOut[0].ActionStatus == 1)
                        {
                            if (listCheckInOut[0].ActionCode == "CHECK_IN")
                            {
                                item.TruckStatus = TruckConstants.TruckCheckIn;
                            }
                            if (listCheckInOut[0].ActionCode == "CHECK_OUT")
                            {
                                item.TruckStatus = TruckConstants.TruckCheckOut;
                            }
                        }
                        else
                        {
                            var findTruck = listCheckInOut.FirstOrDefault(c => c.ActionValue == "GATEOUT");
                            if (findTruck != null)
                            {
                                item.TruckStatus = TruckConstants.TruckCheckOut;
                            }
                            else
                            {
                                item.TruckStatus = TruckConstants.TruckCheckIn;
                            }
                        }
                        _listTruckToUpdate.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

                Log.WriteSeriviceLog(ex.ToString(), "TrackAndTraceTruckExceptionLog.text");
            }
         
        }
    }
}
