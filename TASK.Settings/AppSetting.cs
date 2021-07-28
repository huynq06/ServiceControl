using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using Utils;


namespace TASK.Settings
{
    public class AppSetting
    {
        #region Connection
        public static string ConnectionString
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["MSSQLConnectionString"].ConnectionString; }
            set
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["MSSQLConnectionString"].ConnectionString = value;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                _SqlConnectionStringBuilder = new SqlConnectionStringBuilder(value);
            }
        }
        public static string ConnectionStringSyncData
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["ALSC_SyncData"].ConnectionString; }
            set
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["ALSC_SyncData"].ConnectionString = value;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                _SqlConnectionStringBuilder = new SqlConnectionStringBuilder(value);
            }
        }
        public static string ConnectionStringFlightControl
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["ALSC_FlightControl"].ConnectionString; }
            set
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["ALSC_FlightControl"].ConnectionString = value;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                _SqlConnectionStringBuilder = new SqlConnectionStringBuilder(value);
            }
        }
        public static string ConnectionStringPXKControl
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["ALSC_PXKControl"].ConnectionString; }
            set
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["ALSC_PXKControl"].ConnectionString = value;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                _SqlConnectionStringBuilder = new SqlConnectionStringBuilder(value);
            }
        }
        public static string ConnectionStringConnectALS
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectALS"].ConnectionString; }
            set
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["ConnectALS"].ConnectionString = value;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                _SqlConnectionStringBuilder = new SqlConnectionStringBuilder(value);
            }
        }
        public static string ConnectionStringLogCapso
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["CapSoConnection"].ConnectionString; }
            set
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["CapSoConnection"].ConnectionString = value;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                _SqlConnectionStringBuilder = new SqlConnectionStringBuilder(value);
            }
        }
        public static string HermesConnection
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["HermesConnection"].ConnectionString; }
        }
        public static string OracleConnection
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString; }
        }
        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static SqlConnectionStringBuilder _SqlConnectionStringBuilder;
        public static SqlConnectionStringBuilder ConnectionStringBuilder
        {
            get
            {
                if (_SqlConnectionStringBuilder == null) _SqlConnectionStringBuilder = new SqlConnectionStringBuilder(ConnectionString);
                return _SqlConnectionStringBuilder;
            }
        }

        public static string DataSource
        {
            get
            {
                return ConnectionStringBuilder.DataSource;
            }
            set
            {
                ConnectionStringBuilder.DataSource = value;
                ConnectionString = ConnectionStringBuilder.ConnectionString;
            }
        }

        public static string Database
        {
            get
            {
                return ConnectionStringBuilder.InitialCatalog;
            }
            set
            {
                ConnectionStringBuilder.InitialCatalog = value;
                ConnectionString = ConnectionStringBuilder.ConnectionString;
            }
        }

        public static string UserID
        {
            get
            {
                return ConnectionStringBuilder.UserID;
            }
            set
            {
                ConnectionStringBuilder.UserID = value;
                ConnectionString = ConnectionStringBuilder.ConnectionString;
            }
        }

        public static string Password
        {
            get
            {
                return ConnectionStringBuilder.Password;
            }
            set
            {
                ConnectionStringBuilder.Password = value;
                ConnectionString = ConnectionStringBuilder.ConnectionString;
            }
        }

        /// <summary>
        /// Tạo 1 transaction scope
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <param name="timeOut"></param>  
        /// <returns></returns>
        public static TransactionScope GetTransactionScope(System.Transactions.IsolationLevel isolationLevel, TimeSpan? timeOut)
        {
            try
            {
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = isolationLevel;
                if (timeOut != null) option.Timeout = timeOut.Value;
                TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                return scope;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Tạo 1 transaction scope
        /// </summary>
        /// <returns></returns>
        public static TransactionScope GetTransactionScope()
        {
            try
            {
                return GetTransactionScope(System.Transactions.IsolationLevel.ReadCommitted, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private static string _ApplicationName;
        public static string ApplicationName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ApplicationName)) _ApplicationName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                return _ApplicationName;
            }
        }

        public static int ConnectionTimeout
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ConnectionTimeout"].ToInt();
            }
            set
            {
                SetAppConfig("ConnectionTimeout", value.ToString());
            }
        }
        public static string IpServer
        {
            get
            {
                return GetAppConfig("IpSita").ToString();
            }
            set
            {
                SetAppConfig("IpSita", value.ToString());
            }
        }
        public static string USER_NAME
        {
            get { return GetAppConfig("username"); }
        }

        public static string PASSWORD
        {
            get { return GetAppConfig("password"); }
        }
        public static int ServiceTimeOut
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ServiceTimeOut"].ToInt();
            }
            set
            {
                SetAppConfig("ServiceTimeOut", value.ToString());
            }
        }

        public static string LogPath
        {
            get
            {
                return Path.Combine(Application.StartupPath, GetAppConfig("LogPath"));
            }
        }

        public static string AppBackupPath
        {
            get
            {
                string appPath = Path.GetDirectoryName(typeof(AppSetting).Assembly.Location);
                string backupPath = GetAppConfig("App_Backup_Path");
                string appbackupPath = Path.Combine(appPath, backupPath);

                if (!Directory.Exists(appbackupPath))
                {
                    Directory.CreateDirectory(appbackupPath);
                }
                return appbackupPath;
            }
        }

        public static string LogBackupFileName
        {
            get
            {
                return Path.Combine(AppBackupPath, GetAppConfig("LogBackupFileName"));
            }
        }

        public static int BatchSize
        {
            get
            {
                return GetAppConfig("BatchSize").ToInt();
            }
            set
            {
                SetAppConfig("BatchSize", value.ToString());
            }
        }

        public static int UpdateDataInterval
        {
            get
            {
                return GetAppConfig("UpdateDataInterval").ToInt();
            }
            set
            {
                SetAppConfig("UpdateDataInterval", value.ToString());
            }
        }

        public static int WakeUpInterval
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WakeUpInterval"].ToInt();
            }
            set
            {
                SetAppConfig("WakeUpInterval", value.ToString());
            }
        }

        public static int AlertInterval
        {
            get
            {
                return GetAppConfig("AlertInterval").ToInt();
            }
            set
            {
                SetAppConfig("AlertInterval", value.ToString());
            }
        }

        public static string GetAppConfig(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key] ?? "";
        }

        public static void SetAppConfig(string key, string value)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ((AppSettingsSection)config.GetSection("appSettings")).Settings[key].Value = value;
                config.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public struct Status
        {
            public const string EDITING = "EDITING";
            public const string REJECTED = "REJECTED";

            public const string WAITINGFORAPPROVE = "WAITINGFORAPPROVE";
            public const string APPROVED = "APPROVED";
            public const string SENDING = "SENDING";
            public const string FINISHED = "FINISHED";

            public const string PENDING = "PENDING";
            public const string SENTGATEWAY = "SENTGATEWAY";
            public const string SENTPARTNER = "SENTPARTNER";

            public const string SENT = "SENT";
            public const string FAIL = "FAIL";
            public const string TIMEOUT = "TIMEOUT";
        }

        public struct WarningType
        {
            public const string application_error = "application_error";
            public const string warning = "warning";
            public const string database = "database";
            public const string mo_error = "mo_error";
            public const string mt_error = "mt_error";
            public const string mo_message_warning = "mo_message_warning";
            public const string mt_message_warning = "mt_message_warning";
            public const string upload_error = "upload_error";
        }

        private static string _SystemID = "FTPManagement";
        public static string SystemID { get { return _SystemID; } }

        public static bool SystemConnected = false;
    }
}
