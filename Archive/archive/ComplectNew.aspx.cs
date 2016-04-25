using System;

namespace WebArchiveR6
{
    public partial class ComplectNew : System.Web.UI.Page
    {
        public faListComplect list;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;

            list = new faListComplect();
            list.RouteName = "complectnew";

            //if (true)//(Session[Master.cur_basename + "_access_complect_view"] != null || Session[Master.cur_basename + "_access_complect_edit"] != null || list.Page == faPage.select)
            //{
            list.IDBase = "0";// Session[Master.cur_basename + "_id"].ToString();
            list.BaseName = "";// Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;
            list.ShowArrows = true;
            list.EditFormWidth = 1100;
            list.EditFormHeight = 350;
            list.ActionMenuItems.Add("Поиск в Документационном фонде", "window.open('/complectregstorcompare?id=" + (Request.QueryString["id"] ?? "").ToString() + "&id_base='+$('#_complectnew_id_base').val());");

            // Шапка
            cur = new faCursor("_complectnew");
            //cur.AliasAlt = cur.Alias + "_" + list.Page;
            cur.TableID = 26;
            cur.Caption = "Архив / Комплекты";
            cur.EnableViewButton = cur.EnableExcelButton = true;
            cur.EnableFileButton = false; cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = true;//Session[Master.cur_basename + "_access_complect_edit"] != null;

            list.JSReadyList.Add("hr", "$('#_complectnew_date_upd').parent().after('<hr style=\"border-top: 1px solid #428BCA;\"/>');$('#_complectnew_name').parent().after('<hr style=\"border-top: 1px solid #428BCA;\"/>');$('#_complectnew_prim').parent().after('<hr style=\"border-top: 1px solid #428BCA;\"/>');");
            list.JSReadyList.Add("clear_inet", "if($('#_complectnew_inet').val()=='4'){$('#_complectnew_inet').val('0');$('#cph__complectnew_inet').val('');}");

            if (Session[cur.Alias + "_sortcolumn"] == null)
            {
                cur.SortColumn = "id";
                cur.SortDirection = "desc";
            }

            #region Поля

            //ID
            fld = new faField();
            fld.Data.FieldName = "id";
            fld.View.CaptionShort = "ID";
            fld.View.TextAlign = "center";
            fld.View.Width = 68;
            fld.Edit.Enable = false;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            //Дата Создания
            fld = new faField();
            fld.Data.FieldName = "date_reg";
            fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
            fld.View.TextAlign = "center";
            fld.View.Hint = "Дата создания";
            fld.View.CaptionShort = "Дата созд.";
            fld.View.Width = 98;
            fld.Edit.Enable = false;
            fld.Edit.Control = faControl.DateTimePicker;
            fld.Edit.Auto = faAutoType.NowDateTime;
            fld.Edit.AddOnly = true;
            fld.Filter.Control = faControl.DatePicker;
            cur.AddField(fld);

            //Редактор
            fld = new faField();
            fld.Data.FieldName = "id_editor";
            fld.View.Width = 185;
            fld.View.CaptionShort = "Редактировал";
            fld.Edit.Enable = false;
            fld.Edit.Auto = faAutoType.UserID;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_user";
            fld.LookUp.TableAlias = "user2";
            cur.AddField(fld);

            //Дата Редактирования
            fld = new faField();
            fld.Data.FieldName = "date_upd";
            fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
            fld.View.TextAlign = "center";
            fld.View.Hint = "Дата редактирования";
            fld.View.CaptionShort = "Дата ред.";
            fld.View.Width = 98;
            fld.Edit.Enable = false;
            fld.Edit.Auto = faAutoType.NowDateTime;
            fld.Filter.Control = faControl.DateTimePicker;
            cur.AddField(fld);

            //-------

