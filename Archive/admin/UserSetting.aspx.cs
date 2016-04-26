using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace ArchNet {
    public partial class UserSetting : System.Web.UI.Page {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e) {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "usersetting";
            if (Session["common_access_admin_user_view"] != null || Session["common_access_admin_user_edit"] != null) {

                list.IDBase = "0";
                list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;

                #region Шапка
                cur = new faCursor("_user_setting");
                cur.TableID = 6;
                cur.Caption = "Администратор / Настройки пользователей";
                cur.EnableViewButton = true;
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
                fld.View.CaptionShort = "ФИО";
                fld.View.Width = 300;
                fld.Edit.Enable = false;
                fld.Edit.Control = faControl.AutoComplete;
                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_user";
                cur.AddField(fld);

                //Дата
                fld = new faField();
                fld.Data.FieldName = "last_upd";
                fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
                fld.View.TextAlign = "center";
                fld.View.CaptionShort = "Обновление";
                fld.View.Width = 98;
                fld.Edit.Visible = true;
                fld.Edit.Enable = false;
                fld.Edit.Auto = faAutoType.NowDateTime;
                fld.Edit.Max = 250;
                fld.Filter.Control = faControl.DateTimePicker;
                cur.AddField(fld);

                //Ключ
                fld = new faField();
                fld.Data.FieldName = "key";
                fld.View.CaptionShort = "Параметр";
                fld.View.Width = 200;
                cur.AddField(fld);

                //Значение
                fld = new faField();
                fld.Data.FieldName = "value";
                fld.View.CaptionShort = "Значение";
                fld.View.Width = 300;
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