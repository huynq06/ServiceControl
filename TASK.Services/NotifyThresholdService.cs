using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

namespace TASK.Services
{
    public static class NotifyThresholdService
    {
        public static bool CheckThreshold()
        {
            bool check = false;
            try
            {
              
                //lay danh sach ca ULD đang xử lý
                List<ULDByFlight> ulds = ULDByFlight.GetULDProcessing().Where(c => c.NotifyID == 1).ToList();
                if (ulds.Count > 0)
                {
                    foreach (var uld in ulds)
                    {
                        int threshold = ULD_TYPE.GetThresholdByID(uld.ULD_TYPE.Value);
                        int limit = ULD_TYPE.GetOverTimeByID(uld.ULD_TYPE.Value);
                        int timeOpearation = (int)Math.Round((DateTime.Now - uld.StartTime.Value).TotalMinutes, 0);
                        if (timeOpearation >= threshold && timeOpearation < limit)
                        {
                            check = true;
                            break;
                        }

                    }
                }

            }
            catch (Exception)
            {

                Log.WriteLog("Canh bao ULD vuot qua thoi gian khai thac","NotifyThreshol.txt");
            }
            return check;
        }
        public static void UpdateThreshold()
        {
          
            List<ULDByFlight> ulds = ULDByFlight.GetULDProcessing();
            if (ulds.Count > 0)
            {
                foreach (var uld in ulds)
                {
                    try
                    {
                        int threshold = ULD_TYPE.GetThresholdByID(uld.ULD_TYPE.Value);
                        int limit = ULD_TYPE.GetOverTimeByID(uld.ULD_TYPE.Value);
                        int timeOpearation = (int)Math.Round((DateTime.Now - uld.StartTime.Value).TotalMinutes, 0);
                        if (timeOpearation >= threshold && timeOpearation < limit)
                        {
                            uld.NotifyID = 2;
                            uld.NotifyMessage = "Sắp hết giờ khai thác";
                            ULDByFlight.UpdateStatus(uld);
                        }
                    }
                    catch (Exception ex)
                    {

                        Log.WriteLog(ex.ToString() + uld.ULDID, "UpdateNotifyThreshol.txt");
                    }
                  

                }
            }
        }
    }
}
