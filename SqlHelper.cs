namespace client
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Xml;

    public sealed class SqlHelper
    {
        public static string ConStr;

        static SqlHelper()
        {
            old_acctor_mc();
        }

        private SqlHelper()
        {
        }

        private static void AssignParameterValues(SqlParameter[] sqlParameter_0, DataRow dataRow_0)
        {
            if ((sqlParameter_0 != null) && (dataRow_0 != null))
            {
                int num = 0;
                foreach (SqlParameter parameter in sqlParameter_0)
                {
                    if ((parameter.ParameterName == null) || (parameter.ParameterName.Length <= 1))
                    {
                        throw new Exception(string.Format("请提供参数{0}一个有效的名称{1}.", num, parameter.ParameterName));
                    }
                    if (dataRow_0.Table.Columns.IndexOf(parameter.ParameterName.Substring(1)) != -1)
                    {
                        parameter.Value = dataRow_0[parameter.ParameterName.Substring(1)];
                    }
                    num++;
                }
            }
        }

        private static void AssignParameterValues(SqlParameter[] sqlParameter_0, object[] object_0)
        {
            if ((sqlParameter_0 != null) && (object_0 != null))
            {
                if (sqlParameter_0.Length != object_0.Length)
                {
                    throw new ArgumentException("参数值个数与参数不匹配.");
                }
                int index = 0;
                int length = sqlParameter_0.Length;
                while (index < length)
                {
                    if (object_0[index] is IDbDataParameter)
                    {
                        IDbDataParameter parameter = (IDbDataParameter) object_0[index];
                        if (parameter.Value == null)
                        {
                            sqlParameter_0[index].Value = DBNull.Value;
                        }
                        else
                        {
                            sqlParameter_0[index].Value = parameter.Value;
                        }
                    }
                    else if (object_0[index] == null)
                    {
                        sqlParameter_0[index].Value = DBNull.Value;
                    }
                    else
                    {
                        sqlParameter_0[index].Value = object_0[index];
                    }
                    index++;
                }
            }
        }

        private static void AttachParameters(SqlCommand sqlCommand_0, SqlParameter[] sqlParameter_0)
        {
            if (sqlCommand_0 == null)
            {
                throw new ArgumentNullException("command");
            }
            if (sqlParameter_0 != null)
            {
                foreach (SqlParameter parameter in sqlParameter_0)
                {
                    if (parameter != null)
                    {
                        if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        sqlCommand_0.Parameters.Add(parameter);
                    }
                }
            }
        }

        public static SqlCommand CreateCommand(SqlConnection sqlConnection_0, string string_0, params string[] string_1)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            SqlCommand command = new SqlCommand(string_0, sqlConnection_0);
            command.CommandType = CommandType.StoredProcedure;
            if ((string_1 != null) && (string_1.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnection_0, string_0);
                for (int i = 0; i < string_1.Length; i++)
                {
                    spParameterSet[i].SourceColumn = string_1[i];
                }
                AttachParameters(command, spParameterSet);
            }
            return command;
        }

        public static DataSet ExecuteDataset(SqlConnection sqlConnection_0, CommandType commandType_0, string string_0)
        {
            return ExecuteDataset(sqlConnection_0, commandType_0, string_0, null);
        }

        public static DataSet ExecuteDataset(SqlConnection sqlConnection_0, string string_0, params object[] object_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteDataset(sqlConnection_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnection_0, string_0);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteDataset(sqlConnection_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static DataSet ExecuteDataset(SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0)
        {
            return ExecuteDataset(sqlTransaction_0, commandType_0, string_0, null);
        }

        public static DataSet ExecuteDataset(SqlTransaction sqlTransaction_0, string string_0, params object[] object_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteDataset(sqlTransaction_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlTransaction_0.Connection, string_0);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteDataset(sqlTransaction_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static DataSet ExecuteDataset(string string_0, CommandType commandType_0, string string_1)
        {
            return ExecuteDataset(string_0, commandType_0, string_1, null);
        }

        public static DataSet ExecuteDataset(string string_0, string string_1, params object[] object_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((string_1 == null) || (string_1.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteDataset(string_0, CommandType.StoredProcedure, string_1);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(string_0, string_1);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteDataset(string_0, CommandType.StoredProcedure, string_1, spParameterSet);
        }

        public static DataSet ExecuteDataset(SqlConnection sqlConnection_0, CommandType commandType_0, string string_0, params SqlParameter[] sqlParameter_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlCommand command = new SqlCommand();
            bool flag = false;
            PrepareCommand(command, sqlConnection_0, null, commandType_0, string_0, sqlParameter_0, out flag);
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                command.Parameters.Clear();
                if (flag)
                {
                    sqlConnection_0.Close();
                }
                return dataSet;
            }
        }

        public static DataSet ExecuteDataset(SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0, params SqlParameter[] sqlParameter_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand command = new SqlCommand();
            bool flag = false;
            PrepareCommand(command, sqlTransaction_0.Connection, sqlTransaction_0, commandType_0, string_0, sqlParameter_0, out flag);
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                command.Parameters.Clear();
                return dataSet;
            }
        }

        public static DataSet ExecuteDataset(string string_0, CommandType commandType_0, string string_1, params SqlParameter[] sqlParameter_0)
        {
            if ((string_0 != null) && (string_0.Length != 0))
            {
                using (SqlConnection connection = new SqlConnection(string_0))
                {
                    connection.Open();
                    return ExecuteDataset(connection, commandType_0, string_1, sqlParameter_0);
                }
            }
            throw new ArgumentNullException("connectionString");
        }

        public static DataSet ExecuteDatasetTypedParams(SqlConnection sqlConnection_0, string string_0, DataRow dataRow_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteDataset(sqlConnection_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnection_0, string_0);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteDataset(sqlConnection_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static DataSet ExecuteDatasetTypedParams(SqlTransaction sqlTransaction_0, string string_0, DataRow dataRow_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteDataset(sqlTransaction_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlTransaction_0.Connection, string_0);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteDataset(sqlTransaction_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static DataSet ExecuteDatasetTypedParams(string string_0, string string_1, DataRow dataRow_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((string_1 == null) || (string_1.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteDataset(string_0, CommandType.StoredProcedure, string_1);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(string_0, string_1);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteDataset(string_0, CommandType.StoredProcedure, string_1, spParameterSet);
        }

        public static int ExecuteNonQuery(SqlConnection sqlConnection_0, CommandType commandType_0, string string_0)
        {
            return ExecuteNonQuery(sqlConnection_0, commandType_0, string_0, null);
        }

        public static int ExecuteNonQuery(SqlConnection sqlConnection_0, string string_0, params object[] object_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteNonQuery(sqlConnection_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnection_0, string_0);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteNonQuery(sqlConnection_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static int ExecuteNonQuery(SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0)
        {
            return ExecuteNonQuery(sqlTransaction_0, commandType_0, string_0, null);
        }

        public static int ExecuteNonQuery(SqlTransaction sqlTransaction_0, string string_0, params object[] object_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteNonQuery(sqlTransaction_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlTransaction_0.Connection, string_0);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteNonQuery(sqlTransaction_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static int ExecuteNonQuery(string string_0, CommandType commandType_0, string string_1)
        {
            return ExecuteNonQuery(string_0, commandType_0, string_1, null);
        }

        public static int ExecuteNonQuery(string string_0, string string_1, params object[] object_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((string_1 == null) || (string_1.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteNonQuery(string_0, CommandType.StoredProcedure, string_1);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(string_0, string_1);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteNonQuery(string_0, CommandType.StoredProcedure, string_1, spParameterSet);
        }

        public static int ExecuteNonQuery(SqlConnection sqlConnection_0, CommandType commandType_0, string string_0, params SqlParameter[] sqlParameter_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlCommand command = new SqlCommand();
            bool flag = false;
            PrepareCommand(command, sqlConnection_0, null, commandType_0, string_0, sqlParameter_0, out flag);
            int num = command.ExecuteNonQuery();
            command.Parameters.Clear();
            if (flag)
            {
                sqlConnection_0.Close();
            }
            return num;
        }

        public static int ExecuteNonQuery(SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0, params SqlParameter[] sqlParameter_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand command = new SqlCommand();
            bool flag = false;
            PrepareCommand(command, sqlTransaction_0.Connection, sqlTransaction_0, commandType_0, string_0, sqlParameter_0, out flag);
            int num = command.ExecuteNonQuery();
            command.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(string string_0, CommandType commandType_0, string string_1, params SqlParameter[] sqlParameter_0)
        {
            if ((string_0 != null) && (string_0.Length != 0))
            {
                using (SqlConnection connection = new SqlConnection(string_0))
                {
                    connection.Open();
                    return ExecuteNonQuery(connection, commandType_0, string_1, sqlParameter_0);
                }
            }
            throw new ArgumentNullException("connectionString");
        }

        public static int ExecuteNonQueryTypedParams(SqlConnection sqlConnection_0, string string_0, DataRow dataRow_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteNonQuery(sqlConnection_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnection_0, string_0);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteNonQuery(sqlConnection_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static int ExecuteNonQueryTypedParams(SqlTransaction sqlTransaction_0, string string_0, DataRow dataRow_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteNonQuery(sqlTransaction_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlTransaction_0.Connection, string_0);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteNonQuery(sqlTransaction_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static int ExecuteNonQueryTypedParams(string string_0, string string_1, DataRow dataRow_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((string_1 == null) || (string_1.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteNonQuery(string_0, CommandType.StoredProcedure, string_1);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(string_0, string_1);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteNonQuery(string_0, CommandType.StoredProcedure, string_1, spParameterSet);
        }

        public static SqlDataReader ExecuteReader(SqlConnection sqlConnection_0, CommandType commandType_0, string string_0)
        {
            return ExecuteReader(sqlConnection_0, commandType_0, string_0, null);
        }

        public static SqlDataReader ExecuteReader(SqlConnection sqlConnection_0, string string_0, params object[] object_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteReader(sqlConnection_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnection_0, string_0);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteReader(sqlConnection_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static SqlDataReader ExecuteReader(SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0)
        {
            return ExecuteReader(sqlTransaction_0, commandType_0, string_0, null);
        }

        public static SqlDataReader ExecuteReader(SqlTransaction sqlTransaction_0, string string_0, params object[] object_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteReader(sqlTransaction_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlTransaction_0.Connection, string_0);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteReader(sqlTransaction_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static SqlDataReader ExecuteReader(string string_0, CommandType commandType_0, string string_1)
        {
            return ExecuteReader(string_0, commandType_0, string_1, null);
        }

        public static SqlDataReader ExecuteReader(string string_0, string string_1, params object[] object_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((string_1 == null) || (string_1.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteReader(string_0, CommandType.StoredProcedure, string_1);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(string_0, string_1);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteReader(string_0, CommandType.StoredProcedure, string_1, spParameterSet);
        }

        public static SqlDataReader ExecuteReader(SqlConnection sqlConnection_0, CommandType commandType_0, string string_0, params SqlParameter[] sqlParameter_0)
        {
            return ExecuteReader(sqlConnection_0, null, commandType_0, string_0, sqlParameter_0, Enum0.External);
        }

        public static SqlDataReader ExecuteReader(SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0, params SqlParameter[] sqlParameter_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            return ExecuteReader(sqlTransaction_0.Connection, sqlTransaction_0, commandType_0, string_0, sqlParameter_0, Enum0.External);
        }

        public static SqlDataReader ExecuteReader(string string_0, CommandType commandType_0, string string_1, params SqlParameter[] sqlParameter_0)
        {
            if ((string_0 != null) && (string_0.Length != 0))
            {
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection(string_0);
                    connection.Open();
                    return ExecuteReader(connection, null, commandType_0, string_1, sqlParameter_0, Enum0.Internal);
                }
                catch
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    throw;
                }
            }
            throw new ArgumentNullException("connectionString");
        }

        private static SqlDataReader ExecuteReader(SqlConnection sqlConnection_0, SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0, SqlParameter[] sqlParameter_0, Enum0 enum0_0)
        {
            SqlDataReader reader2;
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            bool flag = false;
            SqlCommand command = new SqlCommand();
            try
            {
                SqlDataReader reader;
                PrepareCommand(command, sqlConnection_0, sqlTransaction_0, commandType_0, string_0, sqlParameter_0, out flag);
                if (enum0_0 == Enum0.External)
                {
                    reader = command.ExecuteReader();
                }
                else
                {
                    reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                bool flag2 = true;
                foreach (SqlParameter parameter in command.Parameters)
                {
                    if (parameter.Direction != ParameterDirection.Input)
                    {
                        flag2 = false;
                    }
                }
                if (flag2)
                {
                    command.Parameters.Clear();
                }
                reader2 = reader;
            }
            catch
            {
                if (flag)
                {
                    sqlConnection_0.Close();
                }
                throw;
            }
            return reader2;
        }

        public static SqlDataReader ExecuteReaderTypedParams(SqlConnection sqlConnection_0, string string_0, DataRow dataRow_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteReader(sqlConnection_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnection_0, string_0);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteReader(sqlConnection_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static SqlDataReader ExecuteReaderTypedParams(SqlTransaction sqlTransaction_0, string string_0, DataRow dataRow_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteReader(sqlTransaction_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlTransaction_0.Connection, string_0);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteReader(sqlTransaction_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static SqlDataReader ExecuteReaderTypedParams(string string_0, string string_1, DataRow dataRow_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((string_1 == null) || (string_1.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteReader(string_0, CommandType.StoredProcedure, string_1);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(string_0, string_1);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteReader(string_0, CommandType.StoredProcedure, string_1, spParameterSet);
        }

        public static object ExecuteScalar(SqlConnection sqlConnection_0, CommandType commandType_0, string string_0)
        {
            return ExecuteScalar(sqlConnection_0, commandType_0, string_0, null);
        }

        public static object ExecuteScalar(SqlConnection sqlConnection_0, string string_0, params object[] object_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteScalar(sqlConnection_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnection_0, string_0);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteScalar(sqlConnection_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static object ExecuteScalar(SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0)
        {
            return ExecuteScalar(sqlTransaction_0, commandType_0, string_0, null);
        }

        public static object ExecuteScalar(SqlTransaction sqlTransaction_0, string string_0, params object[] object_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteScalar(sqlTransaction_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlTransaction_0.Connection, string_0);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteScalar(sqlTransaction_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static object ExecuteScalar(string string_0, CommandType commandType_0, string string_1)
        {
            return ExecuteScalar(string_0, commandType_0, string_1, null);
        }

        public static object ExecuteScalar(string string_0, string string_1, params object[] object_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((string_1 == null) || (string_1.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteScalar(string_0, CommandType.StoredProcedure, string_1);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(string_0, string_1);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteScalar(string_0, CommandType.StoredProcedure, string_1, spParameterSet);
        }

        public static object ExecuteScalar(SqlConnection sqlConnection_0, CommandType commandType_0, string string_0, params SqlParameter[] sqlParameter_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlCommand command = new SqlCommand();
            bool flag = false;
            PrepareCommand(command, sqlConnection_0, null, commandType_0, string_0, sqlParameter_0, out flag);
            object obj2 = command.ExecuteScalar();
            command.Parameters.Clear();
            if (flag)
            {
                sqlConnection_0.Close();
            }
            return obj2;
        }

        public static object ExecuteScalar(SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0, params SqlParameter[] sqlParameter_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand command = new SqlCommand();
            bool flag = false;
            PrepareCommand(command, sqlTransaction_0.Connection, sqlTransaction_0, commandType_0, string_0, sqlParameter_0, out flag);
            object obj2 = command.ExecuteScalar();
            command.Parameters.Clear();
            return obj2;
        }

        public static object ExecuteScalar(string string_0, CommandType commandType_0, string string_1, params SqlParameter[] sqlParameter_0)
        {
            if ((string_0 != null) && (string_0.Length != 0))
            {
                using (SqlConnection connection = new SqlConnection(string_0))
                {
                    connection.Open();
                    return ExecuteScalar(connection, commandType_0, string_1, sqlParameter_0);
                }
            }
            throw new ArgumentNullException("connectionString");
        }

        public static object ExecuteScalarTypedParams(SqlConnection sqlConnection_0, string string_0, DataRow dataRow_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteScalar(sqlConnection_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnection_0, string_0);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteScalar(sqlConnection_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static object ExecuteScalarTypedParams(SqlTransaction sqlTransaction_0, string string_0, DataRow dataRow_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteScalar(sqlTransaction_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlTransaction_0.Connection, string_0);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteScalar(sqlTransaction_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static object ExecuteScalarTypedParams(string string_0, string string_1, DataRow dataRow_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((string_1 == null) || (string_1.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteScalar(string_0, CommandType.StoredProcedure, string_1);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(string_0, string_1);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteScalar(string_0, CommandType.StoredProcedure, string_1, spParameterSet);
        }

        public static XmlReader ExecuteXmlReader(SqlConnection sqlConnection_0, CommandType commandType_0, string string_0)
        {
            return ExecuteXmlReader(sqlConnection_0, commandType_0, string_0, null);
        }

        public static XmlReader ExecuteXmlReader(SqlConnection sqlConnection_0, string string_0, params object[] object_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteXmlReader(sqlConnection_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnection_0, string_0);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteXmlReader(sqlConnection_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static XmlReader ExecuteXmlReader(SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0)
        {
            return ExecuteXmlReader(sqlTransaction_0, commandType_0, string_0, null);
        }

        public static XmlReader ExecuteXmlReader(SqlTransaction sqlTransaction_0, string string_0, params object[] object_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 == null) || (object_0.Length <= 0))
            {
                return ExecuteXmlReader(sqlTransaction_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlTransaction_0.Connection, string_0);
            AssignParameterValues(spParameterSet, object_0);
            return ExecuteXmlReader(sqlTransaction_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static XmlReader ExecuteXmlReader(SqlConnection sqlConnection_0, CommandType commandType_0, string string_0, params SqlParameter[] sqlParameter_0)
        {
            XmlReader reader2;
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            bool flag = false;
            SqlCommand command = new SqlCommand();
            try
            {
                PrepareCommand(command, sqlConnection_0, null, commandType_0, string_0, sqlParameter_0, out flag);
                XmlReader reader = command.ExecuteXmlReader();
                command.Parameters.Clear();
                reader2 = reader;
            }
            catch
            {
                if (flag)
                {
                    sqlConnection_0.Close();
                }
                throw;
            }
            return reader2;
        }

        public static XmlReader ExecuteXmlReader(SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0, params SqlParameter[] sqlParameter_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand command = new SqlCommand();
            bool flag = false;
            PrepareCommand(command, sqlTransaction_0.Connection, sqlTransaction_0, commandType_0, string_0, sqlParameter_0, out flag);
            XmlReader reader = command.ExecuteXmlReader();
            command.Parameters.Clear();
            return reader;
        }

        public static XmlReader ExecuteXmlReaderTypedParams(SqlConnection sqlConnection_0, string string_0, DataRow dataRow_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteXmlReader(sqlConnection_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnection_0, string_0);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteXmlReader(sqlConnection_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static XmlReader ExecuteXmlReaderTypedParams(SqlTransaction sqlTransaction_0, string string_0, DataRow dataRow_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow_0 == null) || (dataRow_0.ItemArray.Length <= 0))
            {
                return ExecuteXmlReader(sqlTransaction_0, CommandType.StoredProcedure, string_0);
            }
            SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlTransaction_0.Connection, string_0);
            AssignParameterValues(spParameterSet, dataRow_0);
            return ExecuteXmlReader(sqlTransaction_0, CommandType.StoredProcedure, string_0, spParameterSet);
        }

        public static void FillDataset(SqlConnection sqlConnection_0, CommandType commandType_0, string string_0, DataSet dataSet_0, string[] string_1)
        {
            FillDataset(sqlConnection_0, commandType_0, string_0, dataSet_0, string_1, null);
        }

        public static void FillDataset(SqlConnection sqlConnection_0, string string_0, DataSet dataSet_0, string[] string_1, params object[] object_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (dataSet_0 == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 != null) && (object_0.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlConnection_0, string_0);
                AssignParameterValues(spParameterSet, object_0);
                FillDataset(sqlConnection_0, CommandType.StoredProcedure, string_0, dataSet_0, string_1, spParameterSet);
            }
            else
            {
                FillDataset(sqlConnection_0, CommandType.StoredProcedure, string_0, dataSet_0, string_1);
            }
        }

        public static void FillDataset(SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0, DataSet dataSet_0, string[] string_1)
        {
            FillDataset(sqlTransaction_0, commandType_0, string_0, dataSet_0, string_1, null);
        }

        public static void FillDataset(SqlTransaction sqlTransaction_0, string string_0, DataSet dataSet_0, string[] string_1, params object[] object_0)
        {
            if (sqlTransaction_0 == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((sqlTransaction_0 != null) && (sqlTransaction_0.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (dataSet_0 == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((object_0 != null) && (object_0.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(sqlTransaction_0.Connection, string_0);
                AssignParameterValues(spParameterSet, object_0);
                FillDataset(sqlTransaction_0, CommandType.StoredProcedure, string_0, dataSet_0, string_1, spParameterSet);
            }
            else
            {
                FillDataset(sqlTransaction_0, CommandType.StoredProcedure, string_0, dataSet_0, string_1);
            }
        }

        public static void FillDataset(string string_0, CommandType commandType_0, string string_1, DataSet dataSet_0, string[] string_2)
        {
            if ((string_0 != null) && (string_0.Length != 0))
            {
                if (dataSet_0 == null)
                {
                    throw new ArgumentNullException("dataSet");
                }
                using (SqlConnection connection = new SqlConnection(string_0))
                {
                    connection.Open();
                    FillDataset(connection, commandType_0, string_1, dataSet_0, string_2);
                    return;
                }
            }
            throw new ArgumentNullException("connectionString");
        }

        public static void FillDataset(string string_0, string string_1, DataSet dataSet_0, string[] string_2, params object[] object_0)
        {
            if ((string_0 != null) && (string_0.Length != 0))
            {
                if (dataSet_0 == null)
                {
                    throw new ArgumentNullException("dataSet");
                }
                using (SqlConnection connection = new SqlConnection(string_0))
                {
                    connection.Open();
                    FillDataset(connection, string_1, dataSet_0, string_2, object_0);
                    return;
                }
            }
            throw new ArgumentNullException("connectionString");
        }

        public static void FillDataset(SqlConnection sqlConnection_0, CommandType commandType_0, string string_0, DataSet dataSet_0, string[] string_1, params SqlParameter[] sqlParameter_0)
        {
            FillDataset(sqlConnection_0, null, commandType_0, string_0, dataSet_0, string_1, sqlParameter_0);
        }

        public static void FillDataset(SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0, DataSet dataSet_0, string[] string_1, params SqlParameter[] sqlParameter_0)
        {
            FillDataset(sqlTransaction_0.Connection, sqlTransaction_0, commandType_0, string_0, dataSet_0, string_1, sqlParameter_0);
        }

        public static void FillDataset(string string_0, CommandType commandType_0, string string_1, DataSet dataSet_0, string[] string_2, params SqlParameter[] sqlParameter_0)
        {
            if ((string_0 != null) && (string_0.Length != 0))
            {
                if (dataSet_0 == null)
                {
                    throw new ArgumentNullException("dataSet");
                }
                using (SqlConnection connection = new SqlConnection(string_0))
                {
                    connection.Open();
                    FillDataset(connection, commandType_0, string_1, dataSet_0, string_2, sqlParameter_0);
                    return;
                }
            }
            throw new ArgumentNullException("connectionString");
        }

        private static void FillDataset(SqlConnection sqlConnection_0, SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0, DataSet dataSet_0, string[] string_1, params SqlParameter[] sqlParameter_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (dataSet_0 == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            SqlCommand command = new SqlCommand();
            bool flag = false;
            PrepareCommand(command, sqlConnection_0, sqlTransaction_0, commandType_0, string_0, sqlParameter_0, out flag);
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                if ((string_1 != null) && (string_1.Length > 0))
                {
                    string sourceTable = "Table";
                    for (int i = 0; i < string_1.Length; i++)
                    {
                        if ((string_1[i] == null) || (string_1[i].Length == 0))
                        {
                            throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                        }
                        adapter.TableMappings.Add(sourceTable, string_1[i]);
                        sourceTable = sourceTable + ((i + 1)).ToString();
                    }
                }
                adapter.Fill(dataSet_0);
                command.Parameters.Clear();
            }
            if (flag)
            {
                sqlConnection_0.Close();
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnSting());
        }

        public static string GetConnSting()
        {
            return ConStr;
        }

        private static void old_acctor_mc()
        {
            ConStr = "";
        }

        private static void PrepareCommand(SqlCommand sqlCommand_0, SqlConnection sqlConnection_0, SqlTransaction sqlTransaction_0, CommandType commandType_0, string string_0, SqlParameter[] sqlParameter_0, out bool bool_0)
        {
            if (sqlCommand_0 == null)
            {
                throw new ArgumentNullException("command");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            if (sqlConnection_0.State != ConnectionState.Open)
            {
                bool_0 = true;
                sqlConnection_0.Open();
            }
            else
            {
                bool_0 = false;
            }
            sqlCommand_0.Connection = sqlConnection_0;
            sqlCommand_0.CommandText = string_0;
            if (sqlTransaction_0 != null)
            {
                if (sqlTransaction_0.Connection == null)
                {
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                }
                sqlCommand_0.Transaction = sqlTransaction_0;
            }
            sqlCommand_0.CommandType = commandType_0;
            if (sqlParameter_0 != null)
            {
                AttachParameters(sqlCommand_0, sqlParameter_0);
            }
        }

        public static void UpdateDataset(SqlCommand sqlCommand_0, SqlCommand sqlCommand_1, SqlCommand sqlCommand_2, DataSet dataSet_0, string string_0)
        {
            if (sqlCommand_0 == null)
            {
                throw new ArgumentNullException("insertCommand");
            }
            if (sqlCommand_1 == null)
            {
                throw new ArgumentNullException("deleteCommand");
            }
            if (sqlCommand_2 == null)
            {
                throw new ArgumentNullException("updateCommand");
            }
            if ((string_0 != null) && (string_0.Length != 0))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    adapter.UpdateCommand = sqlCommand_2;
                    adapter.InsertCommand = sqlCommand_0;
                    adapter.DeleteCommand = sqlCommand_1;
                    adapter.Update(dataSet_0, string_0);
                    dataSet_0.AcceptChanges();
                    return;
                }
            }
            throw new ArgumentNullException("tableName");
        }

        private enum Enum0
        {
            Internal,
            External
        }
    }
}

