using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Model.DBModel;
using TASK.Model.ViewModel;

namespace DATAACCESS
{
    public class PXKLagiAccess : DATABASE.OracleProvider
    {
        private PXKHermesViewModel GetProperties(OracleDataReader reader)
        {
            PXKHermesViewModel pxk = new PXKHermesViewModel();
            pxk.PXKNo = Convert.ToString(GetValueField(reader, "PXKNO", string.Empty));
            pxk.AWB = Convert.ToString(GetValueField(reader, "AWB", string.Empty));
            pxk.Hawb = Convert.ToString(GetValueField(reader, "HAWB", string.Empty));
            pxk.quantity = Convert.ToInt32(GetValueField(reader, "QUANTITY", 0));
            pxk.weight = Convert.ToString(GetValueField(reader, "WEIGHT", string.Empty));
            pxk.Location = Convert.ToString(GetValueField(reader, "LOCATION", string.Empty));
            return pxk;
        }
        public List<PXKHermesViewModel> GetPXKHermes(List<tblPXK> pxks)
        {
            List<PXKHermesViewModel> listPXKViewModel = new List<PXKHermesViewModel>();
            string listPXK = "";
            for (int i = 0; i < pxks.Count; i++)
            {
                if (i == 0)
                {
                    listPXK += "'" + pxks[0].PXK + "'";
                }
                else
                {
                    listPXK += "," + "'" + pxks[i].PXK + "'";
                }
            }
            string sql = "select distinct "
+"cusf.cusf_form_number as PXKNO, "
+"grai.grai_object_group_isn as objGROUP, "
+"(l.lagi_mawb_prefix || l.lagi_mawb_no) as AWB, "
+"l.lagi_hawb as HAWB,l.lagi_quantity_received as QUANTITY,l.lagi_weight_received as WEIGHT, "
+"sslp.sslp_rack_row as LOCATION "
+"from cusf_customs_forms cusf "
+"join lagi l on cusf.cusf_ident_no = l.lagi_ident_no "
+"join grai_group_additional_info grai on cusf.cusf_ident_no = grai.grai_object_isn "
       +"and grai.grai_group_type = 'PIECES' "
                         +"AND grai.grai_group_code = 'RECEIVED' "
                         +"join han_w1_hl.locs_locations locs "
                         +"on cusf.cusf_ident_no = locs.locs_object_isn "
                         +"and grai.grai_object_group_isn = locs.locs_group_isn "
                         +"join han_w1_hl.sslp_physical_locations sslp "
                         +"on locs.locs_physical_isn = sslp.sslp_physical_isn "
                + " where cusf.cusf_form_number in (" + listPXK + ")";
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                while (reader.Read())
                {
                    PXKHermesViewModel pxkViewModel = GetProperties(reader);
                    listPXKViewModel.Add(GetProperties(reader));
                }
            }
            return listPXKViewModel;
        }
    }
}
