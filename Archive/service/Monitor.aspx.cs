using System;

namespace WebArchiveR6
{
    public partial class Monitor : System.Web.UI.Page
    {
        public faList list;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;

            list = new faList();
            list.RouteName = "monitor";
            list.EditFormWidth = 1165;

            list.IDBase = "0";
            list.BaseName = "";
            list.ShowFilterPanel = true;
            list.ShowArrows = true;
            ////

            // Шапка
            cur = new faCursor("_monitor");
            cur.TableID = 47;
            cur.Caption = "Архив / Поступление оригиналов";
            cur.EnableFileButton = false;
            cur.EnableViewButton = cur.EnableExcelButton = cur.EnableCSVButton = cur.EnableCopyButton = true;
            cur.EnableAddButton = cur.EnableDelButton = cur.EnableSaveButton = true;

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

            //Организация
            fld = new faField();
            fld.Data.FieldName = "id_base";
            fld.View.CaptionShort = "Организация";
            fld.View.Width = 180;
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

            // Код ЭА
            fld = new faField();
            fld.Data.FieldName = "id_archive";
            fld.View.Hint = "Код Электронного Архива";
            fld.View.CaptionShort = "Код ЭА";
            fld.View.TextAlign = "center";
            fld.View.Width = 68;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            // Статус
            fld = new faField();
            fld.Data.FieldName = "id_status";
            fld.View.CaptionShort = "Статус";
            fld.View.Width = 180;
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Control = faControl.DropDown;
            fld.Edit.Required = true;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_status2";
            cur.AddField(fld);

            //Документ
            fld = new faField();
            fld.Data.FieldName = "id_doctree";
            fld.View.CaptionShort = "Документ";
            fld.View.Width = 180;
            fld.Edit.Required = true;
            fld.Edit.Control = faControl.TreeGrid;
            fld.Filter.Control = faControl.TreeGrid;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_doctree";
            cur.AddField(fld);

            //Номер документа
            fld = new faField();
            fld.Data.FieldName = "num_doc";
            fld.View.CaptionShort = "№ Документа";
            fld.View.Width = 180;
            fld.Edit.Required = true;
            fld.Edit.Max = 250;
            fld.Filter.Control = faControl.TextBox;
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
            fld.Edit.Required = true;
            cur.AddField(fld);

            #region Вид документа

            fld = new faField();
            fld.Data.FieldName = "id_doctype";
            fld.View.Hint = "Вид документа";
            fld.View.CaptionShort = "Вид док.";
            fld.View.Width = 75;
            fld.Edit.Required = true;
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Caption = "Вид докум.";
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_doctype";
            cur.AddField(fld);

            #endregion Вид документа

            //Контрагент
            fld = new faField();
            fld.Data.FieldName = "id_frm_contr";
            fld.View.CaptionShort = "Контрагент";
            fld.View.Width = 210;
            fld.Edit.Required = list.Page == faPage.dog;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_frm";
            fld.LookUp.TableAlias = "frm1";
            cur.AddField(fld);

            // Сумма
            fld = new faField();
            fld.Data.FieldName = "summ";
            fld.View.CaptionShort = "Сумма";
            fld.View.FormatString = "{0:n2}";
            fld.View.TextAlign = "right";
            fld.View.Width = 83;
            fld.Edit.Control = faControl.TextBoxNumber;
            fld.Filter.Control = faControl.TextBoxNumber;
            cur.AddField(fld);

            // Исполнитель
            fld = new faField();
            fld.Data.FieldName = "perf";
            fld.View.CaptionShort = "Исполнитель";
            fld.View.Width = 180;
            fld.Edit.Max = 100;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            #region Статус основной версии

            fld = new faField();
            fld.Data.FieldName = "id_status_ver";
            fld.View.CaptionShort = "Статус осн.вер";
            fld.View.Width = 100;
            fld.Filter.Control = faControl.DropDown;
            fld.Edit.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_status";
            cur.AddField(fld);

            #endregion Статус основной версии

            #endregion Поля

            list.MainCursor = cur;

            if (Request.QueryString["id"] != null || Request.QueryString["act"] != null)
            {
                #region Курсор "Действия"

                cur = new faCursor("_monitor_list");
                cur.TableID = 48;
                cur.Caption = "Действия";
                cur.EnableViewButton = true;
                cur.EnableFileButton = true;
                cur.EnableAddButton = cur.EnableEditButton = cur.EnableDelButton = cur.EnableSaveButton = true;

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
                fld.Data.FieldName = "id_monitor";
                fld.Filter.Control = faControl.TextBoxInteger;
                fld.Filter.Enable = true;
                fld.View.Visible = false;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                cur.AddField(fld);

                // ПланФакт
                fld = new faField();
                fld.Data.FieldName = "id_planfact";
                fld.View.CaptionShort = "План/Факт";
                fld.View.Width = 100;
                fld.Edit.Control = faControl.DropDown;
                fld.Edit.Required = true;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_planfact";
                cur.AddField(fld);

                //Дата События
                fld = new faField();
                fld.Data.FieldName = "date_ev";
                fld.View.FormatString = "{0:dd.MM.yyyy}";
                fld.View.TextAlign = "center";
                fld.View.CaptionShort = "Дата";
                fld.View.Width = 84;
                fld.Edit.Control = faControl.DatePicker;
                fld.Filter.Control = faControl.DatePicker;
                fld.Edit.Required = true;
                cur.AddField(fld);

                // Событие
                fld = new faField();
                fld.Data.FieldName = "id_event";
                fld.View.CaptionShort = "Событие";
                fld.View.Width = 180;
                fld.Edit.Control = faControl.DropDown;
                fld.Edit.Required = true;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_event";
                cur.AddField(fld);

                // Путь к файлу
                fld = new faField();
                fld.Data.FieldName = "file";
                fld.View.CaptionShort = "Файл";
                fld.View.Visible = false;
                fld.View.Width = 50;
                fld.Edit.Visible = true;
                fld.Edit.Enable = true;
                //fld.Edit.AddOnly = true;
                fld.Edit.Control = faControl.File;
                //fld.Edit.Required = true;
                cur.AddField(fld);

                // Файл
                fld = new faField();
                fld.Data.FieldName = "filetype";
                fld.Data.FieldCalc = "(UPPER(SUBSTRING(a.[file],LEN(a.[file])-2,3)))";
                fld.View.CaptionShort = "Файл";
                fld.View.Width = 40;
                fld.View.TextAlign = "center";
                fld.Edit.Control = faControl.TextBox;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                cur.AddField(fld);

                // Примечание
                fld = new faField();
                fld.Data.FieldName = "prim";
                fld.View.CaptionShort = "Комментарий";
                fld.View.Width = 180;
                fld.Edit.Required = false;
                fld.Edit.Max = 240;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                //
                list.AddCursor(cur);

                #endregion Курсор "Действия"
            }
            list.Render(form1, this);
        }
    }
}