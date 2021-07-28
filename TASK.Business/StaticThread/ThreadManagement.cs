using PluggableModulesInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Business.StaticThread
{
    public class ThreadManagement
    {

        #region singleton pattern

        List<WorkingBaseTimer> ThreadList;

        private ThreadManagement()
        {
            ThreadList = new List<WorkingBaseTimer>();
            WakeupTimer wakeup = new WakeupTimer();
            ThreadList.Add(wakeup);



            //ThreadList.Add(new NotifycationThresholdThreadManagement()); //ok
            //ThreadList.Add(new NotifyOverTimeThreadManagement());  //ok
            ThreadList.Add(new ThreadTestManagement());//ok
            //Add thread sử sụng wakeup_timer
            //hreadList.Add(new LoggerThreadManage(wakeup));disable
            // ThreadList.Add(new ThreadCloseIssue(wakeup)); disable

            //ThreadList.Add(new IssueThreadManagement());//ok
            //ThreadList.Add(new CreateIssueThreadManagement());//ok
            //ThreadList.Add(new SpecialHandlingCodeThreadManagement());//ok
            //ThreadList.Add(new UpdateBookingTimeThreadManagement());//ok

            //ThreadList.Add(new UpdateSortValueThread());//ok
            //ThreadList.Add(new CreateFlightThreadManagement());//ok
            //ThreadList.Add(new CheckHawbBqlThreadManagement());//ok
            //ThreadList.Add(new SHCHawbThreadManangement());//ok


            //ThreadList.Add(new CheckProcessAwbThreadManagement());//ok
            //ThreadList.Add(new CheckConditionALSWManagement());//ok
            //ThreadList.Add(new CheckProcessThreadManagement());//ok
            //  ThreadList.Add(new UpdateCutOffThreadManager());



            //Service
            //ThreadList.Add(new ThreadTrackTruckMonthlyManagement());
            //  ThreadList.Add(new GetFlightDenThreadManagement());
            //   ThreadList.Add(new PXKThreadManagement());
            //ThreadList.Add(new CreatFlupThreadManagement());  //ok
        }

        private static object _lock = new object();
        private static ThreadManagement _instance;
        public static ThreadManagement Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance = new ThreadManagement();
                    }
                }
                return _instance;
            }
        }

        #endregion singleton pattern

        public bool Running
        {
            get
            {
                return ThreadList.Any(p => p.Started);
            }
        }

        public void Start()
        {
            try
            {
                foreach (var item in ThreadList)
                {
                    if (!item.Started) item.Start();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Stop()
        {
            try
            {
                foreach (var item in ThreadList)
                {
                    item.Wait();
                    item.Stop();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Exit()
        {
            Stop();
            //xử lý dữ liệu còn tồn đọng trong queueu
            //Đẩy lại dữ liệu trong queue của mo_queue_waiting vào db
            //tudn mo_queue_waiting.ReInsertFromQueueToDB();

            //Gửi nốt tin nhắn mt đang chờ trong queue
            //tudn mt_queue_log.SendAllMt();
        }

        /// <summary>
        /// Xử lý dữ liệu còn nằm trên file xml, đẩy vào csdl.
        /// </summary>
        public void ProcessXmlData()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExportXMLToSQL()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
