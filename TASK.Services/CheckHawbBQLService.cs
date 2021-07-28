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
    public static class CheckHawbBQLService
    {
        public static void CheckHawbBqlOpenFlight()
        {
            //lay ra nhug hawb can check bql
            List<Flight> flights = Flight.GetAllBQL();
            //lay ra hawb cua cac chuyen bay open
            List<HawbInAwb> hawbs = HawbInAwb.GetAllBQL(flights);
            if (hawbs.Count > 0)
            {
                foreach (var hawb in hawbs)
                {
                    if (new CheckHawbBqlAccess().CheckBQLHawb(hawb.Lagi_Identity.Value))
                    {
                        var Awb = AWBByULD.GetByID(hawb.AWB_ID.Value);
                        var flight = Flight.GetByGuid(hawb.FlightID.Value);
                        HawbInAwb.UpdateBql(hawb);
                        Log.WriteLog("Hawb: " + hawb.HAWB + " Lô hàng: " + Awb + " Trên chuyến bay: " + flight.FlightNumber + " được set BQL");
                    }
                }
            }
        }
    }
}
