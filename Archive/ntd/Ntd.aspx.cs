using System;

namespace WebArchiveR6
{
    public partial class Ntd : System.Web.UI.Page
    {
        //public faListNtd list;
        public faListNtd list;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;
            //скрыть фильтр категорий чз js
            list = new faListNtd();
            list.RouteName = "ntd";
            list.EditFormWidth = 1165;
            list.EditFormLeftSideWidth = 640;
            list.EditFormInputWidth = 400;
            list.EditFormLabelWidth = 160;
            list.Page = faPage.none;

            list.IDBase = "0";
            list.BaseName = "";
            list.ShowFilterPanel = true;
            ////
            list.ShowCheckBox = false;
            list.EnableSuperVisorCheckBox = false;
            list.ShowHiddenDoc = false;
            list.JSReadyList.Add("hide",
                "$('.navbar-brand').html('Корпоративный фонд нормативно-технической документации (Фонд НТД)');" +
                "$('.navbar-brand').attr('href', '/ntdstart');" +
                "$('.navbar-right').html(''); " +
                "$('#_ntd_categoryid_category').parent().parent().hide();" +
                "$('#clear__ntd_categoryid_category').remove();"+
                "$('#jqgh_cph_jqGrid__ntd_helplink').html('Рабочая копия');");
            
            // Шапка
            cur = new faCursor("_ntd");
            //cur.AliasAlt = cur.Alias + "_" + list.Page;
            cur.TableID = 42;
            cur.Caption = (Session["_ntd_id_category_filter_text"] ?? "Нормативно-техническая документация").ToString();
            cur.EnableViewButton = cur.EnableFileButton = cur.EnableExcelButton = cur.EnableCSVButton = true;
            cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_ntd_edit"] != null;

            cur.EnablAllFileButton = false; //Session[Master.cur_basename + "_access_archive_filemulti"] != null;
            cur.EnableBulkEditButton = false; //Session["common_access_archive_bulk_edit"] != null;
            cur.ShowColumnViewButtons = false;

            //cur.RelTable = new string[1] { "_archive.id_parent" };
            cur.SortColumn = "id";
            cur.SortDirection = "desc";

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

            //Дата Редактирования
            fld = new faField();
            fld.Data.FieldName = "date_upd";
            fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
            fld.View.TextAlign = "center";
            fld.View.Hint = "Дата редактирования";
            fld.View.CaptionShort = "Дата ред.";
            fld.View.Width = 98;
            fld.Edit.Visible = true;
            fld.Edit.Enable = false;
            fld.Edit.BaseCopied = false;
            fld.Edit.Auto = faAutoType.NowDateTime;
            fld.Edit.Max = 250;
            fld.Filter.Control = faControl.DateTimePicker;
            cur.AddField(fld);

            // Редактировал
            fld = new faField();
            fld.Data.FieldName = "id_user";
            fld.View.Width = 142;
            fld.View.CaptionShort = "Редактировал";
            fld.Edit.Enable = false;
            fld.Edit.BaseCopied = false;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Edit.Auto = faAutoType.UserID;
            //  fld.Edit.AddOnly = (Session[Master.cur_basename + "_id_role"] ?? "").ToString() == "8"; // Супервайзеры не меняют поле "Оператор"
            fld.Filter.Enable = false;
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "sname";
            fld.LookUp.Table = "_user";
            cur.AddField(fld);

            // Шифр
            fld = new faField();
            fld.Data.FieldName = "code";
            fld.View.CaptionShort = "Шифр";
            fld.View.Width = 180;
            fld.Edit.Required = true;
            fld.Edit.Max = 250;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            // Наименование
            fld = new faField();
            fld.Data.FieldName = "sname";
            fld.View.CaptionShort = "Наименование";
            fld.View.Width = 00;
            fld.Edit.Max = 250;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            // Наименование полное
            fld = new faField();
            fld.Data.FieldName = "name";
            fld.View.CaptionShort = "Наим.полн.";
            fld.View.Width = 180;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            // Категория
            fld = new faField();
            fld.Data.FieldName = "id_category";
            fld.View.Width = 100;
            fld.View.CaptionShort = "Категория";
            fld.Edit.Control = faControl.DropDown;
            fld.Edit.Required = true;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_ntd_category";
            cur.AddField(fld);

