using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Settings;

namespace TASK.DATA
{
    public partial class tblDangKyGoiXe
    {
        public static List<tblDangKyGoiXe> GetListTruckAllow(int count)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                List<tblDangKyGoiXe> temp = dbConext.tblDangKyGoiXes.Where(c => c.TrangThai == 0 && c.ViTri == 2 && c.ThoiGianDangKy.Value.Day == DateTime.Now.Day && c.ThoiGianDangKy.Value.Month == DateTime.Now.Month && c.ThoiGianDangKy.Value.Year == DateTime.Now.Year && c.ThoiGianDangKy.Value.Hour >= DateTime.Now.Hour-2).ToList();
                foreach(var item in temp)
                {
                    if (item.SortValue == 1)
                        item.SortValue = 0;
                }
                return temp.OrderByDescending(c=>c.SortValue).ThenBy(c=>c.ThoiGianDangKy).Take(count).ToList();
            }
        }
        public static List<tblDangKyGoiXe> GetListTruckFloor2Select(int count)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblDangKyGoiXes.Where(c => c.TrangThai == 0 && c.ViTri == 2 && c.ThoiGianDangKy.Value.Day == DateTime.Now.Day && c.ThoiGianDangKy.Value.Month == DateTime.Now.Month && c.ThoiGianDangKy.Value.Year == DateTime.Now.Year).OrderByDescending(c => c.SortValue).ThenBy(c=>c.ThoiGianDangKy).Take(count).ToList();
            }
        }
        public static List<tblDangKyGoiXe> GetListTruckFloorToCheck()
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblDangKyGoiXes.Where(c => c.TrangThai == 0 && c.ViTri == 2 && c.ThoiGianDangKy.Value.Day == DateTime.Now.Day && c.ThoiGianDangKy.Value.Month == DateTime.Now.Month && c.ThoiGianDangKy.Value.Year == DateTime.Now.Year && c.ThoiGianDangKy.Value.Hour >= DateTime.Now.Hour - 2).ToList();
            }
        }
        public static List<tblDangKyGoiXe> GetListTruckFloor1(int count)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblDangKyGoiXes.Where(c => c.TrangThai == 0 && c.ViTri == 1 && c.ThoiGianDangKy.Value.Day == DateTime.Now.Day && c.ThoiGianDangKy.Value.Month == DateTime.Now.Month && c.ThoiGianDangKy.Value.Year == DateTime.Now.Year && c.ThoiGianDangKy.Value.Hour >= DateTime.Now.Hour - 2).OrderByDescending(c => c.SortValue).ThenBy(c => c.ThoiGianDangKy).Take(count).ToList();
            }
        }
        public static void UpdateSortValue(int sortValue,int ID)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {

                var objectDB = dbConext.tblDangKyGoiXes.FirstOrDefault(c => c.ID == ID);
                objectDB.SortValue = sortValue;
                dbConext.SubmitChanges();
            }
        }
        public static List<tblDangKyGoiXe> GetListTruckUpdateSortValue()
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblDangKyGoiXes.Where(c => c.TrangThai == 0 && c.ViTri == 1 && c.ThoiGianDangKy.Value.Day == DateTime.Now.Day && c.ThoiGianDangKy.Value.Month == DateTime.Now.Month && c.ThoiGianDangKy.Value.Year == DateTime.Now.Year && c.Remark.Length > 0).ToList();
            }
        }
        public static void Update(List<tblDangKyGoiXe> trucks)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                foreach (var truck in trucks)
                {
                    var truckDb = dbConext.tblDangKyGoiXes.Where(c => c.ID == truck.ID).First();
                    truckDb.SortValue = truck.SortValue.HasValue? truck.SortValue.Value : 0;
                    truckDb.SynID = truck.SynID.HasValue? truck.SynID.Value : Guid.Empty;
                    truckDb.TruckStatus = truck.TruckStatus;
                }

                dbConext.SubmitChanges();
            }
        }

        public static void UpdateCallTruck(List<tblDangKyGoiXe> trucks)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                List<tblDangKyGoiXe> listUpdateBeforeProcess = dbConext.tblDangKyGoiXes.Where(c => c.TrangThai == 0  && c.ThoiGianDangKy.Value.Day == DateTime.Now.Day && c.ThoiGianDangKy.Value.Month == DateTime.Now.Month && c.ThoiGianDangKy.Value.Year == DateTime.Now.Year).ToList();
               foreach(var item in listUpdateBeforeProcess)
                {
                    var conditionDb = dbConext.tblDangKyGoiXes.Where(c => c.ID == item.ID).First();
                    conditionDb.QueryStatus = false;
                }
                foreach (var item in trucks)
                {
                    var conditionDb = dbConext.tblDangKyGoiXes.Where(c => c.ID == item.ID).First();
                    conditionDb.QueryStatus = true;
                }

                dbConext.SubmitChanges();
            }
        }
        public static List<tblDangKyGoiXe> GetListTruckForSync(DateTime dateCheck)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblDangKyGoiXes.Where(c => c.GioGoi > dateCheck && c.SynID == null && c.TruckStatus == 1).ToList();
            }
        }
        public static List<tblDangKyGoiXe> GetListTruckForTrackandTrace()
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblDangKyGoiXes.Where(c => c.ThoiGianDangKy.Value.Day == DateTime.Now.Day && c.ThoiGianDangKy.Value.Month == DateTime.Now.Month && c.ThoiGianDangKy.Value.Year == DateTime.Now.Year && c.SynID != null && c.TruckStatus != Utils.TruckConstants.TruckCheckOut).ToList();
            }
        }
        public static List<tblDangKyGoiXe> GetListTruckMonthlyForTrackandTrace(DateTime dateCheck)
        {
            using (PXKControlDbContextDataContext dbConext = new PXKControlDbContextDataContext(AppSetting.ConnectionStringPXKControl))
            {
                return dbConext.tblDangKyGoiXes.Where(c => c.GioGoi > dateCheck && c.SynID == null && c.TruckStatus != Utils.TruckConstants.TruckCheckOut && c.TruckStatus != Utils.TruckConstants.TruckInitial).ToList();
            }
        }
    }
}
