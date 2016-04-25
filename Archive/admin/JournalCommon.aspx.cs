using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace ArchNet
{
    public partial class JournalCommon : System.Web.UI.Page
    {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "journalcommon";

            if (Session["common_access_admin_journal_common"] != null)
            {
                list.IDBase =  "0";
                list.BaseName = "";
                list.ShowFilterPanel = true;
                list.RequestPost = Request.Form;
                list.RequestGet = Request.QueryString;

                // Шапка
                cur = new faCursor("_journal_view");
                cur.TableID = 7;
                cur.Caption = "Администратор / Общий журнал";
                cur.EnableViewButton = true;
                cur.EnableExcelButton = true;
                cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = false;
                cur.EnableCSVButton = true;
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
                fld.View.FormatString = "{0:dd.MM.yyyy HH:mm:ss.fff}";
                fld.View.TextAlign = "center";
                fld.View.CaptionShort = "Время";
                fld.View.Width = 155;
                fld.Filter.Control = faControl.DateTimePicker;
                cur.AddField(fld);

                // Пользователь
                fld = new faField();
                fld.Data.FieldName = "id_user";
                fld.View.Width = 185;
                fld.View.CaptionShort = "Пользователь";
                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "sname";
                fld.LookUp.Table = "_user";
                cur.AddField(fld);

                // Действие
                fld = new faField();
                fld.Data.FieldName = "id_edittype";
                fld.View.CaptionShort = "Действие";
                fld.View.TextAlign = "center";
                fld.View.Width = 105;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Table = "_edittype";
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                cur.AddField(fld);

                // База
                fld = new faField();
                fld.Data.FieldName ="id_base";
                fld.View.CaptionShort = "База";
                fld.View.TextAlign = "center";
                fld.View.Width = 150;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Table = "_base";
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "namerus";
                cur.AddField(fld);
                
                // Таблица
                fld = new faField();
                fld.Data.FieldName ="id_table";
                fld.View.CaptionShort = "Таблица";
                fld.View.TextAlign = "center";
                fld.View.Width = 190;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Table = "_table";
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "description";
                cur.AddField(fld);
                
                // № Записи
                fld = new faField();
                fld.Data.FieldName = "change_id";
                fld.View.CaptionShort = "# Записи";
                fld.View.TextAlign = "center";
                fld.View.Width = 75;
                fld.Filter.Control = faControl.TextBoxInteger;
                cur.AddField(fld);
                
                //Изменения
                fld = new faField();
                fld.Data.FieldName ="changes";
                fld.View.CaptionShort = "Изменения";
                fld.View.Width = 300;
                cur.AddField(fld);

                // Балл
                fld = new faField();
                fld.Data.FieldName = "score";
                fld.View.CaptionShort = "Балл";
                fld.View.TextAlign = "center";
                fld.View.Width = 75;
                fld.Filter.Control = faControl.TextBoxInteger;
                cur.AddField(fld);

                #endregion

                list.MainCursor = cur;

                list.Render(form1, this);
            }
            else
                form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}