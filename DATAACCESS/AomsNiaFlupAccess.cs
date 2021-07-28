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
    public class AomsNiaFlupAccess : DBProvider
    {
        public void Add(FLightExort flight)
        {
            ExecuteNonQuery("AOMS_FlightService.NIA_FLUP_ADD",
                  flight.FlightDepID,
       flight.AreaCode,
       flight.Date,
       flight.DepFltNo,
       flight.STD,
       flight.STD_Change,
       flight.STD_Date,
       flight.ETD,
       flight.ATD,
       flight.AOBT,
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
       flight.ActualBag,
       flight.ActualCargo,
       flight.Status,
       flight.Remark,
       flight.CkiOpen,
       flight.CkiClose,
       flight.PreBDT,
       flight.BDT,
       flight.FHT,
       flight.PBT,
       flight.Config,
       flight.Gate,
       flight.CkiRow,
       flight.DepFlightType,
       flight.EstULD,
       flight.EstPax
               );
        }
        public void Update(FLightExort flight)
        {
            ExecuteNonQuery("AOMS_FlightService.NIA_FLUP_UPDATE",
                  flight.FlightDepID,
       flight.AreaCode,
       flight.Date,
       flight.DepFltNo,
       flight.STD,
       flight.STD_Change,
       flight.STD_Date,
       flight.ETD,
       flight.ATD,
       flight.AOBT,
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
       flight.ActualBag,
       flight.ActualCargo,
       flight.Status,
       flight.Remark,
       flight.CkiOpen,
       flight.CkiClose,
       flight.PreBDT,
       flight.BDT,
       flight.FHT,
       flight.PBT,
       flight.Config,
       flight.Gate,
       flight.CkiRow,
       flight.DepFlightType,
       flight.EstULD,
       flight.EstPax
               );
        }

        public int GetById(Int64 id)
        {
            int count = 0;
            using (OracleDataReader reader = GetScriptOracleDataReader("select * from AOMSNIAFLUP where flightdepid=" + id))
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
