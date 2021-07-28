using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using DATAACCESS;

namespace TASK.Services
{
    public class SHCHawbService
    {
        public static void CheckHawbSHC()
        {
            List<Flight> flights = Flight.GetAllSHC();
           // List<HawbInAwb> listHawbCheckReceiveCheck = HawbInAwb.GetSHC().Where(c => flights.Any(p => p.FlightID == c.FlightID)).Where(c => c.CheckValue == 1 && c.Process == 0).ToList();
            List<HawbInAwb> listHawbCheckReceive = HawbInAwb.GetAllSHC(flights);
            if (listHawbCheckReceive.Count > 0)
            {
                foreach (var hawb in listHawbCheckReceive)
                {
                    Flight flight = Flight.GetByGuid(hawb.FlightID.Value);

                    bool check = new CheckHawbReciveAccess().CheckReciveHAWB(hawb.HAWB, flight.FLUI_LANDED_DATE.ToString());
                    if (check)
                    {
                        hawb.Process = 1;
                        HawbInAwb.UpdateHAWBStatus(hawb);
                        Log.WriteLog("Lô hàng lạnh: " + hawb.HAWB + "Trên chuyến bay" + flight.FlightNumber + " Đã nhận đầy đủ", "HAWB Monitor");
                    }
                }
            }

            List<HawbInAwb> listHAwbCheckMoveLocation = HawbInAwb.GetAllChecked(flights);
            //B2: Kiểm tra các group đã được move về vị trí chưa
            if (listHAwbCheckMoveLocation.Count > 0)
            {
                foreach (var hawb in listHAwbCheckMoveLocation)
                {
                    Flight flight = Flight.GetByGuid(hawb.FlightID.Value);
                    bool check = new CheckHawbReciveAccess().CheckMoveHAwb(hawb.HAWB, flight.FLUI_LANDED_DATE.ToString());
                    if (check)
                    {
                        hawb.Process = 2;
                        HawbInAwb.UpdateHAWBStatus(hawb);
                        Log.WriteLog("Lô hàng lạnh: " + hawb.HAWB + "Trên chuyến bay" + flight.FlightNumber + " Đã được move toàn bộ đến ICR", "HAWB Monitor");
                    }
                }
            }
        }
    }
}
