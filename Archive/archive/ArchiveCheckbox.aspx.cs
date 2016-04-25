using System;
using System.Web.UI;

namespace WebArchiveR6
{
    public partial class ArchiveCheckbox : System.Web.UI.Page
    {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;

            list = new faList();
            list.RouteName = "archivecheckbox";

            list.IDBase = Session[Master.cur_basename + "_id"].ToString();
            list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;

            // Шапка
            cur = new faCursor(Master.cur_basename + "_archive_checkbox_view");
            cur.TableID = 0;
            cur.Caption = "Архив / Отчет по ошибкам операторов ОЦ";
            cur.EnableViewButton = cur.EnableExcelButton = true;

            cur.SortColumn = "id";
            cur.SortDirection = "desc";

            #region Поля

            //Код ЭА
            fld = new faField();
            fld.Data.FieldName = "id";
            fld.View.Hint = "Код Электронного Архива";
            fld.View.CaptionShort = "Код ЭА";
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
            fld.View.Width = 120;
            fld.Edit.Visible = true;
            fld.Edit.Enable = false;
            fld.Edit.Auto = faAutoType.NowDateTime;
            fld.Edit.Max = 250;
            fld.Filter.Control = faControl.DateTimePicker;
            cur.AddField(fld);

            //Оператор ЭА
            fld = new faField();
            fld.Data.FieldName = "id_user";
            fld.View.Width = 250;
            fld.View.CaptionShort = "Оператор ЭА";
            fld.Edit.Enable = false;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_user";
            cur.AddField(fld);

            //Документ 
            fld = new faField();
            fld.Data.FieldName = "id_doctree";
            fld.View.Width = 180;
            fld.View.Visible = false;
            fld.Filter.Enable = false;
            cur.AddField(fld);

            fld = new faField();
            fld.Data.FieldName = "id_state";
            fld.View.Width = 100;
            fld.View.CaptionShort = "Состояние";
            fld.Edit.Control = faControl.DropDown;
            fld.Edit.DefaultValue = "1";
            fld.Edit.DefaultText = "Завершен";
            fld.Edit.Copied = false;
            fld.Edit.Required = true;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_state";
            cur.AddField(fld);
            
            #endregion

            list.MainCursor = cur;
            list.Render(form1, this);
        }
    }
}