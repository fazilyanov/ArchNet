using System;
using System.IO;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebArchiveR6
{
    public partial class SendMailComplect : System.Web.UI.Page
    {
        // public string _path = "", buf = "", cur = "", id = "", sc = "", sd = "", _ret = "";

        protected override void OnInit(EventArgs e)
        {
            string gr = (Request.QueryString["gr"] ?? "").ToString();
            string id = (Request.QueryString["id"] ?? "").ToString();
            string ru = (Request.QueryString["ru"] ?? "").ToString();
            //string id_creator = (Request.QueryString["id_creator"] ?? "").ToString();
            //string id_perf = (Request.QueryString["id_perf"] ?? "").ToString();

            Response.Clear();

            if (gr.Length > 0 && id.Length > 0  && ru.Length > 0)
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                string q1 = @"SELECT a.id,a.name,a.date_reg,a.id_perf,c.name as perf, a.id_creator, b.name as creator FROM [dbo].[_complectnew] as a "+
                    "left join [dbo].[_user] b ON b.id=a.id_creator and b.del=0 "+
                    "left join [dbo].[_user] c ON c.id=a.id_perf and c.del=0 "+
                    "where a.id=@id and a.del=0";
                string q2 = @"SELECT count(*) FROM [dbo].[_complectnew_list] as a where id_complectnew=@id and del=0";
                DataTable dt1 = new DataTable();
                int list_count = 0;
                try
                {
                    SqlCommand cmd1 = new SqlCommand(q1, conn);
                    cmd1.Parameters.AddWithValue("@id", id);
                    SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(cmd1);
                    sqlDataAdapter1.Fill(dt1);
                    //
                    SqlCommand cmd2 = new SqlCommand(q2, conn);
                    cmd2.Parameters.AddWithValue("@id", id);
                    list_count = (int)cmd2.ExecuteScalar();
                    //
                    conn.Close();
                }
                catch(Exception ex)
                {
                    Response.Write("Уведомление не отправлено: Ошибка:" + ex.Message);
                    conn.Close();
                    return;
                }

                if (dt1.Rows.Count > 0)
                {

                    string subject = "", msg = "", res = "";
                    DataRow row = dt1.Rows[0];
                    string ins_query = "";
                    string jor_change = "";
                    switch (gr)
                    {
                        case "pm":
                            subject = "Создан комплект " + row["name"].ToString();
                            msg =
                                "Документы, переданные в ООиХУД отсканированы и занесены в комплект:<a href=\"" + ru + "\"> " + row["name"].ToString() + "</a>.<br/><br/>" +
                                "Количество документов в комплекте: <b>" + list_count + "</b><br/><br/>" +
                                "Сотрудник ООиХУД: <b>" + row["creator"].ToString() + "</b><br/><br/>" +
                                "Дата создания: <b>" + row["date_reg"].ToString() + "</b><br/>";

                            if (faFunc.SendMailUser(row["id_perf"].ToString(), subject, msg, out res))
                            {
                                Response.Write("Сообщение пользователю " + res + " отправлено.");
                                jor_change = "[perf_mail] -> 1\n";
                                ins_query= @"UPDATE [dbo].[_complectnew] SET [perf_mail]=1 where id=@id";
                            }
                            else
                                Response.Write(res);
                            break;

                        case "bm":
                            subject = "Создан комплект " + row["name"].ToString();
                            msg =
                                "Получены новые документы, создан комплект:<a href=\"" + ru + "\"> " + row["name"].ToString() + "</a>.<br/><br/>" +
                                "Создал комплект:<b> " + row["creator"].ToString() + "</b><br/><br/>" +
                                "Количество документов в комплекте: <b>" + list_count + "</b><br/><br/>" +
                                "Дата создания: <b>" + row["date_reg"].ToString() + "</b><br/><br/>" +
                                "Исполнитель:<b> " + row["perf"].ToString() + "</b>";

                            if (faFunc.SendMailGroup("2", subject, msg, out res))
                            {
                                Response.Write("Сообщение пользователям: \r\n\r\n" + res + "\r\nотправлено.");
                                jor_change = "[buh_mail] -> 1\n";
                                ins_query = @"UPDATE [dbo].[_complectnew] SET [buh_mail]=1 where id=@id";
                            }
                            else
                                Response.Write(res);
                            break;

                        case "om":
                            subject = "Создан комплект " + row["name"].ToString();
                            msg =
                                "Получены новые документы, создан комплект:<a href=\"" + ru + "\"> " + row["name"].ToString() + "</a>.<br/><br/>" +
                                "Уведомление отправил: <b>" + HttpContext.Current.Session["user_name"].ToString() + "</b><br/><br/>" +
                                "Исполнитель:<b> " + row["perf"].ToString() + "</b>";
                            if (faFunc.SendMailGroup("3", subject, msg, out res))
                            {
                                Response.Write("Сообщение пользователям: \r\n\r\n" + res + "\r\nотправлено.");
                                jor_change = "[oc_mail] -> 1\n";
                                ins_query = @"UPDATE [dbo].[_complectnew] SET [oc_mail]=1 where id=@id";
                            }
                            else
                                Response.Write(res);
                            break;

                        case "tm":
                            subject = "Проверка документов " + row["name"].ToString();
                            msg =
                                "Завершена проверка документов из комплекта: <a href=\"" + ru + "\"> " + row["name"].ToString() + "</a>.<br/><br/>" +
                                "Результат проверки отражен в столбце \"Статус\".<br/><br/>" +
                                "Неполные - документы не прошли проверку на соответствие требованиям бухгалтерии, их необходимо вернуть исполнителю.<br/><br/>" +
                                "Все остальные документы подготовить для передачи в бухгалтерию.<br/><br/>" +
                                "Уведомление отправил: <b>" + HttpContext.Current.Session["user_name"].ToString() + "</b>";

                            if (faFunc.SendMailUser(row["id_creator"].ToString(), subject, msg, out res))
                            {
                                Response.Write("Сообщение пользователю " + res + " отправлено.");
                                jor_change = "[term_mail] -> 1\n";
                                ins_query = @"UPDATE [dbo].[_complectnew] SET [term_mail]=1 where id=@id";
                            }
                            else
                                Response.Write(res);
                            break;
                    }
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



                /*

                            }
                                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                                conn.Open();
                                string q = String.Format(@"SELECT a.[id_user],a.[name], b.mail, FROM [dbo].[_mailgroup_user] as a left join [dbo].[_user] b on a.id_user=b.id and b.del=0 where a.id_mailgroup=@id_mailgroup");
                            try
                            {






                                string[] stringSeparators = new string[] { "\r\n" };
                                string[] lines = buf.Split(stringSeparators, StringSplitOptions.None);
                                DataTable dt = (Session[cur] as DataTable);

                                DataView dv = dt.DefaultView;
                                dv.Sort = sc + " " + sd;
                                DataTable dt_s = dv.ToTable();

                                try {
                                    //DataRow row = dt.Rows.Find(e.RowKey);

                                    int i = 0;
                                    bool paste = false;
                                    string bc = "";
                                    int vi = 0;
                                    foreach (DataRow r in dt_s.Rows) {
                                        if (r["id"].ToString() == id || paste) {
                                            bc = lines[i++].Trim();
                                            if (bc.Length > 0 ) {
                                                if (Int32.TryParse(bc, out vi)) {
                                                    if (vi > 1000000000 && vi < Int32.MaxValue) {
                                                        r["barcode"] = vi;
                                                        r["status"] = r["status"].ToString() != "1" ? "2" : r["status"];
                                                    }
                                                    else _ret = "Значение «" + bc + "» не является штрихкодом (Строка " + i + "). Вставка значений отменена ";
                                                }
                                                else _ret = "Значение «" + bc + "» не является числом. (Строка " + i + "). Вставка значений отменена";
                                            }
                                            paste = true;
                                        }
                                        if (i >= lines.Length || _ret.Length > 0) break;
                                    }
                                    if (_ret.Length == 0) {
                                        Session[cur] = dt_s;
                                    }
                                    else
                                        Response.Write(_ret);
                                }
                                catch (Exception ex) {
                                    Response.StatusCode = 400;
                                    Response.Write(ex.Message);
                                }

                            }
                            else
                                Response.Write("В буфере обмена нет данных.");



                */
                Response.End();
            }

        }
    }
}