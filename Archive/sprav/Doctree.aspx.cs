using System;

namespace ArchNet
{
    public partial class Doctree : System.Web.UI.Page
    {
        public faList list;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "doctree";

            list.IDBase = "0";
            list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;

            #region Шапка

            cur = new faCursor("_doctree");
            cur.TableID = 24;
            cur.Caption = "Справочники / Формы документов";
            cur.EnableViewButton = cur.EnableExcelButton = true;
            cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_doctree_edit"] != null;
            cur.RelTable = new string[1] { "_archive.id_doctree" };

            //ID
            fld = new faField();
            fld.Data.FieldName = "id";
            fld.View.CaptionShort = "ID";
            fld.View.TextAlign = "center";
            fld.View.Width = 100;
            fld.Edit.Enable = false;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            //Родитель
            //!!!  костыль - Для показа всего дерева
            list.Page = faPage.srch;
            //!!!

            fld = new faField();
            fld.Data.FieldName = "id_parent";
            fld.View.CaptionShort = "Родитель";
            fld.View.Width = 180;
            fld.Edit.Required = true;
            fld.Edit.Control = faControl.TreeGrid;
            fld.Filter.Control = faControl.TreeGrid;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_doctree";
            cur.AddField(fld);
            //

            //Наименование
            fld = new faField();
            fld.Data.FieldName = "name";
            fld.View.CaptionShort = "Наименование";
            fld.View.Width = 400;
            fld.Edit.Required = true;
            fld.Edit.Max = 250;
            cur.AddField(fld);

            //Форма
            fld = new faField();
            fld.Data.FieldName = "form";
            fld.View.CaptionShort = "Форма";
            fld.View.Width = 100;
            fld.Edit.Max = 100;
            cur.AddField(fld);

            #endregion Шапка

            list.MainCursor = cur;
            list.EditFormWidth = 450;
            list.Render(form1, this);
        }
    }
}