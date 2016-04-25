using System;
using System.Web.UI;

namespace ArchNet
{
    public partial class ArchiveDel : System.Web.UI.Page
    {
        public faListDel list;
        protected void Page_PreRender(object sender, EventArgs e)
        {

            faField fld;
            faCursor cur;

            list = new faListDel();
            list.RouteName = "archive";
            list.EditFormWidth = 1165;
            list.Page = faPage.srch;
            if (Session[Master.cur_basename + "_access_archive_" + list.Page + "_edit"] != null )
            {
                list.IDBase = Session[Master.cur_basename + "_id"].ToString();
                list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;
                list.ShowHiddenDoc = true;
                list.ShowDeletedOnly = true;
                ////


                // Шапка
                cur = new faCursor(Master.cur_basename + "_archive");
                cur.AliasAlt = cur.Alias + "_" + list.Page;
                cur.TableID = 1;
                cur.Caption = "Архив / Удаленные записи";
                cur.EnableViewButton = true;
                cur.EnableFileButton = cur.EnableExcelButton = cur.EnableCSVButton = false;
                cur.EnableAddButton = cur.EnableDelButton = cur.EnableSaveButton = false;

                cur.EnableCopyButton = false;
                cur.EnablAllFileButton = false;
                cur.EnableBulkEditButton = false;

                //cur.RelTable = new string[1] { "_archive.id_parent" };
                cur.SortColumn = "id";
                cur.SortDirection = "desc";

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

                //// Департамент оператора
                //fld = new faField();
                //fld.Data.FieldName = "id_department";
                //fld.Data.Table = "_user";
                //fld.Data.Again = true;
                //fld.Data.RefField2 = "id_user";
                //fld.View.CaptionShort = "Департамент";
                //fld.View.Hint = "Департамент оператора";
                //fld.View.Width = 84;
                //fld.Edit.Visible = false;
                //fld.Edit.Enable = false;
                //fld.Filter.Control = faControl.DropDown;
                //fld.LookUp.Key = "id";
                //fld.LookUp.Field = "name";
                //fld.LookUp.Table = "_department";
                //cur.AddField(fld);

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
                fld.Data.Where = ((Session[Master.cur_basename + "_id_role"] ?? 0).ToString() == "30") ? "date_doc<= CONVERT(DATETIME,'30.06.2015 00:00',104)" : "";
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
                    fld = new faField();
                    fld.Data.FieldName = "id_depart";
                    fld.View.CaptionShort = "Получатель";
                    fld.View.Width = 180;
                    fld.Edit.Required = true;
                    fld.Edit.BaseCopied = false;
                    fld.Edit.Control = faControl.AutoComplete;
                    fld.Filter.Control = faControl.AutoComplete;
                    //fld.Filter.DefaultValue = ((int)list.Page).ToString();
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
                #endregion

                list.MainCursor = cur;

                if (Request.QueryString["id"] != null || Request.QueryString["act"] != null)
                {
                    #region Курсор "Версии документов"
                    if (Session[Master.cur_basename + "_access_docversion_view"] != null || Session[Master.cur_basename + "_access_docversion_edit"] != null)
                    {
                        cur = new faCursor(Master.cur_basename + "_docversion");
                        cur.TableID = 3;
                        cur.Caption = "Версии";
                        cur.EnableViewButton = true;
                        cur.EnableFileButton = true;
                        cur.EnableAddButton = cur.EnableEditButton = cur.EnableDelButton = cur.EnableSaveButton
                            = Session[Master.cur_basename + "_access_docversion_edit"] != null;// Session[Master.cur_basename + "_access_docversion_edit"] != null;

                        // Поля
                        // ID
                        fld = new faField();
                        fld.Data.FieldName = "id";
                        fld.View.CaptionShort = "ID";
                        fld.View.TextAlign = "center";
                        fld.View.Width = 50;
                        fld.Edit.Enable = false;
                        fld.Filter.Control = faControl.TextBoxInteger;
                        cur.AddField(fld);

                        // Связывающее поле
                        fld = new faField();
                        fld.Data.FieldName = "id_archive";
                        fld.Filter.Control = faControl.TextBoxInteger;
                        fld.Filter.Enable = true;
                        fld.View.Visible = false;
                        fld.Edit.Visible = false;
                        fld.Edit.Enable = false;
                        cur.AddField(fld);

                        // NN
                        fld = new faField();
                        fld.Data.FieldName = "nn";
                        fld.View.TextAlign = "center";
                        fld.View.Width = 68;
                        fld.View.Visible = false;
                        fld.Edit.Visible = false;
                        fld.Edit.Enable = false;
                        fld.Edit.Auto = faAutoType.Version;
                        fld.Filter.Enable = false;
                        cur.AddField(fld);

                        // Номер версии
                        fld = new faField();
                        fld.Data.FieldName = "ver";
                        fld.Data.FieldCalc = "(CAST(a.id_archive as varchar(30))+'.'+CAST(a.nn as varchar(30)))";
                        fld.View.CaptionShort = "№ Версии";
                        fld.View.TextAlign = "center";
                        fld.View.Width = 61;
                        fld.Edit.Visible = false;
                        fld.Edit.Enable = false;
                        fld.Edit.Control = faControl.TextBox;
                        cur.AddField(fld);

                        // Основная версия
                        fld = new faField();
                        fld.Data.FieldName = "main";
                        fld.View.CaptionShort = "Основная";
                        fld.View.TextAlign = "center";
                        fld.View.Width = 65;
                        fld.Edit.Control = faControl.CheckBox;
                        fld.Edit.Auto = faAutoType.Main;
                        //fld.Edit.DefaultValue = "1";
                        cur.AddField(fld);

                        // Штрихкод
                        fld = new faField();
                        fld.Data.FieldName = "barcode";
                        fld.View.CaptionShort = "Штрихкод";
                        fld.View.Width = 66;
                        fld.View.TextAlign = "center";
                        fld.Edit.Control = faControl.TextBoxInteger;
                        fld.Edit.Min = 1000000000;
                        fld.Edit.Max = Int32.MaxValue;
                        cur.AddField(fld);

                        // Дата создания
                        fld = new faField();
                        fld.Data.FieldName = "date_reg";
                        fld.View.FormatString = "{0:dd.MM.yyyy}";
                        fld.View.TextAlign = "center";
                        fld.View.CaptionShort = "Дата созд.";
                        fld.View.Width = 72;
                        fld.Edit.Visible = false;
                        fld.Edit.Enable = false;
                        fld.Edit.Auto = faAutoType.NowDate;
                        cur.AddField(fld);

                        // Дата передачи
                        fld = new faField();
                        fld.Data.FieldName = "date_trans";
                        fld.View.FormatString = "{0:dd.MM.yyyy}";
                        fld.View.TextAlign = "center";
                        fld.View.CaptionShort = "Дата перед.";
                        fld.View.Width = 72;
                        fld.Edit.Control = faControl.DatePicker;
                        fld.Edit.DefaultValue = DateTime.Now.ToShortDateString();
                        cur.AddField(fld);


                        // Путь к файлу
                        fld = new faField();
                        fld.Data.FieldName = "file";
                        fld.View.CaptionShort = "Файл";
                        fld.View.Visible = false;
                        fld.View.Width = 50;
                        fld.Edit.Visible = true;
                        fld.Edit.Enable = true;
                        fld.Edit.AddOnly = true;
                        fld.Edit.Control = faControl.File;
                        fld.Edit.Required = true;
                        cur.AddField(fld);

                        // Файл
                        fld = new faField();
                        fld.Data.FieldName = "filetype";
                        fld.Data.FieldCalc = "(UPPER(SUBSTRING(a.[file],LEN(a.[file])-2,3)))";
                        fld.View.CaptionShort = "Файл";
                        fld.View.Width = 40;
                        fld.View.TextAlign = "center";
                        fld.Edit.Control = faControl.TextBox;
                        fld.Edit.Visible = false;
                        fld.Edit.Enable = false;
                        cur.AddField(fld);

                        // Размер файла
                        fld = new faField();
                        fld.Data.FieldName = "file_size";
                        fld.Data.FieldCalc = "CAST(a.file_size/1024.0/1024.0 as numeric(13,2))";
                        fld.View.CaptionShort = "Размер";
                        fld.View.Width = 46;
                        fld.View.TextAlign = "center";
                        fld.Edit.Visible = false;
                        fld.Edit.Enable = false;
                        fld.Edit.Auto = faAutoType.FileSize;
                        cur.AddField(fld);


                        // Статус
                        fld = new faField();
                        fld.Data.FieldName = "id_status";
                        fld.View.CaptionShort = "Статус";
                        fld.View.Width = 100;
                        fld.Edit.Control = faControl.DropDown;
                        fld.Edit.Required = true;
                        fld.LookUp.Key = "id";
                        fld.LookUp.Field = "name";
                        fld.LookUp.Table = "_status";
                        cur.AddField(fld);

                        // Источник
                        fld = new faField();
                        fld.Data.FieldName = "id_source";
                        fld.View.CaptionShort = "Источник";
                        fld.View.Width = 100;
                        fld.Edit.Control = faControl.DropDown;
                        fld.Edit.DefaultValue = "2";
                        fld.Edit.Required = true;
                        fld.Edit.DefaultText = "Бумажный экземпляр";
                        fld.LookUp.Key = "id";
                        fld.LookUp.Field = "name";
                        fld.LookUp.Table = "_source";
                        cur.AddField(fld);

                        //
                        list.AddCursor(cur);
                    }
                    #endregion
                    //if (list.Page != faPage.ord && list.Page != faPage.norm)
                    //{
                    //    #region Курсор "Подразделения"
                    //    cur = new faCursor(Master.cur_basename + "_archive_depart");
                    //    cur.TableID = 12;
                    //    cur.CaptionShort = "Подразделения";
                    //    cur.EnableViewButton = true;
                    //    cur.EnableSaveButton = true;
                    //    cur.EnableAddButton = cur.EnableEditButton = cur.EnableDelButton
                    //        = Session[Master.cur_basename + "_access_archive_" + list.Page + "_edit"] != null;//true;//Session[Master.cur_basename + "_access_archive_depart_edit"] != null;
                    //    cur.EditDialogWidth = 300;
                    //    // Поля
                    //    // ID
                    //    fld = new faField();
                    //    fld.Data.FieldName = "id";
                    //    fld.View.CaptionShort = "ID";
                    //    fld.View.TextAlign = "center";
                    //    fld.View.Width = 65;
                    //    fld.Edit.Enable = false;
                    //    cur.AddField(fld);

                    //    // Связывающее поле
                    //    fld = new faField();
                    //    fld.Data.FieldName = "id_archive";
                    //    fld.Filter.Control = faControl.TextBoxInteger;
                    //    fld.Filter.Enable = true;
                    //    fld.View.Visible = false;
                    //    fld.Edit.Visible = false;
                    //    fld.Edit.Enable = false;
                    //    cur.AddField(fld);

                    //    // Подразделение
                    //    fld = new faField();
                    //    fld.Data.FieldName = "id_depart";
                    //    fld.View.CaptionShort = "Подразделение";
                    //    fld.View.Width = 300;
                    //    fld.Edit.Control = faControl.AutoComplete;
                    //    fld.Edit.Required = true;
                    //    fld.LookUp.Key = "id";
                    //    fld.LookUp.Field = "name";
                    //    fld.LookUp.Table = list.BaseName + "_depart";
                    //    cur.AddField(fld);

                    //    //
                    //    list.AddCursor(cur);
                    //    #endregion
                    //}
                    if (list.Page == faPage.tech || list.Page == faPage.acc)
                    {
                        #region Курсор "Бух-Тех"
                        cur = new faCursor(Master.cur_basename + (list.Page == faPage.tech ? "_tech_buh_view" : "_buh_tech_view"));
                        cur.TableID = 25;
                        cur.Caption = list.Page == faPage.tech ? "Бухгалтерские документы" : "Технические документы";
                        cur.EnableViewButton = true;
                        cur.EnableFileButton = true;
                        cur.EnableAddButton = cur.EnableEditButton = cur.EnableDelButton = cur.EnableSaveButton
                            = list.Page == faPage.tech && Session[Master.cur_basename + "_access_archive_" + list.Page + "_edit"] != null;
                        cur.EditDialogWidth = 300;

                        // Поля
                        // ID
                        fld = new faField();
                        fld.Data.FieldName = "id";
                        fld.View.CaptionShort = "ID";
                        fld.View.TextAlign = "center";
                        fld.View.Width = 65;
                        fld.Edit.Enable = false;
                        cur.AddField(fld);

                        // Связывающее поле
                        fld = new faField();
                        fld.Data.FieldName = list.Page == faPage.tech ? "id_archive" : "link";
                        fld.Filter.Control = faControl.TextBoxInteger;
                        fld.Filter.Enable = true;
                        fld.View.Visible = false;
                        fld.Edit.Visible = false;
                        fld.Edit.Enable = false;
                        cur.AddField(fld);

                        //Код ЭА
                        fld = new faField();
                        fld.Data.FieldName = list.Page == faPage.tech ? "link" : "id_archive";
                        fld.View.Hint = "Код Электронного Архива";
                        fld.View.CaptionShort = "Код ЭА";
                        fld.View.TextAlign = "center";
                        fld.View.Width = 68;
                        fld.Edit.Control = faControl.NewWindowArchive;
                        fld.Edit.Required = true;
                        fld.Filter.Control = faControl.TextBoxInteger;
                        cur.AddField(fld);

                        //Дата Документа
                        fld = new faField();
                        fld.Data.FieldName = "date_doc";
                        fld.View.FormatString = "{0:dd.MM.yyyy}";
                        fld.View.TextAlign = "center";
                        fld.View.Hint = "Дата документа";
                        fld.View.CaptionShort = "Дата док.";
                        fld.View.Width = 84;
                        fld.Edit.Control = faControl.DatePicker;
                        fld.Filter.Control = faControl.DatePicker;
                        fld.Edit.Visible = false;
                        cur.AddField(fld);

                        //Документ 
                        fld = new faField();
                        fld.Data.FieldName = "id_doctree";
                        fld.View.CaptionShort = "Документ";
                        fld.View.Width = 180;
                        fld.Edit.Visible = false;
                        fld.Edit.Control = faControl.TreeGrid;
                        fld.Filter.Control = faControl.TreeGrid;
                        fld.LookUp.Key = "id";
                        fld.LookUp.Field = "name";
                        fld.LookUp.Table = "_doctree";
                        cur.AddField(fld);

                        //Номер документа 
                        fld = new faField();
                        fld.Data.FieldName = "num_doc";
                        fld.View.CaptionShort = "№ Документа";
                        fld.View.Width = 180;
                        fld.Edit.Visible = false;
                        fld.Filter.Control = faControl.TextBox;
                        cur.AddField(fld);

                        //Контрагент
                        fld = new faField();
                        fld.Data.FieldName = "id_frm_contr";
                        fld.View.CaptionShort = "Контрагент";
                        fld.View.Width = 210;
                        fld.Edit.Visible = false;
                        fld.Edit.Control = faControl.AutoComplete;
                        fld.Filter.Control = faControl.AutoComplete;
                        fld.LookUp.Key = "id";
                        fld.LookUp.Field = "name";
                        fld.LookUp.Table = "_frm"; ;
                        cur.AddField(fld);

                        // Путь к файлу
                        fld = new faField();
                        fld.Data.FieldName = "file";
                        fld.View.CaptionShort = "Файл";
                        fld.View.Visible = false;
                        fld.View.Width = 50;
                        fld.Edit.Visible = false;
                        cur.AddField(fld);

                        //
                        list.AddCursor(cur);
                        #endregion
                    }

                }
                list.Render(form1, this);
            }

            else
                form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}