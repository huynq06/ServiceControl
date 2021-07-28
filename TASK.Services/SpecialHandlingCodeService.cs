using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using DATAACCESS;

namespace TASK.Services
{
    public class SpecialHandlingCodeService
    {
        // Description: Kiểm tra hàng lạnh
        //B1: Kiểm tra hàng đã nhận đủ chưa
        
        public static void CheckReciveSHC()
        {
            List<Flight> flights = Flight.GetAllSHC();
            List<AWBByULD> listAwbCheckReceive = AWBByULD.GetAllSHC(flights);
            if (listAwbCheckReceive.Count > 0)
            {
                foreach (var awb in listAwbCheckReceive)
                {
                    Flight flight = Flight.GetByGuid(awb.Flight_ID.Value);
                  
                    bool check = new CheckAwbReceiveAccess().CheckReciveAWB(awb.AWB, flight.FLUI_LANDED_DATE.ToString());
                    if (check)
                    {
                        awb.Process = 1;
                        AWBByULD.UpdateAWBStatus(awb);
                        Log.WriteLog("Lô hàng lạnh: " + awb.AWB + "Trên chuyến bay" + flight.FlightNumber + " Đã nhận đầy đủ","ICR Monitor");
                    }
                }
            }

            List<AWBByULD> listAwbCheckMoveLocation = AWBByULD.GetAllCheck(flights);
            //B2: Kiểm tra các group đã được move về vị trí chưa
            if (listAwbCheckMoveLocation.Count > 0)
            {
                foreach (var awb in listAwbCheckMoveLocation)
                {
                    Flight flight = Flight.GetByGuid(awb.Flight_ID.Value);
                    bool check = new CheckAwbReceiveAccess().CheckMoveAwb(awb.AWB, flight.FLUI_LANDED_DATE.ToString());
                    if (check)
                    {
                        awb.Process = 2;
                        AWBByULD.UpdateAWBStatus(awb);
                        Log.WriteLog("Lô hàng lạnh: " + awb.AWB + "Trên chuyến bay" + flight.FlightNumber + " Đã được move toàn bộ đến ICR","ICR Monitor");
                    }
                }
            }
        }
    }
}
