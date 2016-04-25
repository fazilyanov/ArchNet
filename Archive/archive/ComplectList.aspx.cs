using System;
using System.Web.UI;

namespace WebArchiveR6
{
    public partial class ComplectList : System.Web.UI.Page
    {      public faList list;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            
            faField fld;
            faCursor cur;

            list = new faList();
            list.RouteName = "complectlist";
           // list.Page = faFunc.GetPageType((Page.RouteData.Values["p_page"] ?? "").ToString());
            //if (true)//(Session[Master.cur_basename + "_access_complect_view"] != null || Session[Master.cur_basename + "_access_complect_edit"] != null || list.Page==faPage.select)
            //{
                list.IDBase = "0";//Session[Master.cur_basename + "_id"].ToString();
                list.BaseName = "";// Master.cur_basename != "" ? Master.cur_basename : "";
                list.ShowFilterPanel = true;
                list.EditFormWidth = 1000;
                list.EditFormHeight = 200;

                // Шапка
                cur = new faCursor("_complect_list_view2");
                //cur.AliasAlt = cur.Alias + "_" + list.Page;
                cur.TableID = 0;
                cur.Caption = "Архив / Спецификации комплектов (старых)";
                cur.EnableViewButton =  cur.EnableExcelButton = true;
                cur.EnableFileButton =  cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = false;//Session[Master.cur_basename + "_access_complect_edit"] != null;

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

                // Связывающее поле
                fld = new faField();
                fld.Data.FieldName = "id_complect";
                fld.View.CaptionShort = "ID Комплекта";
                fld.View.Width = 100;
                fld.Filter.Control = faControl.TextBoxInteger;
                fld.Filter.Enable = true;
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                cur.AddField(fld);

                //Обработан 
                fld = new faField();
                fld.Data.FieldName = "processed";
                fld.View.CaptionShort = "Обработан";
                fld.View.Width = 68;
                fld.View.TextAlign = "center";
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                fld.Edit.Control = faControl.DropDown;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_yesno";
                cur.AddField(fld);

                // Код ЭА
                fld = new faField();
                fld.Data.FieldName = "id_archive";
                fld.View.CaptionShort = "Код ЭА";
                fld.View.TextAlign = "center";
                fld.View.Width = 50;
                fld.Edit.Enable = true;
                fld.Filter.Control = faControl.TextBoxInteger;
                cur.AddField(fld);

                // Штрихкод
                fld = new faField();
                fld.Data.FieldName = "barcode";
                fld.View.CaptionShort = "Штрихкод";
                fld.View.Width = 66;
                fld.View.TextAlign = "center";
                fld.Edit.Enable = false;
                cur.AddField(fld);
            
                // Имя
                fld = new faField();
                fld.Data.FieldName = "name";
                fld.View.CaptionShort = "Имя";
                fld.View.Width = 180;
                fld.Edit.Enable = false;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                //Дата Создания
                fld = new faField();
                fld.Data.FieldName = "date_create";
                fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
                fld.View.TextAlign = "center";
                fld.View.Hint = "Дата создания";
                fld.View.CaptionShort = "Дата созд.";
                fld.View.Width = 98;
                fld.Edit.Enable = false;
                fld.Filter.Control = faControl.DateTimePicker;
                cur.AddField(fld);

                //Создал
                fld = new faField();
                fld.Data.FieldName = "id_creator";
                fld.View.Width = 185;
                fld.View.CaptionShort = "Создал";
                fld.Edit.Enable = false;
                fld.Edit.Control = faControl.AutoComplete;
                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_user";
                fld.LookUp.TableAlias = "user1";
                cur.AddField(fld);

                //Дата Редактирования
                fld = new faField();
                fld.Data.FieldName = "date_edit";
                fld.View.FormatString = "{0:dd.MM.yyyy HH:mm}";
                fld.View.TextAlign = "center";
                fld.View.Hint = "Дата редактирования";
                fld.View.CaptionShort = "Дата ред.";
                fld.View.Width = 98;
                fld.Edit.Enable = false;
                fld.Filter.Control = faControl.DateTimePicker;
                cur.AddField(fld);

                //Редактор
                fld = new faField();
                fld.Data.FieldName = "id_editor";
                fld.View.Width = 185;
                fld.View.CaptionShort = "Редактировал";
                fld.Edit.Enable = false;
                fld.Edit.Control = faControl.AutoComplete;
                fld.Filter.Control = faControl.AutoComplete;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_user";
                fld.LookUp.TableAlias = "user2";
                cur.AddField(fld);

                //Исполнитель
                fld = new faField();
                fld.Data.FieldName = "executor";
                fld.View.CaptionShort = "Исполнитель";
                fld.View.Width = 180;
                fld.Edit.Enable = false;
                fld.Filter.Control = faControl.TextBox;
                cur.AddField(fld);

                //Оператор ОЦ
                //fld = new faField();
                //fld.Data.FieldName = "operator_oc";
                //fld.View.CaptionShort = "Оператор ОЦ";
                //fld.View.Width = 250;
                //fld.Edit.Enable = false;
                //fld.Filter.Control = faControl.TextBox;
                //cur.AddField(fld);

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
                fld.LookUp.TableAlias = "user3";
                cur.AddField(fld);



                //Эл.кан.связи
                fld = new faField();
                fld.Data.FieldName = "inet";                       
                fld.View.Hint = "Получено по электронным каналам связи";
                fld.View.CaptionShort = "Эл.кан.связи";
                fld.View.Width = 90;
                fld.View.TextAlign = "center";
                fld.Edit.Visible = false;
                fld.Edit.Enable = false;
                fld.Edit.Control = faControl.DropDown;
                fld.Filter.Control = faControl.DropDown;
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.LookUp.Table = "_yesno";
                fld.LookUp.TableAlias = "yn2";
                cur.AddField(fld);
                
                #endregion

                list.MainCursor = cur;
                list.Render(form1, this);
            //}

            //else
            //    form1.Controls.Add(new LiteralControl(faFunc.Alert(faAlert.PageAccessDenied)));
        }
    }
}