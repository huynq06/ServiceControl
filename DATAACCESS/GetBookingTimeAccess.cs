using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Model.DBModel;


namespace DATAACCESS
{
    public class GetBookingTimeAccess : DATABASE.OracleProvider
    {
        public DateTime? GetBookingTime(string lagi_ident,ref string user)
        {
            string sql = "select  a.agen_creation_datetime DT,mita.MITA_NAME EMPLOYEE from labs " +
"join agen a on a.agen_ident_no = labs.labs_ident_no " +
"inner join VN_SHARE_HL.MITA mita on mita.mita_personal_no = a.agen_employee " +
"where labs.labs_ident_no = '" + lagi_ident + "' and a.agen_status_external = 'AWB UPDATE'" +
"and a.agen_remarks like 'AWB Booked on flight%' and a.agen_remarks not like '%00/00/0000'  and rownum = 1";

            DateTime? dt = new DateTime();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                if (reader.Read())
                {
                    dt = Convert.ToDateTime(GetValueField(reader, "DT", 0));
                    user = Convert.ToString(GetValueField(reader, "EMPLOYEE", string.Empty));
                }

            }

            return dt;
        }
    }
}
