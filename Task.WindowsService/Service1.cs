using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TASK.Business.StaticThread;

namespace Task.WindowsService
{
    [RunInstaller(true)]
    public partial class Service1 : ServiceBase
    {
        int ScheduleInterval = 10;
    
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ThreadManagement.Instance.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public void onDebug()
        {
            OnStart(null);
        }
        protected override void OnStop()
        {
            try
            {
                ThreadManagement.Instance.Stop();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
