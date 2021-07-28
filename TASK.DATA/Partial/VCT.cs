using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class VCT
    {
        public static void Inserts(List<VCT> listVCTs)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                if (listVCTs.Count > 0)
                {
                    dbConext.VCTs.InsertAllOnSubmit(listVCTs);
                }
                dbConext.SubmitChanges();
            }
        }
        public static void Insert(VCT VCT)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                
                    dbConext.VCTs.InsertOnSubmit(VCT);
                
                dbConext.SubmitChanges();
            }
        }
        public static void Update(List<VCT> vcts)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                foreach(var item in vcts)
                {
                    var conditionDb = dbConext.VCTs.Where(c => c.ID == item.ID).First();
                    conditionDb.AWB_STATUS = item.AWB_STATUS;
                    conditionDb.LABS_RECIEVED = item.LABS_RECIEVED;
                    conditionDb.LAST_GROUP = item.LAST_GROUP;
                    conditionDb.SortValue = item.SortValue;
                }
             
                dbConext.SubmitChanges();
            }
        }
        public static void UpdateConfirm(List<VCT> vcts)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                foreach (var item in vcts)
                {
                    var conditionDb = dbConext.VCTs.Where(c => c.ID == item.ID).First();
                    conditionDb.ConfirmStatus = 1;
                    conditionDb.LABS_CONFIRMED_AT = DateTime.Now;
                }

                dbConext.SubmitChanges();
            }
        }
        public static void UpdateVCT(int ID)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
               
                    var conditionDb = dbConext.VCTs.SingleOrDefault(c=>c.ID == ID);
                    conditionDb.VCT_DATE = DateTime.Now.Date;
                    dbConext.SubmitChanges();
            }
        }
        public static List<VCT> GetAll()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.VCTs.ToList();
            }
        }
        public static List<VCT> GetAllToday()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.VCTs.Where(c=>c.LABS_CREATED_AT > DateTime.Today).ToList();
            }
        }
        public static List<VCT> GetAllNotConfirmToday()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.VCTs.Where(c => c.LABS_CREATED_AT > DateTime.Today.AddHours(-12) && c.ConfirmStatus == 0).ToList();
            }
        }
        public static List<VCT> GetNotConfirmALSWToday()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.VCTs.Where(c => c.LABS_CREATED_AT > DateTime.Today.AddHours(-12) && c.ConfirmStatus == 0 && (c.AGENT_NAME.Trim() == "ALSW"|| c.AGENT_NAME.Trim()=="ASG" || c.AGENT_NAME.Trim() == "ALSE")).ToList();
            }
        }
        public static List<VCT> GetAllActiveToday()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.VCTs.Where(c => c.LABS_CREATED_AT > DateTime.Today.AddHours(-18) && c.AWB_STATUS == 2).ToList();
            }
        }
        public static List<VCT> GetScaleToday()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.VCTs.Where(c => c.LABS_CREATED_AT > DateTime.Today && c.AWB_STATUS == 3).ToList();
            }
        }
        public static List<VCT> GetListUpdateSortValue()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.VCTs.Where(c => c.LABS_CREATED_AT > DateTime.Today && c.AWB_STATUS  <2 && c.CutOffTime.HasValue).ToList();
            }
        }
        public static VCT GetByID(string LABS_IDENT_NO)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.VCTs.FirstOrDefault(p => p.LABS_IDENT_NO == LABS_IDENT_NO);
            }
        }
        public static List<VCT> GetByLagiIden(string LABS_IDENT_NO)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.VCTs.Where(p => p.LABS_IDENT_NO == LABS_IDENT_NO).ToList();
            }
        }

    }
}