            //Организация
            fld = new faField();
            fld.Data.FieldName = "id_base";
            fld.View.CaptionShort = "Организация";
            fld.View.Width = 150;
            fld.Edit.Required = true;
            fld.Edit.DefaultValue = "1";
            fld.Edit.DefaultText = "ЗАО «Стройтрансгаз»";
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Caption = "Организация.";
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "namerus";
            fld.LookUp.Table = "_base";
            cur.AddField(fld);

            // Источник
            fld = new faField();
            fld.Data.FieldName = "inet";
            fld.View.CaptionShort = "Источник";
            fld.View.Width = 150;
            fld.View.TextAlign = "center";
            fld.Edit.Control = faControl.DropDown;
            fld.Edit.Required = true;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_source2";
            fld.LookUp.Where = "id<>1 AND id<>4";
            cur.AddField(fld);

            //Исполнитель
            fld = new faField();
            fld.Data.FieldName = "id_perf";
            fld.Data.Where = Session["common_access_complect_full"] == null ? "id_perf=" + Session["user_id"].ToString() + " OR a.id_sign=" + Session["user_id"].ToString() : "";
            fld.View.CaptionShort = "Исполнитель";
            fld.View.Width = 180;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Edit.DefaultValue = Session["user_id"].ToString();
            fld.Edit.DefaultText = Session["user_name"].ToString();
            //fld.Edit.Required = true;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_user";
            fld.LookUp.TableAlias = "user3";
            fld.Filter.Control = faControl.AutoComplete;

            cur.AddField(fld);

            // Важный
            fld = new faField();
            fld.Data.FieldName = "important";
            fld.View.CaptionShort = "Важный";
            fld.View.Width = 90;
            fld.View.TextAlign = "center";
            fld.Edit.Control = faControl.DropDown;
            fld.Edit.DefaultValue = "1";
            fld.Edit.DefaultText = "Нет";
            fld.Edit.Required = true;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_yesno";
            fld.LookUp.TableAlias = "yn3";
            cur.AddField(fld);

            // Имя
            fld = new faField();
            fld.Data.FieldName = "name";
            fld.View.CaptionShort = "Имя";
            fld.View.Width = 180;
            fld.Edit.Max = 150;
            fld.Edit.Required = true;
            fld.Edit.DefaultText = DateTime.Now.ToString("ddMMyyyy") + "_";
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            //// Скрытый (бывш. Тип документа)

            //fld = new faField();
            //fld.Data.FieldName = "id_doctype_complect";
            //fld.View.CaptionShort = "Скрытый";
            //fld.View.Width = 75;
            //fld.Edit.Required = true;
            //fld.Edit.DefaultValue = "1";
            //fld.Edit.DefaultText = "Нет";
            //fld.Edit.Enable = (Session["common_access_complect_bank"] ?? "").ToString() == "1";
            //fld.Edit.Control = faControl.DropDown;
            //fld.Filter.Control = faControl.DropDown;
            //fld.LookUp.Key = "id";
            //fld.LookUp.Field = "name";
            //fld.LookUp.Table = "_yesno";
            //cur.AddField(fld);

            //-------

            //Обработан
            fld = new faField();
            fld.Data.FieldName = "processed";
            fld.View.CaptionShort = "Обработан";
            fld.View.Width = 68;
            fld.View.TextAlign = "center";
            fld.Edit.Control = faControl.DropDown;
            fld.Edit.DefaultValue = "1";
            fld.Edit.DefaultText = "Нет";
            fld.Edit.Required = true;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_yesno";
            fld.LookUp.TableAlias = "yn1";
            cur.AddField(fld);

            // Оператор ОЦ
            fld = new faField();
            fld.Data.FieldName = "id_operator_oc";
            fld.View.Width = 185;
            fld.View.CaptionShort = "Оператор ОЦ";
            fld.Edit.Enable = true;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_user";
            fld.LookUp.TableAlias = "user4";
            cur.AddField(fld);

