using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Model.DBModel;
namespace DATAACCESS
{
    public class FlupAccess : DATABASE.OracleProvider
    {
        private FLUP GetProperties(OracleDataReader reader)
        {
            FLUP flight = new FLUP();
            flight.FLIGHT_ID = Convert.ToString(GetValueField(reader, "FLUP_ID", string.Empty));
            flight.STD = Convert.ToDateTime(GetValueField(reader, "SCHEDULED_DATETIME", flight.STD));
            //flight.Status = Convert.ToString(GetValueField(reader, "STATUS", string.Empty));

            flight.ETD = Convert.ToDateTime(GetValueField(reader, "ESTIMATED_DATETIME", flight.STD));
            flight.FlightAirCraffType = Convert.ToString(GetValueField(reader, "FLUP_AIRCRAFF", string.Empty));
            flight.FLIGHT_TYPE = Convert.ToString(GetValueField(reader, "FLUP_TYPE", string.Empty));
            flight.FLIGHT_NO = Convert.ToString(GetValueField(reader, "FLightNo", string.Empty));
            flight.LoadedULD = Convert.ToInt32(GetValueField(reader, "LOADEDULD", 0));
            flight.TotalULD = Convert.ToInt32(GetValueField(reader, "TOTALULD", 0));
            flight.FlightStatus = Convert.ToInt32(GetValueField(reader, "FLIGHTSTATUS", 0));

            return flight;
        }
        public List<FLUP> GetFlightNumber()
        {
            string sql = "select "+
"to_date(to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_scheduled_date || ' ' || to_char(to_date(flup.flup_scheduled_time, 'HH24MISS'), 'HH24:MI:SS'), 'dd/mm/RR HH24:MI:SS') as SCHEDULED_DATETIME,"+
"to_date(to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_estimated_date || ' ' || to_char(to_date(flup.flup_estimated_time, 'HH24MISS'), 'HH24:MI:SS'), 'dd/mm/RR HH24:MI:SS') as ESTIMATED_DATETIME,"+
"flup.flup_int_number as FLUP_ID,"+
"flup.flup_type as FLUP_TYPE,"+
"flup.flup_flight_no_lvg || flup.flup_flight_no as FLightNo,"+
 "(select count(cont.cont_sequ_no_) from CONT cont "+
 "where cont.CONT_FLIGHT_NO_ = flup.flup_flight_no  and to_date('02-01-0001' , 'DD-MM-YYYY') +cont.CONT_DATE = to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_scheduled_date "+
 "and cont.cont_tara_uld > 0) LOADEDULD," +
 "(select count(cont.cont_sequ_no_) from CONT cont " +
 "where cont.CONT_FLIGHT_NO_ = flup.flup_flight_no  and to_date('02-01-0001' , 'DD-MM-YYYY') +cont.CONT_DATE = to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_scheduled_date " +
 "and cont.cont_tara_uld > 0) TOTALULD," +
 "flup.flup_close_flight FLIGHTSTATUS, "+
"flup.flup_type_of_airplane as FLUP_AIRCRAFF " +
"from flup "+
"where flup.flup_actual_date = 0 "+
"and to_date('02-01-0001' , 'DD-MM-YYYY') +flup.flup_scheduled_date >= to_date('"+DateTime.Now.ToString("dd/MM/yyyy")+"', 'dd/mm/yyyy') "+
"order by flup.flup_scheduled_date asc";
            List<FLUP> flights = new List<FLUP>();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                while (reader.Read())
                {
                    FLUP flight = GetProperties(reader);
                    
                        flights.Add(flight);
                    

                }
            }
            return flights;
        }
    }
}
