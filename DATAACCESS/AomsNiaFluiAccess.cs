using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATABASE;
using TASK.Model.DBModel;
using Oracle.ManagedDataAccess.Client;

namespace DATAACCESS
{
    public class AomsNiaFluiAccess : DBProvider
    {
        public void Add(FlightImport flight)
        {
            ExecuteNonQuery("AOMS_FlightService.NIA_FLUI_ADD",
                  flight.FlightArrID,
       flight.AreaCode,
       flight.Date,
       flight.ArrFltNo,
       flight.STA,
       flight.STA_Change,
       flight.STA_Date,
       flight.ETA,
       flight.ATA,
       flight.AOnBT,
       flight.Aircraft,
       flight.RegisterNo,
       flight.MTOW,
       flight.Routing,
       flight.Terminal,
       flight.ParkNo,
       flight.Conveyor_Belt,
       flight.Opt,
       flight.GHID,
       flight.GHCode,
       flight.NatureID,
       flight.Nature,
       flight.CarryID,
       flight.Carry,
       flight.ConnectAirport,
       flight.ActualPax,
       flight.ArrFlightType,
       flight.Status,
       flight.Remark,
      flight.Lobby

               );
        }
        public void Update(FlightImport flight)
        {
            ExecuteNonQuery("AOMS_FlightService.NIA_FLUI_UPDATE",
                    flight.FlightArrID,
       flight.AreaCode,
       flight.Date,
       flight.ArrFltNo,
       flight.STA,
       flight.STA_Change,
       flight.STA_Date,
       flight.ETA,
       flight.ATA,
       flight.AOnBT,
       flight.Aircraft,
       flight.RegisterNo,
       flight.MTOW,
       flight.Routing,
       flight.Terminal,
       flight.ParkNo,
       flight.Conveyor_Belt,
       flight.Opt,
       flight.GHID,
       flight.GHCode,
       flight.NatureID,
       flight.Nature,
       flight.CarryID,
       flight.Carry,
       flight.ConnectAirport,
       flight.ActualPax,
       flight.ArrFlightType,
       flight.Status,
       flight.Remark,
      flight.Lobby
               );
        }

        public int GetById(Int64 id)
        {
            int count = 0;
            using (OracleDataReader reader = GetScriptOracleDataReader("select * from AOMSNIAFLUI where flightarrid=" + id))
            {

                if (reader.Read())
                {
                    count = 1;

                }


            }
            return count;
        }
    }
}
