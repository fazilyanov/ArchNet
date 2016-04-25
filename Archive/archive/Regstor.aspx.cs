using System;

namespace WebArchiveR6
{
    public partial class Regstor : System.Web.UI.Page
    {
        public faListRegstor list;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            faField fld;
            faCursor cur;

            list = new faListRegstor();
            list.RouteName = "regstor";

            list.IDBase = "0";
            list.BaseName = "";// Master.cur_basename != "" ? Master.cur_basename : "";
            list.ShowFilterPanel = true;

            list.EditFormWidth = 1100;
            list.EditFormHeight = 350;

            

            // Шапка
            cur = new faCursor("_regstor");
            //cur.AliasAlt = cur.Alias + "_" + list.Page;
            cur.TableID = 39;
            cur.Caption = "Архив / Регистр хранения дел";
            cur.EnableViewButton = cur.EnableExcelButton = true;
            cur.EnableFileButton = false; cur.EnableAddButton = cur.EnableCopyButton = cur.EnableDelButton = cur.EnableSaveButton = Session["common_access_complect_regstor"] != null;

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

            //Организация
            fld = new faField();
            fld.Data.FieldName = "id_base";
            fld.View.CaptionShort = "Организация";
            fld.View.Width = 150;
            fld.Edit.Required = true;
            fld.Edit.DefaultValue = "1";
            fld.Edit.DefaultText = "ЗАО «Стройтрансгаз»";
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "namerus";
            fld.LookUp.Table = "_base";
            cur.AddField(fld);

            // Имя
            fld = new faField();
            fld.Data.FieldName = "name";
            fld.View.CaptionShort = "Имя";
            fld.View.Width = 200;
            fld.Edit.Max = 150;
            fld.Edit.Required = true;
            fld.Filter.Control = faControl.TextBox;
            cur.AddField(fld);

            // Штрихкод
            fld = new faField();
            fld.Data.FieldName = "barcode";
            fld.View.CaptionShort = "Штрихкод";
            fld.View.Width = 120;
            fld.View.TextAlign = "center";
            fld.Edit.Control = faControl.TextBoxInteger;
            fld.Edit.Min = 1000000000;
            fld.Edit.Max = Int32.MaxValue;
            cur.AddField(fld);

            //Место хранения
            fld = new faField();
            fld.Data.FieldName = "id_storage";
            fld.View.CaptionShort = "Место хранения";
            fld.View.Width = 180;
            fld.Edit.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_storage";
            fld.Filter.Control = faControl.DropDown;
            cur.AddField(fld);

            // Индекс дела
            fld = new faField();
            fld.Data.FieldName = "case_index";
            fld.View.CaptionShort = "Индекс дела";
            fld.View.Width = 100;
            fld.Edit.Max = 20;
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
            cur.AddField(fld);

            //Статус
            fld = new faField();
            fld.Data.FieldName = "id_status2";
            fld.View.CaptionShort = "Статус";
            fld.View.Width = 84;
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_status2";
            cur.AddField(fld);

            //Группа
            fld = new faField();
            fld.Data.FieldName = "id_regstor_group";
            fld.View.CaptionShort = "Группа";
            fld.View.Width = 84;
            fld.Edit.Control = faControl.DropDown;
            fld.Filter.Control = faControl.DropDown;
            fld.LookUp.Key = "id";
            fld.LookUp.Field = "name";
            fld.LookUp.Table = "_regstor_group";
            cur.AddField(fld);

            #endregion Поля

            list.MainCursor = cur;

