using System;
using System.Web.UI;

namespace WebArchiveR6
{
    public partial class ArchiveDub : System.Web.UI.Page
    {
        public faList list;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;

            list = new faList();
            list.RouteName = "archivedub";
            list.EditFormWidth = 1165;
            list.Page = faPage.srch;
            if (Session[Master.cur_basename + "_access_archive_" + list.Page + "_view"] != null || Session[Master.cur_basename + "_access_archive_" + list.Page + "_edit"] != null || list.Page == faPage.select)
            {
                list.IDBase = Session[Master.cur_basename + "_id"].ToString();
                list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;
                if (Session[Master.cur_basename + "_access_archive_" + list.Page + "_edit"] != null)
                {
                    list.ActionMenuItems.Add("Поиск в Журнале", "window.open('/service/" + list.BaseName + "/journal?id_table=1&id_table_text=Архив&change_id=" + (Request.QueryString["id"] ?? "").ToString() + "');");
                    list.ActionMenuItems.Add("Поиск в Журнале выбранной версии",
                         " var grid = jQuery('#cph_jqGrid" + list.BaseName + "_docversion');" +
                            " var id = grid.jqGrid('getGridParam', 'selrow');" +
                            " if (id>0) { " +
                            "    window.open('/service/" + list.BaseName + "/journal?id_table=3&id_table_text=Версии&change_id='+id);" +
                            " }else alert('Версия не выбрана');  return false;");
                    //id="cph_jqGridzao_stg_docversion"
                    list.ActionMenuItems.Add("Поиск по Структуре", "window.open('/archivestructur/" + list.BaseName + "/?id=" + (Request.QueryString["id"] ?? "").ToString() + "');");
                    list.ActionMenuItems.Add("Поиск в комплектах", "window.open('/complectnewlist/?id_archive=" + (Request.QueryString["id"] ?? "").ToString() + "&id_base=" + list.IDBase + "');");
                    list.ActionMenuItems.Add("Копировать в другую базу", "window.open('/copytobase/" + list.BaseName + "/" + list.Page.ToString() + "/" + (Request.QueryString["id"] ?? "0").ToString() + "');");
                }
                ////
                list.ShowCheckBox = Session[Master.cur_basename + "_access_show_checkbox"] != null;
                list.EnableSuperVisorCheckBox = Session["common_access_enable_supervisor_checkbox"] != null;
                list.ShowHiddenDoc = Session[Master.cur_basename + "_access_archive_hidden"] != null;
                list.ShowArrows = true;
                ////

                // Шапка
                cur = new faCursor(Master.cur_basename + "_archive_dub_view");
                cur.TableID = 1;
                cur.Caption = "Архив / Дубликаты документов";
                cur.EnableViewButton = false;
                cur.EnableFileButton = cur.EnableExcelButton = cur.EnableCSVButton = true;
                cur.EnableAddButton = cur.EnableDelButton = cur.EnableSaveButton = false;
                

                //cur.RelTable = new string[1] { "_archive.id_parent" };
                if (Session[cur.Alias + "_sortcolumn"] == null)
                {
                    cur.SortColumn = "id";
                    cur.SortDirection = "desc";
                }

                #region Поля

                //Код ЭА
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

                //Дата Редактирования
                fld = new faField();
                fld.Data.FieldName = "date_upd";
                fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
                fld.View.TextAlign = "center";
                fld.View.Hint = "Дата редактирования";
                fld.View.CaptionShort = "Дата ред.";
                fld.View.Width = 98;
                fld.Edit.Visible = true;
                fld.Edit.Enable = false;
                fld.Edit.BaseCopied = false;
                fld.Edit.Auto = faAutoType.NowDateTime;
                fld.Edit.Max = 250;
                fld.Filter.Control = faControl.DateTimePicker;
                cur.AddField(fld);

                //Оператор ЭА
                fld = new faField();
                fld.Data.FieldName = "id_user";
                fld.View.Width = 142;
                fld.View.CaptionShort = "Оператор ЭА";
                fld.Edit.Enable = false;
                fld.Edit.BaseCopied = false;
                fld.Edit.Control = faControl.AutoComplete;
                fld.Edit.Auto = faAutoType.UserID;
                fld.Edit.AddOnly = (Session[Master.cur_basename + "_id_role"] ?? "").ToString() == "8"; // Супервайзеры не меняют поле "Оператор"

                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "sname";
                fld.LookUp.Table = "_user";
                cur.AddField(fld);


                if (list.Page == faPage.srch)
                {
                    fld = new faField();
                    fld.Data.FieldName = "id_doctree2";
                    fld.Data.FieldCalc = "a.id_doctree";
                    fld.Edit.Enable = false;
                    fld.Edit.Visible = false;
                    fld.View.Visible = false;
                    fld.Filter.Enable = false;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);
                }

