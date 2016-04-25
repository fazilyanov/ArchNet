using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


namespace WebArchiveR6
{
    public partial class CopyToBase : System.Web.UI.Page
    {
        public string
            cb, dest, dest_rus, id, page;
        public int
            id_dest;
        public bool found;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_winlogin"] == null) Session["user_winlogin"] = Context.User.Identity.Name.Trim();

            //int t = (int)Session["1"];
            cb = Page.RouteData.Values["p_base"].ToString();
            page = Page.RouteData.Values["p_page"].ToString();
            id = Page.RouteData.Values["id"].ToString();
            int.TryParse((Request.QueryString["dest"] ?? "0").ToString(), out id_dest);
            if (id_dest == 0)
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                SqlCommand cmd = new SqlCommand("SELECT b.id,b.id_frm FROM [dbo].[_base] as b Where b.id_frm in (Select id_frm_contr From [" + cb + "_archive] Where id=" + id + ")", conn);
                conn.Open();
                id_dest = (int)(cmd.ExecuteScalar() ?? 0);
                conn.Close();
            }
            if (id_dest > 0)
            {
                dest = faFunc.GetBaseNameById(id_dest);
                dest_rus = faFunc.GetBaseNameRusById(id_dest);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            jqArchiveCheck.AppearanceSettings.Caption = "Поиск документа в базе: " + dest_rus;
        }

        protected void jqArchiveCheck_DataRequesting(object sender, Trirand.Web.UI.WebControls.JQGridDataRequestEventArgs e)
        {
            if (!String.IsNullOrEmpty(dest))
            {
                jqArchiveCheck.DataSource = CheckDoc(dest);
                jqArchiveCheck.DataBind();
            }
        }

        protected DataTable CheckDoc(string _base)
        {
            found = false;
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            DataTable dt_pre = new DataTable();
            DataTable dt_res = new DataTable();
            DataTable dt_frm = new DataTable();
            SqlCommand cmd = new SqlCommand("", conn);
            try
            {
                conn.Open();
                cmd.CommandText = "SELECT a.*,d.name as doctree_text FROM [dbo].[" + cb + "_archive] as a Left join _doctree as d on a.id_doctree = d.id WHERE a.id=" + id + " and a.del=0 ";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt_pre);
                if (dt_pre.Rows.Count > 0)
                {
                    DataRow r = dt_pre.Rows[0];

                    cmd.CommandText = "SELECT b.id_frm, f.name FROM [dbo].[_base] as b Left join _frm as f  ON b.id_frm = f.id and f.del=0  WHERE b.name='" + cb + "'";
                    sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dt_frm);
                    int id_frm = (int)(dt_frm.Rows[0]["id_frm"] ?? 0);
                    string frm_name = (dt_frm.Rows[0]["name"] ?? "").ToString();


                    cmd.CommandText = "SELECT a.id,a.docpack,a.date_doc,a.num_doc,f.name as frm,k.name as doctree,a.summ  " +
                        " FROM [dbo].[" + _base + "_archive] as a " +
                        " Left Join _frm f On f.id = a.id_frm_contr " +
                        " Left Join _doctree k On k.id = a.id_doctree " +
                        " WHERE a.del=0 AND" +
                        " id_frm_contr=@id_frm_contr AND id_doctree=@id_doctree AND UPPER(num_doc)=@num_doc AND " +
                        " date_doc=@date_doc"; // AND summ=@summ
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id_frm_contr", id_frm);
                    cmd.Parameters.AddWithValue("@id_doctree", r["id_doctree"]);
                    cmd.Parameters.AddWithValue("@num_doc", r["num_doc"].ToString().Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@date_doc", r["date_doc"].ToString().Trim());
                    //cmd.Parameters.AddWithValue("@summ", r["summ"]);
                    sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dt_res);

                    // Фильтры
                    Session[dest + "_archive_srch_id_frm_contr_filter"] = id_frm;
                    Session[dest + "_archive_srch_id_frm_contr_filter_text"] = frm_name;
                    Session[dest + "_archive_srch_id_doctree_filter"] = "(" + r["id_doctree"].ToString() + ")";
                    Session[dest + "_archive_srch_id_doctree_filter_text"] = r["doctree_text"];
                    //Session[dest + "_archive_srch_num_doc_filter"] = r["num_doc"];
                    //Session[dest + "_archive_srch_num_doc_filter_cond"] = "*";
                    //Session[dest + "_archive_srch_date_doc_filter"] = Session[dest + "_archive_srch_date_doc_filter2"] = ((DateTime)r["date_doc"]).ToShortDateString();
                    //Session[dest + "_archive_srch_summ_filter2"] =Session[dest + "_archive_srch_summ_filter"] =  r["summ"].ToString().Replace(",",".");
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                Response.Write(faFunc.GetExceptionMessage(ex));
            }
            conn.Close();
            //            if (dt_res.Rows.Count > 0) jqArchiveCheck.SelectedRow = dt_res.Rows[0]["id"].ToString();
            return dt_res;
        }

        protected DataTable GetDataDocpack()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            DataTable datatable = new DataTable();
            // string _id = (Session["LetGetCardDocpackId"] ?? id).ToString();
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
                       " zao_stg_person.[name]," +
                       "f.name As frm, " +
                       "dv.[file] " +
                       " FROM " + cb + "_archive a " +
                       " Left Join " + cb + "_docversion dv On dv.id_archive = a.id AND dv.main = 1 and dv.del=0" +
                       " LEFT JOIN _prjcode ON a.[id_prjcode] = _prjcode.[id]" +
                       " LEFT JOIN zao_stg_person ON a.[id_perf] = zao_stg_person.[id]" +
                       " Left Join _frm f On f.id = a.id_frm_contr " +
                       " Left Join _doctree k On k.id = a.id_doctree " +
                       "WHERE (a.del = 0 and  a.id= " + id + ") )";//(a.docpack in (SELECT a.docpack FROM " + cb + "_archive a Where a.id= " + _id + " and a.docpack>0 )  or


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

        protected void add_to_card_Click(object sender, EventArgs e)
        {
             DataTable dt = ((DataTable)HttpContext.Current.Session[cb + "_docversion_cursor_" + id]).Copy() ;
            int i = 0;
            foreach (DataRow row_new in dt.Rows)
            {
                row_new["id"] = --i;
                row_new["status"] = 1;
                row_new["nn"] = 0;
                row_new["ver"] = "";
            }
            HttpContext.Current.Session[dest + "_docversion_cursor_0"] = dt;
            Response.Redirect("/archive/" + dest + "/" + page + "?id=0&from=" + id + "&frombase=" + cb + "&act=copybase");
        }

        protected void add_as_ver_Click(object sender, EventArgs e)
        {
            int id_arch = 0;
            int.TryParse(id_archive.Text, out id_arch);
            if (id_arch > 0)
            {
                DataTable dt_from = Session[cb + "_docversion_cursor_" + id] as DataTable;
                int max_id = 0;
                foreach (DataRow row in dt_from.Rows)
                {
                    if ((int)row["id"] > max_id)
                        Session["copybaserow"] = row;
                }
                Session.Remove(dest + "_docversion_cursor_" + id_arch);
                Response.Redirect("/archive/" + dest + "/" + page + "?id=" + id_arch + "&frombase=" + cb + "&act=view&mode=nvfob");
                //  http://sky-sp1.stg.lan:8013/archive/zao_stg/srch?id=14&act=view&mode=complectnew&from=29&b=1020172521&f=2015\2\20\2015-02-20-15-25-54-807_s.bokareva.pdf&d=20.02.2015 15:26
            }
        }
    }
}