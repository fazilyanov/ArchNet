using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6
{
    public partial class Storage : System.Web.UI.Page
    {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "storage";

            list.IDBase = "0";
            list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;

            #region Шапка
            cur = new faCursor("_storage");
            cur.TableID = 37;
            cur.Caption = "Справочники / Места хранения";
            cur.EnableViewButton = true;
            cur.EnableExcelButton = true;
            cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_complect_regstor"] != null;
            //cur.RelTable = new string[4] { "_archive.id_frm_contr", "_archive.id_frm_dev", "_archive.id_frm_prod", "_prjcode.id_frm" };

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
            fld.View.Width = 400;
            fld.Edit.Required = true;
            cur.AddField(fld);

            #endregion

            list.MainCursor = cur;
            list.EditFormWidth = 450;
            list.Render(form1, this);

        }
    }
}