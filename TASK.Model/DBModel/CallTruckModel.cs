using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.DBModel
{
    public class CallTruckModel
    {
        public int ID { set; get; }
        public string BienSoXe { set; get; }
        public string LoHang { set; get; }
        public string Remark { set; get; }
        public int ViTri { set; get; }
        public DateTime? ThoiGianDangKy { set; get; }
        public int? SortValue { set; get; }
        public string Note { set; get; }
        public DateTime? GioGoi { set; get; }
    }
}
