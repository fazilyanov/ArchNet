using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WebArchiveR6
{
    public partial class UpdateDublicateArchiveTable : System.Web.UI.Page
    {
        public string cb;
  

        protected string FindDublicate()
        {
            string sReport = "";
            string query="";
            try
            {
                DataTable dtBaseList = faFunc.GetData("SELECT  [name] FROM [dbo].[_base] where [active]=1");
                if (dtBaseList.Rows.Count > 0)
                {
                    foreach (DataRow row_bl in dtBaseList.Rows)
                    {
                        query =
                            "TRUNCATE TABLE [dbo].[" + row_bl["name"].ToString() + "_archive_dub]; " +
                            "INSERT INTO [dbo].[" + row_bl["name"].ToString() + "_archive_dub] ([id_archive]) " +
                            "SELECT id " +
                            "FROM dbo." + row_bl["name"].ToString() + "_archive AS a " +
                            "WHERE EXISTS " +
                                "(SELECT cnt " +
                                 "FROM " +
                                   "(SELECT COUNT(*) AS cnt," +
                                           "num_doc," +
                                           "date_doc," +
                                           "id_frm_contr," +
                                           "id_doctree " +
                                    "FROM dbo." + row_bl["name"].ToString() + "_archive AS b " +
                                    "WHERE (del = 0) " +
                                    "GROUP BY num_doc," +
                                             "date_doc," +
                                             "id_frm_contr," +
                                             "id_doctree) AS c " +
                                 "WHERE (a.del = 0)" +
                                   "AND (c.cnt > 1)" +
                                   "AND (c.num_doc = a.num_doc)" +
                                   "AND (c.date_doc = a.date_doc)" +
                                   "AND (c.id_frm_contr = a.id_frm_contr)" +
                                   "AND (c.id_doctree = a.id_doctree))";

                        if (faFunc.ExecuteNonQuery(query, Properties.Settings.Default.constr, 300) != -1)
                            sReport += " Таблица " + row_bl["name"].ToString() + "_archive_dub обновлена<br/>";
                        else
                            sReport += " ОШИБКА при обновлении таблицы " + row_bl["name"].ToString() + "_archive_dub<br/>";
                    }
                }
            }
            catch (Exception ex)
            {
                sReport = ex.Message;
            }
            return sReport;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Write(FindDublicate());
            Response.End();
        }
    }
}