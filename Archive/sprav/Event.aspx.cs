using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6
{
    public partial class Event: System.Web.UI.Page
    {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "event";

            list.IDBase = "0";
            list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;

            #region Шапка
            cur = new faCursor("_event");
            cur.TableID = 49;
            cur.Caption = "Справочники / События";
            cur.EnableViewButton = true;
            cur.EnableExcelButton = true;
            cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = true;
            cur.RelTable = new string[1] { "_monitor.id_event"};

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