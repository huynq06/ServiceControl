using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluggableModulesInterface;
using Utils;
using TASK.Business;

namespace TASK.Business.StaticThread
{
    public static class StaticThreadManage
    {
        public static bool Running { get; private set; }

        //public static WorkingBaseTimer Logger = new LoggerThreadManage(true);
        public static WorkingBaseTimer cutOff = new UpdateCutOffThreadManager();
        public static WorkingBaseTimer transitioner = new TransitionsIssueThreadManager();
        public static WorkingBaseTask Waker = new WakerThreadManagement();
        //public static WorkingBaseTimer pinger = new PingThreadManagement();
        public static WorkingBaseTimer issuer = new IssueThreadManagement();
        

        public static void Start()
        {
            try
            {
                //FTPUploader.Start();
                if (!Waker.Started) Waker.Start();
                //if (!Logger.Started) Logger.Start();
                if (!transitioner.Started) transitioner.Start();
                if (!cutOff.Started) cutOff.Start();
                if (!issuer.Started) issuer.Start();

                Running = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Stop()
        {
            try
            {
                //Waker.Stop();
                //FTPUploader.Wait();
                //FTPUploader.Stop();
                //Logger.Wait();
                //Logger.Stop();
                //XmlReader.Stop();
                //GcCollecter.Stop();
                issuer.Stop();

                Running = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Exit()
        {
            PluggableManage.Stop();
            Stop();
        }

        /// <summary>
        /// Xử lý dữ liệu còn nằm trên file xml, đẩy vào csdl.
        /// </summary>
        public static void ProcessXmlData()
        {
            try
            {
                //SmsCdrReport.InsertFromXmlData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ExportXMLToSQL()
        {
            try
            {
                //SmsCdrReport.ExportXmlToSQL();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
