using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utils;
using TASK.Model.DBModel;
using System.Xml.Linq;
using DATAACCESS;
using TASK.DATA;
using System.Threading;

namespace TASK.Services
{
    public static class GetFLightDiService
    {
        public static void GetData()
        {
            string[] listDay = { DateTime.Now.ToString("MM/dd/yyyy"), DateTime.Now.AddDays(1).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(2).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(3).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(4).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(5).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(6).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(7).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(8).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(9).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(10).ToString("MM/dd/yyyy") };
            try
            {
                foreach (var day in listDay)
                {
                    string url = @"C:\temFligtRequest.xml";
                    //string day = DateTime.Now.ToString("dd/MM/yyyy");
                    Task<string> task = Task.Run<String>(async () => await FlightRequest.Command(url, day));
                    string result = task.Result;
                    //XmlDocument xmldoc = new XmlDocument();
                    //xmldoc.LoadXml(result);
                    var dox = XDocument.Parse(result, LoadOptions.PreserveWhitespace);
                    List<FLightExort> flights = dox.Descendants("Flight").Select(d =>
           new FLightExort
           {
               FlightDepID = int.Parse(d.Element("FlightDepID").Value),
               AreaCode = d.Element("AreaCode").Value.Trim(),
               Date = string.IsNullOrEmpty(d.Element("Date").Value) ? (DateTime?)null : DateTime.Parse(d.Element("Date").Value),
               DepFltNo = d.Element("DepFltNo").Value.Trim(),
               STD = d.Element("STD").Value.Trim(),
               STD_Change = d.Element("STD_Change").Value.Trim(),
               STD_Date = string.IsNullOrEmpty(d.Element("STD_Date").Value) ? (DateTime?)null : DateTime.Parse(d.Element("STD_Date").Value),
               ETD = d.Element("ETD") != null ? d.Element("ETD").Value.Trim() : "",
               ATD = d.Element("ATD") != null ? d.Element("ATD").Value.Trim() : "",
               AOBT = d.Element("AOBT") != null ? d.Element("AOBT").Value.Trim() : "",
               Aircraft = d.Element("Aircraft").Value.Trim(),
               RegisterNo = d.Element("RegisterNo") != null ? d.Element("RegisterNo").Value.Trim() : "",
               MTOW = string.IsNullOrEmpty(d.Element("MTOW").Value) ? 0 : int.Parse(d.Element("MTOW").Value),
               Routing = d.Element("Routing").Value.Trim(),
               Terminal = d.Element("Terminal").Value.Trim(),
               ParkNo = d.Element("ParkNo") != null ? d.Element("ParkNo").Value.Trim() : "",
               Conveyor_Belt = d.Element("Conveyor_Belt").Value.Trim(),
               Opt = d.Element("Opt").Value.Trim(),
               GHID = d.Element("GHID").Value.Trim(),
               GHCode = d.Element("GHCode").Value.Trim(),
               NatureID = string.IsNullOrEmpty(d.Element("NatureID").Value) ? 0 : int.Parse(d.Element("NatureID").Value),
               Nature = d.Element("Nature").Value,
               CarryID = string.IsNullOrEmpty(d.Element("CarryID").Value) ? 0 : int.Parse(d.Element("CarryID").Value),
               Carry = d.Element("Carry").Value.Trim(),
               ConnectAirport = d.Element("ConnectAirport") != null ? d.Element("ConnectAirport").Value : "",
               ActualPax = d.Element("ActualPax").Value.Trim(),
               ActualBag = d.Element("ActualBag").Value.Trim(),
               ActualCargo = d.Element("ActualCargo").Value.Trim(),
               Status = d.Element("Status").Value.Trim(),
               Remark = d.Element("Remark").Value.Trim(),
               CkiOpen = d.Element("CkiOpen") != null ? d.Element("CkiOpen").Value.Trim() : "",
               CkiClose = d.Element("CkiClose") != null ? d.Element("CkiClose").Value.Trim() : "",
               PreBDT = d.Element("PreBDT") != null ? d.Element("PreBDT").Value.Trim() : "",
               BDT = d.Element("BDT") != null ? d.Element("BDT").Value.Trim() : "",
               FHT = d.Element("FHT") != null ? d.Element("FHT").Value.Trim() : "",
               PBT = d.Element("PBT") != null ? d.Element("PBT").Value.Trim() : "",
               Config = string.IsNullOrEmpty(d.Element("Config").Value) ? 0 : int.Parse(d.Element("Config").Value),
               Gate = d.Element("Gate") != null ? d.Element("Gate").Value.Trim() : "",
               CkiRow = d.Element("CkiRow").Value.Trim(),
               DepFlightType = d.Element("DepFlightType").Value.Trim(),
               EstULD = string.IsNullOrEmpty(d.Element("EstULD").Value) ? 0 : int.Parse(d.Element("EstULD").Value),
               EstPax = d.Element("EstPax").Value.Trim(),

           }).ToList();

                    foreach (var item in flights)
                    {
                        int count = new AomsNiaFlupAccess().GetById(item.FlightDepID);
                        if (count == 0)
                            new AomsNiaFlupAccess().Add(item);

                        else
                        {
                            if (item.Date.Value.Day == DateTime.Now.Day)
                                new AomsNiaFlupAccess().Update(item);
                        }


                    }
                    Thread.Sleep(6000);
                }

            }
            catch (Exception ex)
            {

                Log.WriteSeriviceLog(ex.ToString(), "GetFlightDiService");
            }

        }
    }
}
