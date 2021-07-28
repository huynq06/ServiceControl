using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATABASE;
using Oracle.ManagedDataAccess.Client;
using TASK.Model.DBModel;

namespace DATAACCESS
{
    public class VCTAccess : OracleProvider
    {
        private VCTHermes GetProperties(OracleDataReader reader)
        {
            VCTHermes objVCT = new VCTHermes();
            objVCT.LABS_IDENT_NO = Convert.ToString(GetValueField(reader, "LABS_IDENT_NO", string.Empty));
            objVCT.LABS_AWB = Convert.ToString(GetValueField(reader, "LABS_AWB", string.Empty));
            objVCT.LABS_QUANTITY_BOOKED = Convert.ToInt32(GetValueField(reader, "LABS_QUANTITY_BOOKED", 0));
            objVCT.LABS_WEIGHT_BOOKED = Convert.ToDouble(GetValueField(reader, "LABS_WEIGHT_BOOKED", 0));
            objVCT.AGENT_NAME = Convert.ToString(GetValueField(reader, "AGENT_NAME", string.Empty));
            objVCT.BOOKING_FLIGHT = Convert.ToString(GetValueField(reader, "VCT_REMARK", string.Empty));
            objVCT.FLUP_TYPE = Convert.ToString(GetValueField(reader, "FLUP_TYPE", string.Empty));
            objVCT.LABS_CREATED_AT = Convert.ToDateTime(GetValueField(reader, "LABS_CREATED_AT", DateTime.Now));
            objVCT.CutoffTime = GetValueDateTimeField(reader, "CUTOFF_TIME", objVCT.CutoffTime);
            objVCT.VCT_DATE = DateTime.Now.Date;
            objVCT.FLUP_ID = Convert.ToString(GetValueField(reader, "FLUP_ID", string.Empty));
            return objVCT;
        }
        public List<VCTHermes> GetData()
        {
            string sql = "SELECT DISTINCT " + 
  "labs.labs_ident_no as LABS_IDENT_NO," +
  "(labs.labs_mawb_prefix || '-' || labs.labs_mawb_serial_no) as LABS_AWB," +
  "labs.labs_quantity_booked as LABS_QUANTITY_BOOKED," +
  "labs.labs_weight_booked as LABS_WEIGHT_BOOKED," +
  "labs.labs_agent_name as AGENT_NAME," +
  "vhld.vhld_reception_remarks as VCT_REMARK, " +
  "substr(vhld.vhld_reception_remarks, 0, instr(vhld.vhld_reception_remarks, '/') - 1) as BOOKING_FLIGHT," +
  "substr(vhld.vhld_reception_remarks, instr(vhld.vhld_reception_remarks, '/') + 1, instr(vhld.vhld_reception_remarks, '/', -1)) as BOOKING_DATE," +
    "(select flup.flup_type " +
   "from flup " +
   "where to_char(to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_scheduled_date, 'DDMON') = substr(vhld.vhld_reception_remarks, instr(vhld.vhld_reception_remarks, '/') + 1, instr(vhld.vhld_reception_remarks, '/', -1)) " +
      "and to_char(to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_scheduled_date, 'YYYY') = to_char(sysdate, 'YYYY') " +
      "and flup.flup_flight_no_lvg || flup.flup_flight_no = substr(vhld.vhld_reception_remarks, 0, instr(vhld.vhld_reception_remarks, '/') - 1) and rownum = 1 " +
   ") as FLUP_TYPE," +
    "(select flup.flup_int_number " +
   "from flup " +
   "where to_char(to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_scheduled_date, 'DDMON') = substr(vhld.vhld_reception_remarks, instr(vhld.vhld_reception_remarks, '/') + 1, instr(vhld.vhld_reception_remarks, '/', -1)) " +
      "and to_char(to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_scheduled_date, 'YYYY') = to_char(sysdate, 'YYYY') " +
      "and flup.flup_flight_no_lvg || flup.flup_flight_no = substr(vhld.vhld_reception_remarks, 0, instr(vhld.vhld_reception_remarks, '/') - 1) and rownum = 1 " +
   ") as FLUP_ID," +
  "(select to_date(to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_lat_date || ' ' || to_char(to_date(flup.flup_lat_time, 'HH24MISS'), 'HH24:MI:SS'), 'dd/mm/RR HH24:MI:SS') " +
   "from flup " +
   "where to_char(to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_scheduled_date, 'DDMON') = substr(vhld.vhld_reception_remarks, instr(vhld.vhld_reception_remarks, '/') + 1, instr(vhld.vhld_reception_remarks, '/', -1)) " +
      "and to_char(to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_scheduled_date, 'YYYY') = to_char(sysdate, 'YYYY') " +
      "and flup.flup_flight_no_lvg || flup.flup_flight_no = substr(vhld.vhld_reception_remarks, 0, instr(vhld.vhld_reception_remarks, '/') - 1) and rownum = 1 " +
   ") as CUTOFF_TIME," +
  "labs.labs_created_at as LABS_CREATED_AT " +
"from labs " +
"join agen on agen.agen_ident_no = labs.labs_ident_no " +
"join vhld_vehicledetail vhld on vhld.vhld_objectisn = labs.labs_ident_no " +
"join vehicles_registration vhcl on vhcl.vhcl_isn_no = vhld.vhld_vehicleisn " +
"where 1=1 " +
     "and vhcl.vhcl_arrival_date = trunc(sysdate) " +
     "and labs.labs_quantity_del = 0 " +
  "and agen.agen_status_external = 'EXPORT LANDSIDE RECEPTION ENTRY COMPLETED' " +
"order by labs.labs_created_at ASC";
            List<VCTHermes> ListVCTHermes = new List<VCTHermes>();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                while (reader.Read())
                {
                    ListVCTHermes.Add(GetProperties(reader));
                }
            }
            return ListVCTHermes;

        }
    }
}
