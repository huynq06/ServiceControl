using PluggableModulesInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TASK.Business.StaticThread;
using TASK.DATA;
using TASK.Settings;

namespace TASK.UI
{
    public partial class FrmMain : Form
    {
        #region declare
        private static int WM_QUERYENDSESSION = 0x11;
        private static bool systemShutdown = false;
        #endregion declare

        public FrmMain()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(FrmMain_FormClosing);
        }

        private void InitNotifierSql()
        {
            //SQLNotifier notifyCooperate = new SQLNotifier("CooperateProvider", "CooperateID");
            //notifyCooperate.Notify += new SQLNotifier.OnNotify(CooperateProvider.ReloadData);
            //notifyCooperate.Start();
        }

        #region Notify Icon function
        void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (systemShutdown)
            {
                this.Show();
                systemShutdown = false;
                if (MessageBox.Show("Before system shutdown or reboot, must be backup data.\nDo you want backup data and close program now?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    FrmProgressBar.Show("Backup data and exiting...");
                    PluggableManage.Stop();
                    StaticThreadManage.Stop();
                    FrmProgressBar.Hide();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
                return;
            }
            if (e.CloseReason == CloseReason.ApplicationExitCall) return;
            e.Cancel = true;
            this.Hide();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!this.Visible)
                this.Show();
        }

        private void cmnShow_Click(object sender, EventArgs e)
        {
            if (!this.Visible)
                this.Show();
        }

