using System;
using System.Web.UI;

namespace ArchNet
{
    public partial class Docversion : System.Web.UI.Page
    {
        public faList list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;
            list = new faList();
            list.RouteName = "docversion";
            list.Page = faPage.srch;
            if (Session[Master.cur_basename + "_access_docversion_view"] != null || Session[Master.cur_basename + "_access_docversion_edit"] != null)
            {
                list.IDBase = Session[Master.cur_basename + "_id"].ToString();
                list.BaseName = Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;

                // Шапка
                cur = new faCursor(Master.cur_basename + "_docversion_archive");
                cur.TableID = 0;
                cur.Caption = "Архив / Версии документов";
                cur.EnableViewButton = cur.EnableFileButton = cur.EnableExcelButton = true;
                cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = false;

                #region Поля

                // ID версии
                fld = new faField();
                fld.Data.FieldName = "id";
                fld.View.TextAlign = "center";
                fld.View.CaptionShort = "ID";
                fld.View.Width = 84;
                fld.Filter.Control = faControl.TextBoxInteger;
                cur.AddField(fld);

                // ID версии
                fld = new faField();
                fld.Data.FieldName = "id_archive";
                fld.View.TextAlign = "center";
                fld.View.CaptionShort = "Код ЭА";
                fld.View.Width = 84;
                fld.Filter.Control = faControl.TextBoxInteger;
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
                fld.Filter.Enable = false;
                cur.AddField(fld);

                //Дата создания
                fld = new faField();
                fld.Data.FieldName = "date_reg";
                fld.View.FormatString = "{0:dd.MM.yyyy}";
                fld.View.TextAlign = "center";
                fld.View.Hint = "Дата создания";
                fld.View.CaptionShort = "Дата созд.";
                fld.View.Width = 84;
                fld.Filter.Control = faControl.DatePicker;
                cur.AddField(fld);

                //Пакет документов  
                fld = new faField();
                fld.Data.FieldName = "docpack";
                fld.View.Hint = "Пакет документов";
                fld.View.CaptionShort = "Пакет";
                fld.View.Width = 56;
                fld.Filter.Control = faControl.TextBoxInteger;
                cur.AddField(fld);

                //Вид документов
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

                // Для открытия нужной карточки 
                fld = new faField();
                fld.Data.FieldName = "id_doctree2";
                fld.Data.FieldCalc = "a.id_doctree";
                fld.Edit.Enable = false;
                fld.Edit.Visible = false;
                fld.View.Visible = false;
                fld.Filter.Enable = false;
                fld.Filter.Control = faControl.TextBoxInteger;
                cur.AddField(fld);

                //Документ 
                fld = new faField();
                fld.Data.FieldName = "id_doctree";
                fld.View.CaptionShort = "Документ";
                fld.View.Width = 180;
                fld.Filter.Control = faControl.TreeGrid;
                string[] _p = { "acc", "dog", "ord", "oth", "empl", "ohs", "tech", "bank", "norm" };
                fld.Filter.DefaultValue = "";
                foreach (string name in _p)//Enum.GetNames(typeof(faPage))
                    if (Session[Master.cur_basename + "_access_archive_" + name + "_view"] != null || Session[Master.cur_basename + "_access_archive_" + name + "_edit"] != null)
                        fld.Filter.DefaultValue += "(" + ((int)faFunc.GetPageType(name)).ToString() + "),";
                fld.Filter.DefaultValue += "(-1)";
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_doctree";
                cur.AddField(fld);

                //Контрагент
                fld = new faField();
                fld.Data.FieldName = "id_frm_contr";
                fld.View.CaptionShort = "Контрагент";
                fld.View.Width = 210;
                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_frm";
                fld.LookUp.TableAlias = "frm1";
                cur.AddField(fld);

                //Дата Документа
                fld = new faField();
                fld.Data.FieldName = "date_doc";
                fld.View.FormatString = "{0:dd.MM.yyyy}";
                fld.View.TextAlign = "center";
                fld.View.Hint = "Дата документа";
                fld.View.CaptionShort = "Дата док.";
                fld.View.Width = 84;
                fld.Filter.Control = faControl.DatePicker;
                cur.AddField(fld);

                //Номер документа 
                fld = new faField();
                fld.Data.FieldName = "num_doc";
                fld.View.CaptionShort = "№ Документа";
                fld.View.Width = 180;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);


                //Исполнитель
                fld = new faField();
                fld.Data.FieldName = "id_perf";
                fld.View.CaptionShort = "Исполнитель";
                fld.View.Width = 125;
                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = list.BaseName + "_person";
                cur.AddField(fld);

                // Получатель документа
                fld = new faField();
                fld.Data.FieldName = "id_depart";
                fld.View.CaptionShort = "Получатель";
                fld.View.Width = 180;
                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = list.BaseName + "_department";
                cur.AddField(fld);

                //Оператор ЭА
                fld = new faField();
                fld.Data.FieldName = "id_user";
                fld.View.Width = 185;
                fld.View.CaptionShort = "Оператор ЭА";
                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_user";
                cur.AddField(fld);