                //Документ
                fld = new faField();
                fld.Data.FieldName = "id_doctree";
                //if (Session[Master.cur_basename + "_id_role"].ToString() == "32")
                //    fld.Data.Where = "id_doctree in (5022,5023,5029)";
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
                else if (list.Page == faPage.select)
                {
                    fld.Filter.DefaultValue = (Request.QueryString["m"] ?? "").ToString() == "2" ? "(7)" : "(15)";// Старший документ из блока договоров
                }
                else fld.Filter.DefaultValue = "(" + ((int)list.Page).ToString() + ")";
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_doctree";
                cur.AddField(fld);

                //Номер документа
                fld = new faField();
                fld.Data.FieldName = "num_doc";
                fld.View.CaptionShort = "№ Документа";
                fld.View.Width = 180;
                fld.Edit.Required = true;
                fld.Edit.Max = 250;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                //Дата Документа
                fld = new faField();
                fld.Data.FieldName = "date_doc";
                fld.Data.Where = ((Session[Master.cur_basename + "_id_role"] ?? 0).ToString() == "32") ? "date_doc<= CONVERT(DATETIME,'31.12.2015 00:00',104)" : "";
                fld.View.FormatString = "{0:dd.MM.yyyy}";
                fld.View.TextAlign = "center";
                fld.View.Hint = "Дата документа";
                fld.View.CaptionShort = "Дата док.";
                fld.View.Width = 84;
                //fld.Edit.DefaultText = DateTime.Now.ToShortDateString();
                fld.Edit.Control = faControl.DatePicker;
                fld.Filter.Control = faControl.DatePicker;
                fld.Edit.Required = true;
                cur.AddField(fld);

                //Вид документа
                if (list.Page != faPage.dog && list.Page != faPage.empl && list.Page != faPage.tech)
                {
                    fld = new faField();
                    fld.Data.FieldName = "id_doctype";
                    fld.View.Hint = "Вид документа";
                    fld.View.CaptionShort = "Вид док.";
                    fld.View.Width = 75;
                    fld.Edit.Required = true;
                    fld.Edit.Control = faControl.DropDown;
                    fld.Filter.Caption = "Вид докум.";
                    fld.Filter.Control = faControl.DropDown;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "name";
                    fld.LookUp.Table = "_doctype";
                    cur.AddField(fld);
                }

                //Контрагент
                if (list.Page != faPage.empl && list.Page != faPage.norm)
                {
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
                }

                //Сумма
                if (list.Page != faPage.ord && list.Page != faPage.empl && list.Page != faPage.tech && list.Page != faPage.norm)
                {
                    fld = new faField();
                    fld.Data.FieldName = "summ";
                    fld.View.CaptionShort = "Сумма";
                    fld.View.FormatString = "{0:n2}";
                    fld.View.TextAlign = "right";
                    fld.View.Width = 83;
                    fld.Edit.Control = faControl.TextBoxNumber;
                    fld.Filter.Control = faControl.TextBoxNumber;
                    cur.AddField(fld);
                }

                //Пакет документов
                if (list.Page != faPage.dog)
                {
                    fld = new faField();
                    fld.Data.FieldName = "docpack";
                    fld.View.Hint = "Пакет документов";
                    fld.View.CaptionShort = "Пакет";
                    fld.View.Width = 56;
                    fld.Edit.BaseCopied = false;
                    fld.Edit.Control = faControl.NewWindowArchiveID;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    //fld.LookUp.Again = true;
                    //fld.LookUp.Key = "id";
                    //fld.LookUp.Field = "id";
                    //fld.LookUp.Table = list.BaseName + "_archive";
                    cur.AddField(fld);
                    //fld.Edit.Control = faControl.NewWindowArchive;
                    //    fld.Filter.Control = faControl.AutoComplete;
                }

                //Код проекта
                if (list.Page != faPage.ord && list.Page != faPage.empl && list.Page != faPage.tech && list.Page != faPage.oth && list.Page != faPage.norm)
                {
                    fld = new faField();
                    fld.Data.FieldName = "id_prjcode";
                    fld.View.CaptionShort = "Код проекта";
                    fld.View.Width = 110;
                    fld.Edit.Control = faControl.AutoComplete;
                    fld.Edit.BaseCopied = false;
                    fld.Filter.Control = faControl.AutoComplete;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "code_new";
                    fld.LookUp.Table = "_prjcode";
                    cur.AddField(fld);
                }

