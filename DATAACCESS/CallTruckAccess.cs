using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Model.DBModel;
using DATABASE;

namespace DATAACCESS
{
    public class CallTruckAccess : DataProvider
    {
        public CallTruckModel GetPropertiesData(System.Data.IDataReader reader)
        {
            CallTruckModel truck = new CallTruckModel();
            truck.ID = Convert.ToInt32(GetValueField(reader, "ID", 0));
            truck.BienSoXe = Convert.ToString(GetValueField(reader, "BienSoXe", string.Empty));
            truck.LoHang = Convert.ToString(GetValueField(reader, "LoHang", string.Empty));
            truck.Remark = Convert.ToString(GetValueField(reader, "Remark", string.Empty));
            truck.SortValue = Convert.ToInt32(GetValueField(reader, "SortValue", 0));
            truck.ViTri = Convert.ToInt32(GetValueField(reader, "ViTri", string.Empty));
            truck.ThoiGianDangKy = GetValueDateTimeField(reader, "ThoiGianDangKy", truck.ThoiGianDangKy);
            truck.Note = Convert.ToString(GetValueField(reader, "Note", string.Empty));
            truck.GioGoi = GetValueDateTimeField(reader, "GioGoi", truck.GioGoi);
            return truck;
        }
        public IList<CallTruckModel> GetDataFloor1()
        {
            IList<CallTruckModel> truckList = new List<CallTruckModel>();
            using (System.Data.IDataReader reader = CommandDataReader("ListCallTruck"))
            {

                while (reader.Read())
                    truckList.Add(GetPropertiesData(reader));

            }
            return truckList;
        }
        public IList<CallTruckModel> GetDataFloor2()
        {
            IList<CallTruckModel> truckList = new List<CallTruckModel>();
            using (System.Data.IDataReader reader = CommandDataReader("ListCallTruckFloor2"))
            {

                while (reader.Read())
                    truckList.Add(GetPropertiesData(reader));

            }
            return truckList;
        }
        public IList<CallTruckModel> GetDataALSx()
        {
            IList<CallTruckModel> truckList = new List<CallTruckModel>();
            using (System.Data.IDataReader reader = CommandDataReader("ListCallTruckAlsx"))
            {

                while (reader.Read())
                    truckList.Add(GetPropertiesData(reader));

            }
            return truckList;
        }
    }
}
