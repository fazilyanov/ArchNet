using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using Trirand.Web.UI.WebControls;

namespace WebArchiveR6
{
    public partial class ArchiveDocpackFileDownload : System.Web.UI.Page
    {
        public string _data = "jqArchivePerformData";
        public string _ret = "";

        protected void btnAddDockpack_Click(object sender, EventArgs e)
        {
            GetIdArchive(2);
            result.Text = _ret;
        }

        protected void btnAddId_Click(object sender, EventArgs e)
        {
            GetIdArchive(1);
            result.Text = _ret;
        }

        protected void ClearGrid(Object sender, EventArgs e)
        {
            Session[_data] = null;
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
            htw.Write("<th>Номер док.</th>");
            htw.Write("<th>Пакет</th>");
            htw.Write("<th>Дата док.</th>");
            htw.Write("<th>Документ</th>");
            htw.Write("<th>Контрагент</th>");
            htw.Write("<th>Сумма</th>");
            htw.Write("<th>Статус</th>");
            htw.Write("<th>Скрытый</th></tr>");
            foreach (DataRow row in dt.Rows)
            {
                htw.Write("<tr>");
                htw.Write("<td>" + row["id"].ToString() + "</td>");
                htw.Write("<td>" + row["num_doc"].ToString() + "</td>");
                htw.Write("<td>" + row["docpack"].ToString() + "</td>");
                htw.Write("<td>" + row["date_doc"].ToString() + "</td>");
                htw.Write("<td>" + row["doctree"].ToString() + "</td>");
                htw.Write("<td>" + row["frm"].ToString() + "</td>");
                htw.Write("<td>" + row["summ"].ToString() + "</td>");
                htw.Write("<td>" + row["status"].ToString() + "</td>");
                htw.Write("<td>" + row["hidden"].ToString() + "</td>");
                htw.Write("</tr>");
            }
            htw.Write("</table>");

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
        }

        protected DataTable GetData()
        {
            if (Session[_data] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(int));
                dt.Columns.Add("docpack", typeof(string));
                dt.Columns.Add("date_doc", typeof(string));
                dt.Columns.Add("doctree", typeof(string));
                dt.Columns.Add("doctype", typeof(string));
                dt.Columns.Add("frm", typeof(string));
                dt.Columns.Add("num_doc", typeof(string));
                dt.Columns.Add("summ", typeof(string));
                dt.Columns.Add("file", typeof(string));
                dt.Columns.Add("status", typeof(string));
                dt.Columns.Add("hidden", typeof(string));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
                Session[_data] = dt;
            }
            return (Session[_data] as DataTable);
        }

        /// <summary>
        /// t=1 id_archive, t=2 docpack
        /// </summary>
        /// <param name="t"> </param>
        protected bool GetIdArchive(int t)
        {
            _ret = "";
            string ids = tbIDS.Text.Trim();
            if (ids.Length > 0)
            {
                string[] ArrIDS = ids.Split('\n');
                //
                var list = new List<string>(ArrIDS);
                ArrIDS = list.FindAll(val => val.Trim().Length > 0).ToArray();
                //
                int _intvalue = 0;
                string _where = "(-1";
                for (int i = 0; i < ArrIDS.Length; i++)
                {
                    if (ArrIDS[i].Length > 0 && int.TryParse(ArrIDS[i], out _intvalue))
                        _where += "," + _intvalue;
                    else
                        _ret += "Значение '" + ArrIDS[i] + "' не является числом\r\n";
                }
                _where += ")";
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                try
                {
                    conn.Open();
                    string cur_basename = Master.cur_basename != "" ? Master.cur_basename : "";
                    string sql = "SELECT a.id, a.num_doc, a.docpack, e.name as doctype, a.[summ], CONVERT(nvarchar(10), a.date_doc, 104) as date_doc,f.name as frm, g.name as doctree, d.[file], s.name as status,_yn.name as hidden ";
                    sql += "FROM [" + cur_basename + "_archive] a ";
                    sql += "LEFT JOIN [" + cur_basename + "_docversion] d ON d.id_archive=a.id and d.main=1 and d.del=0 ";
                    sql += "LEFT JOIN [_frm] f ON a.id_frm_contr=f.id ";
                    sql += "LEFT JOIN [_doctype] e ON a.id_doctype=e.id ";
                    sql += "LEFT JOIN [_doctree] g ON a.id_doctree=g.id ";
                    sql += "LEFT JOIN [_status] s ON d.id_status=s.id ";
                    sql += "LEFT JOIN [_yesno] _yn ON a.hidden=_yn.id ";
                    sql += "WHERE a.del=0 AND " + (t == 1 ? "a.id" : "a.docpack") + " in " + _where;

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //cmd.Parameters.AddWithValue("@p_where", _where);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable res = new DataTable();
                    sqlDataAdapter.Fill(res);
                    conn.Close();

                    if (res.Rows.Count > 0)
                    {
                        int i = 0;
                        DataTable dt = GetData();
                        foreach (DataRow r in res.Rows)
                        {
                            bool uniq = true;
                            foreach (DataRow ch in dt.Rows)
                            {
                                if (r["id"].ToString() == ch["id"].ToString())
                                {
                                    _ret += "Код ЭА '" + r["id"].ToString() + "' уже добавлен\r\n";
                                    uniq = false;
                                    break;
                                }
                            }
                            if (uniq)
                            {
                                DataRow row = dt.NewRow();
                                row["id"] = r["id"];
                                row["num_doc"] = r["num_doc"];
                                row["docpack"] = r["docpack"];
                                row["doctree"] = r["doctree"];
                                row["doctype"] = r["doctype"];
                                row["date_doc"] = r["date_doc"];
                                row["frm"] = r["frm"];
                                row["summ"] = r["summ"];
                                row["file"] = r["file"];
                                row["status"] = r["status"];
                                row["hidden"] = r["hidden"];
                                dt.Rows.Add(row);
                                i++;
                            }
                        }
                        _ret += "Найдено и добавлено записей: " + i + " из " + ArrIDS.Length;//res.Rows.Count;
                    }
                    else
                    {
                        _ret += "Запрос вернул пустой результат";
                    }
                }
                catch (Exception ex)
                {
                    _ret +=  faFunc.GetExceptionMessage(ex);
                    conn.Close();
                    return false;
                }
                return true;
            }
            else
            {
                _ret = "Нет данных для поиска";
                return false;
            }
        }

