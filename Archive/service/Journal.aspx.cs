using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace ArchNet
{
    public partial class Journal : System.Web.UI.Page
    {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "journal";

            if (Session[Master.cur_basename + "_access_service_journal"] != null)
            {
                list.IDBase = Session[Master.cur_basename + "_id"].ToString();
                list.BaseName = "";
                list.ShowFilterPanel = true;

                Session["_journal_id_table_filter"] = Request.QueryString["id_table"] != null ? Request.QueryString["id_table"].ToString() : Session["_journal_id_table_filter"];
                Session["_journal_id_table_filter_text"] = Request.QueryString["id_table_text"] != null ? Request.QueryString["id_table_text"].ToString() : Session["_journal_id_table_filter_text"];
                Session["_journal_change_id_filter"] = Request.QueryString["change_id"] != null ? Request.QueryString["change_id"].ToString() : Session["_journal_change_id_filter"];


                // Шапка
                cur = new faCursor("_journal");
                cur.TableID = 7;
                cur.Caption = "Сервис / Журнал изменений";
                cur.EnableViewButton = true;
                cur.EnableExcelButton = true;
                cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = false;

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
                fld.LookUp.Field = "name";
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
                fld.Data.FieldName = "id_base";
                fld.Data.Where = "id_base=" + list.IDBase;
                fld.View.CaptionShort = "База";
                fld.View.TextAlign = "center";
                fld.View.Width = 150;
                fld.Filter.Enable = false;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Table = "_base";
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "namerus";
                cur.AddField(fld);

                // Таблица
                fld = new faField();
                fld.Data.FieldName = "id_table";
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
                fld.Data.FieldName = "changes";
                fld.View.CaptionShort = "Изменения";
                fld.View.Width = 300;
                cur.AddField(fld);
                //
                #endregion

                list.MainCursor = cur;

                list.Render(form1, this);
            }
            else
                form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}