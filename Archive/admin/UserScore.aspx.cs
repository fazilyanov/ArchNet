using System;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6
{
    public partial class UserScore : System.Web.UI.Page
    {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "score";
            if (Session["common_access_admin_rating_edit"] != null || Session["common_access_admin_rating_view"] != null)
            {
                list.IDBase = "0";
                list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;

                #region Шапка
                cur = new faCursor("_user_score");
                cur.TableID = 34;
                cur.Caption = "Админ / Эффективность / Баллы";
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

                //Дата Документа
                fld = new faField();
                fld.Data.FieldName = "when";
                fld.View.FormatString = "{0:dd.MM.yyyy}";
                fld.View.TextAlign = "center";
                fld.View.Hint = "Дата документа";
                fld.View.CaptionShort = "Дата док.";
                fld.View.Width = 84;
                fld.Edit.DefaultText = DateTime.Now.ToShortDateString();
                fld.Edit.Control = faControl.DatePicker;
                fld.Filter.Control = faControl.DatePicker;
                fld.Edit.Required = true;
                cur.AddField(fld);

                //
                fld = new faField();
                fld.Data.FieldName = "id_user";
                fld.View.CaptionShort = "ФИО";
                fld.View.Width = 350;
                fld.Edit.Required = true;
                fld.Edit.Control = faControl.AutoComplete;
                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "sname";
                fld.LookUp.Table = "_user";
                cur.AddField(fld);

                //
                fld = new faField();
                fld.Data.FieldName = "id_scoretype";
                fld.View.CaptionShort = "Тип балла";
                fld.View.Width = 150;
                fld.Edit.Required = true;
                fld.Edit.Control = faControl.DropDown;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_scoretype";
                cur.AddField(fld);
                
                // Балл
                fld = new faField();
                fld.Data.FieldName = "score";
                fld.View.CaptionShort = "Балл";
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