                //Старший документ
                if (list.Page != faPage.ord && list.Page != faPage.tech && list.Page != faPage.norm)
                {
                    fld = new faField();
                    fld.Data.FieldName = "id_parent";
                    fld.View.Hint = "Старший документ";
                    fld.View.CaptionShort = "Старший док.";
                    fld.View.Width = 140;
                    fld.Edit.Control = faControl.NewWindowArchive;
                    fld.Edit.BaseCopied = false;
                    fld.Filter.Control = faControl.AutoComplete;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "num_doc";
                    fld.LookUp.Table = list.BaseName + "_archive";
                    cur.AddField(fld);
                }

                //Исполнитель
                if (list.Page != faPage.empl && list.Page != faPage.tech)
                {
                    fld = new faField();
                    fld.Data.FieldName = "id_perf";
                    fld.View.CaptionShort = "Исполнитель";
                    fld.View.Width = 125;
                    fld.Edit.Control = faControl.AutoComplete;
                    fld.Edit.BaseCopied = false;
                    fld.Filter.Control = faControl.AutoComplete;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "name";
                    fld.LookUp.Table = list.BaseName + "_person";
                    cur.AddField(fld);
                }

                // Получатель документа
                if (list.Page != faPage.tech)
                {
                    //fld = new faField();
                    //fld.Data.FieldName = "id_depart";
                    //fld.View.CaptionShort = "Получатель";
                    //fld.View.Width = 180;
                    //fld.Edit.Required = true;
                    //fld.Edit.BaseCopied = false;
                    //fld.Edit.Control = faControl.AutoComplete;
                    //fld.Filter.Control = faControl.AutoComplete;
                    ////fld.Filter.DefaultValue = ((int)list.Page).ToString();
                    //fld.LookUp.Key = "id";
                    //fld.LookUp.Field = "name";
                    //fld.LookUp.Table = list.BaseName + "_depart";
                    //cur.AddField(fld);

                    fld = new faField();
                    fld.Data.FieldName = "id_depart";
                    fld.View.CaptionShort = "Получатель";
                    fld.View.Width = 180;
                    fld.Edit.Required = true;
                    fld.Edit.BaseCopied = false;
                    fld.Edit.Control = faControl.TreeGrid;
                    fld.Filter.Control = faControl.TreeGrid;
                    //fld.Filter.DefaultValue += "(-1)";
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "name";
                    fld.LookUp.Table = list.BaseName + "_department";
                    cur.AddField(fld);
                }

                //Дата создания (Из Версий)
                fld = new faField();
                fld.Data.FieldName = "date_reg";
                fld.Data.Table = Master.cur_basename + "_docversion";
                fld.Data.RefField = "id_archive";
                fld.Data.Where = "main=1";
                fld.View.FormatString = "{0:dd.MM.yyyy}";
                fld.View.TextAlign = "center";
                fld.View.Hint = "Дата создания основной версии";
                fld.View.CaptionShort = "Дата созд.";
                fld.View.Width = 84;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                fld.Filter.Control = faControl.DatePicker;
                cur.AddField(fld);

                //Путь к файлу (Из Версий)
                fld = new faField();
                fld.Data.FieldName = "file";
                fld.Data.Table = Master.cur_basename + "_docversion";
                fld.Data.Again = true;
                fld.Data.RefField = "id_archive";
                fld.Data.Where = "main=1";
                fld.View.Visible = false;
                fld.View.CaptionShort = "Файл";
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                fld.Filter.Enable = false;
                fld.Filter.Control = faControl.DatePicker;
                cur.AddField(fld);

                //Дата передачи (Из Версий)
                fld = new faField();
                fld.Data.FieldName = "date_trans";
                fld.Data.Table = Master.cur_basename + "_docversion";
                fld.Data.Again = true;
                fld.Data.RefField = "id_archive";
                fld.Data.Where = "main=1";
                fld.View.FormatString = "{0:dd.MM.yyyy}";
                fld.View.TextAlign = "center";
                fld.View.Hint = "Дата передачи основной версии";
                fld.View.CaptionShort = "Дата перед.";
                fld.View.Width = 84;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                fld.Filter.Control = faControl.DatePicker;
                cur.AddField(fld);

                //Штрих код (Из Версий)
                fld = new faField();
                fld.Data.FieldName = "barcode";
                fld.Data.Table = Master.cur_basename + "_docversion";
                fld.Data.Again = true;
                fld.Data.RefField = "id_archive";
                fld.Data.Where = "main=1";
                fld.View.TextAlign = "center";
                fld.View.CaptionShort = "Штрихкод";
                fld.View.Width = 84;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                if (list.Page == faPage.tech)
                {
                    // Изготовитель
                    fld = new faField();
                    fld.Data.FieldName = "id_frm_prod";
                    fld.View.CaptionShort = "Изготовитель";
                    fld.View.Width = 210;
                    fld.Edit.Control = faControl.AutoComplete;
                    fld.Filter.Control = faControl.AutoComplete;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "name";
                    fld.LookUp.Table = "_frm";
                    fld.LookUp.TableAlias = "frm2";
                    cur.AddField(fld);

                    // Разработчик
                    fld = new faField();
                    fld.Data.FieldName = "id_frm_dev";
                    fld.View.CaptionShort = "Разработчик";
                    fld.View.Width = 210;
                    fld.Edit.Control = faControl.AutoComplete;
                    fld.Filter.Control = faControl.AutoComplete;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "name";
                    fld.LookUp.Table = "_frm";
                    fld.LookUp.TableAlias = "frm3";
                    cur.AddField(fld);
                }

