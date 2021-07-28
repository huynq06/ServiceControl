using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATAACCESS;
using TASK.DATA;
using TASK.Model.ViewModel;
using TASK.Settings;
using System.Transactions;

namespace TASK.Services
{
    public class CheckCloseAwbService
    {
        public static void CheckCloseAwb()
        {
            List<VCT> listVCT = VCT.GetScaleToday();
        }
    }
}
