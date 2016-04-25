using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace ArchNet {
    public partial class Frm : System.Web.UI.Page {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e) {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "frm";

            list.IDBase = "0";
            list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;
            list.ShowArrows = true;

            #region Шапка
            cur = new faCursor("_frm");
            cur.TableID = 2;
            cur.Caption = "Справочники / Фирмы";
            cur.EnableViewButton = true;
            cur.EnableExcelButton = true;
            cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_frm_edit"] != null;
            cur.RelTable = new string[4] { "_archive.id_frm_contr", "_archive.id_frm_dev", "_archive.id_frm_prod", "_prjcode.id_frm" };

            // ID
            fld = new faField();
            fld.Data.FieldName = "id";
            fld.View.CaptionShort = "ID";
            fld.View.TextAlign = "center";
            fld.View.Width = 100;
            fld.Edit.Enable = false;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            // Наименование
            fld = new faField();
            fld.Data.FieldName = "name";
            fld.View.CaptionShort = "Наименование";
            fld.View.Width = 400;
            fld.Edit.Required = true;
            fld.Edit.Max = 120;
            cur.AddField(fld);

            // Наименование полное
            fld = new faField();
            fld.Data.FieldName = "name_full";
            fld.Edit.Max = 250;
            fld.View.CaptionShort = "Наименование полное";
            fld.View.Width = 400;
            fld.Filter.Caption = "Наим.полное";
            cur.AddField(fld);

            // ИНН
            fld = new faField();
            fld.Data.FieldName = "inn";
            fld.View.CaptionShort = "ИНН";
            fld.View.Width = 80;
            cur.AddField(fld);

            // Код 1С
            fld = new faField();
            fld.Data.FieldName = "id_1c";
            fld.View.CaptionShort = "Код 1С";
            fld.View.Width = 80;
            fld.Edit.Min = 9;
            fld.Edit.Max = 9;
            cur.AddField(fld);

            #endregion

            list.MainCursor = cur;
            list.EditFormWidth = 450;
            list.Render(form1, this);
        }
    }
}