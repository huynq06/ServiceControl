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
using System.IO;

namespace TASK.Services
{
    public class CreatFlupService
    {
        public static List<FLightFlup> _listFlightToInsert;
        public static List<FLightFlup> _listFlightToUpdate;
        public static List<FLightFlup> _listFlightToDelete;
        public static void GetFlight()
        {
                _listFlightToInsert = new List<FLightFlup>();
            _listFlightToUpdate = new List<FLightFlup>();
            _listFlightToDelete = new List<FLightFlup>();

            ProcessData();
            try
            {
                using (TransactionScope scope = AppSetting.GetTransactionScope())
                {
                    if (_listFlightToInsert.Count > 0)
                    {
                        FLightFlup.Insert(_listFlightToInsert);
                    }
                    if (_listFlightToUpdate.Count > 0)
                    {
                        FLightFlup.Update(_listFlightToUpdate);
                    }
                    if (_listFlightToDelete.Count > 0)
                    {
                        FLightFlup.UpdateDeleted(_listFlightToDelete);
                    }
                    scope.Complete();
                }
            }

            catch (Exception ex)
            {
                string path = "C:\\data.txt";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex.ToString() + DateTime.Now);
                    writer.Close();
                }
                _listFlightToInsert = new List<FLightFlup>();
                _listFlightToUpdate = new List<FLightFlup>();
            }
        }
        private static void ProcessData()
        {
            try
            {
                List<FLUP> flights = new FlupAccess().GetFlightNumber();
                List<string> listFlightID = FLightFlup.GetAllToDay();
                List<FLightFlup> listDbIssues = FLightFlup.GetAllOpen();
                foreach (var flight in flights)
                {
                    
                    FlightConfig flightConfig = new FlightConfig();

                    flightConfig = FlightConfig.GetAll().FirstOrDefault(c => c.FlightNumber == flight.FLIGHT_NO.Substring(0, 2) && c.FlightType == flight.FLIGHT_TYPE.Substring(0, 1));


                    if (listFlightID.Count(c=>c==flight.FLIGHT_ID)==0)
                    {
                        FLightFlup itemFlight = new FLightFlup();
                        itemFlight.FlightID = flight.FLIGHT_ID;
                        itemFlight.ETD = flight.ETD;
                        itemFlight.STD = flight.STD;
                        itemFlight.FLightNumber = flight.FLIGHT_NO;
                        itemFlight.FlightAirCraffType = flight.FlightAirCraffType;
                        itemFlight.FlightType = flight.FLIGHT_TYPE;
                        itemFlight.Created = DateTime.Now;
                        itemFlight.BookingFlight = itemFlight.FLightNumber + "/" + flight.STD.Value.ToString("ddMMM").ToUpper();
                        itemFlight.TotalULD = flight.TotalULD;
                        itemFlight.UldLoaded = flight.LoadedULD;
                        itemFlight.FlightStatus = 0;
                        itemFlight.FlightDeleted = 0;
                        if (flightConfig == null)
                            itemFlight.FinalLoad = 150;
                        else
                        {
                            itemFlight.FinalLoad = flightConfig.FinalLoad;
                        }
                       
                        _listFlightToInsert.Add(itemFlight);
                    }
                    else
                    {
                        FLightFlup flightDb = FLightFlup.GetByID(flight.FLIGHT_ID);
                        try
                        {
                            flightDb.ETD = flight.ETD;
                            flightDb.STD = flight.STD;
                            //flightDb.TotalULD = flight.TotalULD;
                            flightDb.UldLoaded = flight.LoadedULD;
                            flightDb.FlightStatus = flight.FlightStatus;
                            _listFlightToUpdate.Add(flightDb);
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                      
                        if (flight != null)
                        {
                         
                        }
                    }
                }
                foreach (var item in listDbIssues)
                {
                   
                        if (flights.Count(c => c.FLIGHT_ID == item.FlightID) == 0)
                        {
                            item.FlightDeleted = 1;
                            _listFlightToDelete.Add(item);
                        }
                    
                    


                }
            }
            catch (Exception ex)
            {
                string path = "C:\\data.txt";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex.ToString() + DateTime.Now);
                    writer.Close();
                }
                _listFlightToInsert = new List<FLightFlup>();
                _listFlightToUpdate = new List<FLightFlup>();
                throw;
            }

        }
    }
}
