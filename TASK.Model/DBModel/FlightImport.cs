using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.DBModel
{
    public class FlightImport
    {
        public int FlightArrID { set; get; }
        public string AreaCode { set; get; }
        public DateTime? Date { set; get; }
        public string ArrFltNo { set; get; }
        public string STA { set; get; }
        public string STA_Change { set; get; }
        public DateTime? STA_Date { set; get; }
        public string ETA { set; get; }
        public string ATA { set; get; }
        public string AOnBT { set; get; }
        public string Aircraft { set; get; }
        public string RegisterNo { set; get; }
        public int? MTOW { set; get; }
        public string Routing { set; get; }
        public string Terminal { set; get; }
    
        public string Lobby { set; get; }
        public string ParkNo { set; get; }
        public string Conveyor_Belt { set; get; }
        public string Opt { set; get; }
        public int GHID { set; get; }
        public string GHCode { set; get; }
        public int? NatureID { set; get; }
        public string Nature { set; get; }
        public int? CarryID { set; get; }
        public string Carry { set; get; }
        public string ConnectAirport { set; get; }
        public string ArrFlightType { set; get; }
        public string ActualPax { set; get; }
        public string ActualBag { set; get; }
        public string ActualCargo { set; get; }
        public string Status { set; get; }
        public string Remark { set; get; }
      
    }
}
