using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Model.DBModel;

namespace DATAACCESS
{
    public class CheckCloseFlightAccess : DATABASE.OracleProvider
    {
        public bool CheckCloseFlight(Flight flight)
        {
            string sql = "SELECT Count(t.FLIGHT_NO) as Checked " +
                         "FROM " +
                            "(SELECT DISTINCT " +
                                "flui.flui_al_2_3_letter_code || flui.flui_flight_no AS FLIGHT_NO " +
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
              "1 = 1 " +
              "AND palo.palo_uld_status = 'BREAKDOWN CLOSED' " +
             " AND(flui.flui_al_2_3_letter_code || flui.flui_flight_no) = '" + flight.FlightNumber + "'" +
              " AND flui.flui_schedule_date =" + flight.FLUI_SCHEDULE_DATE +
             " AND flui.flui_schedule_time = " + flight.FLUI_SCHEDULE_TIME +
              " AND " +
              " to_date('02-01-0001', 'DD-MM-YYYY') + flui.flui_schedule_date between trunc(sysdate) - 1 and trunc(sysdate) + 1) t";
             
            bool check = false;
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                if (reader.Read())
                {
                    var checkFlight = Convert.ToInt32(GetValueField(reader, "FLUI_SCHEDULE_DATE", 0));
                    if (checkFlight > 0)
                    {
                        check = true;
                    }
                   
                }
                 
            }
            return check;
        }
    }
}
