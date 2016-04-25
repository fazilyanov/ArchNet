using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6
{
    public partial class LetGetCardsAlt : System.Web.UI.Page
    {
        public string cb;
        public string justdog;
        private string id;

        //
        protected DataTable GetData()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText = "[dbo].[" + ((Session["LetGetCardAltFull"] ?? "").ToString() == "full" ? ((Session["JustDog"] ?? "").ToString() == "1" ? "GetParentTreeDog" : "GetParentTree") : ((Session["JustDog"] ?? "").ToString() == "1" ? "GetParentTreeShortDog" : "GetParentTreeShort")) + "]";
            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@p_base", cb));
            cmd.Parameters.Add(new SqlParameter("@p_id", id));
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable datatable = new DataTable();
            sqlDataAdapter.Fill(datatable);
            conn.Close();
            return datatable;
        }

        //
        protected DataTable GetDataBuhTech()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            DataTable datatable = new DataTable();
            string _id = (Session["LetGetCardDocpackId"] ?? id).ToString();
            try
            {
                string sqlText =
                       "SELECT " +
                       "a.id, " +
                       "a.docpack, " +
                       "k.name As doctree, " +
                       "CONVERT(VARCHAR(10),a.date_doc,104) as date_doc, " +
                       "a.num_doc, " +
                       "a.summ, " +
                       "_prjcode.code_new," +
                        cb + "_person.[name]," +
                       "f.name As frm, " +
                       "dv.[file] " +
                       " FROM " + cb + "_buh_tech_view z " +
                       " Left Join " + cb + "_archive a on a.id=z.id_archive" +
                       " Left Join " + cb + "_docversion dv On dv.id_archive = a.id AND dv.main = 1 and dv.del=0" +
                       " LEFT JOIN _prjcode ON a.[id_prjcode] = _prjcode.[id]" +
                       " LEFT JOIN " + cb + "_person ON a.[id_perf] = " + cb + "_person.[id]" +
                       " Left Join _frm f On f.id = a.id_frm_contr " +
                       " Left Join _doctree k On k.id = a.id_doctree " +
                       "WHERE (a.del = 0 and z.del = 0 and  z.link= " + _id + ")";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlText, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                sqlDataAdapter.Fill(datatable);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Response.Write(faFunc.GetExceptionMessage(ex));
            }
            return datatable;
        }

        //
        protected DataTable GetDataDocpack()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            DataTable datatable = new DataTable();
            string _id = (Session["LetGetCardDocpackId"] ?? id).ToString();
            try
            {
                string sqlText =
                       "SELECT " +
                       "a.id, " +
                       "docpack, " +
                       "k.name As doctree, " +
                       "CONVERT(VARCHAR(10),date_doc,104) as date_doc, " +
                       "a.num_doc, " +
                       "a.summ, " +
                       "_prjcode.code_new," +
                        cb + "_person.[name]," +
                       "f.name As frm, " +
                       "dv.[file] " +
                       " FROM " + cb + "_archive a " +
                       " Left Join " + cb + "_docversion dv On dv.id_archive = a.id AND dv.main = 1 and dv.del=0" +
                       " LEFT JOIN _prjcode ON a.[id_prjcode] = _prjcode.[id]" +
                       " LEFT JOIN " + cb + "_person ON a.[id_perf] = " + cb + "_person.[id]" +
                       " Left Join _frm f On f.id = a.id_frm_contr " +
                       " Left Join _doctree k On k.id = a.id_doctree " +
                       "WHERE (a.del = 0 and (a.docpack in (SELECT a.docpack FROM " + cb + "_archive a Where a.id= " + _id + " and a.docpack>0 )  or a.id= " + _id + ") )";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlText, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                sqlDataAdapter.Fill(datatable);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Response.Write(faFunc.GetExceptionMessage(ex));
            }
            return datatable;
        }

        private DataRow GetFullArchiveInfo(string _id)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            DataTable datatable = new DataTable();
            try
            {
                string sqlText =
                       "SELECT " +
                       "a.id, " +
                       "a.date_upd, " +
                       "a.docpack, " +
                       "k.name As doctree, " +
                       "CONVERT(VARCHAR(10),a.date_doc,104) as date_doc, " +
                       "_doctype.name as doctype,"+
                       "a.num_doc, " +
                       "par.num_doc as parent, " +
                       "dp.name as depart, " +
                       "a.summ, " +
                       "a.[content], " +
                       "a.[prim], " +
                       "a.doctext,"+
                       "st.name as state,"+
                       "_prjcode.code_new as prjcode," +
                        cb + "_person.[name] as perf," +
                       "f.name As frm, " +
                       "u.name as [user],"+
                       "yn.name as accept,"+
                       "dv.[file] " +
                       " FROM " + cb + "_archive a " +
                       " Left Join " + cb + "_docversion dv On dv.id_archive = a.id AND dv.main = 1 and dv.del=0" +
                       " LEFT JOIN _prjcode ON a.[id_prjcode] = _prjcode.[id]" +
                       " LEFT JOIN " + cb + "_person ON a.[id_perf] = " + cb + "_person.[id]" +
                       " Left Join _frm f On f.id = a.id_frm_contr " +
                       " Left Join _doctree k On k.id = a.id_doctree " +
                       " Left Join _user u On a.id_user = u.id " +
                       " Left Join _doctype On a.id_doctype = _doctype.id " +
                       " LEFT JOIN " + cb + "_archive par ON a.id_parent = par.[id] AND par.del=0"+
                       " LEFT JOIN " + cb + "_department as dp ON a.id_depart = dp.id AND dp.del=0" +
                       " LEFT JOIN _state as st ON a.id_state = st.id AND st.del=0" +
                       " LEFT JOIN _yesno AS yn ON a.[accept] = yn.[id] AND yn.del=0"+
                       "WHERE a.del = 0 and a.id= " + _id;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlText, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                sqlDataAdapter.Fill(datatable);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                Response.Write(faFunc.GetExceptionMessage(ex));
            }
            return datatable.Rows[0];
        }

        // Запрос данных
        protected void jqArchiveBuhTech_DataRequesting(object sender, Trirand.Web.UI.WebControls.JQGridDataRequestEventArgs e)
        {
            jqArchiveBuhTech.DataSource = GetDataBuhTech();
            jqArchiveBuhTech.DataBind();
        }

        // Запрос данных
        protected void jqArchiveDocpack_DataRequesting(object sender, Trirand.Web.UI.WebControls.JQGridDataRequestEventArgs e)
        {
            jqArchiveDocpack.DataSource = GetDataDocpack();
            jqArchiveDocpack.DataBind();
        }

        // Запрос данных
        protected void jqArchiveStructur_DataRequesting(object sender, Trirand.Web.UI.WebControls.JQGridDataRequestEventArgs e)
        {
            jqArchiveStructur.DataSource = GetData();
            jqArchiveStructur.DataBind();
        }

        //
        protected void Page_Load(object sender, EventArgs e)
        {
            cb = Page.RouteData.Values["p_base"].ToString();
            id = Page.RouteData.Values["id"].ToString();
            if ((Request.QueryString["id"] ?? "").ToString() != "")
            {
                string a_id = Request.QueryString["id"].ToString();

                DataRow r = GetFullArchiveInfo(a_id);
                string resp =
                    "<div id = 'r1' style = 'margin-top:-7px;margin-bottom: 7px;float: left;'>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width: 110px; float:left;'>Код ЭА</label><input class='form-control' style='width:180px;' value='" + a_id + "'></div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Дата ред.</label> <input id='date_upd' name='date_upd' class='form-control' style='width: 180px;' value='" + r["date_upd"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Оператор ЭА</label> <input id='user' name='user' class='form-control' style='width: 180px;' value='" + r["user"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Документ </label> <input id='doctree' name='doctree' class='form-control' style='width: 180px;' value='" + r["doctree"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>№ Документа </label> <input id='num_doc' name='num_doc' class='form-control' style='width: 180px;' value='" + r["num_doc"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Дата док. </label> <input id='date_doc' name='date_doc' class='form-control hasDatepicker' style='width: 180px;' value='" + r["date_doc"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Вид док. </label> <input id='doctype' name='doctype' class='form-control' style='width: 180px;' value='" + r["doctype"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Контрагент</label> <input id='frm' name='frm' class='form-control' style='width: 180px;' value='" + r["frm"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Сумма</label> <input id='summ' name='summ' class='form-control' style='width: 180px;' value='" + r["summ"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Пакет</label> <input id='docpack' name='docpack' class='form-control' style='width: 180px;' value='" + r["docpack"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Код проекта</label> <input id='prjcode' name='prjcode' class='form-control' style='width: 180px;' value='" + r["prjcode"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Старший док.</label> <input id='parent' name='parent' class='form-control' style='width: 180px;' value='" + r["parent"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Исполнитель</label> <input id='perf' name='perf' class='form-control' style='width: 180px;' value='" + r["perf"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Получатель </label> <input id='depart' name='depart' class='form-control' style='width: 180px;' value='" + r["depart"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Содержание</label> <input id='content' name='content' class='form-control' style='width: 180px;' value='" + r["content"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Примечание</label> <input id='prim' name='prim' class='form-control' style='width: 180px;' value='" + r["prim"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Состояние </label> <input id='state' name='state' class='form-control' style='width: 180px;' value='" + r["state"].ToString() + "'> </div>" +
                    "<div class='input-group' style='margin-top:8px;'><label style='width:110px;float:left;'>Принят к учету</label> <input id='accept' name='accept' class='form-control' style='width: 180px;' value='" + r["accept"].ToString() + "'> </div>" +
                    "</div>" +
                    "<div id = 'r2' style = 'margin-left:7px; float:left;'>" +
                    "<textarea id='doctext' name='doctext' class='form-control' style='height: 531px; width: 340px;'>" + r["doctext"].ToString() +"</textarea>"+ 
                    "</div>";
                Response.Clear();
                Response.Write(resp);
                Response.End();
            }
            else
            {
               
                justdog = Page.RouteData.Values["justdog"].ToString();

                if (!String.IsNullOrEmpty(id) && !String.IsNullOrEmpty(cb) && !String.IsNullOrEmpty(justdog))
                {
                    string sqltext0 = "Select namerus From _base Where name = '" + cb + "'";
                    SqlConnection conn0 = new SqlConnection(Properties.Settings.Default.constr);
                    conn0.Open();
                    SqlCommand cmd0 = new SqlCommand(sqltext0, conn0);
                    string frm1 = "";
                    try
                    {
                        Object obj = cmd0.ExecuteScalar();
                        frm1 = obj.ToString();
                    }
                    catch
                    {
                        frm1 = "";
                    }
                    conn0.Close();
                }
                else
                {
                    Response.Clear();
                    Response.Write(faFunc.Alert(faAlert.BadParam));
                }
            }
        }

        //
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && jqArchiveStructur.AjaxCallBackMode == AjaxCallBackMode.None && jqArchiveDocpack.AjaxCallBackMode == AjaxCallBackMode.None && jqArchiveBuhTech.AjaxCallBackMode == AjaxCallBackMode.None)
            {
                faFunc.ToLog(6);
            }
            if (Session["JustDog"] == null) Session["JustDog"] = "0";
            jqArchiveStructur.SelectedRow = id;
            jqArchiveDocpack.SelectedRow = id;

            jqArchiveStructur.ToolBarSettings.CustomButtons[2].Text = (Session["LetGetCardAltFull"] ?? "").ToString() == "full" ? "Скрыть полную структуру договора" : "Показать полную структуру договора";
            if (justdog == "0")
            {
                Session["JustDog"] = "0";
                jqArchiveStructur.ToolBarSettings.CustomButtons[3].ButtonIcon = "none";
            }
            else
                jqArchiveStructur.ToolBarSettings.CustomButtons[3].Text = (Session["JustDog"] ?? "").ToString() == "1" ? "Показать бухгалтерские документы" : "Скрыть бухгалтерские документы";
        }
    }
}