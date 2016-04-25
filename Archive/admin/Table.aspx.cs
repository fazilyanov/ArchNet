using System;
using System.Web.UI;

namespace WebArchiveR6
{
    public partial class Table : System.Web.UI.Page
    {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;

            list = new faList();
            list.RouteName = "table";
            if (Session["common_access_admin_table_view"] != null)
            {
                list.ShowFilterPanel = true;
                list.EditFormWidth = 450;

                // Шапка
                cur = new faCursor("_table");
                cur.TableID = 20;
                cur.Caption = "Администратор / Таблицы ";
                cur.EnableViewButton  = cur.EnableExcelButton = true;
                cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = false;

                #region Поля
                // ID
                fld = new faField();
                fld.Data.FieldName = "id";
                fld.View.CaptionShort = "ID";
                fld.View.TextAlign = "center";
                fld.View.Width = 68;
                fld.Edit.Enable = false;
                fld.Filter.Control = faControl.TextBoxInteger;
                cur.AddField(fld);

                // Ключ
                fld = new faField();
                fld.Data.FieldName = "name";
                fld.View.CaptionShort = "Имя";
                fld.View.Width = 200;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                // Описание
                fld = new faField();
                fld.Data.FieldName = "description";
                fld.View.CaptionShort = "Описание";
                fld.View.Width = 330;
                fld.Filter.Control = faControl.TextBox;
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