            // Статус
            fld = new faField();
            fld.Data.FieldName = "id_status";
            fld.View.Width = 100;
            fld.View.CaptionShort = "Статус";
            fld.View.TextAlign = "center";
            fld.Edit.Control = faControl.DropDown;
            fld.Edit.DefaultValue = "1";
            fld.Edit.DefaultText = "Действует";
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_status3";
            cur.AddField(fld);

            //Дата принятия
            fld = new faField();
            fld.Data.FieldName = "date_trans";
            fld.View.FormatString = "{0:dd.MM.yyyy}";
            fld.View.TextAlign = "center";
            fld.View.Hint = "Дата выхода распорядительного документа об утверждении НТД";
            fld.View.CaptionShort = "Дата принятия";
            fld.View.Width = 120;
            //fld.Edit.DefaultText = DateTime.Now.ToShortDateString();
            fld.Edit.Control = faControl.DatePicker;
            fld.Filter.Control = faControl.DatePicker;
            cur.AddField(fld);

            // Область применения
            fld = new faField();
            fld.Data.FieldName = "for_area";
            fld.View.CaptionShort = "Область прим.";
            fld.View.Hint = "Область применения";
            fld.View.Width = 180;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            // Срок действия
            fld = new faField();
            fld.Data.FieldName = "date_validity";
            fld.View.FormatString = "{0:dd.MM.yyyy}";
            fld.View.TextAlign = "center";
            fld.View.CaptionShort = "Срок действия";
            fld.View.Width = 84;
            fld.Edit.Control = faControl.DatePicker;
            fld.Filter.Control = faControl.DatePicker;
            cur.AddField(fld);

            // Утвержден
            fld = new faField();
            fld.Data.FieldName = "approved";
            fld.View.CaptionShort = "Утвержден";
            fld.View.Hint = "Наименование органа, утвердившего НТД";
            fld.View.Width = 250;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            // Ответственный
            fld = new faField();
            fld.Data.FieldName = "id_resp";
            fld.View.CaptionShort = "Ответственный";
            fld.View.Hint = "Подразделение, ответственное за хранение НТД";
            fld.View.Width = 250;
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_ntd_depart";
            cur.AddField(fld);

            // Место хранаения
            fld = new faField();
            fld.Data.FieldName = "id_storage";
            fld.View.CaptionShort = "Место хранения";
            fld.View.Hint = "Подразделение, в котором фактически хранится НТД";
            fld.View.Width = 250;
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_ntd_depart";
            fld.LookUp.TableAlias = "_ntd_depart2";
            cur.AddField(fld);

            // Ключевые слова
            fld = new faField();
            fld.Data.FieldName = "keyword";
            fld.View.CaptionShort = "Ключевые слова";
            fld.View.Width = 400;
            fld.Edit.Max = 250;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            // Дополнительно
            fld = new faField();
            fld.Data.FieldName = "prim";
            fld.View.CaptionShort = "Доп.информ.";
            fld.View.Hint = "Дополнительные сведения, связанные с особенностями практического применения НТД";
            fld.Filter.Control = faControl.TextBox;
            fld.Filter.Enable = false;
            cur.AddField(fld);

            //Дата формирования
            fld = new faField();
            fld.Data.FieldName = "date_form";
            fld.View.FormatString = "{0:dd.MM.yyyy}";
            fld.View.TextAlign = "center";
            fld.View.Hint = "Дата изготовления рабочей копии";
            fld.View.CaptionShort = "Дата формир.";
            fld.View.Width = 120;
            //fld.Edit.DefaultText = DateTime.Now.ToShortDateString();
            fld.Edit.Control = faControl.DatePicker;
            fld.Filter.Control = faControl.DatePicker;
            cur.AddField(fld);

            // Размещение
            fld = new faField();
            fld.Data.FieldName = "helplink";
            fld.View.CaptionShort = "Размещение";
            fld.View.Hint = "Cсылка на корпоративный ресурс, на котором размещена рабочая копия";
            fld.Filter.Control = faControl.URL;
            fld.Edit.Control = faControl.URL;
            cur.AddField(fld);

            //Путь к файлу (Из Версий)
            fld = new faField();
            fld.Data.FieldName = "file";
            fld.Data.Table = "_ntd_version";
            fld.Data.RefField = "id_ntd";
            fld.Data.Where = "main=1";
            fld.View.Visible = false;
            fld.View.CaptionShort = "Файл";
            fld.Edit.Visible = false;
            fld.Edit.Enable = false;
            fld.Filter.Enable = false;
            fld.Filter.Control = faControl.DatePicker;
            cur.AddField(fld);

