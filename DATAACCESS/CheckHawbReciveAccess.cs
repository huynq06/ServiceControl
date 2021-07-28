using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Model.ViewModel;

namespace DATAACCESS
{
    public class CheckHawbReciveAccess : DATABASE.OracleProvider
    {
        private GroupLocation GetProperties(OracleDataReader reader)
        {
            GroupLocation group = new GroupLocation();
            group.Group = Convert.ToString(GetValueField(reader, "OBJGROUP", string.Empty));
            group.Location = Convert.ToString(GetValueField(reader, "LOCATION", string.Empty));
            return group;
        }
        public bool CheckReciveHAWB(string hawb, string dateIn)
        {
            string sql = "select l.lagi_quantity_expected,l.lagi_quantity_received " +
            "from lagi l " +
            "where l.lagi_hawb='" + hawb + "' and l.lagi_flight_date_in = '" + dateIn + "'";
            bool check = false;

            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                if (reader.Read())
                {
                    int expected = Convert.ToInt32(GetValueField(reader, "LAGI_QUANTITY_EXPECTED", 0));
                    int recived = Convert.ToInt32(GetValueField(reader, "LAGI_QUANTITY_RECEIVED", 0));
                    if (expected == recived)
                    {
                        check = true;
                    }
                }

            }

            return check;
        }
        public bool CheckMoveHAwb(string hawb, string dateIn)
        {
            bool check = false;
            string sql = "SELECT distinct " +
                         "lagi.LAGI_HAWB as HAWB, " +
                         "grai.grai_object_group_isn as objGROUP, " +
                         "locs.locs_physical_isn, " +
                         "sslp.sslp_rack_row as location from " +
                         "LAGI lagi " +
                         "join han_w1_hl.grai_group_additional_info grai " +
                         "on lagi.lagi_ident_no = grai.grai_object_isn " +
                         "and grai.grai_group_type = 'PIECES' " +
                         "AND grai.grai_group_code = 'RECEIVED' " +
                         "AND lagi.lagi_hawb='" + hawb + "' " +
                         "and lagi.lagi_flight_date_in = '" + dateIn + "' " +
                         "join han_w1_hl.locs_locations locs " +
                         "on lagi.lagi_ident_no = locs.locs_object_isn " +
                         "and grai.grai_object_group_isn = locs.locs_group_isn " +
                         "join han_w1_hl.sslp_physical_locations sslp " +
                         "on locs.locs_physical_isn = sslp.sslp_physical_isn " +
                         "ORDER BY  HAWB ASC";
            List<GroupLocation> groupsByAwb = new List<GroupLocation>();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                while (reader.Read())
                {
                    GroupLocation group = GetProperties(reader);

                    groupsByAwb.Add(group);
                }
                if (groupsByAwb.Count > 0)
                {
                    if (groupsByAwb.All(c => c.Location.Contains("ICR") || c.Location.Contains("CLC") || c.Location.Contains("I99") || c.Location.Contains("IVA")))
                    {
                        check = true;
                    }
                }

            }
            return check;
        }
    }
}
