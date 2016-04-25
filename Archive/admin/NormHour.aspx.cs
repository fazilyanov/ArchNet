using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6
{
    public partial class NormHour : System.Web.UI.Page
    {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "normhour";
            if (Session["common_access_admin_rating_edit"] != null || Session["common_access_admin_rating_view"] != null)
            {
                list.IDBase = "0";
                list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;

                #region Шапка
                cur = new faCursor("_norm_hour");
                cur.TableID = 35;
                cur.Caption = "Админ / Эффективность / Норма рабочего времени";
                cur.EnableViewButton = true;
                cur.EnableExcelButton = true;
                cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_admin_rating_edit"] != null;
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

                // Год
                fld = new faField();
                fld.Data.FieldName = "year";
                fld.View.CaptionShort = "Год";
                fld.View.Width = 66;
                fld.View.TextAlign = "center";
                fld.Edit.Required = true;
                fld.Edit.Control = faControl.TextBoxInteger;
                fld.Edit.Min = 2015;
                fld.Edit.DefaultText = "2015";
                fld.Edit.Max = 2020;
                cur.AddField(fld);

                //
                fld = new faField();
                fld.Data.FieldName = "month";
                fld.View.CaptionShort = "Месяц";
                fld.View.Width = 75;
                fld.Edit.Required = true;
                fld.Edit.Control = faControl.DropDown;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_month";
                cur.AddField(fld);

                ////
                //fld = new faField();
                //fld.Data.FieldName = "id_scoretype";
                //fld.View.CaptionShort = "Тип балла";
                //fld.View.Width = 75;
                //fld.Edit.Required = true;
                //fld.Edit.Control = faControl.DropDown;
                //fld.Filter.Control = faControl.DropDown;
                //fld.LookUp.Key = "id";
                //fld.LookUp.Field = "name";
                //fld.LookUp.Table = "_scoretype";
                //cur.AddField(fld);

                //// Балл
                //fld = new faField();
                //fld.Data.FieldName = "score";
                //fld.View.CaptionShort = "Балл";
                //fld.View.Width = 66;
                //fld.View.TextAlign = "center";
                //fld.Edit.Control = faControl.TextBoxInteger;
                //cur.AddField(fld);

                // Порог
                fld = new faField();
                fld.Data.FieldName = "min";
                fld.View.CaptionShort = "Порог";
                fld.View.Width = 66;
                fld.View.TextAlign = "center";
                fld.Edit.Required = true;
                fld.Edit.Control = faControl.TextBoxInteger;
                cur.AddField(fld);

                // Максимум
                fld = new faField();
                fld.Data.FieldName = "max";
                fld.View.CaptionShort = "Максимум";
                fld.View.Width = 66;
                fld.View.TextAlign = "center";
                fld.Edit.Required = true;
                fld.Edit.Control = faControl.TextBoxInteger;
                cur.AddField(fld);

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