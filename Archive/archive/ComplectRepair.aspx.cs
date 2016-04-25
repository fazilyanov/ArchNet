using System;
using System.Web.UI;

namespace WebArchiveR6
{
    public partial class ComplectRepair : System.Web.UI.Page
    {
        public faListComplectRepair list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;

            list = new faListComplectRepair();
            list.RouteName = "complectrepair";
            //if (true)//(Session[Master.cur_basename + "_access_complect_view"] != null || Session[Master.cur_basename + "_access_complect_edit"] != null || list.Page == faPage.select)
            //{
            list.IDBase = "0";
            list.BaseName = "";// Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;
            list.ShowArrows = true;
            list.EditFormWidth = 1100;
            list.EditFormHeight = 350;

            // Шапка
            cur = new faCursor("_complect_repair");
            //cur.AliasAlt = cur.Alias + "_" + list.Page;
            cur.TableID = 30;
            cur.Caption = "Архив / Комплекты движения документов";
            cur.EnableViewButton = cur.EnableExcelButton = true;
            cur.EnableFileButton = false; cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = true;// Session[Master.cur_basename + "_access_complect_edit"] != null;

            #region Поля
            //ID
            fld = new faField();
            fld.Data.FieldName = "id";
            fld.View.CaptionShort = "ID";
            fld.View.TextAlign = "center";
            fld.View.Width = 68;
            fld.Edit.Enable = false;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            //Обработан 
            fld = new faField();
            fld.Data.FieldName = "processed";
            fld.View.CaptionShort = "Обработан";
            fld.View.Width = 68;
            fld.View.TextAlign = "center";
            fld.Edit.Control = faControl.DropDown;
            fld.Edit.DefaultValue = "1";
            fld.Edit.DefaultText = "Нет";
            fld.Edit.Required = true;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_yesno";
            fld.LookUp.TableAlias = "yn1";
            cur.AddField(fld);

            //Организация
            fld = new faField();
            fld.Data.FieldName = "id_base";
            fld.View.CaptionShort = "Организация";
            fld.View.Width = 150;
            fld.Edit.Required = true;
            fld.Edit.DefaultValue = "1";
            fld.Edit.DefaultText = "ЗАО «Стройтрансгаз»";
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Caption = "Организация.";
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "namerus";
            fld.LookUp.Table = "_base";
            cur.AddField(fld);

            // Имя
            fld = new faField();
            fld.Data.FieldName = "name";
            fld.View.CaptionShort = "Имя";
            fld.View.Width = 180;
            fld.Edit.Max = 150;
            fld.Edit.Required = true;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            //Дата Создания
            fld = new faField();
            fld.Data.FieldName = "date_reg";
            fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
            fld.View.TextAlign = "center";
            fld.View.Hint = "Дата создания";
            fld.View.CaptionShort = "Дата созд.";
            fld.View.Width = 98;
            fld.Edit.Enable = false;
            fld.Edit.Control = faControl.DateTimePicker;
            fld.Edit.Auto = faAutoType.NowDateTime;
            fld.Edit.AddOnly = true;
            fld.Filter.Control = faControl.DateTimePicker;
            cur.AddField(fld);

            //Создал
            fld = new faField();
            fld.Data.FieldName = "id_creator";
            fld.View.Width = 185;
            fld.View.CaptionShort = "Создал";
            fld.Edit.Enable = false;
            fld.Edit.AddOnly = true;
            fld.Edit.Auto = faAutoType.UserID;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_user";
            fld.LookUp.TableAlias = "user1";
            cur.AddField(fld);

            //Дата Редактирования
            fld = new faField();
            fld.Data.FieldName = "date_upd";
            fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
            fld.View.TextAlign = "center";
            fld.View.Hint = "Дата редактирования";
            fld.View.CaptionShort = "Дата ред.";
            fld.View.Width = 98;
            fld.Edit.Enable = false;
            fld.Edit.Auto = faAutoType.NowDateTime;
            fld.Filter.Control = faControl.DateTimePicker;
            cur.AddField(fld);

            //Редактор
            fld = new faField();
            fld.Data.FieldName = "id_editor";
            fld.View.Width = 185;
            fld.View.CaptionShort = "Редактировал";
            fld.Edit.Enable = false;
            fld.Edit.Auto = faAutoType.UserID;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_user";
            fld.LookUp.TableAlias = "user2";
            cur.AddField(fld);

            //Исполнитель
            fld = new faField();
            fld.Data.FieldName = "id_perf";
            fld.View.CaptionShort = "Исполнитель";
            fld.View.Width = 180;
            fld.Edit.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_user";
            fld.LookUp.TableAlias = "user3";
            fld.Filter.Control = faControl.AutoComplete;
            cur.AddField(fld);

            // Уведомления
            fld = new faField();
            fld.Data.FieldName = "perf_mail";
            fld.View.Visible = false;
            fld.Edit.Visible = false;
            fld.Edit.Enable = false;
            fld.Filter.Enable = false;
            cur.AddField(fld);

            // Кто подписал
            fld = new faField();
            fld.Data.FieldName = "id_sign";
            fld.View.CaptionShort = "Подписал";
            fld.View.Visible = false;
            fld.Edit.Visible = true;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Edit.Enable = true;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_user";
            fld.LookUp.TableAlias = "user4";
            fld.Filter.Enable = false;
            cur.AddField(fld);


