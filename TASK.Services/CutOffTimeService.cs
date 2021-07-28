using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Settings;
using StorageManagement;
using DATAACCESS;
using System.IO;

namespace TASK.Services
{
    //Service này chạy tự động 2 phút/lần update lại cut off time
    public static class CutOffTimeService
    {
        public static void ProcessCutOffTime()
        {
            string path = "C:\\Sample.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format("Windows Service Called on " + DateTime.Now.ToString("dd /MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }
        //Đẩy danh sách vào dictionary

        //update cutoffTime vao Db

    }


}
