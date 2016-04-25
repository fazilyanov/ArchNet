using System;
using System.Web.UI;

namespace WebArchiveR6
{
    public partial class ArchiveDublicate : System.Web.UI.Page
    {
        public faListDublicate list;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;

            list = new faListDublicate();
            list.RouteName = "archivedub";
            list.Page = faPage.srch;

            if (Session[Master.cur_basename + "_access_archive_acc_edit"] != null)
            {
                list.IDBase = Session[Master.cur_basename + "_id"].ToString();
                list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;
                list.ShowHiddenDoc = Session[Master.cur_basename + "_access_archive_hidden"] != null;
                
                // Шапка
                cur = new faCursor(Master.cur_basename + "_archive_dub");
                cur.TableID = 1;
                cur.Caption = "Архив / Дубликаты документов";
                cur.EnableExcelButton = cur.EnableCSVButton = true;
                cur.EnableAddButton = cur.EnableDelButton = cur.EnableSaveButton = cur.EnableCopyButton = false;

                #region Поля

                // Код ЭА
                fld = new faField();
                fld.Data.FieldName = "id";
                fld.View.Hint = "Код Электронного Архива";
                fld.View.CaptionShort = "Код ЭА";
                fld.View.TextAlign = "center";
                fld.View.Width = 68;
                fld.Edit.Enable = false;
                fld.Edit.BaseCopied = false;
                fld.Filter.Control = faControl.TextBoxInteger;
                cur.AddField(fld);

                // Документ
                fld = new faField();
                fld.Data.FieldName = "id_doctree";
                fld.Filter.DefaultValue = "";
                fld.View.CaptionShort = "Документ";
                fld.View.Width = 180;
                fld.Edit.Required = true;
                fld.Edit.Control = faControl.TreeGrid;
                fld.Filter.Control = faControl.TreeGrid;
                //
                if (list.Page == faPage.srch)
                {
                    string[] _p = { "acc", "dog", "ord", "oth", "empl", "ohs", "tech", "bank", "norm" };
                    fld.Filter.DefaultValue = "";
                    foreach (string name in _p)//Enum.GetNames(typeof(faPage))
                        if (Session[Master.cur_basename + "_access_archive_" + name + "_view"] != null || Session[Master.cur_basename + "_access_archive_" + name + "_edit"] != null)
                            fld.Filter.DefaultValue += "(" + ((int)faFunc.GetPageType(name)).ToString() + "),";
                    fld.Filter.DefaultValue += "(-1)";
                }
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_doctree";
                cur.AddField(fld);

                // Номер документа
                fld = new faField();
                fld.Data.FieldName = "num_doc";
                fld.View.CaptionShort = "№ Документа";
                fld.View.Width = 180;
                fld.Edit.Required = true;
                fld.Edit.Max = 250;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                // Дата Документа
                fld = new faField();
                fld.Data.FieldName = "date_doc";
                fld.View.FormatString = "{0:dd.MM.yyyy}";
                fld.View.TextAlign = "center";
                fld.View.Hint = "Дата документа";
                fld.View.CaptionShort = "Дата док.";
                fld.View.Width = 84;
                fld.Edit.Control = faControl.DatePicker;
                fld.Filter.Control = faControl.DatePicker;
                fld.Edit.Required = true;
                cur.AddField(fld);

                // Контрагент

                fld = new faField();
                fld.Data.FieldName = "id_frm_contr";
                fld.View.CaptionShort = "Контрагент";
                fld.View.Width = 210;
                fld.Edit.Required = list.Page == faPage.dog;
                fld.Edit.Control = faControl.AutoComplete;
                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_frm";
                fld.LookUp.TableAlias = "frm1";
                cur.AddField(fld);

                // Сумма

                fld = new faField();
                fld.Data.FieldName = "summ";
                fld.View.CaptionShort = "Сумма";
                fld.View.FormatString = "{0:n2}";
                fld.View.TextAlign = "right";
                fld.View.Width = 83;
                fld.Edit.Control = faControl.TextBoxNumber;
                fld.Filter.Control = faControl.TextBoxNumber;
                cur.AddField(fld);

                #endregion Поля

                list.MainCursor = cur;

                list.Render(form1, this);
            }
            else
                form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}