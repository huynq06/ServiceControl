using StorageManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.DBModel
{
    public class CutOffTime
    {
        public string AWB { set; get; }
        //public string LABS_IDENT_NO { set; get; }
        //public string BOOKING_FLIGHT { set; get; }
        public DateTime BOOKING_DATE { set; get; }
        //public static void LoadFromList(List<CutOffTime> listForm)
        //{
        //    DictionaryStorage<string, DateTime> dictData = new DictionaryStorage<string, DateTime>();
        //    foreach (var form in listForm)
        //    {
        //        dictData.EnqueueElementData(f)
        //    }

        //}
    }
}
