using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6 {
    public partial class UserBarcode : System.Web.UI.Page {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e) {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "userbarcode";
            if (Session["common_access_service_userbarcode"] != null) {

                list.IDBase = "0";
                list.BaseName = "";
                list.ShowFilterPanel = true;

                #region Шапка
                cur = new faCursor("_user_barcode");
                cur.TableID = 32;
                cur.Caption = "Сервис / Штрих-коды исполнителей";
                cur.EnableViewButton = true;
                cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_service_userbarcode"] != null;
                
                //
                fld = new faField();
                fld.Data.FieldName = "id";
                fld.View.CaptionShort = "ID";
                fld.View.Width = 50;
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
               
                // Штрихкод
                fld = new faField();
                fld.Data.FieldName = "barcode";
                fld.View.CaptionShort = "Штрихкод";
                fld.View.Width = 66;
                fld.View.TextAlign = "center";
                fld.Edit.Control = faControl.TextBoxInteger;
                fld.Edit.Min = 1000000000;
                fld.Edit.Max = Int32.MaxValue;
                fld.Edit.Unique = true;
                cur.AddField(fld);
                //
                
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