﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace ArchNet
{
    public partial class Site : System.Web.UI.MasterPage
    {
        /// <summary>
        ///
        /// </summary>
        public string
            WinLogin = "", cur_id_base = "", cur_basename = "", _tmp = "", OpenHelp = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string _base_name = "", menu_enabled = "", menu_disabled = "", list_enabled = "", list_disabled = "";
            string menu_item_enabled_blank = "<li role=\"presentation\"><a role=\"menuitem\" target=\"_blank\" href=\"{0}\"><span class=\"{2}\"></span>&nbsp;&nbsp;{1}</a></li>";
            string menu_item_enabled = "<li role=\"presentation\"><a role=\"menuitem\" href=\"{0}\"><span class=\"{2}\"></span>&nbsp;&nbsp;{1}</a></li>";
            string menu_item_disabled = "<li role=\"presentation\"><a role=\"menuitem\" style=\"color:#aaa;\" href=\"#\" onclick=\"alert('Нет доступа');\" title=\"Нет доступа\" ><span class=\"{1}\"></span>&nbsp;&nbsp;{0}</a></li>";
            string list_item_enabled = "<a href=\"{0}\" class=\"list-group-item\"><span class=\"{2}\"></span>&nbsp;&nbsp;{1}</a>";
            string list_item_enabled_blank = "<a href=\"{0}\" class=\"list-group-item\" target=\"_blank\"><span class=\"{2}\"></span>&nbsp;&nbsp;{1}</a>";
            string list_item_disabled = "<a href=\"\" style=\"color:#aaa;\" class=\"list-group-item\" onclick=\"alert('Нет доступа');\" title=\"Нет доступа\"><span class=\"{1}\"></span>&nbsp;&nbsp;{0}</a>";
            string submenu_begin = "<li class=\"dropdown-submenu\"><a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\"> <span style=\"color:#aaa;\" class=\"hi hi-chevron-left\"></span>&nbsp;&nbsp;{0}</a><ul class=\"dropdown-menu\">";
            string submenu_end = "</ul></li>";
            //
            SqlCommand cmd;
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            DataTable dt = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            // Если была ошибка - продолжать то к чему?
            if (Page.RouteData.Values["p_error"] != null && Page.RouteData.Values["p_error"].ToString() == "usernotfound")
                return;

            // Для отладки: если ВСЕГДА надо загружать данные из базы
            // Session["user_login"] = null;

            // Если пользователь пришел в первый раз
            if (Session["user_login"] == null)
            {
                if (ArchNet.Properties.Settings.Default.Test) Session["isTest"] = 1;
                // Достаем логин пользователя
                WinLogin = Context.User.Identity.Name.Trim();
                // Отсекаем домен.. возможно в будущем эту строчку надо будет убрать
                Session["user_winlogin"] = WinLogin = WinLogin.Substring(WinLogin.LastIndexOf('\\') + 1);
                if ((Application["BlockedUser"] ?? "").ToString() == Session["user_winlogin"].ToString()) return;
                // Пробиваем по базе логин
                conn.Open();
                cmd = new SqlCommand("SELECT a.id, a.login, a.name, a.mail FROM _user a WHERE a.del = 0 AND a.login = @p_login", conn);
                cmd.Parameters.AddWithValue("@p_login", WinLogin);
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                cmd.Dispose();

                // Если есть - читаем
                if (rdr.HasRows)
                {
                    // Определяем местоположение.. пока не нужно..
                    // но возможно файлы будут открываться с разных серверов
                    string _ip = (HttpContext.Current.Request.UserHostAddress.Length > 7 ? HttpContext.Current.Request.UserHostAddress.Substring(0, 8) : "");
                    Session["user_location"] = (_ip == "10.72.80" || _ip == "10.72.81") ? "NFK" : "MSK";

                    Session["user_id"] = rdr["id"];
                    Session["user_login"] = rdr["login"];
                    Session["user_name"] = rdr["name"];
                    Session["user_mail"] = rdr["mail"];

                    // Пользователи онлайн
                    //if (!(Application["ActiveUser"] as List<string>).Contains(rdr["name"].ToString())) (Application["ActiveUser"] as List<string>).Add(rdr["name"].ToString());

                    rdr.Close();

                    // Если есть логин в базе
                    // то возможно и есть доступ
                    // к одной из баз, ищем.. SelectUserRoleBase Session["user_id"]
                    _tmp =
                        "SELECT a.id_role,_base.id as baseid,_base.name as basename, _base.namerus AS basenamerus, _base.tabindex, _base.active " +
                        "FROM  _base " +
                        "LEFT JOIN ( SELECT id_role, id_base FROM _user_role_base WHERE del=0 AND id_user=@p_id_user) a ON a.id_base = _base.id " +
                        "WHERE _base.del = 0 ORDER BY _base.tabindex";
                    cmd = new SqlCommand(_tmp, conn);
                    cmd.Parameters.AddWithValue("@p_id_user", Session["user_id"].ToString());
                    rdr = cmd.ExecuteReader();

                    string menu_other = "", list_other = "";
                    _tmp = "";
                    while (rdr.Read())
                    {
                        _base_name = rdr["basename"].ToString(); // Имя базы
                        if (rdr["id_role"].ToString() != "")
                        {
                            Session[_base_name] = rdr["basenamerus"].ToString();
                            Session[_base_name + "_id"] = rdr["baseid"].ToString();
                            Session[_base_name + "_id_role"] = rdr["id_role"].ToString();
                            _tmp = String.Format(menu_item_enabled, GetRouteUrl("default", new { p_base = _base_name }), rdr["basenamerus"].ToString(), "gi gi-blank");
                            if ((bool)rdr["active"]) menu_enabled += _tmp;
                            else menu_other += _tmp;
                            _tmp = String.Format(list_item_enabled_blank, GetRouteUrl("default", new { p_base = _base_name }), rdr["basenamerus"].ToString(), "gi gi-blank");
                            if ((bool)rdr["active"]) list_enabled += _tmp;
                            else list_other += _tmp;
                        }
                        else
                        {
                            //if ((bool)rdr["active"])
                            //{
                            menu_disabled += String.Format(menu_item_disabled, rdr["basenamerus"].ToString(), "gi gi-lock");
                            list_disabled += String.Format(list_item_disabled, rdr["basenamerus"].ToString(), "gi gi-lock");
                            //}
                            //else
                            //{
                            //    menu_other += String.Format(menu_item_disabled, rdr["basenamerus"].ToString(), "gi gi-lock");
                            //    list_other += String.Format(list_item_disabled, rdr["basenamerus"].ToString(), "gi gi-lock");
                            //}
                        }
                    }
                    Session["menubase"] =
                        "<li role='presentation' class='dropdown-header'>Доступные базы:</li>" + menu_enabled +
                        "<li role='presentation' class='dropdown-header'>Другие:</li>" + menu_other + menu_disabled;

                    Session["listbase"] = "<a href='#' class='list-group-item' style='padding:5px 0px 0px 10px;color: #007cb0;font-weight: 600;'>Доступные:</a>" + list_enabled +
                        "<a href='#' class='list-group-item' style='padding:5px 0px 0px 10px;color: #007cb0;font-weight: 600;'>Другие:</a>" + list_other + list_disabled;// Тут храним HTML код списка баз на странице выбора

                    // Session["menuother"] = menu_enabled + menu_disabled; // Тут храним HTML код выпадающего списка баз
                    // Session["listother"] = list_enabled + list_disabled; // Тут храним HTML код списка баз на странице выбора
                    rdr.Close();
                    cmd.Dispose();
                    // Грузим общие доступа
                    _tmp = "SELECT b.name  FROM _user_access a left join _access b ON a.id_access=b.id where a.id_user=@p_id_user AND a.del=0";
                    cmd = new SqlCommand(_tmp, conn);
                    cmd.Parameters.AddWithValue("@p_id_user", Session["user_id"].ToString());
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                        Session["common_access_" + rdr["name"].ToString()] = 1;
                    rdr.Close();
                    cmd.Dispose();
                    if (Session["common_access_admin_menu"] != null)
                        Session["menuadmin"] =
                             String.Format(submenu_begin, "Пользователи") +
                            (Session["common_access_admin_user_view"] != null || Session["common_access_admin_user_edit"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("user", new { }), "Пользователи", "gi gi-group") : String.Format(menu_item_disabled, "Пользователи", "gi gi-group")) +
                            (Session["common_access_admin_role_view"] != null || Session["common_access_admin_role_edit"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("role", new { }), "Роли", "gi gi-keys") : String.Format(menu_item_disabled, "Роли", "gi gi-keys")) +
                            (Session["common_access_admin_ad"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("ad", new { }), "Синхронизация с AD", "gi gi-roundabout") : String.Format(menu_item_disabled, "Синхронизация с AD", "gi gi-roundabout")) +
                            (Session["common_access_admin_access_view"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("access", new { }), "Ключи доступа", "gi gi-blank") : String.Format(menu_item_disabled, "Ключи доступа", "gi gi-blank")) +
                            (Session["common_access_admin_user_view"] != null || Session["common_access_admin_user_edit"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("userrolebase", new { }), "Доступы пользователей (Отчет)", "gi gi-blank") : String.Format(menu_item_disabled, "Доступы пользователей (Отчет)", "gi gi-blank")) +
                            (Session["common_access_admin_user_view"] != null || Session["common_access_admin_user_edit"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("uservisit", new { }), "Лог авторизации", "gi gi-blank") : String.Format(menu_item_disabled, "Лог авторизации", "gi gi-blank")) +
                            (Session["common_access_admin_user_view"] != null || Session["common_access_admin_user_edit"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("usersetting", new { }), "Настройки пользователей", "gi gi-blank") : String.Format(menu_item_disabled, "Настройки пользователей", "gi gi-blank")) +
                            //"<li role=\"presentation\" class=\"divider\"></li>" +
                            submenu_end +
                            String.Format(submenu_begin, "Эффективность") +
                            (Session["common_access_admin_rating_view"] != null || Session["common_access_admin_rating_edit"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("rating", new { }), "Эффективность (Отчет)", "gi gi-charts") : String.Format(menu_item_disabled, "Эффективность (Отчет)", "gi gi-charts")) +
                            (Session["common_access_admin_rating_view"] != null || Session["common_access_admin_rating_edit"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("normhour", new { }), "Нормы рабочего времени", "gi gi-blank") : String.Format(menu_item_disabled, "Нормы рабочего времени", "gi gi-blank")) +
                             (Session["common_access_admin_rating_view"] != null || Session["common_access_admin_rating_edit"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("workhour", new { }), "Отработанное время", "gi gi-blank") : String.Format(menu_item_disabled, "Отработанное время", "gi gi-blank")) +
                             (Session["common_access_admin_rating_view"] != null || Session["common_access_admin_rating_edit"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("score", new { }), "Баллы", "gi gi-blank") : String.Format(menu_item_disabled, "Баллы", "gi gi-blank")) +
                             (Session["common_access_admin_rating_view"] != null || Session["common_access_admin_rating_edit"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("field", new { }), "Цена полей", "gi gi-blank") : String.Format(menu_item_disabled, "Цена полей", "gi gi-blank")) +
                             submenu_end +
                           // "<li role=\"presentation\" class=\"divider\"></li>" +
                           String.Format(submenu_begin, "Служебные") +

                            // "<li role=\"presentation\" class=\"divider\"></li>" +
                            (Session["common_access_admin_journal_common"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("journalcommon", new { }), "Общий журнал", "gi gi-history") : String.Format(menu_item_disabled, "Общий журнал", "gi gi-history")) +
                             String.Format(menu_item_enabled_blank, GetRouteUrl("sesval", new { }), "Сессия", "gi gi-cogwheel") +
                            //(Session["common_access_admin_sesval"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("cc", new { }), "Очистить кэш", "gi gi-blank") : String.Format(menu_item_disabled, "Очистить кэш", "gi gi-blank")) +
                            (Session["common_access_admin_base_view"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("base", new { }), "Список баз", "gi gi-blank") : String.Format(menu_item_disabled, "Список баз", "gi gi-blank")) +
                            (Session["common_access_admin_table_view"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("table", new { }), "Список таблиц", "gi gi-blank") : String.Format(menu_item_disabled, "Список таблиц", "gi gi-blank")) +
                            (Session["common_access_admin_delete_old_files"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("deleteoldfiles", new { }), "Удаление устаревших файлов", "gi gi-blank") : String.Format(menu_item_disabled, "Удаление устаревших файлов", "gi gi-blank")) +
                            (Session["common_access_admin_ad"] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl("twit", new { }), "Сообщение всем", "gi gi-blank") : String.Format(menu_item_disabled, "Сообщение всем", "gi gi-blank")) +
                            submenu_end;
                    // Пишем в лог посещений

                    //cmd = new SqlCommand("INSERT INTO _user_visit ([id_user],[when]) VALUES(@p_id_user,GETDATE())", conn);
                    //cmd.Parameters.AddWithValue("@p_id_user", Session["user_id"].ToString());
                    //cmd.ExecuteNonQuery();
                    //cmd.Dispose();

                    faFunc.ToLog(1);

                    _tmp = "";
                    //bool allow = (Session["zao_stg_access_complect_view"] != null || Session["zao_stg_access_complect_edit"] != null);
                    //if (allow)
                    _tmp += String.Format(menu_item_enabled, GetRouteUrl("complectnew", new { }), "Комплекты", "gi gi-folder_closed");
                    _tmp += String.Format(menu_item_enabled, GetRouteUrl("complectnewlist", new { }), "Спецификации комплектов", "gi gi-blank");
                    _tmp += String.Format(menu_item_enabled, GetRouteUrl("complectnewlistarchive", new { }), "Спецификации комплектов (полный)", "gi gi-blank");
                    _tmp += "<li role=\"presentation\" class=\"divider\"></li>";
                    _tmp += String.Format(menu_item_enabled, GetRouteUrl("complectrepair", new { }), "Комплекты движения документов", "gi gi-folder_new");
                    _tmp += String.Format(menu_item_enabled, GetRouteUrl("complectrepairlist", new { }), "Движение документов", "gi gi-blank");
                    _tmp += "<li role=\"presentation\" class=\"divider\"></li>";
                    _tmp += String.Format(menu_item_enabled, GetRouteUrl("complect", new { }), "Комплекты (старые)", "gi gi-blank");
                    _tmp += String.Format(menu_item_enabled, GetRouteUrl("complectlist", new { }), "Спецификации комплектов (старых)", "gi gi-blank");

                    Session["menucomplect"] = _tmp;

                    _tmp = "";
                    _tmp += String.Format(menu_item_enabled, GetRouteUrl("regstor", new { }), "Регистр хранения дел", "gi gi-blank");
                    _tmp += String.Format(menu_item_enabled, GetRouteUrl("regstorlist", new { }), "Документационный фонд", "gi gi-blank");
                    Session["menurealarchive"] = _tmp;

                    _tmp = "";
                    //bool allow = (Session["zao_stg_access_complect_view"] != null || Session["zao_stg_access_complect_edit"] != null);
                    //if (allow)

                    _tmp += String.Format(list_item_enabled_blank, GetRouteUrl("complectnew", new { }), "Комплекты", "gi gi-folder_closed");
                    _tmp += String.Format(list_item_enabled_blank, GetRouteUrl("complectnewlist", new { }), "Спецификации комплектов", "gi gi-blank");
                    _tmp += String.Format(list_item_enabled_blank, GetRouteUrl("complectrepair", new { }), "Комплекты движения документов", "gi gi-folder_new");
                    _tmp += String.Format(list_item_enabled_blank, GetRouteUrl("complectrepairlist", new { }), "Движение документов", "gi gi-blank");
                    _tmp += String.Format(list_item_enabled_blank, GetRouteUrl("complect", new { }), "Комплекты (Старые)", "gi gi-blank");
                    _tmp += String.Format(list_item_enabled_blank, GetRouteUrl("complectlist", new { }), "Спецификации комплектов (старых)", "gi gi-blank");

                    Session["listcomplect"] = _tmp;

                    _tmp = "";
                    _tmp += String.Format(list_item_enabled_blank, GetRouteUrl("regstor", new { }), "Регистр хранения дел", "gi gi-blank");
                    _tmp += String.Format(list_item_enabled_blank, GetRouteUrl("regstorlist", new { }), "Документационный фонд", "gi gi-blank");
                    Session["listrealarchive"] = _tmp;

                    // Меню справочников (общие - нет привязки к конкретной базе, добавляется к меню конкретной базы)
                    string[] common_sprav = { "frm", "prjcode", "doctree", "storage", "regstor_group", "event" };
                    string[] common_spravru = { "Фирмы", "Коды проектов", "Формы документов", "Места хранения", "Группы (Регистр хранения дел)", "События (Поступление оригиналов)" };
                    menu_enabled = "";
                    list_enabled = "";
                    /* По умолчанию, доступ к справочникам есть у всех, права выдаются только на редактирование*/
                    for (int i = 0; i < common_sprav.Length; i++)
                    {
                        menu_enabled += String.Format(menu_item_enabled, GetRouteUrl(common_sprav[i], new { }), common_spravru[i], "gi gi-blank");
                        list_enabled += String.Format(list_item_enabled_blank, GetRouteUrl(common_sprav[i], new { }), common_spravru[i], "gi gi-blank");
                    }
                    Session["menusprav"] = menu_enabled;
                    Session["listsprav"] = list_enabled;

                    ///////////

                    // Меню сервисов (общие - нет привязки к конкретной базе, добавляется к меню конкретной базы)
                    string[] common_service = { "mailgroup", "userbarcode", "shutdown", "log", "monitor" };
                    string[] common_serviceru = { "Группы рассылок", "Штрих-коды исполнителей", "Перезапустить текущий сеанс", "Лог", "Поступление оригиналов" };
                    string[] common_serviceicon = { "gi gi-envelope", "gi gi-barcode", "gi gi-rotation_lock", "gi gi-history", "gi gi-blank" };
                    menu_enabled = "";
                    list_enabled = "";

                    for (int i = 0; i < common_service.Length; i++)
                    {
                        if (Session["common_access_service_" + common_service[i]] != null || i == 2 || i == 3 || i == 4)
                        {
                            menu_enabled += String.Format(menu_item_enabled, GetRouteUrl(common_service[i], new { p_base = cur_basename }), common_serviceru[i], common_serviceicon[i]);
                            //list_enabled += String.Format(list_item_enabled, GetRouteUrl(common_service[i], new { p_base = cur_basename }), common_serviceru[i], common_serviceicon[i]);
                        }
                        else
                        {
                            menu_enabled += String.Format(menu_item_disabled, common_serviceru[i], common_serviceicon[i]);
                            //list_enabled += String.Format(list_item_disabled, common_serviceru[i], common_serviceicon[i]);
                        }
                    }

                    Session["menuservice"] = menu_enabled;
                    //Session["listservice"] = list_enabled;
                }
                // Если нет - накуй его
                else
                {
                    rdr.Close();
                    conn.Close();
                    //Response.Redirect(GetRouteUrl("error", new { p_base = "wtf", p_error = "usernotfound" }));
                    // Response.Clear();
                    Response.Write(faFunc.Alert(faAlert.SiteAccessDenied));
                    //Response.End();
                }
                rdr.Close();
                conn.Close();
            }//Session["user_login"]

            // Получаем параметр текущей базы данных
            // Достаем доступа и настройки относящиеся к этой базе

            cur_basename = Page.RouteData.Values["p_base"] != null ? Page.RouteData.Values["p_base"].ToString() : "";
            if (cur_basename != "error" && Session["user_login"] != null)
            {
                if (Session["NewChangeLogCount"] == null)
                {
                    // Достаем id последней прочитанной новости
                    int last_msg_id = 0;
                    int.TryParse(faFunc.GetUserSetting("last_msg_id"), out last_msg_id);

                    // Достаем общее количество новостей
                    int all_change_log_count = 0;
                    int.TryParse(faFunc.GetSiteSetting("all_change_log_count"), out all_change_log_count);

                    Session["NewChangeLogCount"] = all_change_log_count - last_msg_id;
                }

                //if ((int)(Session["NewChangeLogCount"] ?? 0) > 0)
                //{
                //   NoticeButton =
                //        "<button class=\"btn btn-danger btn-xs\" type=\"button\" onclick=\"location.href='" + GetRouteUrl("blog", new { }) + "';\"" +
                //        " onmouseover=\"this.firstChild.data='Просмотреть последние изменения ';\"  onmouseout=\"this.firstChild.data='';\"> " +
                //        "<span class=\"badge\">" + Session["NewChangeLogCount"].ToString() + "</span>" +
                //        "</button>";
                //}

                // Открытие дополнительной вкладки со списком изменений если есть новые
                OpenHelp = ((int)(Session["NewChangeLogCount"] ?? 0) > 0) ? "<script type='text/javascript'>window.open('" + GetRouteUrl("blog", new { }) + "', 'История изменений');</script> " : "";

                // Если пустой - кидаем на страницу выбора баз
                //if (cur_basename == "") Response.Redirect(GetRouteUrl("default", new { p_base = "dbselect" }));
                // Если указана какая то база - проверяем доступ к ней
                if (cur_basename != "" && cur_basename != "dbselect" && Session[cur_basename + "_loaded"] == null)
                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT id FROM _base WHERE name=@p_basename and del=0", conn);
                    cmd.Parameters.AddWithValue("@p_basename", cur_basename);
                    sqlDataAdapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sqlDataAdapter.Fill(dt);
                    cur_id_base = dt.Rows[0]["id"].ToString();
                    if (cur_id_base == "")
                        Response.Redirect(GetRouteUrl("default", new
                        {
                            p_base = "dbselect"
                        }));

                    _tmp =
                         "SELECT c.name as akey " +
                         "FROM _user_role_base a " +
                         "LEFT JOIN _role_access b on a.id_role=b.id_role and b.del=0 " +
                         "LEFT JOIN _access c on b.id_access=c.id " +
                         "WHERE a.id_user=@p_id_user AND a.id_base=@p_id_base AND a.del=0 ";
                    cmd = new SqlCommand(_tmp, conn);
                    cmd.Parameters.AddWithValue("@p_id_user", Session["user_id"].ToString());
                    cmd.Parameters.AddWithValue("@p_id_base", cur_id_base);
                    sqlDataAdapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sqlDataAdapter.Fill(dt);
                    foreach (DataRow r in dt.Rows)
                        Session[cur_basename + "_access_" + r["akey"].ToString()] = 1;
                    cmd.Dispose();

                    _tmp =
                         "SELECT b.id_table,c.name as [table], b.value FROM _user_role_base a " +
                         "JOIN _role_where b on a.id_role=b.id_role AND b.del=0 " +
                         "JOIN _table c on b.id_table=c.id AND c.del=0 " +
                         "WHERE a.id_user=@p_id_user AND a.id_base=@p_id_base AND a.del=0 ";
                    cmd = new SqlCommand(_tmp, conn);
                    cmd.Parameters.AddWithValue("@p_id_user", Session["user_id"].ToString());
                    cmd.Parameters.AddWithValue("@p_id_base", cur_id_base);
                    sqlDataAdapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sqlDataAdapter.Fill(dt);
                    foreach (DataRow r in dt.Rows)
                        Session[cur_basename + "_role_where_" + r["table"].ToString()] = r["value"].ToString();
                    cmd.Dispose();

                    // Меню разделов
                    string _menu = "", _list = "";
                    menu_enabled = menu_disabled = "";
                    list_enabled = list_disabled = "";
                    string[] _p = { "acc", "dog", "ord", "oth", "empl", "ohs", "tech", "bank", "norm" };
                    foreach (string name in _p)//Enum.GetNames(typeof(faPage))
                    {
                        _tmp = faFunc.GetDocTypeName(name);
                        if (Session[cur_basename + "_access_archive_" + name + "_view"] != null || Session[cur_basename + "_access_archive_" + name + "_edit"] != null)
                        {
                            menu_enabled += String.Format(menu_item_enabled, GetRouteUrl("archive", new { p_base = cur_basename, p_page = name }), _tmp, "gi gi-blank");
                            list_enabled += String.Format(list_item_enabled, GetRouteUrl("archive", new { p_base = cur_basename, p_page = name }), _tmp, "gi gi-blank");
                        }
                        else
                        {
                            menu_disabled += String.Format(menu_item_disabled, _tmp, "gi gi-lock");
                            list_disabled += String.Format(list_item_disabled, _tmp, "gi gi-lock");
                        }
                    }

                    _menu = String.Format(menu_item_enabled, GetRouteUrl("archive", new { p_base = cur_basename, p_page = "srch" }), faFunc.GetDocTypeName("srch"), "gi gi-search") +
                       "<li role='presentation' class='dropdown-header'>Тип документа:</li>" + menu_enabled + menu_disabled + "<li role='presentation' class='dropdown-header'>Другое:</li>";

                    _list = "<a href=\"#\" class=\"list-group-item active\" style=\"font-size:15px;\">Архив (" + (Session[cur_basename] ?? "").ToString() + ")</a>" +
                        String.Format(list_item_enabled, GetRouteUrl("archive", new { p_base = cur_basename, p_page = "srch" }), faFunc.GetDocTypeName("srch"), "gi gi-search") +
                         "<a href='#' class='list-group-item' style='padding:5px 0px 0px 10px;color: #007cb0;font-weight: 600;'>Тип документа:</a>" + list_enabled + list_disabled +
                         "<a href='#' class='list-group-item' style='padding:5px 0px 0px 10px;color: #007cb0;font-weight: 600;'>Другое:</a>";

                    if (Session[cur_basename + "_access_docversion_view"] != null || Session[cur_basename + "_access_docversion_edit"] != null)
                    {
                        _menu += String.Format(menu_item_enabled, GetRouteUrl("docversion", new { p_base = cur_basename }), "Версии", "gi gi-blank");
                        _list += String.Format(list_item_enabled, GetRouteUrl("docversion", new { p_base = cur_basename }), "Версии", "gi gi-blank");
                    }
                    else
                    {
                        _menu += String.Format(menu_item_disabled, "Версии", "gi gi-lock");
                        _list += String.Format(list_item_disabled, "Версии", "gi gi-lock");
                    }

                    // _menu += String.Format(menu_item_enabled, GetRouteUrl("archivedepart", new { p_base = cur_basename }), "Подразделения", "gi gi-blank");
                    // _list += String.Format(list_item_enabled, GetRouteUrl("archivedepart", new { p_base = cur_basename }), "Подразделения", "gi gi-blank");
                    if (Session[cur_basename + "_access_archive_acc_edit"] != null)
                    {
                        _menu += String.Format(menu_item_enabled, GetRouteUrl("archivedel", new { p_base = cur_basename, p_page = "srch" }), "Удаленные документы", "gi gi-blank");
                        _list += String.Format(list_item_enabled, GetRouteUrl("archivedel", new { p_base = cur_basename, p_page = "srch" }), "Удаленные документы", "gi gi-blank");
                        if (cur_basename == "zao_stg")
                        {
                            _menu += String.Format(menu_item_enabled, GetRouteUrl("archivedub", new { p_base = cur_basename }), "Дубликаты документов", "gi gi-blank");
                            _list += String.Format(list_item_enabled, GetRouteUrl("archivedub", new { p_base = cur_basename }), "Дубликаты документов", "gi gi-blank");
                        }
                    }
                    else
                    {
                        _menu += String.Format(menu_item_disabled, "Удаленные документы", "gi gi-lock");
                        _list += String.Format(list_item_disabled, "Удаленные документы", "gi gi-lock");

                        _menu += String.Format(menu_item_disabled, "Дубликаты документов", "gi gi-lock");
                        _list += String.Format(list_item_disabled, "Дубликаты документов", "gi gi-lock");
                    }

                    //if (Session[cur_basename + "_access_structur_view"] != null)
                    //{
                    //    _menu += String.Format(menu_item_enabled, GetRouteUrl("archivestructur", new { p_base = cur_basename }), "Структура", "gi gi-git_merge");
                    //    _list += String.Format(list_item_enabled, GetRouteUrl("archivestructur", new { p_base = cur_basename }), "Структура", "gi gi-git_merge");
                    //}
                    //else
                    //{
                    //    _menu += String.Format(menu_item_disabled, "Структура", "gi gi-git_merge");
                    //    _list += String.Format(list_item_disabled, "Структура", "gi gi-git_merge");
                    //}

                    Session[cur_basename + "_menupage"] = _menu;
                    Session[cur_basename + "_listpage"] = _list;

                    // Меню справочников
                    string[] sprav = { "person", "departmentpre" };//"country", "region", "town",
                    string[] spravru = { "Сотрудники", "Подразделения" };//"Страны", "Регионы", "Населенные пункты",
                    menu_enabled = ""; list_enabled = "";
                    /* По умолчанию, доступ к справочникам есть у всех, права выдаются только на редактирование*/
                    for (int i = 0; i < sprav.Length; i++)
                    {
                        menu_enabled += String.Format(menu_item_enabled_blank, GetRouteUrl(sprav[i], new { p_base = cur_basename }), spravru[i], "gi gi-blank");
                        list_enabled += String.Format(list_item_enabled_blank, GetRouteUrl(sprav[i], new { p_base = cur_basename }), spravru[i], "gi gi-blank");
                    }
                    Session[cur_basename + "_menusprav"] = menu_enabled;
                    Session[cur_basename + "_listsprav"] = list_enabled;
                    // Меню сервис
                    string[] service = { "journal", "settings", "fillbybarcode", "archiveperform", "archivecheckbox", "checkbybarcode", "barcodesearch", "archivedocpackdown" };
                    string[] serviceru = { "Журнал изменений", "Настройки", "Подготовка описи архивного дела", "Загрузка проведенных", "Отчет по ошибкам операторов ОЦ", "Анализ документов по штрихкоду", "Поиск документов по штрихкоду", "Выгрузка документов" };
                    string[] serviceicon = { "gi gi-history", "gi gi-settings", "gi gi-notes", "gi gi-inbox_in", "gi gi-check", "gi gi-blank", "gi gi-blank", "gi gi-blank" };
                    menu_enabled = "";

                    //for (int i = 0; i < service.Length; i++)
                    //{
                    menu_enabled += Session[cur_basename + "_access_service_" + service[0]] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl(service[0], new
                    {
                        p_base = cur_basename
                    }), serviceru[0], serviceicon[0]) : String.Format(menu_item_disabled, serviceru[0], "gi gi-lock");
                    menu_enabled += String.Format(menu_item_enabled_blank, GetRouteUrl(service[1], new
                    {
                        p_base = cur_basename
                    }), serviceru[1], serviceicon[1]);

                    menu_enabled += Session[cur_basename + "_access_service_" + service[2]] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl(service[2], new
                    {
                        p_base = cur_basename
                    }), serviceru[2], serviceicon[2]) : String.Format(menu_item_disabled, serviceru[2], "gi gi-lock");

                    menu_enabled += Session["common_access_service_" + service[3]] != null ? String.Format(menu_item_enabled_blank, GetRouteUrl(service[3], new
                    {
                        p_base = cur_basename
                    }), serviceru[3], serviceicon[3]) : String.Format(menu_item_disabled, serviceru[3], "gi gi-lock");

                    menu_enabled += String.Format(menu_item_enabled_blank, GetRouteUrl(service[4], new { p_base = cur_basename }), serviceru[4], serviceicon[4]);
                    menu_enabled += String.Format(menu_item_enabled_blank, GetRouteUrl(service[5], new { p_base = cur_basename }), serviceru[5], serviceicon[5]);
                    menu_enabled += String.Format(menu_item_enabled_blank, GetRouteUrl(service[6], new { p_base = cur_basename }), serviceru[6], serviceicon[6]);

                    menu_enabled += Session[cur_basename + "_access_archive_srch_edit"] != null || Session[cur_basename + "_access_archive_srch_view"] != null ?
                        string.Format(menu_item_enabled_blank, GetRouteUrl(service[7], new { p_base = cur_basename }), serviceru[7], serviceicon[7]) :
                        String.Format(menu_item_disabled, serviceru[7], "gi gi-lock");
                    //}

                    Session[cur_basename + "_menuservice"] = menu_enabled;

                    Session[cur_basename + "_loaded"] = 1;

                    conn.Close();
                }
            }
        }
    }
}