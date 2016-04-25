using System;
using System.Web.UI;

namespace WebArchiveR6
{
    public partial class Base : System.Web.UI.Page
    {
        public faList list;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;

            list = new faList();
            list.RouteName = "base";
            if (Session["common_access_admin_base_view"] != null)
            {
                list.IDBase = "0";
                list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;

                #region Шапка

                cur = new faCursor("_base");
                cur.TableID = 19;
                cur.Caption = "Администратор / Базы ";

                cur.EnableViewButton = cur.EnableExcelButton = true;
                cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = false;

                // ID
                fld = new faField();
                fld.Data.FieldName = "id";
                fld.View.CaptionShort = "ID";
                fld.View.TextAlign = "center";
                fld.View.Width = 68;
                fld.Edit.Enable = false;
                fld.Filter.Control = faControl.TextBoxInteger;
                cur.AddField(fld);
                // Имя
                fld = new faField();
                fld.Data.FieldName = "name";
                fld.View.CaptionShort = "Имя";
                fld.View.Width = 200;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                // Описание
                fld = new faField();
                fld.Data.FieldName = "namerus";
                fld.View.CaptionShort = "Описание";
                fld.View.Width = 330;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                #endregion Шапка

                list.MainCursor = cur;

                list.Render(form1, this);
            }
            else
                form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}