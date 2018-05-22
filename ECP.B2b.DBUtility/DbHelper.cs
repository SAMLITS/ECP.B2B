using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace ECP.B2b.DBUtility
{
    /// DbHelper通用数据库类  事务处理使用的类与Trans一起使用，在就用层处理数据库的事务
    ///
    public class DbHelper
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DbHelperOra)); 
        private static string dbConnectionString = DbStartConfig.DbOracleConnectionString; 



        private DbConnection connection;
        public DbHelper()
        {
            this.connection = CreateConnection(DbHelper.dbConnectionString);
        }
        public DbHelper(string connectionString)
        {
            this.connection = CreateConnection(connectionString);
        }
        public static DbConnection CreateConnection()
        { 
            DbConnection dbconn = new OracleConnection(DbHelper.dbConnectionString); 
            return dbconn;
        }
        public static DbConnection CreateConnection(string connectionString)
        {
            DbConnection dbconn = new OracleConnection(connectionString);
            return dbconn;
        }

        ///

        /// 执行存储过程
        ///
        ///存储过程名
        ///
        public DbCommand GetStoredProcCommand(string storedProcedure)
        {
            try
            {
                DbCommand dbCommand = connection.CreateCommand();
                dbCommand.CommandText = storedProcedure;
                dbCommand.CommandType = CommandType.StoredProcedure;
                return dbCommand;
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
            
        }



        ///

        /// 执行SQL语句
        ///
        ///SQL语句
        ///
        public DbCommand GetSqlStringCommand(string sqlQuery)
        {
             try
            {
                DbCommand dbCommand = connection.CreateCommand();
                dbCommand.CommandText = sqlQuery;
                dbCommand.CommandType = CommandType.Text;
                return dbCommand;
            }
             catch (System.Exception ex)
             {
                 log.Error(ex.Message);
                 throw ex;
             }
        }



        #region 增加参数

        public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }

        ///

        /// 增加输出参数
        ///
        ///
        ///
        ///
        ///
        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }



      
        ///

        /// 增加输入参数
        ///
        ///
        ///
        ///
        ///
        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }



        ///

        /// 增加返回参数
        ///
        ///
        ///
        ///
        public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(dbParameter);
        }



        public DbParameter GetParameter(DbCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }

        #endregion

        #region 执行

        ///

        /// 执行查询返回DataSet
        ///
        ///
        ///
        public DataSet ExecuteDataSet(DbCommand cmd)
        {
            
            DbProviderFactory dbfactory = OracleClientFactory.Instance;
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            try{
              dbDataAdapter.Fill(ds);
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
            return ds;
        }



        ///

        /// 执行查询返回DataTable
        ///
        ///
        ///
        public DataTable ExecuteDataTable(DbCommand cmd)
        {
            DbProviderFactory dbfactory = OracleClientFactory.Instance;
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }



        ///

        /// 执行查询返回DataReader
        ///
        ///
        ///
        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            cmd.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }



        ///

        /// 执行SQL语句，返回影响行数
        ///
        ///
        ///
        public int ExecuteNonQuery(DbCommand cmd)
        {
            try{
            cmd.Connection.Open();
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }



        ///

        /// 返回首行首列对象
        ///
        ///
        ///
        public object ExecuteScalar(DbCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                object ret = cmd.ExecuteScalar();
                cmd.Connection.Close();
                return ret;
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }
        #endregion



        #region 执行事务

        ///

        /// 执行事务返回DataSet
        ///
        ///
        ///
        ///
        public DataSet ExecuteDataSet(DbCommand cmd, Trans t)
        {
            try
            {
                cmd.Connection = t.DbConnection;
                cmd.Transaction = t.DbTrans;
                DbProviderFactory dbfactory = OracleClientFactory.Instance;
                DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                dbDataAdapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dbDataAdapter.Fill(ds);
                return ds;
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }



        ///

        /// 执行事务返回DataTable
        ///
        ///
        ///
        ///
        public DataTable ExecuteDataTable(DbCommand cmd, Trans t)
        {
            try
            {
                cmd.Connection = t.DbConnection;
                cmd.Transaction = t.DbTrans;
                DbProviderFactory dbfactory = OracleClientFactory.Instance;
                DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                dbDataAdapter.SelectCommand = cmd;
                DataTable dataTable = new DataTable();
                dbDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }



        ///

        /// 执行事务返回DataReader
        ///
        ///
        ///
        ///
        public DbDataReader ExecuteReader(DbCommand cmd, Trans t)
        {
            try{
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataReader reader = cmd.ExecuteReader();
            return reader;
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }



        ///

        /// 执行事务SQL语句返回影响行数
        ///
        ///
        ///
        ///
        public int ExecuteNonQuery(DbCommand cmd, Trans t)
        {
            try
            {
                cmd.Connection.Close();
                cmd.Connection = t.DbConnection;
                cmd.Transaction = t.DbTrans;
                int ret = cmd.ExecuteNonQuery();
                return ret;
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }



        ///

        /// 执行事务SQL语句返回首行首列
        ///
        ///
        ///
        ///
        public object ExecuteScalar(DbCommand cmd, Trans t)
        {
            try
            {
                cmd.Connection.Close();
                cmd.Connection = t.DbConnection;
                cmd.Transaction = t.DbTrans;
                object ret = cmd.ExecuteScalar();
                return ret;
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }
        }
        #endregion
    } 
}
