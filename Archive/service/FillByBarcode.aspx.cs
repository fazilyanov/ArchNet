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
using System.IO;

namespace WebArchiveR6
{
    public partial class FillByBarcode : System.Web.UI.Page
    {
        public string _data = "jqBarcodeData";

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ////if (Request.QueryString["jqAutoCompleteID"] != null) return;
            //var _a = (Dictionary<string, Dictionary<string, string>>)Session["access"];
            //if (!(_a.ContainsKey(Master.cur_basename) && _a[Master.cur_basename].ContainsKey("a_admin_user")))
            //    Response.Redirect(GetRouteUrl("error", new { p_base = Master.cur_basename, p_error = "accessdenied" }));
        }

        protected DataTable GetData()
        {
            if (Session[_data] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(int));
                dt.Columns.Add("barcode", typeof(string));
                dt.Columns.Add("date_doc", typeof(string));
                dt.Columns.Add("doctree", typeof(string));
                dt.Columns.Add("frm", typeof(string));
                dt.Columns.Add("num_doc", typeof(string));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
                Session[_data] = dt;
            }
            return (Session[_data] as DataTable);
        }

        // Запрос данных 
        protected void jqBarcode_DataRequesting(object sender, Trirand.Web.UI.WebControls.JQGridDataRequestEventArgs e)
        {
            jqBarcode.DataSource = GetData();
            jqBarcode.DataBind();
        }


        // УДАЛЕНИЕ Пользователя
        protected void jqBarcode_RowDeleting(object sender, Trirand.Web.UI.WebControls.JQGridRowDeleteEventArgs e)
        {
            DataTable dt = GetData();
            DataRow row = dt.Rows.Find(e.RowKey);
            row.Delete();
            jqBarcode.DataSource = Session[_data] = dt;
            jqBarcode.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = GetData();
            string bc = tbBarcode.Text.Trim();
            int bci = 0;
            if (bc.Length > 0 && int.TryParse(bc, out bci))
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                string sql = "SELECT a.id_archive, a.barcode,b.num_doc,CONVERT(nvarchar(10), b.date_doc, 104) as date_doc,f.name as frm, g.name as doctree ";
                sql += "FROM [zao_stg_docversion] a ";
                sql += "LEFT JOIN [zao_stg_archive] b ON a.id_archive=b.id ";
                sql += "LEFT JOIN [_frm] f ON b.id_frm_contr=f.id ";
                sql += "LEFT JOIN [_doctree] g ON b.id_doctree=g.id ";
                sql += "WHERE a.barcode = @p_barcode";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@p_barcode", tbBarcode.Text.Trim());
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable res = new DataTable();
                sqlDataAdapter.Fill(res);
                conn.Close();

                if (res.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    
                    row["barcode"] = res.Rows[0]["barcode"];
                    row["id"] = res.Rows[0]["id_archive"];
                    row["num_doc"] = res.Rows[0]["num_doc"];
                    row["doctree"] = res.Rows[0]["doctree"];
                    row["date_doc"] = res.Rows[0]["date_doc"];
                    row["frm"] = res.Rows[0]["frm"];
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r["id"].ToString() == row["id"].ToString())
                        {
                            Label1.Text = "&nbsp;&nbsp; Код '" + tbBarcode.Text + "' уже добавлен";
                            tbBarcode.Text = "";
                            return;
                        }
                    }
                    dt.Rows.InsertAt(row, 0);
                }

                Label1.Text = "&nbsp;&nbsp; Код '" + tbBarcode.Text + "' добавлен";
                tbBarcode.Text = "";

                Session[_data] = dt;
                jqBarcode.DataSource = dt;
                jqBarcode.DataBind();
            }
            else
            {
                Label1.Text = "&nbsp;&nbsp; Код '" + tbBarcode.Text + "' не найден";
                tbBarcode.Text = "";

            }

        }

        protected void ExportToExcel(Object sender, EventArgs e)
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Exported_Archive(" + DateTime.Now.ToLocalTime() + ").xls");
            HttpContext.Current.Response.ContentType = "application/ms-excel;charset=UTF-8;";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            DataTable dt = new DataTable();

            dt = GetData();

            htw.Write("<table cellspacing='0' rules='all' border='1' id='exportGrid' style='border-collapse:collapse;'>	");
            htw.Write("<tr><th>Код ЭА</th>");
            htw.Write("<th>Штрихкод</th>");
            htw.Write("<th>Дата док.</th>");
            htw.Write("<th>Документ</th>");
            htw.Write("<th>Контрагент</th>");
            htw.Write("<th>Номер докум.</th></tr>");
            foreach (DataRow row in dt.Rows)
            {
                htw.Write("<tr>");
                htw.Write("<td>" + row["id"].ToString() + "</td>");
                htw.Write("<td>" + row["barcode"].ToString() + "</td>");
                htw.Write("<td>" + row["date_doc"].ToString() + "</td>");
                htw.Write("<td>" + row["doctree"].ToString() + "</td>");
                htw.Write("<td>" + row["frm"].ToString() + "</td>");
                htw.Write("<td>" + row["num_doc"].ToString() + "</td>");

                htw.Write("</tr>");
            }
            htw.Write("</table>");

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
        }

    }
}