                // Содержание документа
                if (list.Page != faPage.acc)
                {
                    fld = new faField();
                    fld.Data.FieldName = "content";
                    fld.View.Width = 240;
                    fld.View.Hint = "Содержание документа";
                    fld.View.CaptionShort = "Содержание";
                    fld.Edit.Max = 250;
                    cur.AddField(fld);
                }

                //Примечание
                fld = new faField();
                fld.Data.FieldName = "prim";
                fld.Edit.BaseCopied = false;
                fld.View.CaptionShort = "Примечание";
                fld.View.Width = 190;
                cur.AddField(fld);

                //Статус (Из Версий)
                fld = new faField();
                fld.Data.FieldName = "id_status";
                fld.Data.Table = Master.cur_basename + "_docversion";
                fld.Data.Again = true;
                fld.Data.RefField = "id_archive";
                fld.Data.Where = "main=1";
                fld.View.CaptionShort = "Статус";
                fld.View.Hint = "Статус основной версии";
                fld.View.Width = 84;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_status";
                cur.AddField(fld);

                ////Индекс дела
                //fld = new faField();
                //fld.Data.FieldName = "indexd";
                //fld.View.CaptionShort = "Индекс дела";
                //fld.View.Width = 150;
                //cur.AddField(fld);

                //Текст документа
                fld = new faField();
                fld.Data.FieldName = "doctext";
                fld.View.Visible = false;
                fld.View.CaptionShort = "Текст документа";
                fld.Edit.Control = faControl.TextArea;
                fld.Filter.Caption = "Текст док.";
                fld.Filter.Control = faControl.TextBoxFullSearch;
                cur.AddField(fld);

                //Состояние
                if (Session[Master.cur_basename + "_access_archive_statefield"] != null)
                {
                    fld = new faField();
                    fld.Data.FieldName = "id_state";
                    fld.View.Width = 100;
                    fld.View.CaptionShort = "Состояние";
                    fld.Edit.Control = faControl.DropDown;
                    fld.Edit.DefaultValue = "1";
                    fld.Edit.DefaultText = "Завершен";
                    fld.Edit.Copied = false;
                    fld.Edit.BaseCopied = false;
                    fld.Edit.Required = true;
                    fld.Filter.Control = faControl.DropDown;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "name";
                    fld.LookUp.Table = "_state";
                    cur.AddField(fld);
                }

                //Скрытый документ

                fld = new faField();
                fld.Data.FieldName = "hidden";
                fld.View.Width = 100;
                fld.View.CaptionShort = "Скрытый";
                fld.View.Visible = list.ShowHiddenDoc;
                fld.Edit.Control = faControl.DropDown;
                fld.Edit.Required = true;
                fld.Edit.DefaultValue = "1";
                fld.Edit.DefaultText = "Нет";
                fld.Edit.Copied = false;
                fld.Edit.BaseCopied = false;
                fld.Filter.Control = faControl.DropDown;
                fld.Filter.Enable = list.ShowHiddenDoc;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_yesno";
                fld.LookUp.TableAlias = "yn1";
                cur.AddField(fld);

                //Принят к учету

                fld = new faField();
                fld.Data.FieldName = "accept";
                fld.View.Width = 100;
                fld.View.CaptionShort = "Принят к учету";
                fld.Edit.Control = faControl.DropDown;
                fld.Edit.Visible = true;
                fld.Edit.Enable = false;
                //fld.Edit.DefaultValue = "1";
                //fld.Edit.DefaultText = "Нет";
                fld.Edit.Copied = false;
                fld.Edit.BaseCopied = false;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_yesno";
                fld.LookUp.TableAlias = "yn2";
                cur.AddField(fld);

                //Комментарий к документу
                if (Session["common_access_archive_servprim"] != null)
                {
                    fld = new faField();
                    fld.Data.FieldName = "servprim";
                    fld.Edit.BaseCopied = false;
                    fld.View.CaptionShort = "Комментарий к документу";
                    fld.Filter.Caption = "Комментарий";
                    fld.View.Width = 190;
                    cur.AddField(fld);
                }

                #endregion Поля

                list.MainCursor = cur;
                list.Render(form1, this);
            }
            else
                form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}