            //Создал / Оператор ТП
            fld = new faField();
            fld.Data.FieldName = "id_creator";
            fld.View.Width = 185;
            fld.View.CaptionShort = "Оператор ТП";
            fld.Edit.Enable = true;
            fld.Edit.AddOnly = true;
            fld.Edit.Auto = faAutoType.UserID;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_user";
            fld.LookUp.TableAlias = "user1";
            cur.AddField(fld);

            // Примечание
            fld = new faField();
            fld.Data.FieldName = "prim";
            fld.View.CaptionShort = "Примечание";
            fld.View.Width = 180;
            fld.Edit.Required = false;
            fld.Edit.Max = 240;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            // Кол-во док.
            fld = new faField();
            fld.Data.FieldName = "doccount";
            fld.View.CaptionShort = "Кол-во док.";
            fld.View.Width = 80;
            fld.Edit.Enable = false;
            fld.Edit.Visible = false;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            // Кто подписал
            fld = new faField();
            fld.Data.FieldName = "id_sign";
            fld.View.CaptionShort = "Подписал";
            fld.View.Width = 185;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Edit.Copied = false;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_user";
            fld.LookUp.TableAlias = "user5";
            fld.Filter.Control = faControl.AutoComplete;
            cur.AddField(fld);

            // Номер версии
            fld = new faField();
            fld.Data.FieldName = "date_reg_overdue";
            fld.Data.FieldCalc = " (CASE WHEN (processed=1 AND (DATEADD(day,3,date_reg) - GETDATE())<0) THEN 1 ELSE 0 END )";
            fld.View.CaptionShort = "Просрочено";
            fld.View.Visible = false;
            fld.Edit.Visible = false;
            fld.Edit.Enable = false;
            fld.Edit.Control = faControl.TextBox;
            fld.Filter.Enable = false;
            cur.AddField(fld);

            // Уведомления
            fld = new faField();
            fld.Data.FieldName = "perf_mail";
            fld.View.CaptionShort = "Отправлено исполнителю";
            fld.View.Visible = false;
            fld.Edit.Visible = false;
            fld.Edit.Enable = false;
            fld.Filter.Enable = false;
            cur.AddField(fld);

            //// Уведомления
            //fld = new faField();
            //fld.Data.FieldName = "buh_mail";
            //fld.View.Visible = false;
            //fld.Edit.Visible = false;
            //fld.Edit.Enable = false;
            //fld.Filter.Enable = false;
            //cur.AddField(fld);

            //// Уведомления
            //fld = new faField();
            //fld.Data.FieldName = "oc_mail";
            //fld.View.Visible = false;
            //fld.Edit.Visible = false;
            //fld.Edit.Enable = false;
            //fld.Filter.Enable = false;
            //cur.AddField(fld);

            //// Уведомления
            //fld = new faField();
            //fld.Data.FieldName = "term_mail";
            //fld.View.Visible = false;
            //fld.Edit.Visible = false;
            //fld.Edit.Enable = false;
            //fld.Filter.Enable = false;
            //cur.AddField(fld);

            // Когда подписал
            fld = new faField();
            fld.Data.FieldName = "whensign";
            fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
            fld.View.CaptionShort = "Когда подписано";
            fld.View.Visible = false;
            fld.Edit.Enable = true;
            fld.Edit.Copied = false;
            fld.Edit.Control = faControl.DateTimePicker;
            fld.Filter.Enable = false;
            cur.AddField(fld);

            #endregion Поля

            list.MainCursor = cur;

