using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Model.ViewModel;
using TASK.DATA;

namespace DATAACCESS
{
    public class CheckAwbReceiveAccess : DATABASE.OracleProvider
    {
        private GroupLocation GetProperties(OracleDataReader reader)
        {
            GroupLocation group = new GroupLocation();
            group.Group = Convert.ToString(GetValueField(reader, "OBJGROUP", string.Empty));
            group.Location = Convert.ToString(GetValueField(reader, "LOCATION", string.Empty));
            return group;
        }
        private AwbReceivedViewModel GetPropertiesAwbReceived(OracleDataReader reader)
        {
            AwbReceivedViewModel item = new AwbReceivedViewModel();
            item.Lagi_Ident = Convert.ToString(GetValueField(reader, "LABSIDENT", string.Empty));
            item.Received = Convert.ToInt32(GetValueField(reader, "PIECES", string.Empty));
            return item;
        }

        public List<AwbReceivedViewModel> CheckReceive(List<VCT> vcts)
        {
            string listAwb = "";
            for (int i = 0; i < vcts.Count; i++)
            {
                if (i == 0)
                {
                    listAwb += "'" + vcts[0].LABS_IDENT_NO + "'";
                }
                else
                {
                    listAwb += "," + "'" + vcts[i].LABS_IDENT_NO + "'";
                }
            }
            string sql = "select labs.labs_ident_no as LABSIDENT,  labs.labs_quantity_del  as PIECES from labs where labs.labs_ident_no in (" + listAwb + ")";
            List<AwbReceivedViewModel> listAwbReceived = new List<AwbReceivedViewModel>();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                while (reader.Read())
                {
                    listAwbReceived.Add(GetPropertiesAwbReceived(reader));
                }
            }
            return listAwbReceived;
        }
        public string GetLastGroup(string lab_ident)
        {
            string group = "";
            string sql = "select agen.agen_remarks REMARKS from agen where "+
"agen.agen_status_external = 'GROUP CREATED' and rownum = 1 " +
"and agen.agen_ident_no = '" + lab_ident + "' order by agen.agen_creation_datetime desc ";
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                if (reader.Read())
                {
                    group = Convert.ToString(GetValueField(reader, "REMARKS", 0));
                }
            }
            if(group.Length>14)
            {
                return group.Substring(6, 14);
            }
            
            else
	        {
                return "";
            }
        }
        public bool CheckReciveAWB(string awb, string dateIn)
        {
            string sql = "select l.lagi_quantity_expected,l.lagi_quantity_received " +
            "from lagi l " +
            "where (l.lagi_mawb_prefix || l.lagi_mawb_no)='" + awb + "'";
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
        public bool CheckMoveAwb(string awb, string dateIn)
        {
            bool check = false;
            string sql = "SELECT distinct " +
                         "lagi.lagi_mawb_prefix || lagi.lagi_mawb_no as AWB, " +
                         "lagi.LAGI_HAWB as HAWB, " +
                         "grai.grai_object_group_isn as objGROUP, " +
                         "locs.locs_physical_isn, " +
                         "sslp.sslp_rack_row as location from " +
                         "LAGI lagi " +
                         "join han_w1_hl.grai_group_additional_info grai " +
                         "on lagi.lagi_ident_no = grai.grai_object_isn " +
                         "and grai.grai_group_type = 'PIECES' " +
                         "AND grai.grai_group_code = 'RECEIVED' " +
                         "AND (lagi.lagi_mawb_prefix || lagi.lagi_mawb_no)='" + awb + "' "+
                         //"and lagi.lagi_flight_date_in = '" + dateIn + "' "+
                         "join han_w1_hl.locs_locations locs " +
                         "on lagi.lagi_ident_no = locs.locs_object_isn " +
                         "and grai.grai_object_group_isn = locs.locs_group_isn " +
                         "join han_w1_hl.sslp_physical_locations sslp " +
                         "on locs.locs_physical_isn = sslp.sslp_physical_isn " +
                         "ORDER BY  AWB ASC";
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
                    if (groupsByAwb.All(c => c.Location.Contains("ICR") || c.Location.Contains("I99") || c.Location.Contains("IVA")))
                    {
                        check = true;
                    }
                }

            }
            return check;
        }

        public bool CheckProcessPXK(string awb,string hawb)
        {
            bool check = false;
            int count = 0;
            string sql = "select count(ccf.cusf_form_number) as CHECKPXK from lagi "+
"inner join cusf_customs_forms ccf on ccf.cusf_ident_no = lagi.lagi_ident_no " +
"where lagi.lagi_mawb_prefix || lagi.lagi_mawb_no = '" + awb+ "' and lagi.lagi_hawb = '" + hawb+"'";
            List<GroupLocation> groupsByAwb = new List<GroupLocation>();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                if (reader.Read())
                {
                     count = Convert.ToInt32(GetValueField(reader, "CHECKPXK", 0));
                    if(count > 0)
                    {
                        check = true;
                    }
                }

            }
            return check;
        }
        public bool CheckProcessPXKNoHawb(string awb)
        {
            bool check = false;
            int count = 0;
            string sql = "select count(ccf.cusf_form_number) as CHECKPXK from lagi " +
"inner join cusf_customs_forms ccf on ccf.cusf_ident_no = lagi.lagi_ident_no " +
"where lagi.lagi_mawb_prefix || lagi.lagi_mawb_no = '" + awb + "'";
            List<GroupLocation> groupsByAwb = new List<GroupLocation>();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                if (reader.Read())
                {
                    count = Convert.ToInt32(GetValueField(reader, "CHECKPXK", 0));
                    if (count > 0)
                    {
                        check = true;
                    }
                }

            }
            return check;
        }
        public bool CheckProcessPXKByLagiIdent(string lagiIdent)
        {
            bool check = false;
            int count = 0;
            string sql = "select count(ccf.cusf_form_number) as CHECKPXK from lagi " +
"inner join cusf_customs_forms ccf on ccf.cusf_ident_no = lagi.lagi_ident_no " +
"where lagi.lagi_ident_no = '" + lagiIdent + "'";
            List<GroupLocation> groupsByAwb = new List<GroupLocation>();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                if (reader.Read())
                {
                    count = Convert.ToInt32(GetValueField(reader, "CHECKPXK", 0));
                    if (count > 0)
                    {
                        check = true;
                    }
                }

            }
            return check;
        }
        public List<string> GetAllHawbLagiIdent(string awb)
        {
            List<string> listIdents = new List<string>();
            string lagiIdent = "";
            string sql = "select lagi.lagi_ident_no as  IDENT from lagi " +
"where lagi.lagi_mawb_prefix || lagi.lagi_mawb_no = '" + awb + "'"+
"and lagi.lagi_master_ident_no <> 0";
            List<GroupLocation> groupsByAwb = new List<GroupLocation>();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                while (reader.Read())
                {
                    lagiIdent = Convert.ToString(GetValueField(reader, "IDENT", ""));
                    listIdents.Add(lagiIdent);
                }

            }
            return listIdents;
        }

    }
}
