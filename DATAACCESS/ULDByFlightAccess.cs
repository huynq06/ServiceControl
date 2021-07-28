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
    public class ULDByFlightAccess : DATABASE.OracleProvider
    {
        private ULDByFlightModel GetProperties(OracleDataReader reader)
        {
            ULDByFlightModel uld = new ULDByFlightModel();

            uld.ULD_Name = Convert.ToString(GetValueField(reader, "ULD", string.Empty));
            uld.Total_AWB = Convert.ToInt32(GetValueField(reader, "AWB_COUNT", 0));
            uld.SHC = Convert.ToString(GetValueField(reader, "SHC", string.Empty));
            uld.Check = Convert.ToInt32(GetValueField(reader, "CheckCol", 0));

            return uld;
        }
        public List<ULDByFlightModel> GetULDByFlight(Flight flight )
        {
            string sql = "select t.ULD as ULD, "+
                         "sum(t.AWB_COUNT) as AWB_COUNT, "+
                         "listagg(t.SHC, ',') within group(order by t.SHC) as SHC, " +
                           "CASE " +
            "WHEN listagg(t.SHC, ',') within group(order by t.SHC) LIKE '%COL%' THEN " +
            "1 " +
            "ELSE 0 " +
            "END AS CheckCol " +
                          "from " +
                "(SELECT DISTINCT " +
            "palo.palo_type || palo.palo_serial_no_ || palo.palo_owner as ULD, " +
            "count(awbu.awbu_mawb_serial_no) as AWB_COUNT, " +
            "awbu.awbu_specialhandlingcodes SHC " +
         
            "FROM flui flui " +
            "JOIN PALO palo " +
            "on palo.palo_lvg_in = flui.flui_al_2_3_letter_code " +
            "and palo.palo_flight_no_in = flui.flui_flight_no " +
            "and to_date('02-01-0001' , 'DD-MM-YYYY') +flui.flui_schedule_date = to_date('02-01-0001', 'DD-MM-YYYY') + palo.palo_flight_arrival_date " +
            "JOIN AWBU_AWBPERULD_LIST awbu " +
            "on awbu.awbu_uld_isn = palo.palo_uld_isn " +
            "and awbu.awbu_uld_serial = palo.palo_serial_no_ " +
            "and awbu.awbu_uld_no = palo.palo_type " +
            "and awbu.awbu_uld_owner = palo.palo_owner " +
            "and awbu.awbu_object_type = 'IMPORT AWB' " +
            "Where to_date('02-01-0001', 'DD-MM-YYYY') + flui.flui_schedule_date between trunc(sysdate) - 1 and trunc(sysdate) +1 " +
            "AND flui.flui_al_2_3_letter_code || flui.flui_flight_no = '" + flight.FlightNumber + "' " +
             " AND flui.flui_schedule_date =" + flight.FLUI_SCHEDULE_DATE +
             " AND flui.flui_schedule_time = " + flight.FLUI_SCHEDULE_TIME +
            "GROUP BY palo.palo_type || palo.palo_serial_no_ || palo.palo_owner, " +
            "awbu.awbu_specialhandlingcodes) t  GROUP BY t.ULD";
            List<ULDByFlightModel> ulds = new List<ULDByFlightModel>();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                while (reader.Read())
                {
                    ULDByFlightModel uld = GetProperties(reader);
                    if (uld.ULD_Name.Trim() == "BULK")
                    {
                        if (uld.Total_AWB > 0)
                        {
                            ulds.Add(uld);
                        }
                    }
                    else
                    {
                        ulds.Add(uld);
                    }
                }
            }
            return ulds;
        }
    }
}