            // Подписан?
            fld = new faField();
            fld.Data.FieldName = "sign";
            fld.Data.FieldCalc = "(CASE WHEN [id_sign]>0 THEN 'Да' ELSE 'Нет' END)";
            fld.View.CaptionShort = "Подпись";
            fld.View.Visible = true;
            fld.Edit.Visible = false;
            fld.Edit.Control = faControl.TextBox;
            fld.Filter.Enable = false;
            cur.AddField(fld);

            // Примечание 
            fld = new faField();
            fld.Data.FieldName = "prim";
            fld.View.CaptionShort = "Примечание";
            fld.View.Width = 180;
            fld.Edit.Required = false;
            fld.Edit.Max = 240;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            #endregion

            list.MainCursor = cur;

            if (Request.QueryString["id"] != null || Request.QueryString["act"] != null)
            {
                #region Курсор
                if (true)//(Session[Master.cur_basename + "_access_complect_view"] != null || Session[Master.cur_basename + "_access_complect_edit"] != null)
                {
                    cur = new faCursor("_complect_repair_list");
                    cur.TableID = 31;
                    cur.Caption = "Документы";
                    cur.EnableViewButton = true;
                    cur.EnableFileButton = true;
                    cur.EnableAddButton = cur.EnableEditButton = true;
                    cur.EnableDelButton = cur.EnableSaveButton = true;//Session[Master.cur_basename + "_access_complect_edit"] != null;// Session[Master.cur_basename + "_access_docversion_edit"] != null;
                    cur.ShowPager = false;
                    cur.SortColumn = "id";
                    cur.SortDirection = "asc";

                    // Поля
                    // ID
                    fld = new faField();
                    fld.Data.FieldName = "id";
                    fld.View.CaptionShort = "ID";
                    fld.View.TextAlign = "center";
                    fld.View.Width = 35;
                    fld.Edit.Enable = false;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);

                    // Связывающее поле
                    fld = new faField();
                    fld.Data.FieldName = "id_complect_repair";
                    fld.Filter.Control = faControl.TextBoxInteger;
                    fld.Filter.Enable = true;
                    fld.View.Visible = false;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    cur.AddField(fld);

                    ////Обработан 
                    //fld = new faField();
                    //fld.Data.FieldCalc = "(CASE WHEN a.id_archive > 0 THEN 2 ELSE 1 END)";
                    //fld.Data.FieldName = "processed";
                    //fld.View.CaptionShort = "Обработан";
                    //fld.View.Width = 68;
                    //fld.View.TextAlign = "center";
                    //fld.Edit.Visible = false;
                    //fld.Edit.Enable = false;
                    //fld.Edit.Control = faControl.DropDown;
                    //fld.Filter.Control = faControl.DropDown;
                    //fld.LookUp.Key = "id";
                    //fld.LookUp.Field = "name";
                    //fld.LookUp.Table = "_yesno";
                    //cur.AddField(fld);

                    // Код ЭА
                    fld = new faField();
                    fld.Data.FieldName = "id_archive";
                    fld.View.CaptionShort = "Код ЭА";
                    fld.View.TextAlign = "center";
                    fld.View.Width = 40;
                    fld.Edit.Enable = false;
                    fld.Edit.Control = faControl.TextBoxInteger;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);

                    // Штрихкод
                    fld = new faField();
                    fld.Data.FieldName = "barcode";
                    fld.View.CaptionShort = "Штрихкод";
                    fld.View.Width = 70;
                    fld.View.TextAlign = "center";
                    fld.Edit.Control = faControl.TextBoxInteger;
                    fld.Edit.Min = 1000000000;
                    fld.Edit.Max = Int32.MaxValue;
                    cur.AddField(fld);

                    // Путь к файлу
                    fld = new faField();
                    fld.Data.FieldName = "file";
                    fld.View.CaptionShort = "Файл";
                    fld.View.Width = 150;
                    fld.View.Visible = false;
                    fld.Edit.Visible = false;
                    cur.AddField(fld);

                    // Костыль организации 
                    fld = new faField();
                    fld.Data.FieldName = "id_base";
                    fld.View.CaptionShort = "Организация";
                    fld.View.Visible = false;
                    fld.Edit.Visible = false;
                    cur.AddField(fld);

                    // 
                    fld = new faField();
                    fld.Data.FieldName = "date_doc";
                    fld.View.CaptionShort = "Дата док.";
                    fld.View.Width = 58;
                    fld.View.FormatString = "{0:dd.MM.yyyy}";
                    fld.Edit.Control = faControl.DatePicker;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    cur.AddField(fld);

                    // 
                    fld = new faField();
                    fld.Data.FieldName = "doctype";
                    fld.View.CaptionShort = "Вид док.";
                    fld.View.Width = 65;
                    fld.Edit.Control = faControl.TextBox;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    cur.AddField(fld);

                    // 
                    fld = new faField();
                    fld.Data.FieldName = "doctree";
                    fld.View.CaptionShort = "Документ";
                    fld.View.Width = 150;
                    fld.Edit.Control = faControl.TextBox;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    cur.AddField(fld);
                    // 
                    fld = new faField();
                    fld.Data.FieldName = "num_doc";
                    fld.View.CaptionShort = "Номер док.";
                    fld.View.Width = 100;
                    fld.Edit.Control = faControl.TextBox;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    cur.AddField(fld);

                    // 
                    fld = new faField();
                    fld.Data.FieldName = "frm";
                    fld.View.CaptionShort = "Контрагент";
                    fld.View.Width = 150;
                    fld.Edit.Control = faControl.TextBox;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    cur.AddField(fld);



                    list.AddCursor(cur);
                }
                #endregion
            }
            list.Render(form1, this);
            //}

            //else
            //    form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}