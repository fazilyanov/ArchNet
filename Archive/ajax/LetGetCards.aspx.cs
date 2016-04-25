using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebArchiveR6 {
    public partial class LetGetCards : System.Web.UI.Page {
        string cb;
        string id;
        string docpack;
        protected void Page_Load(object sender, EventArgs e) {
            cb = Page.RouteData.Values["p_base"].ToString();
            id = Page.RouteData.Values["id_archive"].ToString();
            if (String.IsNullOrEmpty(id)) id = "0";
            docpack = Page.RouteData.Values["docpack"].ToString();
            if (String.IsNullOrEmpty(docpack)) docpack = "0";
            //if ((id != "0") && !String.IsNullOrEmpty(cb)) {
            //    Response.Redirect("/ajax/getcard1c.aspx?base=" + cb + "&id=" + id + "&key=" + faFunc.GetMD5Hash(id));
            //}
            //else
            if ((docpack != "0" || id != "0") && !String.IsNullOrEmpty(cb)) {
                string sqltext0 = "Select namerus From _base Where name = '" + cb + "'";
                SqlConnection conn0 = new SqlConnection(Properties.Settings.Default.constr);
                conn0.Open();
                SqlCommand cmd0 = new SqlCommand(sqltext0, conn0);
                string frm1 = "";
                Object obj = cmd0.ExecuteScalar();
                try {
                    frm1 = obj.ToString();
                }
                catch {
                    frm1 = "";
                }
                conn0.Close();
                string sqlText =
                    "SELECT " +
                    "f.name As frm2, " +
                    "a.id, " +
                    "docpack, " +
                    "k.name As doctree, " +
                    "CONVERT(VARCHAR(10),date_doc,104) as date_doc, " +
                    "a.num_doc, " +
                    "f1.name As f1name, " +
                    "a.prim,dv.[file] " +
                    " FROM " + cb + "_archive a " +
                    " Left Join " + cb + "_docversion dv On dv.id_archive = a.id AND dv.main = 1 " +
                    " Left Join _frm f On f.id = a.id_frm_contr " +
                    " Left Join _frm f1 On f1.id = a.id_frm_prod " +
                    " Left Join _doctree k On k.id = a.id_doctree " +
                    (docpack != "0" ? "WHERE (a.del=0 and a.docpack = " + docpack + ")" : "WHERE (a.del=0 and a.id = " + id + ") ");
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlText, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable datatable = new DataTable();
                sqlDataAdapter.Fill(datatable);
                if (datatable.Rows.Count > 0) {
                    GridView1.DataSource = datatable;
                    GridView1.DataBind();
                    for (int r = 0; r < datatable.Rows.Count; r++) {
                        string id1 = datatable.Rows[r][1].ToString();
                        string _f = datatable.Rows[r][8].ToString();
                        GridView1.Rows[r].Cells[1].Text = "<a href='/Archive/GetFile.aspx?b=" + cb + "&id=" + id1 + "&k=" + faFunc.GetMD5Hash(id1) + "&f=" + _f + "'>" + datatable.Rows[r][1].ToString() + "</a>";
                        GridView1.Rows[r].Cells[6].Text = frm1;
                    }

                    GridView1.HeaderRow.Cells[0].Text = "Контрагент";
                    GridView1.HeaderRow.Cells[1].Text = "Код ЭА";
                    GridView1.HeaderRow.Cells[2].Text = "Пакет док.";
                    GridView1.HeaderRow.Cells[3].Text = "Документ";
                    GridView1.HeaderRow.Cells[4].Text = "Дата док.";
                    GridView1.HeaderRow.Cells[5].Text = "№ Документа";
                    GridView1.HeaderRow.Cells[6].Text = "Организация";
                    GridView1.HeaderRow.Cells[7].Text = "Примечание";
                    GridView1.HeaderRow.Cells[8].Text = "Файл";
                }
                else {
                    Response.Clear();
                    Response.Write("Документ не найден.");
                }
                conn.Close();
                /*}
                else
                {
                    Response.Clear();
                    Response.Write("Документ не найден.");
                }*/
            }
            else {
                Response.Clear();
                Response.Write("Передан неверный параметр id = " + id + " docpack = " + docpack + " base = " + cb);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e) {
            /*GridViewRow row = GridView1.SelectedRow;
            Message.Text = row.Cells[1].Text;*/
        }

        protected void GridView1_PageIndexChanged(object sender, EventArgs e) {
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e) {
        }

        protected void Button1_Click(object sender, EventArgs e) {
            GridViewRow row = GridView1.SelectedRow;
            string selected = row.Cells[1].Text;
            Response.Redirect("/ajax/getfile1c.aspx?base=" + cb + "&id=" + selected + "&key=" + faFunc.GetMD5Hash(selected));
            //Response.Redirect("../../ajax/getcard1c.aspx?base=" + cb + "&id=" + selected + "&key=" + Func.GetMD5Hash(selected));
        }
    }
}