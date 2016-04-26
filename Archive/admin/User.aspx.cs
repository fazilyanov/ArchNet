using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace ArchNet {
    public partial class User : System.Web.UI.Page {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e) {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "user";
            if (Session["common_access_admin_user_view"] != null || Session["common_access_admin_user_edit"] != null) {

                list.IDBase = "0";
                list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;

                #region Шапка
                cur = new faCursor("_user");
                cur.TableID = 4;
                cur.Caption = "Администратор / Пользователи";
                cur.EnableViewButton = true;
                cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_admin_user_edit"] != null;
                //
                fld = new faField();
                fld.Data.FieldName = "id";
                fld.View.CaptionShort = "ID";
                fld.View.Width = 50;
                fld.Edit.Enable = false;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.TextBoxInteger;
                cur.AddField(fld);
                //
                fld = new faField();
                fld.Data.FieldName = "login";
                fld.View.CaptionShort = "Логин";
                fld.View.Width = 250;
                fld.Edit.Required = true;
                fld.Edit.Unique = true;
                fld.Edit.Max = 100;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);
                //
                fld = new faField();
                fld.Data.FieldName = "name";
                fld.View.CaptionShort = "ФИО";
                fld.Edit.Max = 250;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.TextBox;
                fld.View.Width = 350;
                cur.AddField(fld);
                //
                fld = new faField();
                fld.Data.FieldName = "sname";
                fld.View.CaptionShort = "ФИО краткое";
                fld.Edit.Max = 250;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.TextBox;
                fld.View.Width = 350;
                cur.AddField(fld);
                //
                fld = new faField();
                fld.Data.FieldName = "mail";
                fld.View.CaptionShort = "E-mail";
                fld.View.Width = 150;
                fld.Edit.Max = 100;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.TextBox;
                fld.Edit.Enable = true;
                cur.AddField(fld);
                //
                fld = new faField();
                fld.Data.FieldName = "watch";
                fld.View.Width = 100;
                fld.View.CaptionShort = "Отслеживать";
                fld.Edit.Control = faControl.DropDown;
                fld.Edit.Required = true;
                fld.Edit.DefaultValue = "1";
                fld.Edit.DefaultText = "Нет";
                fld.Edit.Copied = false;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_yesno";
                fld.LookUp.TableAlias = "yn1";
                cur.AddField(fld);
                //
                //fld = new faField();
                //fld.Data.FieldName = "id_department";
                //fld.View.Width = 200;
                //fld.View.CaptionShort = "Департамент";
                //fld.Edit.Control = faControl.DropDown;
                //fld.Filter.Control = faControl.DropDown;
                //fld.LookUp.Key = "id";
                //fld.LookUp.Field = "name";
                //fld.LookUp.Table = "_department";
                //cur.AddField(fld);
                #endregion

                list.MainCursor = cur;

                // Просмотр
                if (Request.QueryString["id"] != null || Request.QueryString["act"] != null) {

                    #region Курсор "Роли в базах"
                    cur = new faCursor("_user_role_base");
                    cur.TableID = 6;
                    cur.Caption = "Роли в базах";
                    cur.EnableViewButton = true;
                    cur.EnableAddButton = cur.EnableEditButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_admin_user_edit"] != null;
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
                    fld.Data.FieldName = "id_user";
                    fld.View.Visible = false;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    fld.Filter.Enable = true;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);
                    //
                    fld = new faField();
                    fld.Data.FieldName = "id_base";
                    fld.Edit.Enable = true;
                    fld.Edit.Required = true;
                    fld.Edit.Unique = true;
                    fld.Edit.Control = faControl.DropDown;
                    fld.View.CaptionShort = "База";
                    fld.View.Width = 210;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "namerus";
                    fld.LookUp.Table = "_base";
                    cur.AddField(fld);
                    //
                    fld = new faField();
                    fld.Data.FieldName = "id_role";
                    fld.Edit.Enable = true;
                    fld.Edit.Required = true;
                    fld.Edit.Control = faControl.DropDown;
                    fld.View.CaptionShort = "Роль";
                    fld.View.Width = 210;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "name";
                    fld.LookUp.Table = "_role";
                    cur.AddField(fld);
                    //
                    list.AddCursor(cur);
                    #endregion

                    #region Курсор "Общие доступы"
                    cur = new faCursor("_user_access");
                    cur.TableID = 5;
                    cur.Caption = "Общие доступы";
                    cur.EnableViewButton = true;
                    cur.EnableAddButton = cur.EnableEditButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_admin_user_edit"] != null;

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
                    fld.Data.FieldName = "id_user";
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
                    fld.LookUp.Where = "type = 2";
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