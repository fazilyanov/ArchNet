using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6 {
    public partial class PrjCode : System.Web.UI.Page {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e) {
            faCursor cur;
            faField fld;
            list = new faList();
            list.RouteName = "prjcode";

            list.IDBase = "0";
            list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;

            #region Шапка
            cur = new faCursor("_prjcode");
            cur.TableID = 21;
            cur.Caption = "Справочники / Коды проектов";

            cur.EnableViewButton = true;
            cur.EnableExcelButton = true;
            cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_prjcode_edit"] != null;
            cur.RelTable = new string[1] { "_archive.id_prjcode" };

            //ID
            fld = new faField();
            fld.Data.FieldName = "id";
            fld.View.CaptionShort = "ID";
            fld.View.TextAlign = "center";
            fld.View.Width = 100;
            fld.Edit.Enable = false;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            // Код проекта
            fld = new faField();
            fld.Data.FieldName = "code_new";
            fld.View.CaptionShort = "Код (новый)";
            fld.View.Width = 125;
            fld.Edit.Required = true;
            fld.Edit.Max = 50;
            cur.AddField(fld);

            // Код проекта
            fld = new faField();
            fld.Data.FieldName = "code_old";
            fld.View.CaptionShort = "Код (старый)";
            fld.View.Width = 125;
            fld.Edit.Max = 50;
            cur.AddField(fld);

            //ID 1C
            fld = new faField();
            fld.Data.FieldName = "id_1c";
            fld.View.CaptionShort = "ID 1C";
            fld.View.TextAlign = "center";
            fld.View.Width = 220;
            fld.Edit.Max = 50;
            cur.AddField(fld);


            // Наименование проекта
            fld = new faField();
            fld.Data.FieldName = "name";
            fld.View.CaptionShort = "Наименование";
            fld.View.Width = 300;
            fld.Filter.Caption = "Наим.проекта";
            cur.AddField(fld);

            // Заказчик
            fld = new faField();
            fld.Data.FieldName = "id_frm";
            fld.View.CaptionShort = "Заказчик";
            fld.View.Width = 210;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_frm";
            fld.LookUp.TableAlias = "frm1";
            cur.AddField(fld);

            // Договор
            fld = new faField();
            fld.Data.FieldName = "dogovor";
            fld.View.CaptionShort = "Договор";
            fld.View.Width = 215;
            fld.Edit.Max = 150;
            cur.AddField(fld);

            //Примечание
            fld = new faField();
            fld.Data.FieldName = "prim";
            fld.View.CaptionShort = "Примечание";
            fld.View.Width = 190;
            cur.AddField(fld);

            // Руководитель проекта
            //fld = new faField();
            //fld.Data.FieldName = "head";
            //fld.View.CaptionShort = "Руководитель";
            //fld.View.Width = 190;
            //cur.AddField(fld);
            #endregion

            list.MainCursor = cur;
            list.EditFormWidth = 450;
            list.Render(form1, this);
        }
    }
}