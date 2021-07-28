namespace TASK.UI
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnShow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mnSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnConnectionConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnAppConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnStartStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.sttBarSystem = new System.Windows.Forms.StatusStrip();
            this.lblSystemStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblWakerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblXmlReaderStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblGcCollectorStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLoggerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFTPUploaderStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.sttBarSystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            //this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            //this.notifyIcon.Text = "Ping Management";
            //this.notifyIcon.Visible = true;
            //this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnShow,
            this.cmnExit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 76);
            // 
            // cmnShow
            // 
            this.cmnShow.Name = "cmnShow";
            this.cmnShow.Size = new System.Drawing.Size(148, 36);
            this.cmnShow.Text = "Show";
            this.cmnShow.Click += new System.EventHandler(this.cmnShow_Click);
            // 
            // cmnExit
            // 
            this.cmnExit.Name = "cmnExit";
            this.cmnExit.Size = new System.Drawing.Size(148, 36);
            this.cmnExit.Text = "Exit";
            this.cmnExit.Click += new System.EventHandler(this.mnExit_Click);
            // 
            // menuMain
            // 
            this.menuMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnSystem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuMain.Size = new System.Drawing.Size(704, 44);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // mnSystem
            // 
            this.mnSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnConnect,
            this.mnConnectionConfig,
            this.toolStripSeparator2,
            this.mnAppConfig,
            this.toolStripSeparator1,
            this.mnStartStop,
            this.toolStripSeparator3,
            this.mnExit});
            this.mnSystem.Name = "mnSystem";
            this.mnSystem.Size = new System.Drawing.Size(109, 40);
            this.mnSystem.Text = "System";
            // 
            // mnConnect
            // 
            this.mnConnect.Name = "mnConnect";
            this.mnConnect.Size = new System.Drawing.Size(328, 40);
            this.mnConnect.Text = "Connect";
            this.mnConnect.Click += new System.EventHandler(this.mnConnect_Click);
            // 
            // mnConnectionConfig
            // 
            this.mnConnectionConfig.Name = "mnConnectionConfig";
            this.mnConnectionConfig.Size = new System.Drawing.Size(328, 40);
            this.mnConnectionConfig.Text = "Connection Config";
            this.mnConnectionConfig.Click += new System.EventHandler(this.mnConnectionConfig_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(325, 6);
            // 
            // mnAppConfig
            // 
            this.mnAppConfig.Name = "mnAppConfig";
            this.mnAppConfig.Size = new System.Drawing.Size(328, 40);
            this.mnAppConfig.Text = "App Config";
            this.mnAppConfig.Click += new System.EventHandler(this.mnAppConfig_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(325, 6);
            // 
            // mnStartStop
            // 
            this.mnStartStop.Name = "mnStartStop";
            this.mnStartStop.Size = new System.Drawing.Size(328, 40);
            this.mnStartStop.Text = "Start";
            this.mnStartStop.Click += new System.EventHandler(this.mnStartStop_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(325, 6);
            // 
            // mnExit
            // 
            this.mnExit.Name = "mnExit";
            this.mnExit.Size = new System.Drawing.Size(328, 40);
            this.mnExit.Text = "Exit";
            this.mnExit.Click += new System.EventHandler(this.mnExit_Click);
            // 
            // sttBarSystem
            // 
            this.sttBarSystem.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.sttBarSystem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSystemStatus,
            this.toolStripStatusLabel2,
            this.lblWakerStatus,
            this.toolStripStatusLabel5,
            this.lblXmlReaderStatus,
            this.toolStripStatusLabel6,
            this.lblGcCollectorStatus,
            this.toolStripStatusLabel7,
            this.lblLoggerStatus,
            this.toolStripStatusLabel1,
            this.lblFTPUploaderStatus});
            this.sttBarSystem.Location = new System.Drawing.Point(0, 315);
            this.sttBarSystem.Name = "sttBarSystem";
            this.sttBarSystem.Size = new System.Drawing.Size(704, 37);
            this.sttBarSystem.TabIndex = 1;
            this.sttBarSystem.Text = "System Status";
            // 
            // lblSystemStatus
            // 
            this.lblSystemStatus.Name = "lblSystemStatus";
            this.lblSystemStatus.Size = new System.Drawing.Size(162, 32);
            this.lblSystemStatus.Text = "System Status";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(21, 32);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // lblWakerStatus
            // 
            this.lblWakerStatus.Name = "lblWakerStatus";
            this.lblWakerStatus.Size = new System.Drawing.Size(152, 32);
            this.lblWakerStatus.Text = "Waker Status";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(0, 32);
            // 
            // lblXmlReaderStatus
            // 
            this.lblXmlReaderStatus.Name = "lblXmlReaderStatus";
            this.lblXmlReaderStatus.Size = new System.Drawing.Size(0, 32);
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(0, 32);
            // 
            // lblGcCollectorStatus
            // 
            this.lblGcCollectorStatus.Name = "lblGcCollectorStatus";
            this.lblGcCollectorStatus.Size = new System.Drawing.Size(0, 32);
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(0, 32);
            // 
            // lblLoggerStatus
            // 
            this.lblLoggerStatus.Name = "lblLoggerStatus";
            this.lblLoggerStatus.Size = new System.Drawing.Size(0, 32);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 32);
            // 
            // lblFTPUploaderStatus
            // 
            this.lblFTPUploaderStatus.Name = "lblFTPUploaderStatus";
            this.lblFTPUploaderStatus.Size = new System.Drawing.Size(0, 32);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 352);
            this.Controls.Add(this.sttBarSystem);
            this.Controls.Add(this.menuMain);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuMain;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TASK Management";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.sttBarSystem.ResumeLayout(false);
            this.sttBarSystem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmnShow;
        private System.Windows.Forms.ToolStripMenuItem cmnExit;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem mnSystem;
        private System.Windows.Forms.ToolStripMenuItem mnStartStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnExit;
        private System.Windows.Forms.ToolStripMenuItem mnConnect;
        private System.Windows.Forms.ToolStripMenuItem mnConnectionConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip sttBarSystem;
        private System.Windows.Forms.ToolStripStatusLabel lblSystemStatus;
        //private System.Windows.Forms.ToolStripMenuItem mnExportXMLToSQL;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        //private System.Windows.Forms.ToolStripMenuItem mnProcessXMLData;
        private System.Windows.Forms.ToolStripMenuItem mnAppConfig;
        //private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripStatusLabel lblWakerStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblXmlReaderStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblGcCollectorStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblLoggerStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblFTPUploaderStatus;
    }
}