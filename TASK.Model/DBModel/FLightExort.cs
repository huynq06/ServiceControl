using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TASK.Model.DBModel
{
   
    public class FLightExort
    {
        public int FlightDepID { set; get; }
        public string AreaCode { set; get; }
        public DateTime? Date { set; get; }
        public string DepFltNo { set; get; }
        public string STD { set; get; }
        public string STD_Change { set; get; }
        public DateTime? STD_Date { set; get; }
        public string ETD { set; get; }
        public string ATD { set; get; }
        public string AOBT { set; get; }
        public string Aircraft { set; get; }
        public string RegisterNo { set; get; }
        public int? MTOW { set; get; }
        public string Terminal { set; get; }
        public string Routing { set; get; }
        public string ParkNo { set; get; }
        public string Conveyor_Belt { set; get; }
        public string Opt { set; get; }
        public string GHID { set; get; }
        public string GHCode { set; get; }
        public int? NatureID { set; get; }
        public string Nature { set; get; }
        public int? CarryID { set; get; }
        public string Carry { set; get; }
        public string ConnectAirport { set; get; }
        public string ActualPax { set; get; }
        public string ActualBag { set; get; }
        public string ActualCargo { set; get; }
        public string Status { set; get; }
        public string Remark { set; get; }
        public string CkiOpen { set; get; }
        public string CkiClose { set; get; }
        public string PreBDT { set; get; }
        public string BDT { set; get; }
        public string FHT { set; get; }
        public string PBT { set; get; }
        public int? Config { set; get; }
        public string Gate { set; get; }
        public string CkiRow { set; get; }
        public string DepFlightType { set; get; }
        public int? EstULD { set; get; }
        public string EstPax { set; get; }
    }
}