                //Дата Редактирования
                fld = new faField();
                fld.Data.FieldName = "date_upd";
                fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
                fld.View.TextAlign = "center";
                fld.View.Hint = "Дата редактирования";
                fld.View.CaptionShort = "Дата ред.";
                fld.View.Width = 98;
                fld.Filter.Control = faControl.DateTimePicker;
                cur.AddField(fld);

                //Штрих код
                fld = new faField();
                fld.Data.FieldName = "barcode";
                fld.View.TextAlign = "center";
                fld.View.CaptionShort = "Штрихкод";
                fld.View.Width = 84;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                //Сумма
                fld = new faField();
                fld.Data.FieldName = "summ";
                fld.View.CaptionShort = "Сумма";
                fld.View.Width = 83;
                fld.Filter.Control = faControl.TextBoxNumber;
                cur.AddField(fld);

                //Содержание документа
                fld = new faField();
                fld.Data.FieldName = "content";
                fld.View.Width = 240;
                fld.View.Hint = "Содержание документа";
                fld.View.CaptionShort = "Содержание";
                cur.AddField(fld);

                //Статус
                fld = new faField();
                fld.Data.FieldName = "id_status";
                fld.View.CaptionShort = "Статус";
                fld.View.Hint = "Статус версии";
                fld.View.Width = 84;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_status";
                cur.AddField(fld);


                //Состояние
                fld = new faField();
                fld.Data.FieldName = "id_state";
                fld.View.Width = 100;
                fld.View.CaptionShort = "Состояние";
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_state";
                cur.AddField(fld);

                // Источник
                fld = new faField();
                fld.Data.FieldName = "id_source";
                fld.View.CaptionShort = "Источник";
                fld.View.Width = 100;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_source";
                cur.AddField(fld);

                //Качество
                fld = new faField();
                fld.Data.FieldName = "id_quality";
                fld.View.CaptionShort = "Качество";
                fld.View.Width = 90;
                fld.View.TextAlign = "center";
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_quality";
                cur.AddField(fld);

                //Оператор ТП
                fld = new faField();
                fld.Data.FieldName = "tp";
                fld.View.CaptionShort = "Оператор ТП";
                fld.View.Width = 90;
                fld.Filter.Enable = false;
                cur.AddField(fld);

                // Размер файла
                fld = new faField();
                fld.Data.FieldName = "file_size";
                fld.Data.FieldCalc = "CAST(a.file_size/1024.0/1024.0 as numeric(13,2))";
                fld.View.CaptionShort = "Размер";
                fld.View.Width = 46;
                fld.View.TextAlign = "center";
                cur.AddField(fld);

                //Дата передачи (Из Версий)
                fld = new faField();
                fld.Data.FieldName = "date_trans";
                fld.View.FormatString = "{0:dd.MM.yyyy}";
                fld.View.TextAlign = "center";
                fld.View.Hint = "Дата передачи";
                fld.View.CaptionShort = "Дата перед.";
                fld.View.Width = 84;
                fld.Filter.Control = faControl.DatePicker;
                cur.AddField(fld);

                //Путь к файлу (Из Версий)
                fld = new faField();
                fld.Data.FieldName = "file";
                fld.View.Visible = false;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                fld.Filter.Enable = false;
                cur.AddField(fld);



                /*
                //Вид документа
                
                    fld = new faField();
                    fld.Data.FieldName = "id_doctype";
                    fld.View.Hint = "Вид документа";
                    fld.View.CaptionShort = "Вид док.";
                    fld.View.Width = 75;
                    fld.Edit.Control = faControl.DropDown;
                    fld.Filter.CaptionShort = "Вид докум.";
                    fld.Filter.Control = faControl.DropDown;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "name";
                    fld.LookUp.Table = "_doctype";
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

                

                //Примечание
                fld = new faField();
                fld.Data.FieldName = "prim";
                fld.View.CaptionShort = "Примечание";
                fld.View.Width = 190;
                cur.AddField(fld);

                //Старший документ
                if (list.Page != faPage.ord)
                {
                    fld = new faField();
                    fld.Data.FieldName = "id_parent";
                    fld.View.Hint = "Старший документ";
                    fld.View.CaptionShort = "Старший док.";
                    fld.View.Width = 140;
                    fld.Edit.Control = faControl.AutoComplete;
                    fld.Filter.Control = faControl.AutoComplete;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "num_doc";
                    fld.LookUp.Table = Master.cur_basename + "_archive";
                    cur.AddField(fld);
                }
                
                //Код проекта
                if (list.Page != faPage.ord && list.Page != faPage.empl)
                {
                    fld = new faField();
                    fld.Data.FieldName = "id_prjcode";
                    fld.View.CaptionShort = "Код проекта";
                    fld.View.Width = 110;
                    fld.Edit.Control = faControl.AutoComplete;
                    fld.Filter.Control = faControl.AutoComplete;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "code";
                    fld.LookUp.Table = Master.cur_basename + "_prjcode";
                    cur.AddField(fld);
                }

               

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
                fld.Filter.CaptionShort = "Текст док.";
                fld.Filter.Control = faControl.TextBoxFullSearch;
                cur.AddField(fld);*/
                #endregion

                list.MainCursor = cur;
                list.EditFormWidth = 536;
                list.Render(form1, this);
            }
            else
                form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}