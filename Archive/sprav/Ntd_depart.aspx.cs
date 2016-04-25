using System;

namespace WebArchiveR6
{
    public partial class Ntd_depart : System.Web.UI.Page
    {
        public faList list;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "ntddepart";

            list.IDBase = "0";
            list.BaseName = "";
            list.ShowFilterPanel = true;
            list.JSReadyList.Add("hide",
                "$('.navbar-brand').html('Корпоративный фонд нормативно-технической документации (Фонд НТД)');" +
                "$('.navbar-brand').attr('href', '/ntdstart');" +
                "$('.navbar-right').html(''); " +
                "$('#_ntd_categoryid_category').parent().parent().hide();" +
                "$('#clear__ntd_categoryid_category').remove();");

            #region Шапка

            cur = new faCursor("_ntd_depart");
            cur.TableID = 45;
            cur.Caption = "Справочники / Подразделения для НТД";
            cur.EnableViewButton = true;
            cur.EnableExcelButton = true;
            cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_ntd_edit"] != null;
            cur.RelTable = new string[2] { "_ntd.id_resp", "_ntd.id_storage" };

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

            #endregion Шапка

            list.MainCursor = cur;
            list.EditFormWidth = 450;
            list.Render(form1, this);
        }
    }
}