using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Model.ViewModel;

namespace DATAACCESS
{
    public class CheckHawbBqlAccess : DATABASE.OracleProvider
    {
        public bool CheckBQLHawb(int lagi_iden)
        {
            string sql = "select count(*) as CheckBQL from agen " +
            "where agen.agen_ident_no =" + lagi_iden + " and agen.agen_remarks like '%BQL%'";
            bool check = false;
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                if (reader.Read())
                {
                    int expected = Convert.ToInt32(GetValueField(reader, "CheckBQL", 0));
                    if (expected > 0)
                    {
                        check = true;
                    }
                }

            }
            return check;
        }
    }
  
}
