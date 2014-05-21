namespace client
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class SqlHelperParameterCache
    {
        private static Hashtable paramCache;

        static SqlHelperParameterCache()
        {
            old_acctor_mc();
        }

        private SqlHelperParameterCache()
        {
        }

        public static void CacheParameterSet(string string_0, string string_1, params SqlParameter[] sqlParameter_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((string_1 == null) || (string_1.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            string str = string_0 + ":" + string_1;
            paramCache[str] = sqlParameter_0;
        }

        private static SqlParameter[] CloneParameters(SqlParameter[] sqlParameter_0)
        {
            SqlParameter[] parameterArray = new SqlParameter[sqlParameter_0.Length];
            int index = 0;
            int length = sqlParameter_0.Length;
            while (index < length)
            {
                parameterArray[index] = (SqlParameter) ((ICloneable) sqlParameter_0[index]).Clone();
                index++;
            }
            return parameterArray;
        }

        private static SqlParameter[] DiscoverSpParameterSet(SqlConnection sqlConnection_0, string string_0, bool bool_0)
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
            sqlConnection_0.Open();
            SqlCommandBuilder.DeriveParameters(command);
            sqlConnection_0.Close();
            if (!bool_0)
            {
                command.Parameters.RemoveAt(0);
            }
            SqlParameter[] array = new SqlParameter[command.Parameters.Count];
            command.Parameters.CopyTo(array, 0);
            foreach (SqlParameter parameter in array)
            {
                parameter.Value = DBNull.Value;
            }
            return array;
        }

        public static SqlParameter[] GetCachedParameterSet(string string_0, string string_1)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((string_1 == null) || (string_1.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            string str = string_0 + ":" + string_1;
            SqlParameter[] parameterArray = paramCache[str] as SqlParameter[];
            if (parameterArray != null)
            {
                return CloneParameters(parameterArray);
            }
            return null;
        }

        internal static SqlParameter[] GetSpParameterSet(SqlConnection sqlConnection_0, string string_0)
        {
            return GetSpParameterSet(sqlConnection_0, string_0, false);
        }

        public static SqlParameter[] GetSpParameterSet(string string_0, string string_1)
        {
            return GetSpParameterSet(string_0, string_1, false);
        }

        internal static SqlParameter[] GetSpParameterSet(SqlConnection sqlConnection_0, string string_0, bool bool_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            using (SqlConnection connection = (SqlConnection) ((ICloneable) sqlConnection_0).Clone())
            {
                return GetSpParameterSetInternal(connection, string_0, bool_0);
            }
        }

        public static SqlParameter[] GetSpParameterSet(string string_0, string string_1, bool bool_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((string_1 != null) && (string_1.Length != 0))
            {
                using (SqlConnection connection = new SqlConnection(string_0))
                {
                    return GetSpParameterSetInternal(connection, string_1, bool_0);
                }
            }
            throw new ArgumentNullException("spName");
        }

        private static SqlParameter[] GetSpParameterSetInternal(SqlConnection sqlConnection_0, string string_0, bool bool_0)
        {
            if (sqlConnection_0 == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((string_0 == null) || (string_0.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            string str = sqlConnection_0.ConnectionString + ":" + string_0 + (bool_0 ? ":include ReturnValue Parameter" : "");
            SqlParameter[] parameterArray = paramCache[str] as SqlParameter[];
            if (parameterArray == null)
            {
                SqlParameter[] parameterArray2 = DiscoverSpParameterSet(sqlConnection_0, string_0, bool_0);
                paramCache[str] = parameterArray2;
                parameterArray = parameterArray2;
            }
            return CloneParameters(parameterArray);
        }

        private static void old_acctor_mc()
        {
            paramCache = Hashtable.Synchronized(new Hashtable());
        }
    }
}

