using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6 {
    public partial class Depart : System.Web.UI.Page {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e) {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "depart";

            list.IDBase = Session[Master.cur_basename + "_id"].ToString();
            list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;

            #region Шапка
            cur = new faCursor(list.BaseName + "_depart");
            cur.TableID = 17;
            cur.Caption = "Справочники / Подразделения";
            cur.EnableViewButton = true;
            cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session[Master.cur_basename + "_access_depart_edit"] != null;
            cur.RelTable = new string[1] { "_archive.id_depart" };
            cur.RelBase = list.BaseName;// справочник depart есть у каждой базы

            //ID
            fld = new faField();
            fld.Data.FieldName = "id";
            fld.View.CaptionShort = "ID";
            fld.View.TextAlign = "center";
            fld.View.Width = 100;
            fld.Edit.Enable = false;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            //Наименование
            fld = new faField();
            fld.Data.FieldName = "name";
            fld.View.CaptionShort = "Наименование";
            fld.View.Width = 380;
            fld.Edit.Required = true;
            cur.AddField(fld);

            //Наименование
            fld = new faField();
            fld.Data.FieldName = "prim";
            fld.View.CaptionShort = "Примечание";
            fld.View.Width = 200;
            cur.AddField(fld);

            fld = new faField();
            fld.Data.FieldName = "active";
            fld.View.Width = 100;
            fld.View.CaptionShort = "Действует";
            fld.Edit.Control = faControl.DropDown;
            fld.Edit.Required = true;
            fld.Edit.DefaultValue = "2";
            fld.Edit.DefaultText = "Да";
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_yesno";
            fld.LookUp.TableAlias = "yn1";
            cur.AddField(fld);

            #endregion

            list.MainCursor = cur;
            list.EditFormWidth = 450;
            list.Render(form1, this);
        }
    }
}