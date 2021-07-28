using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class tblPXK
    {
        public static List<tblPXK> GetAll()
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblPXKs.Where(c => c.Created.Value.Date == DateTime.Now.Date && c.Status != 3).ToList().ToList();
            }
        }
        public static List<tblPXK> GetListCheck()
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblPXKs.Where(c => c.Created.Value.Date == DateTime.Now.Date && (c.Status == 1 || (c.Status == 2 && c.VCT == "0") || (c.Status == 3 && c.VCT == null))).ToList();
            }
        }
        public static List<tblPXK> GetByPXK(string pxk)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblPXKs.Where(c => c.PXK.Trim() == pxk && c.Created.Value.Date == DateTime.Now.Date).ToList();
            }
        }
        public static void UpdatePXK(tblPXK pxk)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {

                var pxkDB = dbConext.tblPXKs.FirstOrDefault(c => c.ID == pxk.ID);
                pxkDB.VCT = pxk.VCT;
                pxkDB.AWB = pxk.AWB;
                pxkDB.Hawb = pxk.Hawb;
                pxkDB.Pieces = pxk.Pieces;
                pxkDB.Weight = pxk.Weight;
                pxkDB.Status = pxk.Status;
                pxkDB.Finish = pxk.Finish;
                pxkDB.GroupNumer = pxk.GroupNumer;
                dbConext.SubmitChanges();
            }
        }
        public static void UpdatePXK(List<tblPXK> pxks)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                foreach (var pxk in pxks)
                {
                    var pxkDB = dbConext.tblPXKs.FirstOrDefault(c => c.ID == pxk.ID);
                    pxkDB.VCT = pxk.VCT;
                    pxkDB.AWB = pxk.AWB;
                    pxkDB.Hawb = pxk.Hawb;
                    pxkDB.Pieces = pxk.Pieces;
                    pxkDB.Weight = pxk.Weight;
                    pxkDB.Status = pxk.Status;
                    pxkDB.Finish = pxk.Finish;
                    dbConext.SubmitChanges();

                }
                dbConext.SubmitChanges();
             
            }
        }
    }
}
