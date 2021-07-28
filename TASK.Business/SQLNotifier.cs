using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using TASK.Settings;

namespace TASK.Business
{
    public class SQLNotifier : IDisposable
    {
        private event OnChangeEventHandler handler;
        public delegate void OnNotify();
        /// <summary>
        /// Sự kiện xảy ra khi có notify
        /// </summary>
        public event OnNotify Notify;
        private string SelectQuery;

        public SQLNotifier(string tableName, string idFieldName)
        {
            if (!tableName.StartsWith("dbo", StringComparison.OrdinalIgnoreCase))
                tableName = "dbo." + tableName;
            SelectQuery = string.Format("SELECT {0} FROM {1}", idFieldName, tableName);
            try
            {
                CreatePermission();
                SqlDependency.Start(AppSetting.ConnectionString);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Start()
        {
            handler += new OnChangeEventHandler(SQLNotifier_handler);
            this.RegisterDependency();
        }

        private void SQLNotifier_handler(object sender, SqlNotificationEventArgs e)
        {
            RegisterDependency();
        }

        private void RegisterDependency()
        {
            try
            {
                using (SqlConnection conn = AppSetting.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(SelectQuery, conn))
                    {
                        SqlDependency dependency = new SqlDependency(cmd);
                        dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                        cmd.ExecuteScalar();
                        if (Notify != null)
                            Notify();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            ((SqlDependency)sender).OnChange -= this.dependency_OnChange;
            if (handler != null)
                handler(this, e);
        }

        private void CreatePermission()
        {
            // Make sure client has permissions 
            try
            {
                SqlClientPermission perm = new SqlClientPermission(System.Security.Permissions.PermissionState.Unrestricted);
                perm.Demand();
            }
            catch
            {
                throw new ApplicationException("No permission");
            }
        }

        #region IDisposable Members
        public void Dispose()
        {
            SqlDependency.Stop(AppSetting.ConnectionString);
        }
        #endregion
    }
}
