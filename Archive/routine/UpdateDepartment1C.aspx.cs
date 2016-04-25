using System;
using System.Data;
using System.Data.SqlClient;

namespace WebArchiveR6
{
    public partial class UpdateDepartment1C : System.Web.UI.Page
    {
        protected string CompareTables()
        {
            string sReport = "";
            SqlConnection conn1 = new SqlConnection(Properties.Settings.Default.constr);
            SqlConnection conn2 = new SqlConnection(Properties.Settings.Default.constr1c);
            try
            {
                conn1.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT a.*, b.full_name as parent_name FROM [dbo].[_department_1c] a Left Join [dbo].[_department_1c] b on a.parent_ID=b.id", conn1);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                conn1.Close();
                cmd1.Dispose();
                sda1.Dispose();

                conn2.Open();
                SqlCommand cmd2 = new SqlCommand("SELECT a.*, b.full_name as parent_name FROM [dbo].[departments] a Left Join [dbo].[departments] b on a.parent_ID=b.id", conn2);
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                SqlCommand cmd3 = new SqlCommand("SELECT * FROM [dbo].[organizations]", conn2);
                SqlDataAdapter sda3 = new SqlDataAdapter(cmd3);
                DataTable dt3 = new DataTable();
                sda3.Fill(dt3);
                conn2.Close();

                if (dt1.Rows.Count > 0 && dt2.Rows.Count > 0)
                {
                    DataView view = new DataView(dt1);
                    string sQuery = "";
                    foreach (DataRow r2 in dt2.Rows)
                    {
                        view.RowFilter = "ID='" + r2["ID"].ToString() + "'";
                        if (view.Count > 0)
                        {
                            if (view[0]["full_name"].ToString() != r2["full_name"].ToString())
                            {
                                sReport += "Cмена наименования: <br/>&nbsp;&nbsp;&nbsp;ID=" + r2["ID"].ToString() + " [" + view[0]["full_name"].ToString() + "] на [" + r2["full_name"].ToString() + "]<br/>";
                                sQuery += "UPDATE [dbo].[_department_1c] SET [full_name] = '" + r2["full_name"].ToString() + "' WHERE ID='" + r2["ID"].ToString() + "';";
                            }
                            if (view[0]["parent_ID"].ToString() != r2["parent_ID"].ToString())
                            {
                                sReport += "Cмена родителя:<br/>&nbsp;&nbsp;&nbsp; ID=" + r2["ID"].ToString() + " Имя= " + view[0]["full_name"].ToString() + " [" + view[0]["parent_name"].ToString() + "] на [" + r2["parent_name"].ToString() + "]<br/>";
                                sQuery += "UPDATE [dbo].[_department_1c] SET [parent_ID] = '" + r2["parent_ID"].ToString() + "' WHERE ID='" + r2["ID"].ToString() + "';";
                            }
                        }
                        else
                        {
                            sReport += "Новая запись: <br/>&nbsp;&nbsp;&nbsp;ID = " + r2["ID"].ToString() + " [" + r2["full_name"].ToString() + "] <br/> ";
                            sQuery +=
                                "INSERT INTO [dbo].[_department_1c]([ID],[organization_id],[full_name],[parent_ID]) " +
                                "VALUES('" + r2["ID"].ToString() + "','" + r2["organization_id"].ToString() + "','" + r2["full_name"].ToString() + "','" + r2["parent_ID"].ToString() + "');";
                        }
                    }
                    if (sQuery != "")
                    {
                        conn1.Open();
                        SqlCommand cmd = new SqlCommand(sQuery, conn1);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "TRUNCATE TABLE [dbo].[_organization_1c];";
                        foreach (DataRow r3 in dt3.Rows)
                        {
                            cmd.CommandText +=
                                "INSERT INTO[dbo].[_organization_1c] ([ID] ,[org_name],[org_full_name],[priority]) " +
                                "VALUES('" + r3["ID"].ToString() + "','" + r3["org_name"].ToString() + "','" + r3["org_full_name"].ToString() + "'," + r3["priority"].ToString() + ");";
                        }
                        cmd.ExecuteNonQuery();

                        cmd.CommandTimeout = 600;
                        cmd.CommandText = "PreDepartmentTree1c";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        conn1.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                conn1.Close();
                conn2.Close();
                sReport = ex.Message;
            }
            return sReport;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            string res = CompareTables();
            Response.Clear();
            if (res != "")
            {
                System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
                mm.From = new System.Net.Mail.MailAddress(Properties.Settings.Default.ArchiveMail);
                mm.To.Add(new System.Net.Mail.MailAddress("a.fazilyanov@stg.ru"));
                mm.To.Add(new System.Net.Mail.MailAddress("r.talipova@stg.ru"));
                mm.To.Add(new System.Net.Mail.MailAddress("r.mahmutov@stg.ru"));
                mm.Subject = "Изменения в структуре предприятия";
                mm.IsBodyHtml = true;
                mm.Body = (DateTime.Now.ToString() + "<br/>" + res);
                faFunc.SendMail(mm);
                Response.Write("Таблица[dbo].[_department_1c] обновлена.");
            }
            else
                Response.Write("Нет изменений");
            Response.End();
        }
    }
}