using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data; 
using System.Configuration;
using System.Text;
using System.Collections.Generic; 
using System.Web;
using log4net;
using Oracle.ManagedDataAccess.Client;

namespace ECP.B2b.DBUtility
{
	/// <summary>
    /// Copyright (C) ECP
	/// 数据访问基础类(基于Oracle)
	/// 可以用户可以修改满足自己项目的需要。
	/// </summary>
    /// 
    public abstract class DbHelperOra 
	{
         
        private static ILog  log = LogManager.GetLogger(typeof(DbHelperOra));   
        public static string connectionString = DbStartConfig.DbOracleConnectionString;
        
		public DbHelperOra()
		{			
		} 
        /// <summary>
        /// 将DataReader转DataTable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static DataTable DataReaderToDataTable(OracleDataReader reader)
        {
            try
            {
                DataTable dataTable = new DataTable();
                //字段数目
                int fieldCount = reader.FieldCount;
                //加表头
                for (int i = 0; i < fieldCount; ++i)
                {
                    dataTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                }
                //加数据
                dataTable.BeginLoadData();
                object[] objValues = new object[fieldCount];
                //开始读
                while (reader.Read())
                {
                    reader.GetValues(objValues);
                    dataTable.LoadDataRow(objValues, true);
                }
                reader.Close();
                dataTable.EndLoadData();
                return dataTable;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }
        #region 公用方法
        
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        public static bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Exists(string strSql, params OracleParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

       
        #endregion

		
		#region  执行简单SQL语句

		/// <summary>
		/// 执行SQL语句，返回影响的记录数
		/// </summary>
		/// <param name="SQLString">SQL语句</param>
		/// <returns>影响的记录数</returns>
		public static int ExecuteSql(string SQLString)
		{ 
            using (OracleConnection connection = new OracleConnection(connectionString))
			{				
				using (OracleCommand cmd = new OracleCommand(SQLString,connection))
				{
					try
					{		
						connection.Open(); 
						int rows=cmd.ExecuteNonQuery();
						return rows;
					}
					catch(OracleException E)
					{					
						connection.Close();
                        log.Error(E.Message);
						throw new Exception(E.Message);
					}
				}				
			}
		}
		
		/// <summary>
		/// 执行多条SQL语句，实现数据库事务。
		/// </summary>
		/// <param name="SQLStringList">多条SQL语句</param>		
		public static int ExecuteSqlTran(ArrayList SQLStringList)
		{ 
            using (OracleConnection conn = new OracleConnection(connectionString))
			{
				conn.Open();
				OracleCommand cmd = new OracleCommand();
				cmd.Connection=conn; 
				OracleTransaction tx=conn.BeginTransaction();			
				cmd.Transaction=tx;				
				try
				{   		
					for(int n=0;n<SQLStringList.Count;n++)
					{
						string strsql=SQLStringList[n].ToString();
						if (strsql.Trim().Length>1)
						{
							cmd.CommandText=strsql;
							cmd.ExecuteNonQuery();
						}
					}										
					tx.Commit();
                    return 1;
				}
				catch(OracleException E)
				{		
					tx.Rollback();
                    log.Error(E.Message);
					throw new Exception(E.Message);
				}
			}
		}
         
		/// <summary>
		/// 执行一条计算查询结果语句，返回查询结果（object）。
		/// </summary>
		/// <param name="SQLString">计算查询结果语句</param>
		/// <returns>查询结果（object）</returns>
		public static object GetSingle(string SQLString)
		{ 
            using (OracleConnection connection = new OracleConnection(connectionString))
			{
				using(OracleCommand cmd = new OracleCommand(SQLString,connection))
				{
					try
					{
						connection.Open(); 
						object obj = cmd.ExecuteScalar();
						if((Object.Equals(obj,null))||(Object.Equals(obj,System.DBNull.Value)))
						{					
							return null;
						}
						else
						{
                            if (obj.GetType() == typeof(string))
                            {
                                return System.Web.HttpUtility.HtmlDecode(obj.ToString());
                            }

							return obj;
						}				
					}
					catch(OracleException e)
					{						
						connection.Close();
                        log.Error(e.Message + "/" + SQLString);
						throw new Exception(e.Message);
					}	
				}
			}
		}
       
		/// <summary>
        /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
		/// </summary>
		/// <param name="strSQL">查询语句</param>
		/// <returns>OracleDataReader</returns>
		public static OracleDataReader ExecuteReader(string strSQL)
		{ 
			OracleConnection connection = new OracleConnection(connectionString);			
			OracleCommand cmd = new OracleCommand(strSQL,connection);				
			try
			{
				connection.Open(); 
                OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				return myReader;
			}
			catch(OracleException e)
			{
                log.Error(e.Message);
				throw new Exception(e.Message);
			}			
			
		}		
		/// <summary>
		/// 执行查询语句，返回DataSet
		/// </summary>
		/// <param name="SQLString">查询语句</param>
		/// <returns>DataSet</returns>
        private static DataSet CheckSqlInto(string SQLString, Func<DataSet> action) 
        {
            string vali_sql = SQLString.ToLower();
            string[] ors = System.Text.RegularExpressions.Regex.Split(vali_sql, " or ");
            for (int i = 1; i < ors.Length; i++)
            {
                if (ors[i].IndexOf("=") >= 0)
                {
                    string[] equals = System.Text.RegularExpressions.Regex.Split(ors[i], "=");
                    string firstVal = equals[0].Trim().Replace(" ", "");
                    string nextVal = equals[1].Trim().Replace(" ", "");
                    if (firstVal.Length <= nextVal.Length)
                    {
                        nextVal = nextVal.Substring(0, firstVal.Length);
                        if (nextVal == firstVal)
                        {
                            //不通过
                            DataSet ds = new DataSet();
                            ds.Tables.Add();
                            return ds;
                        }
                    }
                }
            }
             
            //通过  继续查询
            return action();
        }
        private static void UpdateDataTableStringEncode(DataSet ds) 
        {
            #region  修改DataTable string 列
            List<int> stringCols = new List<int>();
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                if (ds.Tables[0].Columns[i].DataType == typeof(string))
                {
                    //找出字符串列
                    stringCols.Add(i);
                }
            }
            //编辑行
            if (stringCols.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //开始编辑行
                    ds.Tables[0].Rows[i].BeginEdit();

                    //编辑操作单元格
                    foreach (int colIndex in stringCols)
                    {
                        if (ds.Tables[0].Rows[i][colIndex] != DBNull.Value)
                        {
                            //调用字符编码转换方法
                            ds.Tables[0].Rows[i][colIndex] = System.Web.HttpUtility.HtmlDecode(ds.Tables[0].Rows[i][colIndex].ToString());
                        }
                    }

                    //结束编辑行
                    ds.Tables[0].Rows[i].EndEdit();
                }
            }
            #endregion
        }
        public static DataSet Query(string SQLString)
        {
            return CheckSqlInto(SQLString,() =>
            { 
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        connection.Open();
                        OracleDataAdapter command = new OracleDataAdapter(SQLString, connection); 
                        command.Fill(ds, "ds");

                        // 修改DataTable string 列
                        UpdateDataTableStringEncode(ds);
                    }
                    catch (OracleException ex)
                    {
                        log.Error(ex.Message);
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            });
        }