            if (Request.QueryString["id"] != null || Request.QueryString["act"] != null)
            {
                #region Курсор

                if (true)//(Session[Master.cur_basename + "_access_complect_view"] != null || Session[Master.cur_basename + "_access_complect_edit"] != null)
                {
                    cur = new faCursor("_complectnew_list");
                    cur.TableID = 27;
                    cur.Caption = "Документы";
                    cur.EnableViewButton = true;
                    cur.EnableFileButton = false;
                    cur.EnableAddButton = cur.EnableEditButton = false;
                    cur.EnableDelButton = cur.EnableSaveButton = true;// Session[Master.cur_basename + "_access_complect_edit"] != null;// Session[Master.cur_basename + "_access_docversion_edit"] != null;
                    cur.ShowPager = false;
                    cur.SortColumn = "id";
                    cur.SortDirection = "desc";

                    // Поля
                    // ID
                    fld = new faField();
                    fld.Data.FieldName = "id";
                    fld.View.CaptionShort = "ID";
                    fld.View.TextAlign = "center";
                    fld.View.Width = 50;
                    fld.Edit.Enable = false;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);

                    // Связывающее поле
                    fld = new faField();
                    fld.Data.FieldName = "id_complectnew";
                    fld.Filter.Control = faControl.TextBoxInteger;
                    fld.Filter.Enable = true;
                    fld.View.Visible = false;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    cur.AddField(fld);

                    ////Обработан
                    //fld = new faField();
                    //fld.Data.FieldCalc = "(CASE WHEN a.id_archive > 0 THEN 2 ELSE 1 END)";
                    //fld.Data.FieldName = "processed";
                    //fld.View.CaptionShort = "Обработан";
                    //fld.View.Width = 68;
                    //fld.View.TextAlign = "center";
                    //fld.Edit.Visible = false;
                    //fld.Edit.Enable = false;
                    //fld.Edit.Control = faControl.DropDown;
                    //fld.Filter.Control = faControl.DropDown;
                    //fld.LookUp.Key = "id";
                    //fld.LookUp.Field = "name";
                    //fld.LookUp.Table = "_yesno";
                    //cur.AddField(fld);

                    // Код ЭА
                    fld = new faField();
                    fld.Data.FieldName = "id_archive";
                    fld.View.CaptionShort = "Код ЭА";
                    fld.View.TextAlign = "center";
                    fld.View.Width = 50;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);

                    // Штрихкод
                    fld = new faField();
                    fld.Data.FieldName = "barcode";
                    fld.View.CaptionShort = "Штрихкод";
                    fld.View.Width = 66;
                    fld.View.TextAlign = "center";
                    fld.Edit.Control = faControl.TextBoxInteger;
                    fld.Edit.Min = 1000000000;
                    fld.Edit.Max = Int32.MaxValue;
                    cur.AddField(fld);

                    // Путь к файлу
                    fld = new faField();
                    fld.Data.FieldName = "file";
                    fld.View.CaptionShort = "Врем.файл";
                    fld.View.Visible = false;
                    fld.View.Width = 50;
                    fld.Edit.Visible = true;
                    fld.Edit.Enable = false;
                    //fld.Edit.AddOnly = true;
                    fld.Edit.Control = faControl.File;
                    fld.Edit.Required = true;
                    cur.AddField(fld);

                    // Файл
                    fld = new faField();
                    fld.Data.FieldName = "file_orig";
                    fld.View.CaptionShort = "Файл";
                    fld.View.Width = 100;
                    fld.View.TextAlign = "center";
                    fld.Edit.Visible = true;
                    fld.Edit.Enable = false;
                    fld.Edit.Control = faControl.TextBox;
                    cur.AddField(fld);

                    // Путь к файлу
                    fld = new faField();
                    fld.Data.FieldName = "file_archive";
                    fld.View.CaptionShort = "Файл архива";
                    fld.View.Visible = false;
                    fld.View.Width = 50;
                    fld.Edit.Visible = true;
                    fld.Edit.Enable = false;
                    fld.Edit.AddOnly = true;
                    fld.Edit.Control = faControl.TextBox;
                    fld.Edit.Required = true;
                    cur.AddField(fld);

