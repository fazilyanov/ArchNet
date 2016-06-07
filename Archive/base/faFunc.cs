// ***********************************************************************
// Assembly         : ArchNet
// Author           : Artur Fazilyanov (a.fazilyanov@gmail.com)
// Created          : 02-25-2016
//
// Last Modified By : a.fazilyanov
// Last Modified On : 03-22-2016
// ***********************************************************************
// <copyright file="faFunc.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace ArchNet
{
    /// <summary>
    /// Перечисление видов сообщений
    /// </summary>
    public enum faAlert
    {
        /// <summary>
        /// The dangers
        /// </summary>
        Danger,

        /// <summary>
        /// The success
        /// </summary>
        Success,

        /// <summary>
        /// The warning
        /// </summary>
        Warning,

        /// <summary>
        /// The information
        /// </summary>
        Info,

        /// <summary>
        /// The base access denied
        /// </summary>
        BaseAccessDenied,

        /// <summary>
        /// The page access denied
        /// </summary>
        PageAccessDenied,

        /// <summary>
        /// The site access denied
        /// </summary>
        SiteAccessDenied,

        /// <summary>
        /// The not found
        /// </summary>
        NotFound,

        /// <summary>
        /// The row not found or access denied
        /// </summary>
        RowNotFoundOrAccessDenied,

        /// <summary>
        /// The bad parameter
        /// </summary>
        BadParam,

        /// <summary>
        /// The saved
        /// </summary>
        Saved,

        /// <summary>
        /// The da hui ego znaet
        /// </summary>
        DaHuiEgoZnaet
    }

    /// <summary>
    /// Класс сборник общих методов для приложения
    /// </summary>
    public class faFunc
    {
        /// <summary>
        /// The english
        /// </summary>
        private const string English = "qwertyuiop[]asdfghjkl;'zxcvbnm,.";

        /// <summary>
        /// The russian
        /// </summary>
        private const string Russian = "йцукенгшщзхъфывапролджэячсмитьбю";

        /// <summary>
        /// Выводит сообщение
        /// </summary>
        /// <param name="_a">Вид сообщения</param>
        /// <param name="_msg">Текст сообщения</param>
        public static string Alert(faAlert _a, string _msg = "")
        {
            string _type = "";
            int r = DateTime.Now.Millisecond;
            switch (_a)
            {
                case faAlert.Danger:
                    _type = "danger";
                    break;

                case faAlert.Success:
                    _type = "success";
                    break;

                case faAlert.Warning:
                    _type = "warning";
                    break;

                case faAlert.Info:
                    _type = "info";
                    break;

                case faAlert.BaseAccessDenied:
                    _type = "danger";
                    _msg = "Нет доступа к этой базе";
                    break;

                case faAlert.PageAccessDenied:
                    _type = "danger";
                    _msg = "Нет доступа к этой странице";
                    break;

                case faAlert.SiteAccessDenied:
                    _type = "danger";
                    _msg = "Пользователь не найден. Доступ запрещен.";
                    break;

                case faAlert.NotFound:
                    _type = "info";
                    _msg = "Не найден";
                    break;

                case faAlert.RowNotFoundOrAccessDenied:
                    _type = "danger";
                    _msg = "Запись не найдена или к ней нет доступа";
                    break;

                case faAlert.BadParam:
                    _type = "danger";
                    _msg = "Переданы неверные параметры";
                    break;

                case faAlert.Saved:
                    _type = "success";
                    _msg = "Успешно сохранено";
                    break;

                case faAlert.DaHuiEgoZnaet:
                    _type = "warning";
                    _msg = "Выражение неуверенности или отсутствия информации по этому вопросу";
                    break;

                default:
                    break;
            }
            return "<div id='alert_" + _type + "_" + r + "' class='alert alert-" + _type + "'  role='alert' style='margin-right: auto;margin-left: auto;width:auto;'><button type='button' class='close' onclick=\"$('#alert_" + _type + "_" + r +
                "').hide(200);\"><span aria-hidden='true'>&times;</span><span class='sr-only'>Закрыть</span></button><div id='alert_" + _type + "_msg'>" + _msg + "</div></div>";
        }

        /// <summary>
        /// Проверяет правильность условия сравнения
        /// </summary>
        public static string CheckdNumericCond(string cond)
        {
            string ret = "";
            switch (cond)
            {
                case "=":
                case "!=":
                case ">":
                case "<":
                case ">=":
                case "<=":
                    ret = cond;
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Проверяет правильность условия сравнения
        /// </summary>
        public static string CheckTextCond(string cond)
        {
            string ret = "";
            switch (cond)
            {
                case "=":
                case "*":
                    ret = cond;
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Отрисовывает таблицу из DataTable в HTML
        /// </summary>
        /// <param name="dt">Таблица</param>
        /// <returns>HTML</returns>
        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            int _count = dt.Rows.Count > 199 ? 200 : dt.Rows.Count;
            for (int i = 0; i < _count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        /// <summary>
        /// Заменяет латинские символы на кирилистические, соответствующие им на клавиатуре
        /// </summary>
        /// <param name="input">Строка в латинице</param>
        /// <returns>Строка в кириллице</returns>
        public static string ConvertEngToRus(string input)
        {
            var result = new StringBuilder(input.Length);
            int index;
            foreach (var symbol in input)
                result.Append((index = English.IndexOf(symbol)) != -1 ? Russian[index] : symbol);
            return result.ToString();
        }

        /// <summary>
        /// Заменяет кирилистические символы на латинские, соответствующие им на клавиатуре
        /// </summary>
        /// <param name="input">Строка в кириллице</param>
        /// <returns>Строка в латинице</returns>
        public static string ConvertRusToEng(string input)
        {
            var result = new StringBuilder(input.Length);
            int index;
            foreach (var symbol in input)
                result.Append((index = Russian.IndexOf(symbol)) != -1 ? English[index] : symbol);
            return result.ToString();
        }

        /// <summary>
        /// Возвращает внутреннее имя базы по id
        /// </summary>
        /// <param name="_id">ID</param>
        public static string GetBaseNameById(int _id)
        {
            string[] arr = new string[25];
            //
            if (HttpContext.Current.Session["basearray"] == null)
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT a.id, a.name FROM [dbo].[_base] as a where del=0", conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) arr[(int)rdr["id"]] = rdr["name"].ToString();
                HttpContext.Current.Session["basearray"] = arr;
                conn.Close();
            }
            else
                arr = (string[])HttpContext.Current.Session["basearray"];
            return arr[_id].ToString();
        }

        /// <summary>
        /// Возвращает видимое имя базы по id
        /// </summary>
        /// <param name="_id">ID</param>
        public static string GetBaseNameRusById(int _id)
        {
            string[] arr = new string[25];
            //
            if (HttpContext.Current.Session["basearrayrus"] == null)
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT a.id, a.namerus FROM [dbo].[_base] as a where del=0", conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) arr[(int)rdr["id"]] = rdr["namerus"].ToString();
                HttpContext.Current.Session["basearrayrus"] = arr;
                conn.Close();
            }
            else
                arr = (string[])HttpContext.Current.Session["basearrayrus"];
            return arr[_id].ToString();
        }

        /// <summary>
        /// Генерирует текстовое описание изменения для поля в журнале
        /// </summary>
        /// <param name="name">Имя поля</param>
        /// <param name="src">Исходное значение</param>
        /// <param name="dest">Новое значение</param>
        /// <param name="fld">Поле</param>
        /// <param name="_page">Страница</param>
        /// <param name="_act">Действие на странице</param>
        /// <returns>Текстовое описание изменения</returns>
        public static string GetChange(string name, string src, string dest, faField fld,  faPage _page, string _act)
        {
            return src == dest ? "" : "[" + name + "] " + src + " -> " + dest +"\n";
        }

        /// <summary>
        /// Генерирует текстовое описание изменения для поля в журнале
        /// </summary>
        /// <param name="name">Имя поля</param>
        /// <param name="row">Строка таблицы</param>
        /// <param name="fld">Поле</param>
        /// <param name="_score">Баллы для операторов</param>
        /// <param name="_page">Страница</param>
        /// <param name="_act">Действие на странице</param>
        /// <returns>Текстовое описание изменения</returns>
        public static string GetChange(string name, DataRow row, faField fld, faPage _page, string _act)
        {
            string src = "", dest = "";
            switch (fld.Edit.Control)
            {
                case faControl.TextBox:
                case faControl.TextBoxInteger:
                case faControl.DropDown:
                case faControl.TreeGrid:
                case faControl.File:
                case faControl.AutoComplete:
                case faControl.DateTimePicker:
                case faControl.NewWindowArchive:
                case faControl.NewWindowArchiveID:
                    src = row[fld.Data.FieldName].ToString().Trim();
                    dest = fld.Edit.Value.Trim();
                    break;

                case faControl.TextBoxNumber:
                    src = ((decimal)row[fld.Data.FieldName]).ToString(CultureInfo.InvariantCulture);
                    dest = fld.Edit.Value.Trim();
                    break;

                case faControl.DatePicker:
                    src = row[fld.Data.FieldName].ToString() == "" ? "" : ((DateTime)row[fld.Data.FieldName]).ToShortDateString();
                    dest = (fld.Edit.Value ?? "").Trim();
                    break;

                case faControl.TextArea:
                    src = row[fld.Data.FieldName].ToString().Trim();
                    dest = fld.Edit.Value.Trim();
                    if (src == dest) src = dest = "";
                    else { src = src.Length > 0 ? "old" : "empty"; dest = dest.Length > 0 ? "new" : "empty"; }

                    break;

                default:
                    break;
            }
            
            return src == dest ? "" : "[" + name + "] " + src + " -> " + dest + "\n";
        }

        /// <summary>
        /// Генерирует текстовое описание изменения для поля в журнале, для новой записи
        /// </summary>
        /// <param name="name">Имя поля</param>
        /// <param name="dest">Новое значение</param>
        /// <param name="fld">Поле</param>
        /// <param name="_page">Страница</param>
        /// <param name="_act">Действие на странице</param>
        /// <returns>Текстовое описание изменения</returns>
        public static string GetChangeNew(string name, string dest, faField fld, faPage _page, string _act)
        {
            return dest != "" && dest != "0" ? "[" + name + "] -> " + dest + "\n" : "";
        }

        /// <summary>
        /// Определяет тип документа (страницы) для указанной формы документа
        /// </summary>
        /// <param name="id_doctree">ID Формы документа</param>
        public static string GetDocTypeByIdDoctree(string id_doctree)
        {
            string _key = "data_tree__doctree";
            DataTable buf = new DataTable();
            buf = HttpContext.Current.Cache[_key] as DataTable;
            if (buf == null)
            {
                buf = new DataTable();
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                string sql = "SELECT a.* FROM [_doctree_pre] a ORDER BY a.pos";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 600;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(buf);
                conn.Close();
                buf.PrimaryKey = new DataColumn[] { buf.Columns["id"] };
                HttpContext.Current.Cache.Insert(_key, buf, null, DateTime.Now.AddHours(2), Cache.NoSlidingExpiration);
            }

            string ret = "none";
            DataRow[] foundRows = buf.Select("id = '" + id_doctree + "'");
            if (foundRows.Length > 0)
            {
                string top_parent = foundRows[0]["top_parent"].ToString();
                switch (top_parent)
                {
                    case "7":
                        ret = "acc";
                        break;

                    case "15":
                        ret = "dog";
                        break;

                    case "2":
                        ret = "ord";
                        break;

                    case "56":
                        ret = "oth";
                        break;

                    case "50":
                        ret = "empl";
                        break;

                    case "60":
                        ret = "ohs";
                        break;

                    case "11":
                        ret = "tech";
                        break;

                    case "5574":
                        ret = "bank";
                        break;

                    case "5596":
                        ret = "norm";
                        break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Возвращает заголовок страницы, по сокращенному названию
        /// </summary>
        /// <param name="page">Сокращенное название</param>
        public static string GetDocTypeName(string page)
        {
            string ret = "";
            switch (page)
            {
                case "none":
                    ret = "";
                    break;

                case "srch":
                    ret = "Поиск документов";
                    break;

                case "select":
                    ret = "Выбор документа";
                    break;

                case "acc":
                    ret = "Бухгалтерские документы";
                    break;

                case "dog":
                    ret = "Договоры";
                    break;

                case "ord":
                    ret = "ОРД";
                    break;

                case "oth":
                    ret = "Прочие документы";
                    break;

                case "empl":
                    ret = "Документы по личному составу";
                    break;

                case "ohs":
                    ret = "Документы по охране труда";
                    break;

                case "tech":
                    ret = "Техническая документация";
                    break;

                case "bank":
                    ret = "Банковские документы";
                    break;

                case "norm":
                    ret = "Локальные нормативные документы";
                    break;

                default:
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Возвращает заголовок страницы, по элементу перечисления
        /// </summary>
        /// <param name="page">Элемент перечисление <see cref="faPage"/></param>
        public static string GetDocTypeName(faPage page)
        {
            string ret = "";
            switch (page)
            {
                case faPage.none:
                    ret = "";
                    break;

                case faPage.srch:
                    ret = "Поиск документов";
                    break;

                case faPage.select:
                    ret = "Выбор документа";
                    break;

                case faPage.acc:
                    ret = "Бухгалтерские документы";
                    break;

                case faPage.dog:
                    ret = "Договоры";
                    break;

                case faPage.ord:
                    ret = "ОРД";
                    break;

                case faPage.oth:
                    ret = "Прочие документы";
                    break;

                case faPage.empl:
                    ret = "Документы по личному составу";
                    break;

                case faPage.ohs:
                    ret = "Документы по охране труда";
                    break;

                case faPage.tech:
                    ret = "Техническая документация";
                    break;

                case faPage.bank:
                    ret = "Банковские документы";
                    break;

                case faPage.norm:
                    ret = "Локальные нормативные документы";
                    break;

                default:
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Обрабатывает исключение
        /// </summary>
        /// <param name="ex">Исключение </param>
        /// <returns> Сообщение исключения, или его замена</returns>
        public static string GetExceptionMessage(Exception ex)
        {
            string ret = ex.Message;
            if (ex is SqlException)
            {
                if ((ex as SqlException).Number == -2)
                    ret = "Время ожидания ответа от сервера истекло";
            }
            return ret;
        }

        //
        /// <summary>
        /// Подсчитывает количество баллов для поля
        /// </summary>
        /// <param name="fn">Имя поля</param>
        /// <param name="_page">Страница/Тип документа</param>
        /// <returns>System.Byte Число баллов</returns>
        public static byte GetFieldScore(string fn, faPage _page)
        {
            DataTable buf = new DataTable();

            buf = HttpContext.Current.Cache["FieldScoreList"] as DataTable;
            if (buf == null)
            {
                buf = new DataTable();
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                string sql = "SELECT a.name, a.score FROM [_field] a where a.del=0";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(buf);
                conn.Close();
                buf.PrimaryKey = new DataColumn[] { buf.Columns["id"] };
                HttpContext.Current.Cache.Insert("FieldScoreList", buf, null, DateTime.Now.AddHours(2), Cache.NoSlidingExpiration);
            }

            string _alt_fn = "";
            switch (_page)
            {
                case faPage.acc:
                    _alt_fn = fn + "_acc";
                    break;

                case faPage.dog:
                    _alt_fn = fn + "_dog";
                    break;

                case faPage.ord:
                    _alt_fn = fn + "_ord";
                    break;

                case faPage.oth:
                    _alt_fn = fn + "_oth";
                    break;

                case faPage.empl:
                    _alt_fn = fn + "_empl";
                    break;

                case faPage.ohs:
                    _alt_fn = fn + "_ohs";
                    break;

                case faPage.tech:
                    _alt_fn = fn + "_tech";
                    break;

                case faPage.bank:
                    _alt_fn = fn + "_bank";
                    break;

                case faPage.norm:
                    _alt_fn = fn + "_norm";
                    break;

                default:
                    _alt_fn = fn;
                    break;
            }
            DataRow[] foundRows = buf.Select("name = '" + fn + "' or name = '" + _alt_fn + "'");
            if (foundRows.Length > 0) return (byte)foundRows[0]["score"];
            else return 0;
        }

        /// <summary>
        /// Возвращает MD5 хеш строки
        /// </summary>
        /// <param name="input">Строка для подсчета</param>
        /// <returns>System.String. MD5</returns>
        public static string GetMD5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        /// <summary>
        /// Возвращает элементу перечисления <see cref="faPage"/> по краткому наименованию
        /// </summary>
        /// <param name="p_key">Краткое наименование</param>
        /// <returns>Элемент перечисления <see cref="faPage"/></returns>
        public static faPage GetPageType(string p_key)
        {
            faPage ret;
            switch (p_key)
            {
                case "acc":
                    ret = faPage.acc;
                    break;

                case "dog":
                    ret = faPage.dog;
                    break;

                case "ord":
                    ret = faPage.ord;
                    break;

                case "ohs":
                    ret = faPage.ohs;
                    break;

                case "empl":
                    ret = faPage.empl;
                    break;

                case "tech":
                    ret = faPage.tech;
                    break;

                case "oth":
                    ret = faPage.oth;
                    break;

                case "srch":
                    ret = faPage.srch;
                    break;

                case "select":
                    ret = faPage.select;
                    break;

                case "bank":
                    ret = faPage.bank;
                    break;

                case "norm":
                    ret = faPage.norm;
                    break;

                default:
                    ret = faPage.none;
                    break;
            }
            return ret;
        }

        /// <summary>
        /// Генерит HTML табличку со всеми значениями сессии
        /// </summary>
        public static string GetSessionValues()
        {
            string _ret = "<style type=\"text/css\">table {border: thin solid black;border-collapse:collapse;} td,th{ border: thin solid black;padding:5px;}</style>";
            string _buf = "";
            try
            {
                string[] ignore_value = { };
                long totalSessionBytes = 0;

                _ret += "<b>Активных сессий:</b>  " + (HttpContext.Current.Application["ActiveSession"] ?? 0).ToString() + " <br/><br/>UrlReferrer:";
                _ret += HttpContext.Current.Request.UrlReferrer + "<br/>AbsoluteUri:" + HttpContext.Current.Request.Url.AbsoluteUri + "<br/><br/>";
                _ret += "<br/><br/><table ><caption><b>Переменные сессии</b></caption><tr><th>Ключ</th><th>Значение</th><th>Размер</th></tr>";
                BinaryFormatter b = new BinaryFormatter();
                foreach (string item in HttpContext.Current.Session.Contents)
                {
                    var m = new MemoryStream();
                    b.Serialize(m, HttpContext.Current.Session[item]);
                    totalSessionBytes += m.Length;
                    _buf = "";
                    if (HttpContext.Current.Session[item].ToString() == "Table1")
                    {
                        _buf = ConvertDataTableToHTML((DataTable)HttpContext.Current.Session[item]);
                    }
                    else if (HttpContext.Current.Session[item].ToString().IndexOf("href") > 0)
                        _buf = "html";
                    else
                        _buf = HttpContext.Current.Session[item].ToString();
                    _ret += "<tr><td>" + item + "</td><td>" + _buf + "</td><td>" + m.Length + " байт</td></tr>";
                }
                _ret += "<tr><th>Всего</th><td></td><th>" + (int)totalSessionBytes / 1024 + " КБайт</th></tr>";
                _ret += "</table><br/><br/>";

                return _ret;
            }
            catch (Exception ex)
            {
                return faFunc.GetExceptionMessage(ex); ;
            }
        }

        /// <summary>
        /// Достает настройки из таблицы [dbo._site_setting]
        /// </summary>
        /// <param name="key">Имя настройки в таблице</param>
        public static string GetSiteSetting(string key)
        {
            string value = "";
            if (HttpContext.Current.Session["setting_" + key] == null)
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);

                string _sql =
                    " SELECT a.[value] " +
                    " FROM [dbo].[_site_setting] a " +
                    " WHERE a.[del]=0 AND a.[key]=@key";
                SqlCommand cmd = new SqlCommand(_sql, conn);
                cmd.Parameters.AddWithValue("@key", key);//last_msg_id
                conn.Open();
                var res = cmd.ExecuteScalar();
                value = (res is DBNull || res == null ? "" : res.ToString());
                conn.Close();
                HttpContext.Current.Session["setting_" + key] = value;
            }
            else
                value = HttpContext.Current.Session["setting_" + key].ToString();
            return value;
        }

        /// <summary>
        /// Достает настройки пользователя из таблицы [dbo].[_user_setting]
        /// </summary>
        /// <param name="key">Имя настройки в таблице</param>
        public static string GetUserSetting(string key)
        {
            string value = "";
            if (HttpContext.Current.Session["setting_" + key] == null)
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);

                string _sql =
                    " SELECT a.[value] " +
                    " FROM [dbo].[_user_setting] a " +
                    " WHERE a.[del]=0 AND a.[id_user]=@id_user AND a.[key]=@key";
                SqlCommand cmd = new SqlCommand(_sql, conn);
                cmd.Parameters.AddWithValue("@id_user", HttpContext.Current.Session["user_id"].ToString());
                cmd.Parameters.AddWithValue("@key", key);//last_msg_id
                conn.Open();
                var res = cmd.ExecuteScalar();
                value = (res is DBNull || res == null ? "" : res.ToString());
                conn.Close();
                HttpContext.Current.Session["setting_" + key] = value;
            }
            else
                value = HttpContext.Current.Session["setting_" + key].ToString();
            return value;
        }

        #region Mail Send

        /// <summary>
        /// Отправляет письмо
        /// </summary>
        /// <param name="mm">Сообщение</param>
        public static void SendMail(System.Net.Mail.MailMessage mm)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Properties.Settings.Default.MailServerHost, Properties.Settings.Default.MailServerPort);
            client.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.ArchiveMailLogin, Properties.Settings.Default.ArchiveMailPassword);
            try
            {
                client.Send(mm);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Отправляет письмо Админу
        /// </summary>
        /// <param name="subject">Тема</param>
        /// <param name="msg">Сообщение</param>
        public static void SendMailAdmin(string msg, string subject = "Архив: Регламентная операция")
        {
            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
            mm.From = new System.Net.Mail.MailAddress(Properties.Settings.Default.ArchiveMail);
            mm.To.Add(new System.Net.Mail.MailAddress(Properties.Settings.Default.AdminMail));
            mm.Subject = subject;
            mm.IsBodyHtml = true;
            mm.Body = msg;
            SendMail(mm);
        }

        /// <summary>
        /// Посылает письмо группе пользователей
        /// </summary>
        /// <param name="id_mailgroup">ID группы в таблице [dbo].[_mailgroup_user]</param>
        /// <param name="subject">Тема</param>
        /// <param name="msg">Сообщение</param>
        /// <param name="result">Текст ошибки, если есть</param>
        public static bool SendMailGroup(string id_mailgroup, string subject, string msg, out string result)
        {
            result = "";
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();
            string q = String.Format(@"SELECT a.[id_user],b.[name], b.mail FROM [dbo].[_mailgroup_user] as a left join [dbo].[_user] b on a.id_user=b.id and b.del=0 where a.id_mailgroup=@id_mailgroup and a.del=0");
            try
            {
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@id_mailgroup", id_mailgroup);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                conn.Close();
                System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
                mm.From = new System.Net.Mail.MailAddress(Properties.Settings.Default.ArchiveMail);
                foreach (DataRow row in dt.Rows)
                {
                    if (row["mail"].ToString().Trim().Length > 0)
                    {
                        mm.To.Add(new System.Net.Mail.MailAddress(row["mail"].ToString()));
                        result += row["name"] + " (" + row["mail"].ToString() + ")\r\n";
                    }
                }
                mm.Subject = subject;
                mm.Body = msg;
                mm.IsBodyHtml = true;
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Properties.Settings.Default.MailServerHost, Properties.Settings.Default.MailServerPort);
                client.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.ArchiveMailLogin, Properties.Settings.Default.ArchiveMailPassword);
                client.Send(mm);
                faFunc.ToLog(8, "Уведомление");
                return true;
            }
            catch (Exception ex)
            {
                result = faFunc.GetExceptionMessage(ex);
                conn.Close();
                return false;
            }
        }

        /// <summary>
        /// Отправляет письмо пользователю
        /// </summary>
        /// <param name="id_user">ID пользователя</param>
        /// <param name="subject">Тема</param>
        /// <param name="msg">Сообщение</param>
        /// <param name="result">Текст ошибки, если есть</param>
        public static bool SendMailUser(string id_user, string subject, string msg, out string result)
        {
            result = "";
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();
            string q = String.Format(@"SELECT a.[name], a.mail FROM [dbo].[_user] as a where a.id=@id_user and a.del=0");
            try
            {
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@id_user", id_user);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 0)
                {
                    System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
                    mm.From = new System.Net.Mail.MailAddress(Properties.Settings.Default.ArchiveMail);
                    mm.To.Add(new System.Net.Mail.MailAddress(dt.Rows[0]["mail"].ToString()));
                    mm.Subject = subject;
                    mm.Body = msg;
                    mm.IsBodyHtml = true;//письмо в html формате (если надо)
                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Properties.Settings.Default.MailServerHost, Properties.Settings.Default.MailServerPort);

                    client.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.ArchiveMailLogin, Properties.Settings.Default.ArchiveMailPassword);
                    client.Send(mm);
                    faFunc.ToLog(8, "Уведомление");
                    result = dt.Rows[0]["name"].ToString() + " (" + dt.Rows[0]["mail"].ToString() + ")";
                    return true;
                }
                else
                {
                    result = "Пользователь не найден";
                    return false;
                }
            }
            catch (Exception ex)
            {
                result = faFunc.GetExceptionMessage(ex);
                conn.Close();
                return false;
            }
        }

        #endregion Mail Send

        /// <summary>
        /// Сохраняет настройку пользователя
        /// </summary>
        /// <param name="key">Имя настройки</param>
        /// <param name="value">Значение</param>
        public static bool SetUserSetting(string key, string value)
        {
            if (HttpContext.Current.Session["setting_" + key] == null || HttpContext.Current.Session["setting_" + key].ToString() != value)
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction("setting_set_tr");
                try
                {
                    SqlCommand cmd = new SqlCommand("", conn, trans);
                    cmd.CommandText =
                        " SELECT a.[value] " +
                        " FROM [dbo].[_user_setting] a " +
                        " WHERE a.[del]=0 AND a.[id_user]=@id_user AND a.[key]=@key";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id_user", HttpContext.Current.Session["user_id"].ToString());
                    cmd.Parameters.AddWithValue("@key", key);
                    var res = cmd.ExecuteScalar();
                    string old_value = (res is DBNull || res == null ? "" : res.ToString().Trim());
                    if (old_value != value.Trim())
                    {
                        if (old_value == "")
                            cmd.CommandText = "INSERT INTO [dbo].[_user_setting] ([id_user],[key],[value],[del],[last_upd])VALUES(@id_user,@key,@value,0,GETDATE())";
                        else
                            cmd.CommandText = " UPDATE [dbo].[_user_setting] SET [value] = @value, [last_upd] = GETDATE() WHERE [del]=0 AND [id_user]=@id_user AND [key]=@key";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@id_user", HttpContext.Current.Session["user_id"].ToString());
                        cmd.Parameters.AddWithValue("@key", key);
                        cmd.Parameters.AddWithValue("@value", value);
                        cmd.ExecuteNonQuery();
                    }
                    HttpContext.Current.Session["setting_" + key] = value;
                    trans.Commit();
                    conn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    conn.Close();
                    HttpContext.Current.Session["last_error"] = ex.GetType() + ": " + ex.Message;
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Запись в журнал
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="p_id_user">ID текущего пользователя</param>
        /// <param name="p_id_edittype">Действие 1 - добавление,2 -редактирование, 3 - удаление</param>
        /// <param name="p_change_id">ID записи в которой произошли изменения</param>
        /// <param name="p_id_base">ID Базы</param>
        /// <param name="p_id_table">ID Таблицы</param>
        /// <param name="p_changes">Текстовое описание изменения</param>
        /// <param name="p_score">Количество баллов за изменение</param>
        public static void ToJournal(SqlCommand cmd, string p_id_user, int p_id_edittype, int p_change_id, string p_id_base, int p_id_table, string p_changes)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@p_id_user", int.Parse(p_id_user));
            cmd.Parameters.AddWithValue("@p_id_edittype", p_id_edittype);
            cmd.Parameters.AddWithValue("@p_change_id", p_change_id);
            cmd.Parameters.AddWithValue("@p_id_base", p_id_base);
            cmd.Parameters.AddWithValue("@p_id_table", p_id_table);
            cmd.Parameters.AddWithValue("@p_changes", p_changes);
            cmd.CommandText =
                "INSERT INTO [dbo].[_journal]([when],[id_user],[id_edittype],[change_id],[id_base],[id_table],[changes])" +
                "VALUES (GetDate(), @p_id_user, @p_id_edittype, @p_change_id, @p_id_base, @p_id_table, @p_changes)";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Добавляет запись в таблицу Log
        /// </summary>
        /// <param name="p_id_type">1-Авторизация,
        /// 2-Ошибка,
        /// 3-Выгрузка в CSV,
        /// 4-Массовая выгрузка файлов,
        /// 5-Выгрузка в Excel,
        /// 6-Доступ из 1С,
        /// 7-Массовое редактирование,
        /// 8-Отправка письма</param>
        /// <param name="p_what">The p_what.</param>
        public static void ToLog(int p_id_type, string p_what = "")
        {
            int id_user = int.Parse((HttpContext.Current.Session["user_id"] ?? "0").ToString());
            p_what = id_user > 0 ? p_what : HttpContext.Current.User.Identity.Name.Trim().ToString() + "\n" + p_what;
            ExecuteNonQuery("INSERT INTO [dbo].[_log]([id_user],[when],[id_type],[what]) VALUES ( " + id_user + ", GetDate(), " + p_id_type + ",'" + p_what + "')");
        }

        /// <summary>
        /// Переделывает полное ФИО пользователя в сокращенное
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="name">Полное ФИО</param>
        /// <returns>System.String Сокращенное </returns>
        public static string UpdateUserSName(string login, string name)
        {
            string ret = "";
            name = name.Trim();
            if (name.Length > 0)
            {
                name = name.Replace("оглы", "").Replace("Оглы", "").Trim();
                name = Regex.Replace(name, @"\s{2,}", " ", RegexOptions.IgnoreCase);
                string[] fio = name.Split(' ');
                string sname = "";
                if (fio.Length == 3 && fio[0].Trim().Length > 0 && fio[1].Trim().Length > 0 && fio[2].Trim().Length > 0)
                    sname = fio[0] + " " + fio[1][0] + "." + fio[2][0] + ".";
                else
                    sname = name;

                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                string _sql = "UPDATE [dbo].[_user] SET [sname] = @p_sname WHERE [login] = @p_login";
                SqlCommand cmd = new SqlCommand(_sql, conn);
                cmd.Parameters.AddWithValue("@p_login", login);
                cmd.Parameters.AddWithValue("@p_sname", sname);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return ret;
        }

        #region SQL

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
        /// Выполняет скалярный запрос
        /// </summary>
        /// <param name="query">SQL запрос</param>
        /// <returns>Если вернул "-1" значит ошибка</returns>
        /// <overloads>Cтрока подключения и таймаут по умолчанию</overloads>
        public static int ExecuteScalarInt(string query)
        {
            return ExecuteScalarInt(query, Properties.Settings.Default.constr);
        }

        /// <summary>
        /// Выполняет скалярный запрос
        /// </summary>
        /// <param name="query">SQL запрос</param>
        /// <param name="connstring">Строка подключения</param>
        /// <param name="timeout">Таймаут (По умолчанию - 120)</param>
        /// <returns>Количество строк, "-1" - ошибка</returns>
        /// <remarks>аааааа</remarks>
        public static int ExecuteScalarInt(string query, string connstring, int timeout = 120)
        {
            int ret = -1;
            SqlCommand cmd;
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
            cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = timeout;
            try
            {
                int resInt = 0;
                var res = cmd.ExecuteScalar();
                if (int.TryParse((res is DBNull || res == null ? "-1" : res.ToString()), out resInt))
                    ret = resInt;
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
            return GetData(query, Properties.Settings.Default.constr);
        }

        /// <summary>
        /// Возвращает таблицу - результат запроса
        /// </summary>
        /// <param name="query">SQL запрос</param>
        /// <param name="connstring">Строка подключения</param>
        /// <param name="timeout">Таймаут (По умолчанию - 120)</param>
        /// <returns>DataTable.</returns>
        public static DataTable GetData(string query, string connstring, int timeout = 120)
        {
            SqlCommand cmd;
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
            cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = timeout;
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
                conn.Close();
            }
            catch
            {
                conn.Close();
                dt = null;
            }
            return dt;
        }

        #endregion SQL
    }
}