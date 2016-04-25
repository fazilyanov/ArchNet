using System;
using System.Data;

namespace WebArchiveR6
{
    public partial class UpdateMonitor : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            string resp = "";
            string query = "";
            string res_for_base = "";

            DataTable dtBaseList = faFunc.GetData("SELECT [id], [name], [namerus] FROM [dbo].[_base] where id in (1, 17)");
            if (dtBaseList.Rows.Count > 0)
            {
                foreach (DataRow row_bl in dtBaseList.Rows)
                {
                    res_for_base = "";

                    #region Проверка внесенных

                    query =
                    "SELECT a.id_archive, " +
                    "       ISNULL(b.evcount,0) as evcount, " +
                    "       d.id_status, " +
                    "       f.del " +
                    "FROM[_monitor] AS a " +
                    "LEFT JOIN " +
                    "  (SELECT id_monitor, " +
                    "          count(id)AS evcount " +
                    "   FROM[_monitor_list] " +
                    "   GROUP BY id_monitor) b ON b.id_monitor = a.id " +
                    "LEFT JOIN[" + row_bl["name"].ToString() + "_docversion] d ON d.id_archive = a.id_archive AND d.main = 1 AND d.del = 0 " +
                    "LEFT JOIN[" + row_bl["name"].ToString() + "_archive] f ON f.id = a.id_archive " +
                    "WHERE a.id_base = " + row_bl["id"].ToString();
                    DataTable dt = faFunc.GetData(query);
                    if (dt != null)
                    {
                        string id_for_del = "-1";
                        int i = 0;
                        foreach (DataRow row in dt.Rows)
                            if (row["evcount"].ToString() == "0" && ((bool)row["del"]  || (int)row["id_status"] == 1 || (int)row["id_status"] == 2 || (int)row["id_status"] == 5 || (int)row["id_status"] == 7 || (int)row["id_status"] == 6))
                            {
                                id_for_del += "," + row["id_archive"].ToString();
                                i++;
                            }
                        if (i > 0)
                        {
                            query = "DELETE FROM[dbo].[_monitor] WHERE id_archive in (" + id_for_del + ")";
                            if (faFunc.ExecuteNonQuery(query) != -1) res_for_base += "&nbsp; Удалено записей : " + i + " <br/>";
                            else res_for_base += " Ошибка при удалении! <br/>";
                        }
                    }

                    #endregion Проверка внесенных

                    #region Добавление новых

                    query =
                        "INSERT INTO [dbo].[_monitor] ([id_archive] ,[num_doc] ,[date_doc] ,[id_frm_contr] ,[summ] ,[id_doctree] ,[id_doctype] ,[id_base], [perf], [id_status_ver]) " +
                        "SELECT a.[id] , " +
                               "a.[num_doc] , " +
                               "a.[date_doc] , " +
                               "a.[id_frm_contr] , " +
                               "a.[summ] , " +
                               "a.[id_doctree] , " +
                               "a.[id_doctype] , " +
                               row_bl["id"].ToString() +", "+
                               "ISNULL(p.[name],''), " +
                               "b.[id_status] " +
                        "FROM [dbo].[" + row_bl["name"].ToString() + "_archive] a " +
                        "LEFT JOIN [dbo].[" + row_bl["name"].ToString() + "_docversion] b ON b.id_archive=a.id AND b.main=1 AND b.del=0 " +
                        "LEFT JOIN [dbo].[" + row_bl["name"].ToString() + "_person] p ON a.id_perf=p.id " +
                        "WHERE a.del=0 " +
                          "AND a.id_doctree IN (5022, 5086, 5565, 5568) " +
                          "AND a.[date_doc]>= CONVERT(DATETIME,'01.01.2016 00:00', 104) " +
                          "AND b.id_status NOT IN (1, 2, 5, 6, 7) " +
                          "AND NOT EXISTS " +
                            "(SELECT * " +
                             "FROM [dbo].[_monitor] m " +
                             "WHERE a.id = m.id_archive AND m.id_base = " + row_bl["id"].ToString() + ") ";
                    int new_count = faFunc.ExecuteNonQuery(query);
                    if (new_count != -1)
                        res_for_base += (new_count > 0) ? "&nbsp;Добалено записей : " + new_count + "<br/>" : "";
                    else
                        res_for_base += " Ошибка при добавлении новых записей! <br/>";

                    #endregion Добавление новых

                    resp += res_for_base != "" ? "<br/>База: " + row_bl["namerus"].ToString() + ": <br/> " + res_for_base : "";
                }
            }
            Response.Clear();
            Response.Write(resp);
            Response.End();
        }
    }
}