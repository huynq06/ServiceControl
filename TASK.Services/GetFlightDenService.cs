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
    public static class GetFlightDenService
    {
        public static void GetData()
        {
            string[] listDay = { DateTime.Now.ToString("MM/dd/yyyy"), DateTime.Now.AddDays(1).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(2).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(3).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(4).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(5).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(6).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(7).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(8).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(9).ToString("MM/dd/yyyy"), DateTime.Now.AddDays(10).ToString("MM/dd/yyyy") };
            try
            {
                foreach(var day in listDay)
                {
                    string url = @"C:\temFligtDenRequest.xml";
                    //string day = DateTime.Now.ToString("dd/MM/yyyy");
                    Task<string> task = Task.Run<String>(async () => await FlightRequest.Command(url, day));
                    string result = task.Result;
                    //XmlDocument xmldoc = new XmlDocument();
                    //xmldoc.LoadXml(result);
                    var dox = XDocument.Parse(result, LoadOptions.PreserveWhitespace);
                    List<FlightImport> flights = dox.Descendants("Flight").Select(d =>
           new FlightImport
           {
               FlightArrID = int.Parse(d.Element("FlightArrID").Value),
               AreaCode = d.Element("AreaCode").Value.Trim(),
               Date = string.IsNullOrEmpty(d.Element("Date").Value) ? (DateTime?)null : DateTime.Parse(d.Element("Date").Value),
               ArrFltNo = d.Element("ArrFltNo").Value.Trim(),
               STA = d.Element("STA") != null ? "" : d.Element("STA").Value.Trim(),
               STA_Change = d.Element("STA_Change").Value.Trim(),
               STA_Date = string.IsNullOrEmpty(d.Element("STA_Date").Value) ? (DateTime?)null : DateTime.Parse(d.Element("STA_Date").Value),
               ETA = d.Element("ETA") != null ? d.Element("ETA").Value.Trim() : "",
               ATA = d.Element("ATA") != null ? d.Element("ATA").Value.Trim() : "",
               AOnBT = d.Element("AOnBT") != null ? d.Element("AOnBT").Value.Trim() : "",
               Aircraft = d.Element("Aircraft").Value.Trim(),
               RegisterNo = d.Element("RegisterNo") != null ? d.Element("RegisterNo").Value.Trim() : "",
               MTOW = string.IsNullOrEmpty(d.Element("MTOW").Value) ? 0 : int.Parse(d.Element("MTOW").Value),
               Routing = d.Element("Routing").Value.Trim(),
               Terminal = d.Element("Terminal").Value.Trim(),
               ParkNo = d.Element("ParkNo") != null ? d.Element("ParkNo").Value.Trim() : "",
               Conveyor_Belt = d.Element("Conveyor_Belt").Value.Trim(),
               Opt = d.Element("Opt").Value.Trim(),
               GHID = string.IsNullOrEmpty(d.Element("GHID").Value) ? 0 : int.Parse(d.Element("GHID").Value),
               GHCode = d.Element("GHCode").Value.Trim(),
               NatureID = string.IsNullOrEmpty(d.Element("NatureID").Value) ? 0 : int.Parse(d.Element("NatureID").Value),
               Nature = d.Element("Nature").Value,
               CarryID = string.IsNullOrEmpty(d.Element("CarryID").Value) ? 0 : int.Parse(d.Element("CarryID").Value),
               Carry = d.Element("Carry").Value.Trim(),
               ConnectAirport = d.Element("ConnectAirport") != null ? d.Element("ConnectAirport").Value : "",
               ArrFlightType = d.Element("ArrFlightType").Value,
               ActualPax = d.Element("ActualPax") != null ? d.Element("ActualPax").Value.Trim() : "",
               ActualBag = d.Element("ActualBag") != null ? d.Element("ActualBag").Value.Trim() : "",
               ActualCargo = d.Element("ActualCargo") != null ? d.Element("ActualCargo").Value.Trim() : "",
               Status = d.Element("Status").Value.Trim(),
               Remark = d.Element("Remark").Value.Trim(),
               Lobby = d.Element("Lobby").Value.Trim()
           }).ToList();

                    foreach (var item in flights)
                    {
                        int count = new AomsNiaFluiAccess().GetById(item.FlightArrID);
                        if (count == 0)
                            new AomsNiaFluiAccess().Add(item);
                        else
                        {
                            if (item.Date.Value.Day == DateTime.Now.Day)
                                new AomsNiaFluiAccess().Update(item);
                        }
                            
                    }
                    Thread.Sleep(1000 * 60);
                }
             
            }
            catch (Exception ex)
            {

                Log.WriteSeriviceLog(ex.ToString(), "GetFlightDenSevice");
            }
        }
    }
}
