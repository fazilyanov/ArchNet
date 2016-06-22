using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ArchNet
{
    public enum TableColumnType
    {
        String,
        Integer,
        Money,
        DateTime,
        Date,
    }

    public enum TableColumnAlign
    {
        Left,
        Center,
        Right,
    }

    public class TableColumn
    {
        public string Name { get; set; } = string.Empty;
        public string NameSql { get; set; } = string.Empty;
        public TableColumnType Type { get; set; } = TableColumnType.String;
        public int Width { get; set; } = 100;
        public TableColumnAlign Align { get; set; } = TableColumnAlign.Left;
    }

    public class TableData
    {
        public List<TableColumn> ColumnList { get; set; } = null;
    }

    public class ComFunc
    {
        public static string GenerateHtmlTableColumns(TableData tableData)
        {
            string ret = Environment.NewLine;
            foreach (TableColumn item in tableData.ColumnList)
            {
                ret += "                        <th>" + item.Name + "</th>" + Environment.NewLine;
            }
            return ret;
        }

        public static string GenerateJSTableColumns(TableData tableData)
        {
            string ret = Environment.NewLine;
            foreach (TableColumn item in tableData.ColumnList)
            {
                ret += "                    { \"data\": \"" + item.NameSql + "\", className:\"dt-body-" + item.Align.ToString().ToLower() + "\", \"width\": \"" + item.Width + "px\" }," + Environment.NewLine;
            }
            return ret;
        }

        #region SqlData

        /// <summary>
        /// Выполняет запрос
        /// </summary>
        /// <param name="query">SQL запрос</param>
        /// <returns>Если вернул "-1" значит ошибка</returns>
        /// <overloads>Cтрока подключения и таймаут по умолчанию</overloads>
        public static int ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, Properties.Settings.Default.constr);
        }

        /// <summary>
        /// Выполняет запрос
        /// </summary>
        /// <param name="query">SQL запрос</param>
        /// <param name="connstring">Строка подключения</param>
        /// <param name="timeout">Таймаут (По умолчанию - 120)</param>
        /// <returns>Количество строк, "-1" - ошибка</returns>
        /// <remarks>аааааа</remarks>
        public static int ExecuteNonQuery(string query, string connstring, int timeout = 120)
        {
            int ret = -1;
            SqlCommand cmd;
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
            cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = timeout;
            try
            {
                ret = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.Message.Trim();
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

        /// <summary>
        /// Возвращает таблицу - результат запроса, строка подключения и таймаут по умолчанию
        /// </summary>
        /// <param name="query">SQL запрос</param>
        /// <returns>DataTable.</returns>
        public static DataTable GetData(string query)
        {
            return GetData(query, Properties.Settings.Default.constr, null);
        }

        /// <summary>
        /// Возвращает таблицу - результат запроса, строка подключения и таймаут по умолчанию
        /// </summary>
        /// <param name="query">SQL запрос</param>
        /// <returns>DataTable.</returns>
        public static DataTable GetData(string query, SqlParameter[] sqlParameterArray)
        {
            return GetData(query, Properties.Settings.Default.constr, sqlParameterArray);
        }

        /// <summary>
        /// Возвращает таблицу - результат запроса
        /// </summary>
        /// <param name="query">SQL запрос</param>
        /// <param name="connstring">Строка подключения</param>
        /// <param name="timeout">Таймаут (По умолчанию - 120)</param>
        /// <returns>DataTable.</returns>
        public static DataTable GetData(string query, string connstring, SqlParameter[] sqlParameterArray, int timeout = 120)
        {
            SqlCommand cmd;
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddRange(sqlParameterArray);
            cmd.CommandTimeout = timeout;
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                ex.Message.Trim();
                dt = null;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        #endregion SqlData
    }
}