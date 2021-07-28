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
    public class FlightControlService
    {
        public static List<Flight> _listFlightToInsert;
        public static List<Flight> _listFlightToUpdate;
        public static List<ULDByFlight> _listULDByFlightToInsert;
        public static List<AWBByULD> _listAwbByULDToInsert;
        public static List<HawbInAwb> _listHawbInAwbToInsert;
        public static void GetFlight()
        {
             _listFlightToInsert = new List<Flight>();
            _listULDByFlightToInsert = new List<ULDByFlight>();
            _listFlightToUpdate = new List<Flight>();
            _listAwbByULDToInsert = new List<AWBByULD>();
            _listHawbInAwbToInsert = new List<HawbInAwb>();
            ProcessData();
            try
            {
                using (TransactionScope scope = AppSetting.GetTransactionScope())
                {
                    if (_listFlightToInsert.Count > 0)
                    {
                        Flight.Insert(_listFlightToInsert);
                    }
                    if (_listULDByFlightToInsert.Count > 0)
                    {
                        ULDByFlight.Insert(_listULDByFlightToInsert);
                    }
                    if (_listAwbByULDToInsert.Count > 0)
                    {
                        AWBByULD.Insert(_listAwbByULDToInsert);
                    }
                    if (_listFlightToUpdate.Count > 0)
                    {
                        Flight.Update(_listFlightToUpdate);
                    }
                    if (_listHawbInAwbToInsert.Count > 0)
                    {
                        HawbInAwb.Insert(_listHawbInAwbToInsert);
                    }
                    scope.Complete();
                }
            }

            catch (Exception ex)
            {
                Log.WriteLog(ex.ToString());
            }
        }
        public static void ProcessData()
        {
            List<FlightModel> flights = new FlightAccess().GetFlightNumber();
            if (flights.Count > 0)
            {
                List<Flight> listDbIssues = Flight.GetAll();
                foreach (var flight in flights)
                {
                   
                        if (listDbIssues.Any(c => (c.FlightNumber == flight.FlightNumber && c.FLUI_SCHEDULE_DATE == flight.FLUI_SCHEDULE_DATE && c.FLUI_SCHEDULE_TIME == flight.FLUI_SCHEDULE_TIME)))
                        {
                            var flightDB = Flight.GetFlightByCondition(flight.FlightNumber, flight.FLUI_SCHEDULE_DATE.Value, flight.FLUI_SCHEDULE_TIME.Value);
                            List<AWBByULD> awbsTest = new AWBByULDAccess().GetAWBByULD(flightDB);
                            //kiem tra xem dc dong chuyen chua
                            if (flightDB.Status == false)
                            {
                                var listUld = ULDByFlight.GetByFlight(flightDB.FlightID);
                                if (listUld.Count > 0 && listUld.All(c => c.Status == 2))
                                {
                                    flightDB.Status = true;
                                    flightDB.FinishTime = DateTime.Now;
                                    Flight.CloseFlight(flightDB);
                                }
                            }
                            //kiểm tra landed time co thay doi hay ko
                            if (flightDB.LandedDate != flight.LandedTime)
                            {
                                flightDB.LandedDate = flight.LandedTime;
                                Flight.UpdateLandedTime(flightDB);
                            }

                            //Get ULD by Flight
                        }
                        else
                        {
                            var flightConfig = new FlightConfig();
                            if ((flight.FlightLetterCode == "CX" || flight.FlightLetterCode == "KA") && flight.FlightType == "P")
                            {
                                flightConfig = FlightConfig.GetAll().SingleOrDefault(c => c.FlightNumber == flight.FlightLetterCode && c.FlightType == flight.FlightType && c.FlightTypeOfAirCraft.Contains(flight.FlightAirCraftType));
                            }
                            else
                            {
                                flightConfig = FlightConfig.GetAll().SingleOrDefault(c => c.FlightNumber == flight.FlightLetterCode && c.FlightType == flight.FlightType);
                            }
                            if (flightConfig != null)
                            {
                                var newFlight = new Flight();
                                newFlight.FlightID = Guid.NewGuid();
                                newFlight.FlightNumber = flight.FlightNumber;
                                newFlight.Schedule = flight.Schedule;
                                newFlight.Status = false;
                                newFlight.LandedDate = flight.LandedTime;
                                newFlight.FLUI_SCHEDULE_DATE = flight.FLUI_SCHEDULE_DATE;
                                newFlight.FLUI_SCHEDULE_TIME = flight.FLUI_SCHEDULE_TIME;
                                newFlight.FLUI_LANDED_DATE = flight.FLUI_LANDED_DATE;
                                newFlight.FLUI_LANDED_TIME = flight.FLUI_LANDED_TIME;
                                newFlight.FlightLetter = flight.FlightLetterCode;
                                newFlight.FlightType = flight.FlightType;
                                newFlight.FlightTypeOfAirCraft = flight.FlightAirCraftType;
                                newFlight.SOPTIME = flightConfig.SopTime;
                                newFlight.AlertTime1 = flightConfig.AlertTime1;
                                newFlight.AlertTime2 = flightConfig.AlertTime2;
                                newFlight.AlertTime3 = flightConfig.AlertTime3;
                                newFlight.SHCTIME = flightConfig.SHCTIME;
                                newFlight.AlertSHC1 = flightConfig.AlertSHC1;
                                newFlight.AlertSHC2 = flightConfig.AlertSHC2;
                                newFlight.Created = DateTime.Now;
                                _listFlightToInsert.Add(newFlight);
                                List<ULDByFlightModel> ulds = new ULDByFlightAccess().GetULDByFlight(newFlight);
                                foreach (var uld in ulds)
                                {
                                    var newUld = new ULDByFlight();
                                    newUld.Name = uld.ULD_Name;
                                    newUld.ULDID = Guid.NewGuid();
                                    newUld.Status = 0;
                                    newUld.StatusMessage = "Waiting";
                                    newUld.FlightNumber = flight.FlightNumber;
                                    newUld.Flight_ID = newFlight.FlightID;
                                    newUld.Priority = 1;
                                    newUld.SHC = uld.SHC;
                                    newUld.CheckValue = uld.Check;
                                    _listULDByFlightToInsert.Add(newUld);

                                }
                                List<AWBByULD> awbs = new AWBByULDAccess().GetAWBByULD(newFlight);
                                foreach (var awb in awbs)
                                {
                                    var newAwb = new AWBByULD();
                                    newAwb.Flight_ID = newFlight.FlightID;
                                    newAwb.Lagi_Identity = awb.Lagi_Identity;
                                    newAwb.AWB = awb.AWB;
                                    newAwb.SHC = awb.SHC;
                                    newAwb.CheckValue = awb.CheckValue;
                                    newAwb.Process = 0;
                                    newAwb.AWB_ID = Guid.NewGuid();
                                    if (newAwb.CheckValue == 1)
                                    {
                                        List<HawbInAwb> hawbs = new HawbInAwbAccess().GetHawbInAwb(newAwb, newFlight);

                                        if (hawbs.Count > 1)
                                        {
                                            newAwb.HaveMultiHawb = true;
                                            foreach (var hawb in hawbs)
                                            {
                                                var newHawb = new HawbInAwb();
                                                newHawb.AWB_ID = newAwb.AWB_ID;
                                                newHawb.HAWB = hawb.HAWB;
                                                newHawb.Lagi_Identity = hawb.Lagi_Identity;
                                                newHawb.Process = 0;
                                                newHawb.FlightID = newFlight.FlightID;
                                                newHawb.CheckValue = 0;
                                                newHawb.Bql = true;
                                                _listHawbInAwbToInsert.Add(newHawb);
                                            }
                                        }
                                        else
                                        {
                                            if (hawbs.Count > 0)
                                            {
                                                foreach (var hawb in hawbs)
                                                {
                                                    var newHawb = new HawbInAwb();
                                                    newHawb.AWB_ID = newAwb.AWB_ID;
                                                    newHawb.HAWB = hawb.HAWB;
                                                    newHawb.Lagi_Identity = hawb.Lagi_Identity;
                                                    newHawb.Process = 0;
                                                    newHawb.FlightID = newFlight.FlightID;
                                                    newHawb.CheckValue = 0;
                                                    newHawb.Bql = false;
                                                    _listHawbInAwbToInsert.Add(newHawb);
                                                }
                                            }
                                            newAwb.HaveMultiHawb = false;

                                        }
                                    }
                                    else
                                    {
                                        List<HawbInAwb> hawbs = new HawbInAwbAccess().GetHawbInAwb(newAwb, newFlight);
                                        if (hawbs.Count > 0)
                                        {
                                            foreach (var hawb in hawbs)
                                            {
                                                var newHawb = new HawbInAwb();
                                                newHawb.AWB_ID = newAwb.AWB_ID;
                                                newHawb.HAWB = hawb.HAWB;
                                                newHawb.Lagi_Identity = hawb.Lagi_Identity;
                                                newHawb.Process = 0;
                                                newHawb.FlightID = newFlight.FlightID;
                                                newHawb.CheckValue = 0;
                                                newHawb.Bql = false;
                                                _listHawbInAwbToInsert.Add(newHawb);
                                            }
                                        }
                                    }
                                    newAwb.TimeFinish = newFlight.LandedDate.Value.AddMinutes(newFlight.SHCTIME.Value);
                                    _listAwbByULDToInsert.Add(newAwb);
                                }
                            }
                        }
                    
                   
                      
                    }
                }
            }
        
    }
}
