using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6
{
    public partial class Department : System.Web.UI.Page
    {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "department";

            list.IDBase = Session[Master.cur_basename + "_id"].ToString();
            list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;


            cur = new faCursor(list.BaseName + "_department");
            cur.TableID = 41;
            cur.Caption = "Справочники / Подразделения";
            cur.EnableViewButton = true;
            cur.EnableExcelButton = true;
             cur.EnableCopyButton = false;
            cur.EnableAddButton = cur.EnableDelButton = cur.EnableSaveButton = Session[Master.cur_basename + "_access_department_edit"] != null;
            cur.RelTable = new string[2] { "_archive.id_depart","_person.id_depart" };
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
            fld.View.Width = 400;
            fld.Edit.Required = true;
            cur.AddField(fld);

            //Родитель
            fld = new faField();
            fld.Data.FieldName = "id_parent";
            fld.View.CaptionShort = "Родитель";
            fld.View.Width = 180;
            //fld.Edit.Required = true;
            fld.Edit.Control = faControl.TreeGrid;
            fld.Filter.Control = faControl.TreeGrid;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = list.BaseName + "_department";
            cur.AddField(fld);

            //ID 1C
            fld = new faField();
            fld.Data.FieldName = "id_1c";
            fld.View.CaptionShort = "ID 1C";
            fld.View.TextAlign = "center";
            fld.View.Width = 200;
            fld.Edit.Max = 50;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            list.MainCursor = cur;
            list.EditFormWidth = 450;
            list.Render(form1, this);
        }
    }
}