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
using System.Drawing;

namespace WebArchiveR6
{
    public partial class CheckByBarcode : System.Web.UI.Page
    {
        public string _data = "jqCheckData";
        public string cb;

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
                dt.Columns.Add("date_trans", typeof(string));
                dt.Columns.Add("status", typeof(string));
                dt.Columns.Add("file", typeof(string));
                //dt.Columns.Add("frm", typeof(string));
                //dt.Columns.Add("num_doc", typeof(string));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
                Session[_data] = dt;
            }
            return (Session[_data] as DataTable);
        }

        // Запрос данных 
        protected void jqCheck_DataRequesting(object sender, Trirand.Web.UI.WebControls.JQGridDataRequestEventArgs e)
        {
            jqCheck.DataSource = GetData();
            jqCheck.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)        
       {
           cb = Page.RouteData.Values["p_base"].ToString();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = GetData();
            string bc = tbBarcode.Text.Trim();
            int bci = 0;
            int id_archive = 0;
            string status = "";
            string file = "";
            string min_date_trans;
            if (bc.Length > 0 && int.TryParse(bc, out bci))
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.Parameters.AddWithValue("@p_barcode", bci);

                cmd.CommandText = "SELECT [id_archive] FROM [dbo].[" + cb + "_docversion] where barcode = @p_barcode and del=0";
                id_archive = (int)(cmd.ExecuteScalar() ?? 0);
                if (id_archive > 0)
                {
                    cmd.CommandText = "SELECT [file] FROM [dbo].[" + cb + "_docversion] where barcode = @p_barcode and del=0";
                    file = cmd.ExecuteScalar().ToString();

                    cmd.Parameters.AddWithValue("@id_archive", id_archive);
                    cmd.CommandText =
                        " SELECT b.name as [status]" +
                        " FROM [dbo].[" + cb + "_docversion] a" +
                        " left join [dbo].[_status] b ON b.id=a.id_status " +
                        " Where a.id_archive = @id_archive and a.main=1 and a.del=0";
                    status = cmd.ExecuteScalar().ToString();

                    cmd.CommandText =
                        " SELECT MIN([date_trans])" +
                        " FROM [dbo].[" + cb + "_docversion] a" +
                        " Where a.id_archive = @id_archive  and a.del=0";
                    min_date_trans = ((DateTime)cmd.ExecuteScalar()).ToShortDateString();

                    DataRow row = dt.NewRow();

                    row["barcode"] = bci;
                    row["id"] = id_archive;
                    row["status"] = status;
                    row["date_trans"] = min_date_trans;
                    row["file"] = file;

                    foreach (DataRow r in dt.Rows)
                    {
                        if (r["id"].ToString() == row["id"].ToString())
                        {
                            Label1.Text = "&nbsp;&nbsp; Код '" + tbBarcode.Text + "' уже добавлен";
                            Label1.ForeColor = Color.Red;
                            tbBarcode.Text = "";
                            return;
                        }
                    }

                    dt.Rows.InsertAt(row, 0);

                    Label1.Text = "&nbsp;&nbsp; Код '" + tbBarcode.Text + "' добавлен";
                    Label1.ForeColor = Color.Black;
                    tbBarcode.Text = "";

                    Session[_data] = dt;
                    jqCheck.DataSource = dt;
                    jqCheck.DataBind();
                }
                else
                {
                    Label1.Text = "&nbsp;&nbsp; Код '" + tbBarcode.Text + "' не найден";
                    Label1.ForeColor = Color.Red;
                    tbBarcode.Text = "";
                }
                conn.Close();
            }
            else
            {
                Label1.Text = "&nbsp;&nbsp; Код '" + tbBarcode.Text + "' не является числом";
                Label1.ForeColor = Color.Red;
                tbBarcode.Text = "";
            }
        }

        protected void ExportToExcel(Object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Exported_Archive(" + DateTime.Now.ToLocalTime() + ").xls");
            Response.ContentType = "application/ms-excel;";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            DataTable dt = new DataTable();

            dt = GetData();

            //<cc1:JQGridColumn DataField="id" Width="100" Editable="False" PrimaryKey="True"  DataType="Int" HeaderText="Код ЭА"/>
            //<cc1:JQGridColumn DataField="barcode" Width="150" DataType="String" HeaderText="Штрихкод"/>
            //<cc1:JQGridColumn DataField="date_trans" Width="250" HeaderText="Самая ранняя дата получения." DataFormatString="{0:dd.MM.yyyy}"/>
            //<cc1:JQGridColumn DataField="status" Width="300" DataType="System.String" HeaderText="Статус основной версии"/>  

            htw.Write("<table cellspacing='0' rules='all' border='1' id='exportGrid' style='border-collapse:collapse;'>	");
            htw.Write("<tr><th>Код ЭА</th>");
            htw.Write("<th>Штрихкод</th>");
            htw.Write("<th>Дата получения.</th>");
            htw.Write("<th>Статус основной версии</th></tr>");

            foreach (DataRow row in dt.Rows)
            {
                htw.Write("<tr>");
                htw.Write("<td>" + row["id"].ToString() + "</td>");
                htw.Write("<td>" + row["barcode"].ToString() + "</td>");
                htw.Write("<td>" + row["date_trans"].ToString() + "</td>");
                htw.Write("<td>" + row["status"].ToString() + "</td>");
                htw.Write("</tr>");
            }
            htw.Write("</table>");

            HttpContext.Current.Response.Clear();
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
        }

    }
}