using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TASK.Settings;
using System.Data;
using System.Data.SqlClient;
using Utils;

namespace TASK.DATA
{
    public class DataAccess
    {
        public static void BulkInsert<T>(IEnumerable<T> source, string tableName)
        {
            if (source == null || source.Count() == 0) return;
            try
            {
                using (DataTable dtSource = ObjectClass.CreateTable(source))
                {
                    BulkInsert(dtSource, tableName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void BulkInsert<T>(IEnumerable<T> source, string tableName, SqlConnection conn)
        {
            if (source == null || source.Count() == 0) return;
            try
            {
                using (DataTable dtSource = ObjectClass.CreateTable(source))
                {
                    BulkInsert(dtSource, tableName, conn);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void BulkInsert<T>(IEnumerable<T> source, string tableName, SqlTransaction trans)
        {
            if (source == null || source.Count() == 0) return;
            try
            {
                using (DataTable dtSource = ObjectClass.CreateTable(source))
                {
                    BulkInsert(dtSource, tableName, trans);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void BulkInsert(DataTable dtSource, string tableName)
        {
            using (SqlConnection conn = AppSetting.GetConnection())
            {
                BulkInsert(dtSource, tableName, conn);
            }
        }
        public static void BulkInsert(DataTable dtSource, string tableName, SqlConnection conn)
        {
            bool mustCloseConnection = false;
            if (conn.State == ConnectionState.Closed)
            {
                mustCloseConnection = true;
                conn.Open();
            }
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                BulkInsert(dtSource, tableName, trans);
                trans.Commit();
            }
            if (mustCloseConnection && conn.State == ConnectionState.Open)
                conn.Close();
        }
        public static void BulkInsert(DataTable dtSource, string tableName, SqlTransaction trans)
        {
            using (SqlBulkCopy bc = new SqlBulkCopy(trans.Connection, SqlBulkCopyOptions.Default, trans))
            {
                bc.DestinationTableName = tableName;
                bc.BatchSize = AppSetting.BatchSize;
                bc.BulkCopyTimeout = AppSetting.ConnectionTimeout;
                foreach (var column in dtSource.Columns)
                    bc.ColumnMappings.Add(column.ToString(), column.ToString());
                bc.WriteToServer(dtSource);
            }
        }

        public static void BatchExecute(DataTable paramTable, string paramName, string paramTypeName, string storeName)
        {
            try
            {
                using (SqlConnection conn = AppSetting.GetConnection())
                {
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        BatchExecute(paramTable, paramName, paramTypeName, storeName, trans);
                        trans.Commit();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static void BatchExecute(DataTable paramTable, string paramName, string paramTypeName, string storeName, SqlTransaction trans)
        {
            try
            {
                SqlParameter param = new SqlParameter(paramName, paramTable);
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = paramTypeName;
                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, storeName, param);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static void BatchExecute(DataTable paramTable, string paramName, string paramTypeName, string storeName, params SqlParameter[] otherParams)
        {
            try
            {
                using (SqlConnection conn = AppSetting.GetConnection())
                {
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        BatchExecute(paramTable, paramName, paramTypeName, storeName, trans, otherParams);
                        trans.Commit();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static void BatchExecute(DataTable paramTable, string paramName, string paramTypeName, string storeName, SqlTransaction trans, params SqlParameter[] otherParams)
        {
            try
            {
                SqlParameter param = new SqlParameter(paramName, paramTable);
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = paramTypeName;
                SqlParameter[] paramAll = new SqlParameter[] { param };
                paramAll = paramAll.Concat(otherParams).ToArray();
                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, storeName, paramAll);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static void BatchExecute<T>(IEnumerable<T> listT, string paramName, string typeName, string storeProcedureName, params string[] exclusionFields)
        {
            if (listT == null || listT.Count() == 0) return;
            try
            {
                using (SqlConnection conn = AppSetting.GetConnection())
                {
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        BatchExecute(trans, listT, paramName, typeName, storeProcedureName, exclusionFields);
                        trans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void BatchExecute<T>(SqlTransaction trans, IEnumerable<T> listT, string paramName, string typeName, string storeProcedureName, params string[] exclusionFields)
        {
            SqlParameter param = CreateStructSqlParameter<T>(paramName, typeName, listT, exclusionFields);
            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, storeProcedureName, param);
        }

        public static SqlParameter CreateStructSqlParameter<T>(string parameterName, string typeName, IEnumerable<T> listObject, params string[] exclusionFields)
        {
            using (DataTable dt = ObjectClass.CreateTable<T>(listObject))
            {
                foreach (string s in exclusionFields)
                {
                    if (dt.Columns.Contains(s))
                    {
                        dt.Columns.Remove(s);
                    }
                }
                SqlParameter param = new SqlParameter(parameterName, dt);
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = typeName;
                return param;
            }
        }

        public static void Execute(string storeName, params SqlParameter[] sqlParams)
        {
            try
            {
                using (SqlConnection conn = AppSetting.GetConnection())
                {
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        Execute(trans, storeName, sqlParams);
                        trans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Execute(SqlTransaction trans, string storeName, params SqlParameter[] sqlParams)
        {
            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, storeName, sqlParams);
        }

        public static object ExecuteScalar(string storeName, params SqlParameter[] sqlParams)
        {
            using (SqlConnection conn = AppSetting.GetConnection())
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    return ExecuteScalar(trans, storeName, sqlParams);
                }
            }
        }
        public static object ExecuteScalar(SqlTransaction trans, string storeName, params SqlParameter[] sqlParams)
        {
            object rs = SqlHelper.ExecuteScalar(trans, CommandType.StoredProcedure, storeName, sqlParams);
            trans.Commit();
            return rs;
        }

        public static DataTable CreatDataTableType<T>(IEnumerable<T> source)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("TypeName", typeof(T));
            foreach (var item in source)
            {
                DataRow nr = tbl.NewRow();
                nr["TypeName"] = item;
                tbl.Rows.Add(nr);
            }
            return tbl;
        }
    }
}
