using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Model.DBModel;

namespace DATAACCESS
{
    public class FlightAccess : DATABASE.OracleProvider
    {
        private FlightModel GetProperties(OracleDataReader reader)
        {
            FlightModel flight = new FlightModel();

            flight.FlightNumber = Convert.ToString(GetValueField(reader, "FLIGHT_NO", string.Empty));
            flight.Schedule = Convert.ToDateTime(GetValueField(reader, "SCHEDULED_DATETIME", flight.Schedule));
            //flight.Status = Convert.ToString(GetValueField(reader, "STATUS", string.Empty));

            flight.FlightStatus = Convert.ToBoolean(GetValueField(reader, "FLIGHT_STATUS", false));
            flight.FFM = GetValueDateTimeField(reader, "FFM", flight.FFM);
            flight.LandedTime = GetValueDateTimeField(reader, "ATA_DATETIME", flight.LandedTime);
            flight.FLUI_SCHEDULE_DATE = Convert.ToInt32(GetValueField(reader, "FLUI_SCHEDULE_DATE", 0));
            flight.FLUI_SCHEDULE_TIME = Convert.ToInt32(GetValueField(reader, "FLUI_SCHEDULE_TIME", 0));
            flight.FLUI_LANDED_DATE = Convert.ToInt32(GetValueField(reader, "FLUI_LANDED_DATE", 0));
            flight.FLUI_LANDED_TIME = Convert.ToInt32(GetValueField(reader, "FLUI_LANDED_TIME", 0));
            flight.FlightLetterCode = Convert.ToString(GetValueField(reader, "FLIGHTCODE", string.Empty));
            flight.FlightType = Convert.ToString(GetValueField(reader, "FLUI_TYPE", string.Empty));
            flight.FlightAirCraftType = Convert.ToString(GetValueField(reader, "FLUI_TYPE_OF_AIRCRAFT", string.Empty));


            return flight;
        }
        public List<FlightModel> GetFlightNumber()
        {
            string sql = "SELECT " +
      "t.FLIGHT_NO, " +
      "t.SCHEDULED_DATETIME, " +
      "case when t.ATA_DATE is null then null " +
      "when t.ATA_DATE is not null then to_char(to_date(t.ATA_DATE || ' ' || t.ATA_TIME, 'dd/mm/RR HH24:MI:SS'), 'mm/dd/RR HH24:MI:SS') " +
      "end as ATA_DATETIME, " +
      //"t.status, " +
      "t.flight_status, " +
      "t.FFM, " +
      "t.flui_schedule_date, t.flui_schedule_time,t.flui_landed_date,t.flui_landed_time, " +
      "t.flui_type, " +
      "t.FlightCode, " +
      "t.flui_type_of_aircraft " +
  "FROM( " +
      "SELECT DISTINCT " +
        "flui.flui_al_2_3_letter_code || flui.flui_flight_no AS FLIGHT_NO, " +
         "to_date(to_date('02-01-0001', 'DD-MM-YYYY') + flui.flui_schedule_date || ' ' || to_char(to_date(flui.flui_schedule_time, 'HH24MISS'), 'HH24:MI:SS'), 'dd/mm/RR HH24:MI:SS') as SCHEDULED_DATETIME, " +
          "to_char(to_date('02-01-0001', 'DD-MM-YYYY') + flui.flui_landed_date, 'DD-MM-YYYY') AS ATA_DATE, " +
         "to_char(to_date(flui.flui_landed_time, 'HH24MISS'), 'HH24:MI:SS') AS ATA_TIME, " +
         //"palo.palo_uld_status as status, " +
         "flui.flui_ffm_date AS FFM, " +
          " flui.flui_flight_completed as flight_status, " +
           "flui.flui_schedule_date, " +
          "flui.flui_schedule_time, " +
          "flui.flui_landed_date, " +
          "flui.flui_landed_time, " +
          "flui.flui_type, " +
          "flui.flui_al_2_3_letter_code as FlightCode, " +
          "flui.flui_type_of_aircraft " +
      "FROM flui flui " +
       "JOIN PALO palo " +
           "on palo.palo_lvg_in = flui.flui_al_2_3_letter_code " +
           "and palo.palo_flight_no_in = flui.flui_flight_no " +
           "and to_date('02-01-0001', 'DD-MM-YYYY') + flui.flui_schedule_date = to_date('02-01-0001', 'DD-MM-YYYY') + palo.palo_flight_arrival_date " +
      "LEFT JOIN AWBU_AWBPERULD_LIST awbu " +
           "on awbu.awbu_uld_isn = palo.palo_uld_isn " +
           "and awbu.awbu_uld_serial = palo.palo_serial_no_ " +
           "and awbu.awbu_uld_no = palo.palo_type " +
           "and awbu.awbu_uld_owner = palo.palo_owner " +
           "and awbu.awbu_object_type = 'IMPORT AWB' " +
      "WHERE " +
         " to_date('02-01-0001' ,'DD-MM-YYYY') + flui.flui_schedule_date between trunc(sysdate)-1 and trunc(sysdate) +1 " +
   ") t order by 2,1";
            List<FlightModel> flights = new List<FlightModel>();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                while (reader.Read())
                {
                    FlightModel flight = GetProperties(reader);
                    if (flight.LandedTime.HasValue)
                    {
                        flights.Add(flight);
                    }
                   
                }
            }
            return flights;
        }
    }
}
