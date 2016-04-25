using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Configuration;
using Trirand.Web.UI.WebControls;
using System.IO;

namespace WebArchiveR6
{
    public partial class JournalList : System.Web.UI.Page
    {
        public faList list;
        public faField fld;
        public faCursor cur;
        public string js = "";

        /*protected void Page_PreRender(object sender, EventArgs e)
        {
            string RouteName = "_journal";
            if (Session["common_access_admin_journal"] != null)
            {
                list = new faList();
                list.ShowFilterPanel = true;
                list.RequestPost = Request.Form;
                list.RequestGet = Request.QueryString;               
                list.FormWidth = 800;
                list.FormHeight = 300; 
                // Шапка
                cur = new faCursor("_journal");
                cur.TableID = 7;
                cur.Caption = "Администратор / Общий журнал";
                //
                fld = new faField("id");
                fld.View.Caption = "ID";
                fld.View.Width = 80;          
                cur.AddField(fld);
                //
                fld = new faField("when");
               
                fld.View.Caption = "Время";
                fld.View.Width = 115;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.DateTimePicker;
                cur.AddField(fld);
                //
                fld = new faField("id_user");
                
                fld.LookUp.Table = "_user";
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.View.Caption = "Пользователь";
                fld.View.Width = 185;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.AutoComplete;
                cur.AddField(fld);
                //
                fld = new faField("id_edittype");
                            fld.LookUp.Table = "_edittype";
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "name";
                fld.View.Caption = "Действие";
                fld.View.Width = 105;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.DropDown;
                cur.AddField(fld);
                //
                fld = new faField("id_base");
  
                fld.LookUp.Table = "_base";
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "namerus";
                fld.View.Caption = "База";
                fld.View.Width = 165;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.DropDown;
                cur.AddField(fld);
                //
                fld = new faField("id_table");
 
                fld.LookUp.Table = "_table";
                fld.LookUp.Key = "id";
                fld.LookUp.Field = "description";
                fld.View.Caption = "Таблица";
                fld.View.Width = 165;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.DropDown;
                cur.AddField(fld);
                //
                fld = new faField("change_id");
             
                fld.View.Caption = "# Записи";
                fld.View.Width = 75;
                fld.Filter.Enable = true;
                fld.Filter.Control = faControl.TextBoxInteger;
                cur.AddField(fld);
                //
                fld = new faField("changes");
                
                fld.View.Caption = "Изменения";
                fld.View.Width = 300;
                cur.AddField(fld);
                //
                list.MainCursor = cur;
                //
                if (Request.QueryString["id"] == null || Request.QueryString["act"] == null)
                {
                    JQGrid jqAlias = new JQGrid();
                    list.PrepareJQGrid(out jqAlias);
                    form1.Controls.Add(new LiteralControl(list.RenderFilterPanel()));
                    form1.Controls.Add(jqAlias);
                    js = list.GenerateListJS(jqAlias);
                }
                else
                {
                    string alert = "";
                    if (Request.Form["cph_btnSave"]!=null)
                    {
                        string res = "";
                        //
                        if (list.Save(out res))
                        {
                            Response.Redirect(GetRouteUrl(RouteName, new { }) + "?id=" + res + "&act=view&saved=ok");
                        }
                        else
                        {
                            alert = faFunc.GenAlert("danger", res);
                        }
                    }
                    if (Request.QueryString["saved"] != null)
                    {
                        alert = faFunc.GenAlert("success", "Успешно сохранено");
                    }
                    if (Request.Form["cph_btnClose"] != null)
                    {
                       // list.Save();
                    }
                    //
                    form1.Controls.Add(new LiteralControl(list.RenderEditForm(alert)));
                    js = list.GenerateFormJS();
                }
            }
            else
                form1.Controls.Add(new LiteralControl(faFunc.AlertAccessDenied));

        }*/
    }
}