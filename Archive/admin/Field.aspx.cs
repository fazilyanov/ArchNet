using System;
using System.Web.UI;

namespace WebArchiveR6
{
    public partial class Field : System.Web.UI.Page
    {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;

            list = new faList();
            list.RouteName = "field";
            if (Session["common_access_admin_rating_edit"] != null || Session["common_access_admin_rating_view"] != null)
            {
                list.ShowFilterPanel = true;
                list.EditFormWidth = 450;

                // Шапка
                cur = new faCursor("_field");
                cur.TableID = 36;
                cur.Caption = "Администратор / Поля ";
                cur.EnableViewButton  = cur.EnableExcelButton = true;
                cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_admin_rating_edit"] != null; 

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

                fld = new faField();
                fld.Data.FieldName = "id_table";
                fld.View.CaptionShort = "Таблица";
                fld.View.Width = 150;
                fld.Edit.Required = true;
                fld.Edit.Control = faControl.DropDown;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "description";
                fld.LookUp.Table = "_table";
                cur.AddField(fld);

                // 
                fld = new faField();
                fld.Data.FieldName = "name";
                fld.View.CaptionShort = "Имя";
                fld.View.Width = 200;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                //
                fld = new faField();
                fld.Data.FieldName = "description";
                fld.View.CaptionShort = "Описание";
                fld.View.Width = 330;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                // Балл
                fld = new faField();
                fld.Data.FieldName = "score";
                fld.View.CaptionShort = "Балл";
                fld.View.Width = 66;
                fld.View.TextAlign = "center";
                fld.Edit.Required = true;
                fld.Edit.Max = 10;
                fld.Edit.Control = faControl.TextBoxInteger;
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