            if (Request.QueryString["id"] != null || Request.QueryString["act"] != null)
            {
                #region Курсор

                if (true)//(Session[Master.cur_basename + "_access_complect_view"] != null || Session[Master.cur_basename + "_access_complect_edit"] != null)
                {
                    cur = new faCursor("_regstor_list");
                    cur.TableID = 40;
                    cur.Caption = "Документы";
                    cur.EnableViewButton = true;
                    cur.EnableFileButton = false;
                    cur.EnableDelButton = false;
                    cur.EnableAddButton = cur.EnableEditButton =
                    cur.EnableSaveButton = Session["common_access_complect_regstor"] != null;// Session[Master.cur_basename + "_access_docversion_edit"] != null;
                    cur.ShowPager = false;
                    cur.SortColumn = "id";
                    cur.SortDirection = "asc";

                    // Поля
                    // ID
                    fld = new faField();
                    fld.Data.FieldName = "id";
                    fld.View.CaptionShort = "ID";
                    fld.View.TextAlign = "center";
                    fld.View.Width = 100;
                    fld.Edit.Enable = false;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);

                    // Связывающее поле
                    fld = new faField();
                    fld.Data.FieldName = "id_regstor";
                    fld.Filter.Control = faControl.TextBoxInteger;
                    fld.Filter.Enable = true;
                    fld.View.Visible = false;
                    fld.Edit.Visible = false;
                    fld.Edit.Enable = false;
                    cur.AddField(fld);

                    // Код ЭА
                    fld = new faField();
                    fld.Data.FieldName = "id_archive";
                    fld.View.CaptionShort = "Код ЭА";
                    fld.View.TextAlign = "center";
                    fld.View.Width = 100;
                    // fld.Edit.Enable = false;
                    fld.Edit.Control = faControl.TextBoxInteger;
                    fld.Filter.Control = faControl.TextBoxInteger;
                    cur.AddField(fld);

                    // Штрихкод
                    fld = new faField();
                    fld.Data.FieldName = "barcode";
                    fld.View.CaptionShort = "Штрихкод";
                    fld.View.Width = 120;
                    fld.View.TextAlign = "center";
                    fld.Edit.Control = faControl.TextBoxInteger;
                    fld.Edit.Min = 1000000000;
                    fld.Edit.Max = Int32.MaxValue;
                    cur.AddField(fld);

                    //Качество
                    fld = new faField();
                    fld.Data.FieldName = "id_quality";
                    fld.View.CaptionShort = "Качество";
                    fld.View.Width = 120;
                    fld.View.TextAlign = "center";
                    fld.Edit.Enable = false;
                    fld.Edit.Visible = false;
                    fld.Filter.Control = faControl.DropDown;
                    fld.LookUp.Key = "id";
                    fld.LookUp.Field = "name";
                    fld.LookUp.Table = "_quality";
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


                    list.JSReadyList.Add("form" + cur.Alias + "dialog_nextbuttonbind",
                                 "$('#" + cur.Alias + "_barcode').keypress(function(event){" +
                                 "var keycode = (event.keyCode ? event.keyCode : event.which);" +
                                 "var idlist=$('#" + cur.Alias + "_id').val();" +
                                 "if(keycode == '13'){" +
                                 "   $.ajax({" +
                                 "   url: '/ajax/InsertFromScanerRegstor.aspx?id=" + Request.QueryString["id"].ToString() + "&cur=" + cur.Alias + "_cursor_" + Request.QueryString["id"].ToString() + "&idlist='+idlist," +
                                 "   type: 'POST'," +
                                 "       data: $('#" + cur.Alias + "_barcode').val()," +
                                 "       cache: false," +
                                 "       contentType: false," +
                                 "       processData: false," +
                                 "       success: function (html) { if (html.length>0){alert(html);} else{ if (idlist=='')NavigateNew(); else NavigateNext($('#" + cur.Alias + "_barcode').val());}}," +
                                 "       error: function (request, status, error) {  alert(request.responseText); }" +
                                 "   });" +
                                 "}});");
                    list.JSFunctionList.Add("NavigateNext(barcode)",
                        "var grid = $('#cph_jqGrid" + cur.Alias + "');" +
                        "var gridArr = grid.getDataIDs();" +
                        "var selrow = grid.getGridParam('selrow');" +
                        "grid.setCell(selrow,'barcode',barcode);" +
                        "var curr_index = 0;" +
                        "$('#form" + cur.Alias + "_edit').dialog('close');" +
                        "for (var i = 0; i < gridArr.length; i++) {if (gridArr[i] == selrow)curr_index = i;}" +
                        "if ((curr_index + 1) < gridArr.length){grid.resetSelection().setSelection(gridArr[curr_index + 1], true);ViewRow" + cur.Alias + "();}");
                    list.JSFunctionList.Add("NavigateNew()",
                        "jQuery('#cph_jqGrid_regstor_list').trigger('reloadGrid');" +
                        "$('#form" + cur.Alias + "_edit').dialog('close');" +
                        "AddRow" + cur.Alias + "();");


                    list.AddCursor(cur);
                }

                #endregion Курсор
            }
            list.Render(form1, this);
        }
    }
}