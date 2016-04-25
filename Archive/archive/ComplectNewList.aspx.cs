using System;
using System.Web.UI;

namespace WebArchiveR6
{
    public partial class ComplectNewList : System.Web.UI.Page
    {
        public faListComplect list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // 666215
            //_complectnew_view_id_archive_filter_cond = 
            //


            if (Request.QueryString["id_archive"] != null)
                Session["_complectnew_view_id_archive_filter"] = Request.QueryString["id_archive"].ToString();
            if (Request.QueryString["id_base"] != null)
            {
                Session["_complectnew_view_id_base_filter"] = Request.QueryString["id_base"].ToString();
                Session["_complectnew_view_id_base_filter_text"] = faFunc.GetBaseNameRusById(int.Parse(Request.QueryString["id_base"].ToString()));
            }
            faField fld;
            faCursor cur;
            list = new faListComplect();
            list.RouteName = "complectnewlist";
            list.Page = faPage.srch;
            //if (true)//(Session[Master.cur_basename + "_access_complect_view"] != null || Session[Master.cur_basename + "_access_complect_edit"] != null)
            //{
            list.IDBase = "0";// Session[Master.cur_basename + "_id"].ToString();
            list.BaseName = "";// Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;

            // Шапка
            cur = new faCursor("_complectnew_view");
            cur.TableID = 0;
            cur.Caption = "Архив / Спецификации комплектов";
            cur.EnableFileButton = cur.EnableCSVButton = true;cur.EnableExcelButton=true;
            cur.EnableViewButton = cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = false;
            // =

            #region Поля

            // ID
            fld = new faField();
            fld.Data.FieldName = "id";
            fld.View.CaptionShort = "ID";
            fld.View.TextAlign = "center";
            fld.View.Width = 50;
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
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Caption = "Организация";
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "namerus";
            fld.LookUp.Table = "_base";
            cur.AddField(fld);

            //Эл.кан.связи
            fld = new faField();
            fld.Data.FieldName = "inet";
            fld.View.CaptionShort = "Источник";
            fld.View.Width = 90;
            fld.View.TextAlign = "center";
            fld.Edit.Required = true;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_source2";
            cur.AddField(fld);

            // Код ЭА
            fld = new faField();
            fld.Data.FieldName = "id_archive";
            fld.View.CaptionShort = "Код ЭА";
            fld.View.TextAlign = "center";
            fld.View.Width = 50;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            // Штрихкод
            fld = new faField();
            fld.Data.FieldName = "barcode";
            fld.View.CaptionShort = "Штрихкод";
            fld.View.Width = 66;
            fld.View.TextAlign = "center";
            fld.Edit.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            
            // Связывающее поле
            fld = new faField();
            fld.Data.FieldName = "id_complectnew";
            fld.View.CaptionShort = "ID Комплекта";
            fld.View.TextAlign = "center";
            fld.View.Width = 100;
            fld.Filter.Control = faControl.TextBoxInteger;
            cur.AddField(fld);

            // Имя
            fld = new faField();
            fld.Data.FieldName = "name";
            fld.View.CaptionShort = "Имя";
            fld.View.Width = 230;
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

            //Создал / Оператор ТП
            fld = new faField();
            fld.Data.FieldName = "id_creator";
            fld.View.Width = 185;
            fld.View.CaptionShort = "Оператор ТП";
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

            // Оператор ОЦ
            fld = new faField();
            fld.Data.FieldName = "id_operator_oc";
            fld.View.Width = 185;
            fld.View.CaptionShort = "Оператор ОЦ";
            fld.Edit.Enable = true;
            fld.Edit.Control = faControl.AutoComplete;
            fld.Filter.Control = faControl.AutoComplete;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_user";
            fld.LookUp.TableAlias = "user4";
            cur.AddField(fld);

            // Путь к файлу
            fld = new faField();
            fld.Data.FieldName = "file";
            fld.View.CaptionShort = "Врем.файл";
            fld.View.Visible = false;
            fld.View.Width = 50;
            fld.Edit.Visible = false;
            fld.Edit.Enable = false;
            fld.Edit.AddOnly = true;
            fld.Edit.Control = faControl.File;
            fld.Edit.Required = true;
            cur.AddField(fld);

