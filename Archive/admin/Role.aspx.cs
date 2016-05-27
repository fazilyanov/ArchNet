using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace ArchNet {
    public partial class Role : System.Web.UI.Page {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e) {   
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "role";
            if (Session["common_access_admin_role_view"] != null || Session["common_access_admin_role_edit"] != null) {
                
                list.IDBase = "0";
                list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;

                #region Шапка
                cur = new faCursor("_role");
                cur.TableID = 11;
                cur.Caption = "Администратор / Роли";

                cur.EnableViewButton = true;
                cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_admin_role_edit"] != null;
                
                // ID
                fld = new faField();
                fld.Data.FieldName = "id";
                fld.View.CaptionShort = "ID";
                fld.View.Width = 50;
                fld.Edit.Enable = false;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.TextBoxInteger;
                cur.AddField(fld);
                // Имя
                fld = new faField();
                fld.Data.FieldName = "name";
                fld.View.CaptionShort = "Название роли";
                fld.View.Width = 400;
                fld.Edit.Required = true;
                fld.Edit.Unique = true;
                fld.Edit.Max = 100;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                #endregion

                list.MainCursor = cur;

                // Просмотр
                if (Request.QueryString["id"] != null || Request.QueryString["act"] != null) {
                    
                    #region Курсор "Ключи доступа"
                    cur = new faCursor("_role_access");
                    cur.TableID = 9;
                    cur.Caption = "Ключи доступа";
                    cur.EnableViewButton = true;
                    cur.EnableAddButton = cur.EnableEditButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_admin_role_edit"] != null;

                    // ID
                    fld = new faField();
                    fld.Data.FieldName = "id";
                    fld.View.CaptionShort = "ID";
                    fld.View.TextAlign = "center";
                    fld.View.Width = 50;
                    fld.Edit.Enable = false;
                    cur.AddField(fld);

                    // Связывающее поле
                    fld = new faField();
                    fld.Data.FieldName = "id_role";
                    fld.View.Visible = false;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    fld.Filter.Enable = true;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);

                    // Доступ
                    fld = new faField();
                    fld.Data.FieldName = "id_access";
                    fld.View.CaptionShort = "Доступ";
                    fld.View.Width = 430;
                    fld.Edit.Enable = true;
                    fld.Edit.Required = true;
                    fld.Edit.Unique = true;
                    fld.Edit.Control = faControl.AutoComplete;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "description";
                    fld.LookUp.Table = "_access";
                    fld.LookUp.Where = "type = 1";
                    cur.AddField(fld);
                    //
                    list.AddCursor(cur);
                    #endregion

                    #region Курсор "Дополнительные условия"
                    cur = new faCursor("_role_where");
                    cur.TableID = 10;
                    cur.Caption = "Дополнительные условия";
                    cur.EnableViewButton = true;
                    cur.EnableAddButton = cur.EnableEditButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_admin_role_edit"] != null;
                    cur.EditDialogWidth = 300;
                    // ID
                    fld = new faField();
                    fld.Data.FieldName = "id";
                    fld.Edit.Enable = false;
                    fld.View.CaptionShort = "ID";
                    fld.View.Width = 50;
                    fld.View.TextAlign = "center";
                    cur.AddField(fld);
                    // Связывающее поле
                    fld = new faField();
                    fld.Data.FieldName = "id_role";
                    fld.View.Visible = false;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    fld.Filter.Enable = true;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);
                    //Таблица
                    fld = new faField();
                    fld.Data.FieldName = "id_table";
                    fld.View.CaptionShort = "Таблица";
                    fld.View.Width = 210;
                    fld.Edit.Enable = true;
                    fld.Edit.Required = true;
                    fld.Edit.Unique = true;
                    fld.Edit.Control = faControl.DropDown;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "description";
                    fld.LookUp.Table = "_table";
                    cur.AddField(fld);
                    //Условие
                    fld = new faField();
                    fld.Data.FieldName = "value";
                    fld.View.CaptionShort = "Условие";
                    fld.View.Width = 210;
                    fld.Edit.Enable = true;
                    fld.Edit.Required = true;
                    fld.Edit.Control = faControl.TextBox;
                    cur.AddField(fld);
                    //
                    list.AddCursor(cur);
                    #endregion
                }
                list.Render(form1, this);
            }
            else
                form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}