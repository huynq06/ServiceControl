using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;

namespace TASK.Services
{
    public static class NotifyOverTimeService
    {
        public static bool CheckOverTime()
        {
            bool check = false;
            //lay danh sach ca ULD đang xử lý đang vượt ngưỡng Threshold
            List<ULDByFlight> ulds = ULDByFlight.GetULDProcessing().Where(c=>c.NotifyID == 2).ToList();
            if (ulds.Count > 0)
            {
                foreach (var uld in ulds)
                {
                    int threshold = ULD_TYPE.GetThresholdByID(uld.ULD_TYPE.Value);
                    int limit = ULD_TYPE.GetOverTimeByID(uld.ULD_TYPE.Value);
                    int timeOpearation = (int)Math.Round((DateTime.Now - uld.StartTime.Value).TotalMinutes, 0);
                    if (timeOpearation > limit)
                    {
                        check = true;
                        break;
                    }

                }
            }
            return check;

        }
        public static void UpdateOverTime()
        {
            List<ULDByFlight> ulds = ULDByFlight.GetULDProcessing();
            if (ulds.Count > 0)
            {
                foreach (var uld in ulds)
                {
                    int threshold = ULD_TYPE.GetThresholdByID(uld.ULD_TYPE.Value);
                    int limit = ULD_TYPE.GetOverTimeByID(uld.ULD_TYPE.Value);
                    int timeOpearation = (int)Math.Round((DateTime.Now - uld.StartTime.Value).TotalMinutes, 0);
                    if (timeOpearation > limit)
                    {
                        uld.NotifyID = 3;
                        uld.NotifyMessage = "Đã hết giờ khai thác";
                        ULDByFlight.UpdateStatus(uld);
                    }

                }
            }
        }

    }
}
