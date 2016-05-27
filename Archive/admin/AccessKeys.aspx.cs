using System;
using System.Web.UI;

namespace ArchNet
{
    public partial class AccessKeys : System.Web.UI.Page
    {
        public faList list;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;

            list = new faList();
            list.RouteName = "access";
            if (Session["common_access_admin_access_view"] != null)
            {
                list.ShowFilterPanel = true;
                list.EditFormWidth = 450;

                // Шапка
                cur = new faCursor("_access");
                cur.TableID = 18;
                cur.Caption = "Администратор / Ключи доступа ";
                cur.EnableViewButton = cur.EnableExcelButton = true;

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
                fld.View.CaptionShort = "Ключ";
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

                #endregion Поля

                list.MainCursor = cur;

                list.Render(form1, this);
            }
            else
                form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}