            //// Файл
            //fld = new faField();
            //fld.Data.FieldName = "file_orig";
            //fld.View.CaptionShort = "Файл";
            //fld.View.Width = 100;
            //fld.View.TextAlign = "center";
            //fld.Edit.Visible = true;
            //fld.Edit.Enable = false;
            //fld.Edit.Control = faControl.TextBox;
            //cur.AddField(fld);

            // Путь к файлу
            fld = new faField();
            fld.Data.FieldName = "file_archive";
            fld.View.CaptionShort = "Файл архива";
            fld.View.Visible = false;
            fld.View.Width = 50;
            fld.Edit.Visible = false;
            fld.Edit.Enable = false;
            fld.Edit.AddOnly = true;
            fld.Edit.Control = faControl.TextBox;
            fld.Edit.Required = true;
            cur.AddField(fld);

            ////Дата Создания
            //fld = new faField();
            //fld.Data.FieldName = "date_reg_list";
            //fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
            //fld.View.TextAlign = "center";
            //fld.View.Hint = "Дата создания";
            //fld.View.CaptionShort = "Дата созд.док.";
            //fld.View.Width = 98;
            //fld.Edit.Visible = false;
            //fld.Edit.Enable = false;
            //fld.Edit.Auto = faAutoType.NowDateTime;
            //fld.Edit.AddOnly = true;
            //fld.Filter.Control = faControl.DateTimePicker;
            //cur.AddField(fld);

            ////Создал
            //fld = new faField();
            //fld.Data.FieldName = "id_creator_list";
            //fld.View.Width = 185;
            //fld.View.CaptionShort = "Создал док.";
            //fld.Edit.Visible = false;
            //fld.Edit.Enable = false;
            //fld.Edit.Control = faControl.AutoComplete;
            //fld.Edit.Auto = faAutoType.UserID;
            //fld.Edit.AddOnly = true;
            //fld.Filter.Control = faControl.AutoComplete;
            //fld.LookUp.Key = "id";
            //fld.LookUp.Field = "name";
            //fld.LookUp.Table = "_user";
            //fld.LookUp.TableAlias = "user5";
            //cur.AddField(fld);

            ////Дата Редактирования
            //fld = new faField();
            //fld.Data.FieldName = "date_upd_list";
            //fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
            //fld.View.TextAlign = "center";
            //fld.View.Hint = "Дата редактирования";
            //fld.View.CaptionShort = "Дата ред.";
            //fld.View.Width = 98;
            //fld.Edit.Visible = false;
            //fld.Edit.Enable = false;
            //fld.Edit.Auto = faAutoType.NowDateTime;
            //fld.Filter.Control = faControl.DateTimePicker;
            //cur.AddField(fld);

            ////Редактор
            //fld = new faField();
            //fld.Data.FieldName = "id_editor_list";
            //fld.View.Width = 185;
            //fld.View.CaptionShort = "Редакт.док.";
            //fld.Edit.Visible = false;
            //fld.Edit.Enable = false;
            //fld.Edit.Control = faControl.AutoComplete;
            //fld.Edit.Auto = faAutoType.UserID;
            //fld.Filter.Control = faControl.AutoComplete;
            //fld.LookUp.Key = "id";
            //fld.LookUp.Field = "name";
            //fld.LookUp.Table = "_user";
            //fld.LookUp.TableAlias = "user6";
            //cur.AddField(fld);
            //

            // Примечание 
            fld = new faField();
            fld.Data.FieldName = "prim";
            fld.View.CaptionShort = "Примечание";
            fld.View.Width = 180;
            fld.Edit.Required = false;
            fld.Edit.Max = 240;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            //Проведен 
            fld = new faField();
            fld.Data.FieldName = "accept";
            fld.View.CaptionShort = "Проведен";
            fld.View.Width = 68;
            fld.View.TextAlign = "center";
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_yesno";
            fld.LookUp.TableAlias = "yn3";
            cur.AddField(fld);

            

            #endregion

            list.MainCursor = cur;
            list.EditFormWidth = 450;
            list.Render(form1, this);
            //}
            //else
            //    form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}