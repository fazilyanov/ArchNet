using System;
using System.Web.UI;

namespace WebArchiveR6
{
    public partial class RegstorList : System.Web.UI.Page
    {
        public faListRegstor list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;

            list = new faListRegstor();
            list.RouteName = "regstorlist";

            list.IDBase = "0";
            list.BaseName = "";// Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;
            list.EditFormWidth = 1100;
            list.EditFormHeight = 350;

            // Шапка
            cur = new faCursor("_regstor_list_view");
            //cur.AliasAlt = cur.Alias + "_" + list.Page;
            cur.TableID = 0;
            cur.Caption = "Архив / Документационный фонд";
            cur.EnableViewButton = cur.EnableExcelButton = cur.EnableCSVButton = true;
            cur.EnableFileButton = false; cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_complect_regstor"] != null;

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

            // Связывающее поле
            fld = new faField();
            fld.Data.FieldName = "id_regstor";
            fld.View.CaptionShort = "ID Дела";
            fld.View.TextAlign = "center";
            fld.View.Width = 50;
            fld.Edit.Enable = false;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);


            // Костыль организации 
            fld = new faField();
            fld.Data.FieldName = "id_base";
            fld.View.Visible = false;
            fld.Edit.Visible = false;
            fld.Filter.Enable = false;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_base";
            cur.AddField(fld);

            //Организация
            fld = new faField();
            fld.Data.FieldName = "id_base";
            fld.View.CaptionShort = "Организация";
            fld.View.Width = 150;
            fld.Edit.Required = true;
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Caption = "Организация";
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "namerus";
            fld.LookUp.Table = "_base";
            fld.LookUp.Again = true;
            cur.AddField(fld);

            // Имя
            fld = new faField();
            fld.Data.FieldName = "name";
            fld.View.CaptionShort = "Имя";
            fld.View.Width = 200;
            fld.Edit.Max = 150;
            fld.Edit.Required = true;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            //Место хранения
            fld = new faField();
            fld.Data.FieldName = "id_storage";
            fld.View.CaptionShort = "Место хранения";
            fld.View.Width = 180;
            fld.Edit.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_storage";
            fld.Filter.Control = faControl.DropDown;
            cur.AddField(fld);

            // Индекс дела
            fld = new faField();
            fld.Data.FieldName = "case_index";
            fld.View.CaptionShort = "Индекс дела";
            fld.View.Width = 100;
            fld.Edit.Max = 20;
            fld.Filter.Control = faControl.TextBox;
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
            fld.Filter.Control = faControl.DateTimePicker;
            cur.AddField(fld);

            //Создал
            fld = new faField();
            fld.Data.FieldName = "id_creator";
            fld.View.Width = 185;
            fld.View.CaptionShort = "Создал";
            fld.Edit.Enable = false;
            fld.Edit.AddOnly = true;
            fld.Edit.Auto = faAutoType.UserID;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_user";
            cur.AddField(fld);


            //Статус
            fld = new faField();
            fld.Data.FieldName = "id_status2";
            fld.View.CaptionShort = "Статус";
            fld.View.Width = 84;
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_status2";
            cur.AddField(fld);

            //Группа
            fld = new faField();
            fld.Data.FieldName = "id_regstor_group";
            fld.View.CaptionShort = "Группа";
            fld.View.Width = 84;
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_regstor_group";
            cur.AddField(fld);

            // Штрихкод
            fld = new faField();
            fld.Data.FieldName = "barcode";
            fld.View.CaptionShort = "Штрихкод";
            fld.View.Width = 120;
            fld.View.TextAlign = "center";
            fld.Edit.Control = faControl.TextBoxInteger;
            fld.Edit.Min = 1000000000;
            fld.Edit.Max = Int32.MaxValue;
            cur.AddField(fld);

            // Код ЭА
            fld = new faField();
            fld.Data.FieldName = "id_archive";
            fld.View.CaptionShort = "Код ЭА";
            fld.View.TextAlign = "center";
            fld.View.Width = 100;
            // fld.Edit.Enable = false;
            fld.Edit.Control = faControl.TextBoxInteger;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            //Документ 
            fld = new faField();
            fld.Data.FieldName = "id_doctree";
            fld.View.CaptionShort = "Документ";
            fld.View.Width = 180;
            fld.Edit.Required = true;
            fld.Filter.Control = faControl.TreeGrid;
            fld.Filter.Enable = false;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_doctree";
            cur.AddField(fld);

            //Дата Документа
            fld = new faField();
            fld.Data.FieldName = "date_doc";
            fld.View.FormatString = "{0:dd.MM.yyyy}";
            fld.View.TextAlign = "center";
            fld.View.Hint = "Дата документа";
            fld.View.CaptionShort = "Дата док.";
            fld.View.Width = 84;
            //fld.Edit.DefaultText = DateTime.Now.ToShortDateString();
            fld.Edit.Control = faControl.DatePicker;
            fld.Filter.Enable = false;
            fld.Filter.Control = faControl.DatePicker;
            fld.Edit.Required = true;
            cur.AddField(fld);

            fld = new faField();
            fld.Data.FieldName = "id_frm_contr";
            fld.View.CaptionShort = "Контрагент";
            fld.View.Width = 210;
            fld.Edit.Required = list.Page == faPage.dog;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Filter.Enable = false;
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_frm";
            fld.LookUp.TableAlias = "frm1";
            cur.AddField(fld);

            //Качество
            fld = new faField();
            fld.Data.FieldName = "id_quality";
            fld.View.CaptionShort = "Качество";
            fld.View.Width = 90;
            fld.View.TextAlign = "center";
            fld.Edit.Control = faControl.DropDown;
            fld.Edit.DefaultValue = "1";
            fld.Edit.DefaultText = "Соответствует";
            fld.Edit.Required = true;
            fld.Filter.Enable = false;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_quality";
            cur.AddField(fld);

            // Путь к файлу
            fld = new faField();
            fld.Data.FieldName = "file";
            fld.View.CaptionShort = "Файл";
            fld.View.Width = 150;
            fld.View.Visible = false;
            fld.Edit.Visible = false;
            cur.AddField(fld);
            #endregion

            list.MainCursor = cur;
            list.Render(form1, this);
        }
    }
}