using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TASK.UI
{
    public partial class FrmProgressBar : Form
    {
        private static Thread threadShowProgress;
        private static bool _ShowProgressBar = false;
        public bool ShowProgressBar
        {
            get
            {
                return _ShowProgressBar;
            }
            set
            {
                _ShowProgressBar = value;
            }
        }
        private static string _Status = "Processing...";
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        public FrmProgressBar()
        {
            InitializeComponent();
            System.Windows.Forms.Timer timerCheckClose = new System.Windows.Forms.Timer();
            timerCheckClose.Tick += new EventHandler(timerCheckClose_Tick);
            timerCheckClose.Start();
        }

        void timerCheckClose_Tick(object sender, EventArgs e)
        {
            if (_Status == "")
            {
                this.lblStatus.Text = "Processing...";
            }
            else
            {
                this.lblStatus.Text = _Status;
            }
            if (!_ShowProgressBar)
            {
                this.Close();
            }
        }

        private static void ShowForm()
        {
            FrmProgressBar form = new FrmProgressBar();
            form.ShowDialog();
        }

        public static void Show()
        {
            try
            {
                _ShowProgressBar = true;
                threadShowProgress = new Thread(new ThreadStart(ShowForm));
                threadShowProgress.IsBackground = true;
                threadShowProgress.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void Show(string status)
        {
            _Status = status;
            Show();
        }

        public static void Hide()
        {
            _Status = "";
            _ShowProgressBar = false;
        }
    }
}