        private void mnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    FrmProgressBar.Show("Backup data and exiting...");
                    ThreadManagement.Instance.Exit();
                    FrmProgressBar.Hide();
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                FrmProgressBar.Hide();
                MessageBox.Show(ex.ToString());
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_QUERYENDSESSION)
            {
                //MessageBox.Show("queryendsession: this is a logoff, shutdown, or reboot");
                systemShutdown = true;
            }

            // If this is WM_QUERYENDSESSION, the closing event should be
            // raised in the base WndProc.
            base.WndProc(ref m);

        } //WndProc
        #endregion Notify Icon function

        #region Event
        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                EnableSystem(false);
            }
            catch (Exception ex)
            {
                FrmProgressBar.Hide();
                if (ex.Message.Contains("The server was not found or was not accessible"))
                {
                    MessageBox.Show("The server was not found or was not accessible.\n Program will be closed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void mnStartStop_Click(object sender, EventArgs e)
        {
            if (mnStartStop.Text == "Start")
            {
                Start();
            }
            else
            {
                if (MessageBox.Show("Are you sure want to stop all running module?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    Stop();
                }
            }
            //SetMenuStatus();
            foreach (Form f in this.MdiChildren)
            {
                //if (f.Name == typeof(FrmRouterList).Name)
                //{
                //    ((FrmRouterList)f).LoadData();
                //}
            }
        }

        private void mnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProgressBar.Show("Connecting...");
                using (SqlConnection con = AppSetting.GetConnection())
                {
                    if (con.State == ConnectionState.Open)
                    {
                        EnableSystem(true);
                    }
                    else if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                    {
                        EnableSystem(false);
                        MessageBox.Show("The server was not found or was not accessible.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                FrmProgressBar.Hide();
            }
            catch (Exception ex)
            {
                EnableSystem(false);
                FrmProgressBar.Hide();
                if (ex.Message.Contains("The server was not found or was not accessible") || ex.Message.Contains("Timeout expired"))
                {
                    MessageBox.Show("The server was not found or was not accessible.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ex.Message.Contains("Cannot open database"))
                {
                    MessageBox.Show("Cannot open database.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ex.Message.Contains("Login failed for user"))
                {
                    MessageBox.Show("UserID or password not correct.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void mnConnectionConfig_Click(object sender, EventArgs e)
        {
            FrmConnectionConfig frm = new FrmConnectionConfig();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StaticThreadManage.Stop();
                this.EnableSystem(false);
            }
        }

        private void mnAppConfig_Click(object sender, EventArgs e)
        {
            FrmAppConfig frm = new FrmAppConfig();
            frm.ShowDialog();
        }

        //private void mnProcessXMLData_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        FrmProgressBar.Show();
        //        StaticThreadManage.ProcessXmlData();
        //        FrmProgressBar.Hide();
        //    }
        //    catch (Exception ex)
        //    {
        //        FrmProgressBar.Hide();
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        //private void mnExportXMLDataToSQL_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        FrmProgressBar.Show();
        //        StaticThreadManage.ExportXMLToSQL();
        //        FrmProgressBar.Hide();
        //    }
        //    catch (Exception ex)
        //    {
        //        FrmProgressBar.Hide();
        //        MessageBox.Show(ex.ToString());
        //    }
        //}        
        #endregion Event

        public void SetMenuStatus()
        {
            mnConnect.Enabled = !StaticThreadManage.issuer.Started;
            mnConnectionConfig.Enabled = mnConnect.Enabled;
            mnAppConfig.Enabled = true;
            mnStartStop.Enabled = AppSetting.SystemConnected;
            //mnProcessXMLData.Enabled = AppSetting.SystemConnected;

            //if (StaticThreadManage.issuer.Started && StaticThreadManage.issuer.Started
            //  ) mnStartStop.Text = "Stop";
            //else mnStartStop.Text = "Start";
        }

        private void Start()
        {
            try
            {
                FrmProgressBar.Show("Starting...");
                mnStartStop.Text = "Stop";
                FrmProgressBar.Hide();
                ThreadManagement.Instance.Start();
            }
            catch (Exception ex)
            {
                FrmProgressBar.Hide();
                MessageBox.Show(ex.ToString());
            }
        }

        private void Stop()
        {
            try
            {
                FrmProgressBar.Show("Stopping...");
                ThreadManagement.Instance.Stop();
                mnStartStop.Text = "Start";
                FrmProgressBar.Hide();
            }
            catch (Exception ex)
            {
                FrmProgressBar.Hide();
                MessageBox.Show(ex.ToString());
            }
        }

        private void EnableSystem(bool enableSystem)
        {
            try
            {
                AppSetting.SystemConnected = enableSystem;
                if (enableSystem)
                {
                    lblSystemStatus.Text = "System Connected";
                    lblSystemStatus.ForeColor = Color.Green;
                    mnConnect.Text = "Connected";
                    mnConnect.ForeColor = Color.Black;
                    FrmProgressBar.Show("Initializing...");
                    this.FormClosing -= new FormClosingEventHandler(FrmMain_FormClosing);
                    this.FormClosing += new FormClosingEventHandler(FrmMain_FormClosing);
                    InitNotifierSql();
                    //PluggableManage.LoadExistingModules(Settings.AppSetting.AppModulesPath);
                    FrmProgressBar.Hide();
                }
                else
                {
                    mnConnect.Text = "Connect";
                    mnConnect.ForeColor = Color.Red;
                    lblSystemStatus.Text = "System DisConnected";
                    lblSystemStatus.ForeColor = Color.Red;
                }
                SetMenuStatus();
                foreach (Form f in this.MdiChildren)
                {
                    //if (f.Name == typeof(FrmRouterList).Name)
                    //{
                    //    ((FrmRouterList)f).EnableAllControl(enableSystem);
                    //}
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //if (StaticThreadManage.issuer.Started)
                //{
                //    lblWakerStatus.Text = "Get Issue Started";
                //    lblWakerStatus.ForeColor = Color.Green;
                //}
                //else
                //{
                //    lblWakerStatus.Text = "Get Issue Stopped";
                //    lblWakerStatus.ForeColor = Color.Red;
                //}
                GC.Collect();
                //if (StaticThreadManage.XmlReader.Started)
                //{
                //    lblXmlReaderStatus.Text = "XmlReader Started";
                //    lblXmlReaderStatus.ForeColor = Color.Green;
                //}
                //else
                //{
                //    lblXmlReaderStatus.Text = "XmlReader Stopped";
                //    lblXmlReaderStatus.ForeColor = Color.Red;
                //}

                //if (StaticThreadManage.GcCollecter.Started)
                //{
                //    lblGcCollectorStatus.Text = "GcCollecter Started";
                //    lblGcCollectorStatus.ForeColor = Color.Green;
                //}
                //else
                //{
                //    lblGcCollectorStatus.Text = "GcCollecter Stopped";
                //    lblGcCollectorStatus.ForeColor = Color.Red;
                //}

                //if (StaticThreadManage.Logger.Started)
                //{
                //    if (StaticThreadManage.Logger.Sleeping)
                //    {
                //        lblLoggerStatus.Text = "Logger Sleeping";
                //        lblLoggerStatus.ForeColor = Color.Blue;
                //    }
                //    else
                //    {
                //        lblLoggerStatus.Text = "Logger Working";
                //        lblLoggerStatus.ForeColor = Color.Green;
                //    }
                //}
                //else
                //{
                //    lblLoggerStatus.Text = "Logger Stopped";
                //    lblLoggerStatus.ForeColor = Color.Red;
                //}

                //if (StaticThreadManage.FTPUploader.Started)
                //{
                //    if (StaticThreadManage.FTPUploader.Sleeping)
                //    {
                //        lblFTPUploaderStatus.Text = "FTPUploader Sleeping";
                //        lblFTPUploaderStatus.ForeColor = Color.Blue;
                //    }
                //    else
                //    {
                //        lblFTPUploaderStatus.Text = "FTPUploader Working";
                //        lblFTPUploaderStatus.ForeColor = Color.Green;
                //    }
                //}
                //else
                //{
                //    lblFTPUploaderStatus.Text = "FTPUploader Stopped";
                //    lblFTPUploaderStatus.ForeColor = Color.Red;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
