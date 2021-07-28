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
    public class PXKAccess : DATABASE.OracleProvider
    {
        private PXKHermesViewModel GetProperties(OracleDataReader reader)
        {
            PXKHermesViewModel pxk = new PXKHermesViewModel();
            pxk.PXKNo = Convert.ToString(GetValueField(reader, "PXKNo", string.Empty));
            pxk.VCTNo = Convert.ToString(GetValueField(reader, "VCTNo", string.Empty));
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
                         + "vhcr.vhcl_cfs_number as VCTNo,"
                         + "cusf.cusf_form_number as PXKNo,"
                         + "vhld.vhld_releasetype "
                         + "from vhld_vehicledetail vhld "
                         + "join cusf_customs_forms cusf on cusf.cusf_ident_no = vhld.vhld_objectisn "
                         + "join vehicles_registration vhcr on vhld.vhld_vehicleisn = vhcr.vhcl_isn_no "
                         + "where cusf.cusf_form_number in (" + listPXK + ") and vhld.vhld_releasetype = 'RELEASE NOTE' and vhcr.vhcl_arrival_date = trunc(sysdate)";
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