		#endregion

		#region 执行带参数的SQL语句

		/// <summary>
		/// 执行SQL语句，返回影响的记录数
		/// </summary>
		/// <param name="SQLString">SQL语句</param>
		/// <returns>影响的记录数</returns>
		public static int ExecuteSql(string SQLString,params OracleParameter[] cmdParms)
        { 
            using (OracleConnection connection = new OracleConnection(connectionString))
			{				
				using (OracleCommand cmd = new OracleCommand())
				{
					try
					{ 
						PrepareCommand(cmd, connection, null,SQLString, cmdParms);
						int rows=cmd.ExecuteNonQuery();
						cmd.Parameters.Clear();
						return rows;
					}
					catch(OracleException E)
					{
                        log.Error(E.Message);
						throw new Exception(E.Message);
					}
				}				
			}
		}

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn; 
                OracleTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch(Exception ex)
                {
                    log.Error(ex.Message);
                    tx.Rollback();
                    return 0;
                }
            }
        }	
		/// <summary>
		/// 执行多条SQL语句，实现数据库事务。
		/// </summary>
		/// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的OracleParameter[]）</param>
		public static void ExecuteSqlTran(Hashtable SQLStringList)
		{			
			using (OracleConnection conn = new OracleConnection(connectionString))
			{
				conn.Open(); 
				using (OracleTransaction trans = conn.BeginTransaction()) 
				{
					OracleCommand cmd = new OracleCommand();
					try 
					{
						//循环
						foreach (DictionaryEntry myDE in SQLStringList)
						{	
							string 	cmdText=myDE.Key.ToString();
                   
                            OracleParameter[] cmdParms = (OracleParameter[])myDE.Value;
							PrepareCommand(cmd,conn,trans,cmdText, cmdParms);
							int val = cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							
						}
                        trans.Commit();
					}
					catch (Exception ex)
					{
                        log.Error(ex.Message);
						trans.Rollback();
						throw;
					}
				}				
			}
		}

       
		/// <summary>
		/// 执行一条计算查询结果语句，返回查询结果（object）。
		/// </summary>
		/// <param name="SQLString">计算查询结果语句</param>
		/// <returns>查询结果（object）</returns>
		public static object GetSingle(string SQLString,params OracleParameter[] cmdParms)
		{ 
            using (OracleConnection connection = new OracleConnection(connectionString))
			{ 
				using (OracleCommand cmd = new OracleCommand())
				{
					try
					{
						PrepareCommand(cmd, connection, null,SQLString, cmdParms);
						object obj = cmd.ExecuteScalar();
						cmd.Parameters.Clear();
						if((Object.Equals(obj,null))||(Object.Equals(obj,System.DBNull.Value)))
						{					
							return null;
						}
						else
						{
                            if (obj.GetType() == typeof(string))
                            {
                                return System.Web.HttpUtility.HtmlDecode(obj.ToString());
                            }

							return obj;
						}				
					}
					catch(OracleException e)
					{
                        log.Error(SQLString+e.Message);
						throw new Exception(e.Message);
					}					
				}
			}
		}
		
		/// <summary>
        /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
		/// </summary>
		/// <param name="strSQL">查询语句</param>
		/// <returns>OracleDataReader</returns>
		public static OracleDataReader ExecuteReader(string SQLString,params OracleParameter[] cmdParms)
		{ 
            OracleConnection connection = new OracleConnection(connectionString);
			OracleCommand cmd = new OracleCommand();				
			try
			{ 
				PrepareCommand(cmd, connection, null,SQLString, cmdParms);
                OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				cmd.Parameters.Clear();
				return myReader;
			}
			catch(OracleException e)
			{
                log.Error(e.Message);	
				throw new Exception(e.Message);
			}					
			
		}		
		
		/// <summary>
		/// 执行查询语句，返回DataSet
		/// </summary>
		/// <param name="SQLString">查询语句</param>
		/// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params OracleParameter[] cmdParms)
        {

            return CheckSqlInto(SQLString, () =>
            { 
                using (OracleConnection connection = new OracleConnection(connectionString))
                { 
                    OracleCommand cmd = new OracleCommand();
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        try
                        {
                            da.Fill(ds, "ds");

                            // 修改DataTable string 列
                            UpdateDataTableStringEncode(ds);

                            cmd.Parameters.Clear();
                        }
                        catch (OracleException ex)
                        {
                            log.Error(ex.Message);
                            throw new Exception(ex.Message);
                        }
                        return ds;
                    }
                }
            });
        }


		private static void PrepareCommand(OracleCommand cmd,OracleConnection conn,OracleTransaction trans, string cmdText, OracleParameter[] cmdParms) 
		{
            if (conn.State != ConnectionState.Open)
            {
                conn.Open(); 
            }
			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			if (trans != null)
				cmd.Transaction = trans;
			cmd.CommandType = CommandType.Text;//cmdType;
			if (cmdParms != null) 
			{
                foreach (OracleParameter parm in cmdParms)
                {
                    if (parm.Value == null)
                    {
                        parm.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parm);
                }
			}
		}

		#endregion

		#region 存储过程操作

		/// <summary>
        /// 执行存储过程 返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
		/// </summary>
		/// <param name="storedProcName">存储过程名</param>
		/// <param name="parameters">存储过程参数</param>
		/// <returns>OracleDataReader</returns>
		public static OracleDataReader RunProcedure(string storedProcName, IDataParameter[] parameters )
		{ 
            OracleConnection connection = new OracleConnection(connectionString);
            
			OracleDataReader returnReader;
			connection.Open(); 
			OracleCommand command = BuildQueryCommand( connection,storedProcName, parameters );
			command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);				
			return returnReader;			
		}
		
		
		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="storedProcName">存储过程名</param>
		/// <param name="parameters">存储过程参数</param>
		/// <param name="tableName">DataSet结果中的表名</param>
		/// <returns>DataSet</returns>
		public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName )
		{ 
            using (OracleConnection connection = new OracleConnection(connectionString))
			{
				DataSet dataSet = new DataSet();
				connection.Open(); 
				OracleDataAdapter sqlDA = new OracleDataAdapter();
				sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters );
				sqlDA.Fill( dataSet, tableName );
				connection.Close();
				return dataSet;
			}
		}

		
		/// <summary>
		/// 构建 OracleCommand 对象(用来返回一个结果集，而不是一个整数值)
		/// </summary>
		/// <param name="connection">数据库连接</param>
		/// <param name="storedProcName">存储过程名</param>
		/// <param name="parameters">存储过程参数</param>
		/// <returns>OracleCommand</returns>
		private static OracleCommand BuildQueryCommand(OracleConnection connection,string storedProcName, IDataParameter[] parameters)
		{			
			OracleCommand command = new OracleCommand( storedProcName, connection );
			command.CommandType = CommandType.StoredProcedure;
			foreach (OracleParameter parameter in parameters)
			{
				command.Parameters.Add( parameter );
			}
			return command;			
		}
		
		/// <summary>
		/// 执行存储过程，返回影响的行数		
		/// </summary>
		/// <param name="storedProcName">存储过程名</param>
		/// <param name="parameters">存储过程参数</param>
		/// <param name="rowsAffected">影响的行数</param>
		/// <returns></returns>
		public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected )
		{
         
			using (OracleConnection connection = new OracleConnection(connectionString))
			{ 
				int result;
				connection.Open(); 
				OracleCommand command = BuildIntCommand(connection,storedProcName, parameters );
				rowsAffected = command.ExecuteNonQuery();
				result = (int)command.Parameters["ReturnValue"].Value;
				//Connection.Close();
				return result;
			}
		}

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure2(string storedProcName, IDataParameter[] parameters)
        {
            //string pri_con = SetUserConn();
            using (OracleConnection connection = new OracleConnection(connectionString))
            { 
                int result;
                connection.Open(); 
                OracleCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                try
                {
                    result = command.ExecuteNonQuery();
                }
    			catch(OracleException E)
				{
                    log.Error(E.Message);
					throw new Exception(E.Message);
				}
                connection.Close();
                return result;
            }
        }
		/// <summary>
		/// 创建 OracleCommand 对象实例(用来返回一个整数值)	
		/// </summary>
		/// <param name="storedProcName">存储过程名</param>
		/// <param name="parameters">存储过程参数</param>
		/// <returns>OracleCommand 对象实例</returns>
		private static OracleCommand BuildIntCommand(OracleConnection connection,string storedProcName, IDataParameter[] parameters)
		{
			OracleCommand command = BuildQueryCommand(connection,storedProcName, parameters );
			command.Parameters.Add( new OracleParameter ( "ReturnValue",
                OracleDbType.Int32, 4, ParameterDirection.ReturnValue,
				false,0,0,string.Empty,DataRowVersion.Default,null ));
			return command;
		}
		#endregion	



       
        #region 数据分页
        /// <summary>
        /// 摘要:
        ///     数据分页接口
        /// 参数：
        ///     sql：传入要执行sql语句
        ///     param：参数化
        ///     orderField：排序字段
        ///     orderType：排序类型
        ///     pageIndex：当前页
        ///     pageSize：页大小
        ///     count：返回查询条数
        /// </summary>
        public static DataSet GetPageList(string sql, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                int num = ((pageIndex - 1) * pageSize)+1;
                int num1 = (pageIndex) * pageSize;
                sb.Append("SELECT * FROM (SELECT ROWNUM rwid , a.*");
                sb.Append(" FROM (" + sql + " ORDER BY " + orderField + " " + orderType + " " + ")  a )  aa ");
                sb.AppendFormat(" WHERE aa.rwid BETWEEN {0} AND {1} ORDER BY rwid", num, num1);

                count = Convert.ToInt32(GetSingle("SELECT Count(1) FROM (" + sql + ")  T"));
                return Query(sb.ToString());



            }
            catch (Exception e)
            {
                log.Error("GetPageList(数据分页)" + e.Message);
                throw e;
               
            }
        }  
        #endregion


        #region 存储过程查询分页
        /// <summary>
        /// 调用存储过程仅取总数
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int ExecuteProcedureGetCount(string connStr, string proceName, ref int result, params OracleParameter[] values)
        { 
            using (OracleConnection conn = new OracleConnection(connectionString))
            { 
                try
                {
                    conn.Open(); 
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    //设置存储过程名称
                    cmd.CommandText = proceName;
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter[] allParam = new OracleParameter[(values.Length + 4)];
                    for (int i = 0; i < values.Length; i++)
                    {
                        allParam[i] = values[i];
                    }

                    allParam[values.Length] = new OracleParameter("p_cur", OracleDbType.RefCursor);
                    allParam[values.Length + 1] = new OracleParameter("p_count", OracleDbType.Int32);
                    allParam[values.Length + 2] = new OracleParameter("p_result", OracleDbType.Int32);
                    allParam[values.Length + 3] = new OracleParameter("p_err", OracleDbType.NVarchar2, 2000);

                    allParam[values.Length].Direction = ParameterDirection.Output;
                    allParam[values.Length + 1].Direction = ParameterDirection.Output;
                    allParam[values.Length + 2].Direction = ParameterDirection.Output;
                    allParam[values.Length + 3].Direction = ParameterDirection.Output;
                    foreach (OracleParameter p in allParam)
                    {
                        cmd.Parameters.Add(p);
                    }
                    cmd.Parameters.Add(new OracleParameter("p_Qtype", 1));
                    //执行存储过程
                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(allParam[values.Length + 2].Value);
                    int rowCount = Convert.ToInt32(allParam[values.Length + 1].Value == DBNull.Value ? "0" : allParam[values.Length + 1].Value.ToString());
                    cmd.Parameters.Clear();
                    return rowCount;
                }
                catch (Exception ex)
                {
                    log.Error("ExecuteProcedureGetCount(有参数):" + ex.Message);
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public static DataTable ExecuteProcedureDataTable(string connStr, string proceName, ref int result, params OracleParameter[] values)
        { 
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    
                    conn.Open(); 
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    //设置存储过程名称
                    cmd.CommandText = proceName;
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] allParam = new OracleParameter[(values.Length + 4)];
                    for (int i = 0; i < values.Length; i++)
                    {
                        allParam[i] = values[i];
                    }


                    allParam[values.Length] = new OracleParameter("p_cur", OracleDbType.RefCursor);
                    allParam[values.Length + 1] = new OracleParameter("p_count", OracleDbType.Int32);
                    allParam[values.Length + 2] = new OracleParameter("p_result", OracleDbType.Int32);
                    allParam[values.Length + 3] = new OracleParameter("p_err", OracleDbType.NVarchar2, 2000);


                    allParam[values.Length].Direction = ParameterDirection.Output;
                    allParam[values.Length + 1].Direction = ParameterDirection.Output;
                    allParam[values.Length + 2].Direction = ParameterDirection.Output;
                    allParam[values.Length + 3].Direction = ParameterDirection.Output;

                    foreach (OracleParameter p in allParam)
                    {
                        cmd.Parameters.Add(p);
                    }
                    // cmd.Parameters.Add(new OracleParameter("p_Qtype", 0));
                    //执行存储过程
                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(allParam[values.Length + 2].Value);
                    OracleDataReader reader = allParam[values.Length].Value as OracleDataReader;
                    //转DataTable
                    DataTable data = DataReaderToDataTable(reader);
                    cmd.Parameters.Clear();
                    return data;
                }
                catch (Exception ex)
                {
                    log.Error("ExecuteProcedureDataTable(有参数):" + ex.Message);
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static DataTable GetProcedureData(string connStr , string proceName, int pageNo, int ipageSize, ref int RowCount, params OracleParameter[] values)
        {
            int iresult = -1;
            OracleParameter[] allParam = new OracleParameter[(values.Length + 2)];
            for (int i = 0; i < values.Length; i++)
            {
                allParam[i] = values[i];
            }
            pageNo = pageNo < 1 ? 1 : pageNo;
            allParam[values.Length] = new OracleParameter("p_index", pageNo);
            allParam[values.Length + 1] = new OracleParameter("p_size", ipageSize);
            RowCount = ExecuteProcedureGetCount(connStr, proceName, ref iresult, allParam);
            allParam[values.Length].Value = pageNo;
             
            DataTable data = ExecuteProcedureDataTable(connectionString, proceName, ref iresult, allParam);
           
            //返回
            return data;
        }
        #endregion
    }
}
