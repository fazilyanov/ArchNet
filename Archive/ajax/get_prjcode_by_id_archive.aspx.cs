using System;
using System.Data;
using System.Data.SqlClient;

namespace WebArchiveR6.ajax
{
    public partial class get_prjcode_by_id_archive : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string cb = Request.QueryString["cb"];
            if (!String.IsNullOrEmpty(id) && !String.IsNullOrEmpty(cb))
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT a.id,a.id_prjcode,b.code_new as prjcode FROM [dbo].[" + cb + "_archive] as a " +
                    "left join [dbo].[_prjcode] b ON b.id=a.id_prjcode and b.del=0 " +
                    "where a.id=" + id + " and a.del=0", conn);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                conn.Close();
                Response.Clear();
                if (dt.Rows.Count > 0)

                    Response.Write(dt.Rows[0]["id_prjcode"].ToString() + "|" + dt.Rows[0]["prjcode"].ToString());
                else
                    Response.Write("0|0");
                Response.End();
            }
        }
    }
}