using System;
using Trirand.Web.UI.WebControls;
using System.Web.UI;

namespace ArchNet {
    public partial class Person : System.Web.UI.Page {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e) {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "person";
            list.IDBase = Session[Master.cur_basename + "_id"].ToString();
            list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;

            #region Шапка
            cur = new faCursor(list.BaseName + "_person");
            cur.TableID = 16;
            cur.Caption = "Справочники / Сотрудники";

            cur.EnableViewButton = true;
            cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session[Master.cur_basename + "_access_person_edit"] != null;
            cur.RelTable = new string[1] { "_archive.id_perf" };
            cur.RelBase = list.BaseName;// справочник person есть у каждой базы

            //ID
            fld = new faField();
            fld.Data.FieldName = "id";
            fld.View.CaptionShort = "ID";
            fld.View.TextAlign = "center";
            fld.View.Width = 100;
            fld.Edit.Enable = false;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);
            
            ////ID 1C
            //fld = new faField();
            //fld.Data.FieldName = "id_1c";
            //fld.View.CaptionShort = "ID 1C";
            //fld.View.TextAlign = "center";
            //fld.View.Width = 200;
            //fld.Edit.Max = 50;
            //cur.AddField(fld);

            //ФИО
            fld = new faField();
            fld.Data.FieldName = "name";
            fld.View.CaptionShort = "ФИО";
            fld.View.Width = 250;
            fld.Edit.Required = true;
            fld.Edit.Max = 25;
            cur.AddField(fld);

            //ФИО
            fld = new faField();
            fld.Data.FieldName = "name_full";
            fld.View.CaptionShort = "ФИО полное";
            fld.View.Width = 350;
            fld.Edit.Max = 50;
            cur.AddField(fld);

            ////ФИО
            //fld = new faField();
            //fld.Data.FieldName = "id_depart";
            //fld.View.CaptionShort = "Подразделение";
            //fld.View.Width = 180;
            //fld.Edit.Required = true;
            //fld.Edit.Control = faControl.TreeGrid;
            //fld.Filter.Control = faControl.TreeGrid;
            //fld.LookUp.Key = "id";
            //fld.LookUp.Field = "name";
            //fld.LookUp.Table = list.BaseName + "_department";
            //cur.AddField(fld);

            //// Статус
            //fld = new faField();
            //fld.Data.FieldName = "id_status";
            //fld.View.CaptionShort = "Статус";
            //fld.View.Width = 100;
            //fld.Edit.Control = faControl.DropDown;
            //fld.Filter.Control = faControl.DropDown;
            //fld.LookUp.Key = "id";
            //fld.LookUp.Field = "name";
            //fld.LookUp.Table = "_status_person";
            //cur.AddField(fld);

            //Примечание
            fld = new faField();
            fld.Data.FieldName = "prim";
            fld.View.CaptionShort = "Примечание";
            fld.View.Width = 350;
            fld.Edit.Max = 100;
            cur.AddField(fld);

            

            #endregion

            list.MainCursor = cur;
            list.EditFormWidth = 450;
            list.Render(form1, this);
        }
    }
}