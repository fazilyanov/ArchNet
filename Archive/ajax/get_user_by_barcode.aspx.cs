using System;
using System.Data;
using System.Data.SqlClient;

namespace WebArchiveR6.ajax
{
    public partial class get_user_by_barcode : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string barcode = Request.QueryString["barcode"];
            
            if (!String.IsNullOrEmpty(barcode))
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT a.id_user,b.name FROM [dbo].[_user_barcode] as a " +
                    "left join [dbo].[_user] b ON b.id=a.id_user and b.del=0 " +
                    "where a.barcode=" + barcode + " and a.del=0", conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                conn.Close();
                Response.Clear();
                if (dt.Rows.Count > 0)
                    Response.Write(dt.Rows[0]["id_user"].ToString() + "|" + dt.Rows[0]["name"].ToString());
                else
                    Response.Write("0| ");
                Response.End();
            }
        }
    }
}