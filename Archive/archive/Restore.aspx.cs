using System;
using System.Data.SqlClient;

namespace WebArchiveR6
{
    public partial class Restore : System.Web.UI.Page
    {
        public string _ret = "";
        public string _page = "";

        protected void Page_PreRender(object sender, EventArgs e)
        {
            string _table = (Request.QueryString["table"] ?? "").ToString().Trim();
            string _id = (Request.QueryString["id"] ?? "").ToString().Trim();
            string ret = "";
            if (_table != "" && _id != "")
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction("restore_tr");
                try
                {
                    SqlCommand cmd = new SqlCommand("", conn, trans);
                    cmd.CommandText = "UPDATE [dbo].[" + _table + "] SET [del]=0  where id = " + _id;
                    cmd.ExecuteNonQuery();
                    // faFunc.ToJournal(cmd, (Session["user_id"] ?? "0").ToString(), 2, Convert.ToInt32(_id), Session[Master.cur_basename + "_id"].ToString(), 1, "BulkEdit " + _stamp + "\n[id_doctype] " + r["id_doctype"].ToString() + " -> " + archive_id_doctype + "\n", 0);
                    trans.Commit();
                    conn.Close();
                    ret = "Успешно восстановлено ";
                }
                catch (Exception ex)
                {
                    ret = faFunc.GetExceptionMessage(ex); ;
                    try
                    {
                        trans.Rollback();
                        conn.Close();
                    }
                    catch (Exception ex2)
                    {
                        ret = ex2.GetType() + ":" + ex2.Message;
                    }
                }
                Response.Write(ret);
                Response.End();
            }
        }
    }
}