        // Запрос данных
        protected void jqArchivePerform_DataRequesting(object sender, Trirand.Web.UI.WebControls.JQGridDataRequestEventArgs e)
        {
            jqArchivePerform.DataSource = GetData();
            jqArchivePerform.DataBind();
        }

        // УДАЛЕНИЕ Пользователя
        protected void jqArchivePerform_RowDeleting(object sender, Trirand.Web.UI.WebControls.JQGridRowDeleteEventArgs e)
        {
            DataTable dt = GetData();
            DataRow row = dt.Rows.Find(e.RowKey);
            row.Delete();
            jqArchivePerform.DataSource = Session[_data] = dt;
            jqArchivePerform.DataBind();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            JQGridToolBarButton jqBtnFileDown = new JQGridToolBarButton();
            jqBtnFileDown.OnClick = "GoToFileDown";
            jqBtnFileDown.ButtonIcon = "ui-icon-arrowthickstop-1-s";
            jqBtnFileDown.Text = "Все файлы&nbsp;&nbsp;";
            jqBtnFileDown.ToolTip = "Выгрузить файлы соответствующие документам в списке";
            jqArchivePerform.ToolBarSettings.CustomButtons.Add(jqBtnFileDown);
        }

        //protected void SetAccept(Object sender, EventArgs e)
        //{
        //    DataTable dt = GetData();
        //    if (dt.Rows.Count > 0)
        //    {
        //        SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
        //        conn.Open();
        //        SqlTransaction trans = conn.BeginTransaction("set_accept_tr");
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand("", conn, trans);
        //            foreach (DataRow r in dt.Rows)
        //            {
        //                cmd.CommandText = "UPDATE [dbo].[" + Master.cur_basename + "_archive] SET [accept]=2 where id=" + r["id"].ToString();
        //                cmd.ExecuteNonQuery();
        //                faFunc.ToJournal(cmd, (Session["user_id"] ?? "0").ToString(), 2, (int)r["id"], Session[Master.cur_basename + "_id"].ToString(), 1, "[accept] -> 2\n", 0);
        //            }
        //            trans.Commit();
        //            conn.Close();
        //            ClearGrid(null, null);
        //            result.Text = "Атрибут успешно установлен";
        //            tbIDS.Text = "";
        //        }
        //        catch (Exception ex)
        //        {
        //            result.Text =  faFunc.GetExceptionMessage(ex);
        //            try
        //            {
        //                trans.Rollback();
        //                conn.Close();
        //            }
        //            catch (Exception ex2)
        //            {
        //                result.Text = ex2.GetType() + ":" + ex2.Message;
        //            }
        //        }
        //    }
        //}
    }
}