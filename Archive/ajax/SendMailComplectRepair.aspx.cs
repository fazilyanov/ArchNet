using System;
using System.IO;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebArchiveR6
{
    public partial class SendMailComplectRepair : System.Web.UI.Page
    {
        // public string _path = "", buf = "", cur = "", id = "", sc = "", sd = "", _ret = "";

        protected override void OnInit(EventArgs e)
        {
            string gr = (Request.QueryString["gr"] ?? "").ToString();
            string id = (Request.QueryString["id"] ?? "").ToString();
            string ru = (Request.QueryString["ru"] ?? "").ToString();


            Response.Clear();

            if (gr.Length > 0 && id.Length > 0 && ru.Length > 0)
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                string q1 = @"SELECT a.id,a.name,a.date_reg,a.id_perf,c.name as perf, a.id_creator, b.name as creator FROM [dbo].[_complect_repair] as a " +
                    "left join [dbo].[_user] b ON b.id=a.id_creator and b.del=0 " +
                    "left join [dbo].[_user] c ON c.id=a.id_perf and c.del=0 " +
                    "where a.id=@id and a.del=0";
                //string q2 = @"SELECT count(*) FROM [dbo].[" + cb + "_complectnew_list] as a where id_complectnew=@id and del=0";
                DataTable dt1 = new DataTable();
                //int list_count = 0;
                try
                {
                    SqlCommand cmd1 = new SqlCommand(q1, conn);
                    cmd1.Parameters.AddWithValue("@id", id);
                    SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(cmd1);
                    sqlDataAdapter1.Fill(dt1);
                    //
                    //SqlCommand cmd2 = new SqlCommand(q2, conn);
                    //cmd2.Parameters.AddWithValue("@id", id);
                    //list_count = (int)cmd2.ExecuteScalar();
                    //
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Уведомление не отправлено: Ошибка:" + faFunc.GetExceptionMessage(ex));
                    conn.Close();
                    return;
                }

                if (dt1.Rows.Count > 0)
                {

                    string subject = "", msg = "", res = "";
                    DataRow row = dt1.Rows[0];
                    string ins_query = "";
                    string jor_change = "";

                    subject = "Создан комплект движения документов " + row["name"].ToString();
                    msg =

                        "Составлен комплект документов  движения документов <a href=\"" + ru + "\">" + row["name"].ToString() + "</a>.<br/><br/>" +
                        "По вопросу предоставления документов необходимо связаться с сотрудником ООиХУД <b>" + (Session["user_name"]??"").ToString()  + "</b><br/>" +
                        "Дата создания комплекта: <b>" + row["date_reg"].ToString() + "</b>";
                    //+ row["creator"].ToString()

                    if (faFunc.SendMailUser(row["id_perf"].ToString(), subject, msg, out res))
                    {
                        Response.Write("Сообщение пользователю " + res + " отправлено.");
                        jor_change = "[perf_mail] -> 1\n";
                        ins_query = @"UPDATE [dbo].[_complect_repair] SET [perf_mail]=1 where id=@id";
                    }
                    else
                        Response.Write(res);

                    if (ins_query.Length > 0)
                    {
                        conn.Open();
                        SqlTransaction trans = conn.BeginTransaction("set_mail_send");
                        try
                        {

                            SqlCommand cmd1 = new SqlCommand(ins_query, conn, trans);
                            cmd1.Parameters.AddWithValue("@id", id);
                            cmd1.ExecuteNonQuery();
                            faFunc.ToJournal(cmd1, (Session["user_id"] ?? "0").ToString(), 2, int.Parse(id), "0", 26, jor_change,0);
                            trans.Commit();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            Response.Clear();
                            Response.Write(faFunc.GetExceptionMessage(ex));
                            try
                            {
                                trans.Rollback();
                                conn.Close();
                            }
                            catch (Exception ex2)
                            {
                                Response.Write(ex2.GetType() + ":" + ex2.Message);
                            }

                        }
                    }
                }
                Response.End();
            }

        }
    }
}