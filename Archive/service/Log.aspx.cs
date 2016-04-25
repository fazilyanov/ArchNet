using System;

namespace WebArchiveR6
{
    public partial class Log : System.Web.UI.Page
    {
        public faList list;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "log";

            list.IDBase = "0";
            list.BaseName = "";
            list.ShowFilterPanel = true;
            list.RequestPost = Request.Form;
            list.RequestGet = Request.QueryString;

            // Шапка
            cur = new faCursor("_log");
            cur.TableID = 0;
            cur.Caption = "Сервис / Лог";
            cur.EnableViewButton = false;
            cur.EnableExcelButton = true;
            cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = false;
            cur.SortColumn = "id";
            cur.SortDirection = "desc";

            #region Поля

            // ID
            fld = new faField();
            fld.Data.FieldName = "id";
            fld.View.CaptionShort = "ID";
            fld.View.TextAlign = "center";
            fld.View.Width = 68;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            // Время
            fld = new faField();
            fld.Data.FieldName = "when";
            fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
            fld.View.TextAlign = "center";
            fld.View.CaptionShort = "Время";
            fld.View.Width = 95;
            fld.Filter.Control = faControl.DateTimePicker;
            cur.AddField(fld);

            // Пользователь
            fld = new faField();
            fld.Data.FieldName = "id_user";
            fld.View.Width = 155;
            fld.View.CaptionShort = "Пользователь";
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "sname";
            fld.LookUp.Table = "_user";
            cur.AddField(fld);

            // Действие
            fld = new faField();
            fld.Data.FieldName = "id_type";
            fld.View.CaptionShort = "Действие";
            fld.View.TextAlign = "center";
            fld.View.Width = 170;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Table = "_logtype";
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            cur.AddField(fld);

            //Прим
            fld = new faField();
            fld.Data.FieldName = "what";
            fld.View.CaptionShort = "Примечание";
            fld.View.Width = 300;
            cur.AddField(fld);

            #endregion Поля

            list.MainCursor = cur;

            list.Render(form1, this);
        }
    }
}