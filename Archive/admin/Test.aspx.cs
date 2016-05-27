using System;
using System.Data.SqlClient;

namespace ArchNet.Admin
{
    public partial class Test : System.Web.UI.Page
    {
        private string check()
        {
            string ret = string.Empty;
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            try
            {
                conn.Open();
                SqlCommand cmd;
                cmd = new SqlCommand("SELECT top 1  * FROM [dbo].[_base]", conn);
                var res = cmd.ExecuteScalar();
                ret = res.ToString();
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return ret;
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Write(check());
            Response.End();
        }
    }
}