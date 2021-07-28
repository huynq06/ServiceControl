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
    public partial class FrmConnectionConfig : Form
    {
        public FrmConnectionConfig()
        {
            InitializeComponent();
        }

        private void FrmConnectionConfig_Load(object sender, EventArgs e)
        {
            try
            {
                txtDataSource.Text = AppSetting.DataSource;
                txtDatabase.Text = AppSetting.Database;
                txtUserID.Text = AppSetting.UserID;
                txtPassword.Text = AppSetting.Password;
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
                AppSetting.DataSource = txtDataSource.Text.Trim();
                AppSetting.Database = txtDatabase.Text.Trim();
                AppSetting.UserID = txtUserID.Text.Trim();
                AppSetting.Password = txtPassword.Text;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
