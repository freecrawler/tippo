namespace client
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SQLite;
    using System.IO;
    using System.Runtime.InteropServices;

    public class SQLiteHelper
    {
        public static string Conn;
        protected static string connectionString;

        public static DataSet ExecuteDataSet(SQLiteCommand sqliteCommand_0)
        {
            DataSet dataSet = new DataSet();
            SQLiteConnection connection = new SQLiteConnection(Conn);
            SQLiteTransaction transaction = null;
            PrepareCommand(sqliteCommand_0, connection, ref transaction, false, sqliteCommand_0.CommandType, sqliteCommand_0.CommandText, new SQLiteParameter[0]);
            try
            {
                new SQLiteDataAdapter(sqliteCommand_0).Fill(dataSet);
            }
            catch (Exception)
            {
            }
            finally
            {
                if ((sqliteCommand_0.Connection != null) && (sqliteCommand_0.Connection.State == ConnectionState.Open))
                {
                    sqliteCommand_0.Connection.Close();
                }
            }
            if (dataSet.Tables.Count == 0)
            {
                return ExecuteDataSet(sqliteCommand_0);
            }
            return dataSet;
        }

        public static DataSet ExecuteDataSet(string string_0, CommandType commandType_0)
        {
            if ((Conn == null) || (Conn.Length == 0))
            {
                throw new ArgumentNullException("Conn");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            DataSet dataSet = new DataSet();
            if (File.Exists(Conn.Replace("Data Source=", "")))
            {
                int num = 0;
                while (num < 10)
                {
                    SQLiteConnection connection = new SQLiteConnection(Conn);
                    SQLiteCommand command = new SQLiteCommand();
                    SQLiteTransaction transaction = null;
                    PrepareCommand(command, connection, ref transaction, false, commandType_0, string_0, new SQLiteParameter[0]);
                    try
                    {
                        try
                        {
                            new SQLiteDataAdapter(command).Fill(dataSet);
                            num = 10;
                        }
                        catch (Exception)
                        {
                            num++;
                        }
                        continue;
                    }
                    finally
                    {
                        if ((connection != null) && (connection.State == ConnectionState.Open))
                        {
                            connection.Close();
                        }
                    }
                }
            }
            return dataSet;
        }

        public static DataSet ExecuteDataSet(string string_0, CommandType commandType_0, params SQLiteParameter[] sqliteParameter_0)
        {
            if ((Conn == null) || (Conn.Length == 0))
            {
                throw new ArgumentNullException("Conn");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            DataSet dataSet = new DataSet();
            SQLiteConnection connection = new SQLiteConnection(Conn);
            SQLiteCommand command = new SQLiteCommand();
            SQLiteTransaction transaction = null;
            PrepareCommand(command, connection, ref transaction, false, commandType_0, string_0, sqliteParameter_0);
            try
            {
                new SQLiteDataAdapter(command).Fill(dataSet);
            }
            catch (Exception)
            {
            }
            finally
            {
                if ((connection != null) && (connection.State == ConnectionState.Open))
                {
                    connection.Close();
                }
            }
            if (dataSet.Tables.Count != 0)
            {
                return dataSet;
            }
            return ExecuteDataSet(string_0, commandType_0, sqliteParameter_0);
        }

        public static int ExecuteNonQuery(SQLiteCommand sqliteCommand_0)
        {
            int num = 0;
            if ((Conn != null) && (Conn.Length != 0))
            {
                using (SQLiteConnection connection = new SQLiteConnection(Conn))
                {
                    SQLiteTransaction transaction = null;
                    PrepareCommand(sqliteCommand_0, connection, ref transaction, true, sqliteCommand_0.CommandType, sqliteCommand_0.CommandText, new SQLiteParameter[0]);
                    try
                    {
                        num = sqliteCommand_0.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    return num;
                }
            }
            throw new ArgumentNullException("Conn");
        }

        public static int ExecuteNonQuery(string string_0, CommandType commandType_0)
        {
            int num = 0;
            if ((Conn == null) || (Conn.Length == 0))
            {
                throw new ArgumentNullException("Conn");
            }
            if ((string_0 != null) && (string_0.Length != 0))
            {
                SQLiteCommand command = new SQLiteCommand();
                using (SQLiteConnection connection = new SQLiteConnection(Conn))
                {
                    SQLiteTransaction transaction = null;
                    PrepareCommand(command, connection, ref transaction, true, commandType_0, string_0, new SQLiteParameter[0]);
                    try
                    {
                        num = command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception)
                        {
                        }
                        return num;
                    }
                    return num;
                }
            }
            throw new ArgumentNullException("commandText");
        }

        public static int ExecuteNonQuery(string string_0, CommandType commandType_0, SQLiteConnection sqliteConnection_0)
        {
            int num = 0;
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            SQLiteCommand command = new SQLiteCommand();
            SQLiteTransaction transaction = null;
            PrepareCommand(command, sqliteConnection_0, ref transaction, true, commandType_0, string_0, new SQLiteParameter[0]);
            try
            {
                num = command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                }
            }
            return num;
        }

        public static int ExecuteNonQuery(string string_0, CommandType commandType_0, params SQLiteParameter[] sqliteParameter_0)
        {
            int num = 0;
            if ((Conn == null) || (Conn.Length == 0))
            {
                throw new ArgumentNullException("Conn");
            }
            if ((string_0 != null) && (string_0.Length != 0))
            {
                SQLiteCommand command = new SQLiteCommand();
                using (SQLiteConnection connection = new SQLiteConnection(Conn))
                {
                    SQLiteTransaction transaction = null;
                    PrepareCommand(command, connection, ref transaction, true, commandType_0, string_0, new SQLiteParameter[0]);
                    try
                    {
                        num = command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    return num;
                }
            }
            throw new ArgumentNullException("commandText");
        }

        public static DbDataReader ExecuteReader(SQLiteCommand sqliteCommand_0)
        {
            DbDataReader reader = null;
            if ((Conn == null) || (Conn.Length == 0))
            {
                throw new ArgumentNullException("Conn");
            }
            SQLiteConnection connection = new SQLiteConnection(Conn);
            SQLiteTransaction transaction = null;
            PrepareCommand(sqliteCommand_0, connection, ref transaction, false, sqliteCommand_0.CommandType, sqliteCommand_0.CommandText, new SQLiteParameter[0]);
            try
            {
                reader = sqliteCommand_0.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
            }
            return reader;
        }

        public static DbDataReader ExecuteReader(string string_0, CommandType commandType_0)
        {
            DbDataReader reader = null;
            if ((Conn == null) || (Conn.Length == 0))
            {
                throw new ArgumentNullException("Conn");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            SQLiteConnection connection = new SQLiteConnection(Conn);
            SQLiteCommand command = new SQLiteCommand();
            SQLiteTransaction transaction = null;
            PrepareCommand(command, connection, ref transaction, false, commandType_0, string_0, new SQLiteParameter[0]);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
            }
            return reader;
        }

        public static DbDataReader ExecuteReader(string string_0, CommandType commandType_0, params SQLiteParameter[] sqliteParameter_0)
        {
            DbDataReader reader = null;
            if ((Conn == null) || (Conn.Length == 0))
            {
                throw new ArgumentNullException("Conn");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            SQLiteConnection connection = new SQLiteConnection(Conn);
            SQLiteCommand command = new SQLiteCommand();
            SQLiteTransaction transaction = null;
            PrepareCommand(command, connection, ref transaction, false, commandType_0, string_0, sqliteParameter_0);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
            }
            return reader;
        }

        public static object ExecuteScalar(SQLiteCommand sqliteCommand_0)
        {
            object obj2 = 0;
            if ((Conn != null) && (Conn.Length != 0))
            {
                using (SQLiteConnection connection = new SQLiteConnection(Conn))
                {
                    SQLiteTransaction transaction = null;
                    PrepareCommand(sqliteCommand_0, connection, ref transaction, true, sqliteCommand_0.CommandType, sqliteCommand_0.CommandText, new SQLiteParameter[0]);
                    try
                    {
                        obj2 = sqliteCommand_0.ExecuteScalar();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    return obj2;
                }
            }
            throw new ArgumentNullException("Conn");
        }

        public static object ExecuteScalar(string string_0, CommandType commandType_0)
        {
            object obj2 = 0;
            if ((Conn == null) || (Conn.Length == 0))
            {
                throw new ArgumentNullException("Conn");
            }
            if ((string_0 != null) && (string_0.Length != 0))
            {
                SQLiteCommand command = new SQLiteCommand();
                using (SQLiteConnection connection = new SQLiteConnection(Conn))
                {
                    SQLiteTransaction transaction = null;
                    PrepareCommand(command, connection, ref transaction, true, commandType_0, string_0, new SQLiteParameter[0]);
                    try
                    {
                        obj2 = command.ExecuteScalar();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    return obj2;
                }
            }
            throw new ArgumentNullException("commandText");
        }

        public static object ExecuteScalar(string string_0, CommandType commandType_0, params SQLiteParameter[] sqliteParameter_0)
        {
            object obj2 = 0;
            if ((Conn == null) || (Conn.Length == 0))
            {
                throw new ArgumentNullException("Conn");
            }
            if ((string_0 != null) && (string_0.Length != 0))
            {
                SQLiteCommand command = new SQLiteCommand();
                using (SQLiteConnection connection = new SQLiteConnection(Conn))
                {
                    SQLiteTransaction transaction = null;
                    PrepareCommand(command, connection, ref transaction, true, commandType_0, string_0, new SQLiteParameter[0]);
                    try
                    {
                        obj2 = command.ExecuteScalar();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    return obj2;
                }
            }
            throw new ArgumentNullException("commandText");
        }

        private static void PrepareCommand(SQLiteCommand sqliteCommand_0, SQLiteConnection sqliteConnection_0, ref SQLiteTransaction sqliteTransaction_0, bool bool_0, CommandType commandType_0, string string_0, params SQLiteParameter[] sqliteParameter_0)
        {
            if (sqliteConnection_0.State != ConnectionState.Open)
            {
                sqliteConnection_0.Open();
            }
            sqliteCommand_0.Connection = sqliteConnection_0;
            sqliteCommand_0.CommandText = string_0;
            if (bool_0)
            {
                sqliteTransaction_0 = sqliteConnection_0.BeginTransaction(IsolationLevel.ReadCommitted);
                sqliteCommand_0.Transaction = sqliteTransaction_0;
            }
            sqliteCommand_0.CommandType = commandType_0;
            if (sqliteParameter_0 != null)
            {
                foreach (SQLiteParameter parameter in sqliteParameter_0)
                {
                    sqliteCommand_0.Parameters.Add(parameter);
                }
            }
        }

        public static DataTable SelectPaging(string string_0, string string_1, string string_2, string string_3, int int_0, int int_1, out int int_2)
        {
            DataTable table = new DataTable();
            int_2 = Convert.ToInt32(ExecuteScalar("select count(*) from " + string_0, CommandType.Text));
            string format = "select {0} from {1} where {2} order by {3} limit {4} offset {5} ";
            int num = (int_1 - 1) * int_0;
            using (DbDataReader reader = ExecuteReader(string.Format(format, new object[] { string_1, string_0, string_2, string_3, int_0.ToString(), num.ToString() }), CommandType.Text))
            {
                if (reader != null)
                {
                    table.Load(reader);
                }
            }
            if (table.Rows.Count == 0)
            {
                return SelectPaging(string_0, string_1, string_2, string_3, int_0, int_1, out int_2);
            }
            return table;
        }

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }
    }
}

