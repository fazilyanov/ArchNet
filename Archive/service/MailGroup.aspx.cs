using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6 {
    public partial class MailGroup : System.Web.UI.Page {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e) {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "mailgroup";
            if (Session["common_access_service_mailgroup"] != null) {

                list.IDBase = "0";
                list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;

                #region Шапка
                cur = new faCursor("_mailgroup");
                cur.TableID = 28;
                cur.Caption = "Сервис / Группы рассылок";
                cur.EnableViewButton = true;
                cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_service_mailgroup"] != null;
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
                fld.Data.FieldName = "name";
                fld.View.CaptionShort = "Наименование";
                fld.View.Width = 350;
                cur.AddField(fld);
                //
                #endregion

                list.MainCursor = cur;

                // 
                if (Request.QueryString["id"] != null || Request.QueryString["act"] != null)
                {

                    #region Курсор "Пользователи"
                    cur = new faCursor("_mailgroup_user");
                    cur.TableID = 29;
                    cur.Caption = "Пользователи";
                    cur.EnableViewButton = true;
                    cur.EnableAddButton = cur.EnableEditButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_service_mailgroup"] != null;

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
                    fld.Data.FieldName = "id_mailgroup";
                    fld.View.Visible = false;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    fld.Filter.Enable = true;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);

                    // Пользователь
                    fld = new faField();
                    fld.Data.FieldName = "id_user";
                    fld.View.CaptionShort = "Пользователь";
                    fld.View.Width = 430;
                    fld.Edit.Enable = true;
                    fld.Edit.Required = true;
                    fld.Edit.Unique = true;
                    fld.Edit.Control = faControl.AutoComplete;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "name";
                    fld.LookUp.Table = "_user";
                    cur.AddField(fld);
                    //
                    list.AddCursor(cur);
                    #endregion

                }
                //list.EditFormWidth = 450;
                list.Render(form1, this);
            }
            else
                form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));

        }
    }
}