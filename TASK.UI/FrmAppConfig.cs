using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TASK.Settings;

namespace TASK.UI
{
    public partial class FrmAppConfig : Form
    {
        public FrmAppConfig()
        {
            InitializeComponent();
        }

        private void FrmAppConfig_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in tableLayoutPanel1.Controls)
                {
                    if (c is NumericUpDown)
                    {
                        ((NumericUpDown)c).Controls[0].Visible = false;
                    }
                }

                //nudConnectionTimeout.Value = AppSetting.ConnectionTimeout;
                //nudServiceTimeout.Value = AppSetting.ServiceTimeOut;
                txtLogPath.Text = AppSetting.GetAppConfig("LogPath");
                txtApp_Backup_Path.Text = AppSetting.GetAppConfig("IpSita");
                //txtLogBackupFileName.Text = AppSetting.GetAppConfig("LogBackupFileName");

                //nudBatchSize.Value = AppSetting.BatchSize;
                nudUpdateDataInterval.Value = AppSetting.UpdateDataInterval;
                //nudWakeUpInterval.Value = AppSetting.WakeUpInterval;
                //nudAlertInterval.Value = AppSetting.AlertInterval;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //AppSetting.ConnectionTimeout = (int)nudConnectionTimeout.Value;
                //AppSetting.ServiceTimeOut = (int)nudServiceTimeout.Value;
                AppSetting.SetAppConfig("LogPath", txtLogPath.Text.Trim());
                AppSetting.SetAppConfig("IpSita", txtApp_Backup_Path.Text.Trim());
                //AppSetting.SetAppConfig("LogBackupFileName", txtLogBackupFileName.Text.Trim());
                //AppSetting.BatchSize = (int)nudBatchSize.Value;
                AppSetting.UpdateDataInterval = (int)nudUpdateDataInterval.Value;
                //AppSetting.WakeUpInterval = (int)nudWakeUpInterval.Value;
                //AppSetting.AlertInterval = (int)nudAlertInterval.Value;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