            #endregion Поля

            #region Группировка заголовков

            // Странный стиль у группированного столбца, лечить так: "$('th[colspan=2]').css('font-weight', 'bold').css('text-align', 'center').css('font-size', '12px');" +
            //cur.HeaderGroupList.Add(new faHeaderGroup { StartColumnName = "date_form", NumberOfColumns = 2, TitleText = "Рабочая копия" });

            #endregion Группировка заголовков

            #region Зашиваем порядок и вид столбцов

            cur.ColumnViewString = "code=180,sname=450,id_status_name_text=100,date_trans=120,helplink=300";

            #endregion Зашиваем порядок и вид столбцов

            list.MainCursor = cur;

            if (Request.QueryString["id"] != null || Request.QueryString["act"] != null)
            {
                #region Курсор "Версии документов"

                //if (Session["common_access_ntd_view"] != null || Session["common_access_ntd_edit"] != null)
                //{
                cur = new faCursor("_ntd_version");
                cur.TableID = 43;
                cur.Caption = "Версии";
                cur.EnableViewButton = true;
                cur.EnableFileButton = true;
                cur.EnableAddButton = cur.EnableEditButton = cur.EnableDelButton = cur.EnableSaveButton
                    = Session["common_access_ntd_edit"] != null;

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
                fld.Data.FieldName = "id_ntd";
                fld.Filter.Control = faControl.TextBoxInteger;
                fld.Filter.Enable = true;
                fld.View.Visible = false;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                cur.AddField(fld);

                // NN
                fld = new faField();
                fld.Data.FieldName = "nn";
                fld.View.TextAlign = "center";
                fld.View.Width = 68;
                fld.View.Visible = false;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                fld.Edit.Auto = faAutoType.Version;
                fld.Filter.Enable = false;
                cur.AddField(fld);

                // Номер версии
                fld = new faField();
                fld.Data.FieldName = "ver";
                fld.Data.FieldCalc = "(CAST(a.id_ntd as varchar(30))+'.'+CAST(a.nn as varchar(30)))";
                fld.View.CaptionShort = "№ Версии";
                fld.View.TextAlign = "center";
                fld.View.Width = 65;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                fld.Edit.Control = faControl.TextBox;
                cur.AddField(fld);

                // Основная версия
                fld = new faField();
                fld.Data.FieldName = "main";
                fld.View.CaptionShort = "Основная";
                fld.View.TextAlign = "center";
                fld.View.Width = 75;
                fld.Edit.Control = faControl.CheckBox;
                fld.Edit.Auto = faAutoType.Main;
                //fld.Edit.DefaultValue = "1";
                cur.AddField(fld);

                // Дата создания
                fld = new faField();
                fld.Data.FieldName = "date_reg";
                fld.View.FormatString = "{0:dd.MM.yyyy}";
                fld.View.TextAlign = "center";
                fld.View.CaptionShort = "Дата созд.";
                fld.View.Width = 80;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                fld.Edit.Auto = faAutoType.NowDate;
                cur.AddField(fld);

                // Путь к файлу
                fld = new faField();
                fld.Data.FieldName = "file";
                fld.View.CaptionShort = "Файл";
                fld.View.Visible = false;
                fld.View.Width = 50;
                fld.Edit.Visible = true;
                fld.Edit.Enable = true;
                fld.Edit.AddOnly = true;
                fld.Edit.Control = faControl.File;
                fld.Edit.Required = true;
                cur.AddField(fld);

                // Файл
                fld = new faField();
                fld.Data.FieldName = "filetype";
                fld.Data.FieldCalc = "(UPPER(SUBSTRING(a.[file],LEN(a.[file])-2,3)))";
                fld.View.CaptionShort = "Файл";
                fld.View.Width = 50;
                fld.View.TextAlign = "center";
                fld.Edit.Control = faControl.TextBox;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                cur.AddField(fld);

                // Размер файла
                fld = new faField();
                fld.Data.FieldName = "file_size";
                fld.Data.FieldCalc = "CAST(a.file_size/1024.0/1024.0 as numeric(13,2))";
                fld.View.CaptionShort = "Размер";
                fld.View.Width = 80;
                fld.View.TextAlign = "center";
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                fld.Edit.Auto = faAutoType.FileSize;
                cur.AddField(fld);

                list.AddCursor(cur);
                //}

                #endregion Курсор "Версии документов"
            }
            list.Render(form1, this);
        }
    }
}