                    //Дата Создания
                    fld = new faField();
                    fld.Data.FieldName = "date_reg";
                    fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
                    fld.View.TextAlign = "center";
                    fld.View.Hint = "Дата создания";
                    fld.View.CaptionShort = "Дата созд.";
                    fld.View.Width = 98;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    fld.Edit.Auto = faAutoType.NowDateTime;
                    fld.Edit.AddOnly = true;
                    fld.Filter.Control = faControl.DateTimePicker;
                    cur.AddField(fld);

                    //Создал
                    fld = new faField();
                    fld.Data.FieldName = "id_creator";
                    fld.View.Width = 185;
                    fld.View.CaptionShort = "Создал";
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    fld.Edit.Control = faControl.AutoComplete;
                    fld.Edit.Auto = faAutoType.UserID;
                    fld.Edit.AddOnly = true;
                    fld.Filter.Control = faControl.AutoComplete;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "name";
                    fld.LookUp.Table = "_user";
                    fld.LookUp.TableAlias = "user1";
                    cur.AddField(fld);

                    //Дата Редактирования
                    fld = new faField();
                    fld.Data.FieldName = "date_upd";
                    fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
                    fld.View.TextAlign = "center";
                    fld.View.Hint = "Дата редактирования";
                    fld.View.CaptionShort = "Дата ред.";
                    fld.View.Width = 98;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    fld.Edit.Auto = faAutoType.NowDateTime;
                    fld.Filter.Control = faControl.DateTimePicker;
                    cur.AddField(fld);

                    //Редактор
                    fld = new faField();
                    fld.Data.FieldName = "id_editor";
                    fld.View.Width = 185;
                    fld.View.CaptionShort = "Редактировал";
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    fld.Edit.Control = faControl.AutoComplete;
                    fld.Edit.Auto = faAutoType.UserID;
                    fld.Filter.Control = faControl.AutoComplete;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "name";
                    fld.LookUp.Table = "_user";
                    fld.LookUp.TableAlias = "user2";
                    cur.AddField(fld);

                    // Костыль организации
                    fld = new faField();
                    fld.Data.FieldName = "id_base";
                    fld.View.CaptionShort = "Организация";
                    fld.View.Visible = false;
                    fld.Edit.Visible = false;
                    cur.AddField(fld);

                    list.AddCursor(cur);
                }

                #endregion Курсор

                #region Сдача оригиналов

                if (true)//(Session[Master.cur_basename + "_access_complect_view"] != null || Session[Master.cur_basename + "_access_complect_edit"] != null)
                {
                    cur = new faCursor("_complectnew_reg");
                    cur.TableID = 50;
                    cur.Caption = "Электронный реестр";
                    cur.EnableViewButton = true;
                    cur.EnableFileButton = false;
                    cur.EnableAddButton = cur.EnableEditButton = true;
                    cur.EnableDelButton = cur.EnableSaveButton = true;
                    cur.ShowPager = false;
                    cur.SortColumn = "id";
                    cur.SortDirection = "desc";
                    // Поля
                    // ID
                    fld = new faField();
                    fld.Data.FieldName = "id";
                    fld.View.CaptionShort = "ID";
                    fld.View.TextAlign = "center";
                    fld.View.Width = 50;
                    fld.Edit.Enable = false;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);

                    // Связывающее поле
                    fld = new faField();
                    fld.Data.FieldName = "id_complectnew";
                    fld.Filter.Control = faControl.TextBoxInteger;
                    fld.Filter.Enable = true;
                    fld.View.Visible = false;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    cur.AddField(fld);

                    //Дата Документа
                    fld = new faField();
                    fld.Data.FieldName = "date_doc";
                    fld.View.FormatString = "{0:dd.MM.yyyy}";
                    fld.View.TextAlign = "center";
                    fld.View.Hint = "Дата документа";
                    fld.View.CaptionShort = "Дата док.";
                    fld.View.Width = 84;
                    fld.Edit.Control = faControl.DatePicker;
                    fld.Filter.Control = faControl.DatePicker;
                    //fld.Edit.Required = true;
                    cur.AddField(fld);

