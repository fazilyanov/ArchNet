using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6 {
    public partial class UserRoleBase : System.Web.UI.Page {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e) {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "userrolebase";
            if (Session["common_access_admin_user_view"] != null || Session["common_access_admin_user_edit"] != null) {

                list.IDBase = "0";
                list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;

                #region Шапка
                cur = new faCursor("_user_role_base");
                cur.TableID = 6;
                cur.Caption = "Администратор / Доступы пользователей";
                cur.EnableViewButton = true;
                cur.EnableExcelButton = true;
                cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = false;//Session["common_access_admin_user_edit"] != null;
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
                fld.Data.FieldName = "id_user";
                fld.View.CaptionShort = "Логин";
                fld.View.Width = 350;
                fld.Edit.Enable = false;
                fld.Edit.Control = faControl.AutoComplete;
                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "login";
                fld.LookUp.Table = "_user";
                fld.LookUp.TableAlias = "_user";
                cur.AddField(fld);
                //
                fld = new faField();
                fld.Data.FieldName = "id_user";
                fld.View.CaptionShort = "ФИО";
                fld.View.Width = 350;
                fld.Edit.Enable = false;
                fld.Edit.Control = faControl.AutoComplete;
                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "sname";
                fld.LookUp.Table = "_user";
                fld.LookUp.TableAlias = "_user1";
                cur.AddField(fld);
                //
                fld = new faField();
                fld.Data.FieldName = "id_role";
                fld.View.CaptionShort = "Роль";
                fld.View.Width = 350;
                fld.Edit.Enable = false;
                fld.Edit.Control = faControl.AutoComplete;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_role";
                cur.AddField(fld);
                //
                fld = new faField();
                fld.Data.FieldName = "id_base";
                fld.View.CaptionShort = "База";
                fld.View.Width = 350;
                fld.Edit.Enable = false;
                fld.Edit.Control = faControl.AutoComplete;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "namerus";
                fld.LookUp.Table = "_base";
                cur.AddField(fld);
                #endregion

                list.MainCursor = cur;
                list.EditFormWidth = 450;
                list.Render(form1, this);
            }
            else
                form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));

        }
    }
}