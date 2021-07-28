using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Model.DBModel;

namespace DATAACCESS
{
    public class LABSAccess : DATABASE.OracleProvider
    {
        private LABS GetProperties(OracleDataReader reader)
        {
            LABS lab = new LABS();
           
            lab.LABS_IDENT_NO = Convert.ToString(GetValueField(reader, "LABS_IDENT_NO", string.Empty));
            lab.LABS_QUANTITY_DEL = Convert.ToDouble(GetValueField(reader, "LABS_QUANTITY_DEL", 0));
            lab.AWB = Convert.ToString(GetValueField(reader, "AWB", string.Empty)); 
            lab.LABS_QUANTITY_BOOKED = Convert.ToDouble(GetValueField(reader, "LABS_QUANTITY_BOOKED", 0));
            lab.Scale_Status = Convert.ToString(GetValueField(reader, "SCALE_STATUS", string.Empty));
            lab.GetIn_Status = Convert.ToString(GetValueField(reader, "RCS_STATUS", string.Empty));

            return lab;
        }
        public bool CheckProcessingAWB(string awb)
        {
            string sql = "select l.labs_ident_no,(l.labs_mawb_prefix || l.labs_mawb_serial_no) as AWB,l.LABS_QUANTITY_DEL,l.LABS_QUANTITY_BOOKED " +
            "from labs l " +
            "where (l.labs_mawb_prefix || l.labs_mawb_serial_no)='" + awb + "'";
            bool check = false;
            LABS labs = new LABS();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                if (reader.Read())
                    labs = GetProperties(reader);
            }
            if (labs.LABS_QUANTITY_DEL > 0)
            {
                check = true;
            }
            return check;
        }
        public bool CheckCompleteAWB(string awb)
        {
            string sql = "SELECT DISTINCT labs.labs_ident_no,labs.labs_fwbm_serial_no,labs.labs_mawb_prefix,labs.labs_mawb_serial_no, " +
  "CASE " +
    "WHEN labs.labs_quantity_del = 0 THEN 'WAITING' " +
    "WHEN labs.labs_quantity_del > 0 and labs.labs_quantity_del < labs.labs_quantity_booked THEN 'PROCESSING' " +
    "WHEN labs.labs_quantity_del = labs.labs_quantity_booked THEN 'DONE' " +
  "END AS SCALE_STATUS, " +
  "CASE " +
    "WHEN(select count(a.agen_ident_no)  FROM agen a WHERE a.agen_ident_no = labs.labs_ident_no and a.agen_status_external = 'AWB CONFIRMED') = 0 THEN 'WAITING' " +
    " WHEN(select count(a.agen_ident_no)  FROM agen a WHERE a.agen_ident_no = labs.labs_ident_no and a.agen_status_external = 'AWB CONFIRMED') <> 0 THEN 'DONE' " +
  " END AS RCS_STATUS " +
" from labs " +
" where " +
" (labs.labs_mawb_prefix || labs.labs_mawb_serial_no) = " + "'" + awb + "'";
            bool check = false;
            LABS labs = new LABS();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                if (reader.Read())
                    labs = GetProperties(reader);
            }
            if (labs.Scale_Status == "DONE" && labs.GetIn_Status == "DONE")
            {
                check = true;
            }
            return check;
        }
        public bool CheckGroupAWB(string lagi_ident)
        {
            string sql = "select count(distinct grai.grai_object_group_isn ) as count_group from grai_group_additional_info grai " +
"where grai.grai_object_isn = '" + lagi_ident + "'";
            bool check = false;
            int count = 0;
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                if (reader.Read())
                {
                    count = Convert.ToInt32(GetValueField(reader, "COUNT_GROUP", 0));
                    if (count == 1)
                        check = true;
                }

            }

            return check;
        }
        public bool CheckGroupMoveAWB(string lagi_ident)
        {
            string sql = "select count(agen.agen_status_external) as group_move from agen " +
"where agen.agen_ident_no = '" + lagi_ident + "' and agen.agen_status_external = 'GROUP MOVED'";
            bool check = false;
            int count = 0;
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                if (reader.Read())
                {
                    count = Convert.ToInt32(GetValueField(reader, "GROUP_MOVE", 0));
                    if (count > 0)
                        check = true;
                }

            }

            return check;
        }
    }
}