                    //Номер документа
                    fld = new faField();
                    fld.Data.FieldName = "name";
                    fld.View.CaptionShort = "Наименование";
                    fld.View.Width = 180;
                    //  fld.Edit.Required = true;
                    fld.Edit.Control = faControl.TextBox;
                    fld.Edit.Max = 250;
                    cur.AddField(fld);

                    //Номер документа
                    fld = new faField();
                    fld.Data.FieldName = "num_doc";
                    fld.View.CaptionShort = "№ Документа";
                    fld.View.Width = 180;
                    fld.Edit.Control = faControl.TextBox;
                    // fld.Edit.Required = true;
                    fld.Edit.Max = 250;
                    cur.AddField(fld);

                    // Контрагент
                    fld = new faField();
                    fld.Data.FieldName = "frm_contr";
                    fld.View.CaptionShort = "Контрагент";
                    fld.View.Width = 210;
                    // fld.Edit.Required = true;
                    cur.AddField(fld);

                    // Кол-во листов.
                    fld = new faField();
                    fld.Data.FieldName = "sheets";
                    fld.View.CaptionShort = "Листов";
                    fld.View.Width = 80;
                    fld.Edit.Control = faControl.TextBoxInteger;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);

                    //Примечание/штрихкод
                    fld = new faField();
                    fld.Data.FieldName = "prim";
                    fld.Edit.BaseCopied = false;
                    fld.View.CaptionShort = "Прим./штрихкод";
                    fld.View.Width = 160;
                    cur.AddField(fld);

                    list.AddCursor(cur);
                }

                list.JSReadyList.Add("form" + cur.Alias + "dialog_nextbuttonbind",
                                 "$('#" + cur.Alias + "_prim').keypress(function(event){" +
                                 "var keycode = (event.keyCode ? event.keyCode : event.which);" +
                                 "var idlist=$('#" + cur.Alias + "_id').val();" +
                                 "if(keycode == '13'){" +
                                 "   $.ajax({" +
                                 "   url: '/ajax/InsertFromScanerComplectNew.aspx?id=" + Request.QueryString["id"].ToString() + "&cur=" + cur.Alias + "_cursor_" + Request.QueryString["id"].ToString() + "&idlist='+idlist," +
                                 "   type: 'POST'," +
                                 "       data: $('#" + cur.Alias + "_prim').val()," +
                                 "       cache: false," +
                                 "       contentType: false," +
                                 "       processData: false," +
                                 "       success: function (html) { if (html.length>0){alert(html);} else{ if (idlist=='')NavigateNew(); else NavigateNext($('#" + cur.Alias + "_prim').val());}}," +
                                 "       error: function (request, status, error) {  alert(request.responseText); }" +
                                 "   });" +
                                 "}});");
                list.JSFunctionList.Add("NavigateNext(prim)",
                    "var grid = $('#cph_jqGrid" + cur.Alias + "');" +
                    "var gridArr = grid.getDataIDs();" +
                    "var selrow = grid.getGridParam('selrow');" +
                    "grid.setCell(selrow,'prim',prim);" +
                    "var curr_index = 0;" +
                    "$('#form" + cur.Alias + "_edit').dialog('close');" +
                    "for (var i = 0; i < gridArr.length; i++) {if (gridArr[i] == selrow)curr_index = i;}" +
                    "if ((curr_index + 1) < gridArr.length){grid.resetSelection().setSelection(gridArr[curr_index + 1], true);ViewRow" + cur.Alias + "();}");
                list.JSFunctionList.Add("NavigateNew()",
                    "jQuery('#cph_jqGrid_complectnew_reg').trigger('reloadGrid');" +
                    "$('#form" + cur.Alias + "_edit').dialog('close');" +
                    "AddRow" + cur.Alias + "();$('#_complectnew_reg_prim').focus();");

                #endregion Сдача оригиналов
            }

            list.Render(form1, this);
            //}
            //else form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}