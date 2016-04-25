using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WebArchiveR6
{
    public partial class UpdateUser1C : System.Web.UI.Page
    {
        protected string CompareTables()
        {
            string sReport = "Таблица [dbo].[_user_1c] обновлена.";
            SqlConnection conn1 = new SqlConnection(Properties.Settings.Default.constr);
            SqlConnection conn2 = new SqlConnection(Properties.Settings.Default.constr1c);
            try
            {
                conn2.Open();
                SqlCommand cmd2 = new SqlCommand("", conn2);
                cmd2.CommandTimeout = 270;
                cmd2.CommandText =
                    " SELECT a.[ID_1C], a.[fio], a.[department_ID], a.[state], a.[ID_1C_NP], b.[organization_id] " +
                    " FROM [dbo].[users] a " +
                    " Left Join [dbo].[departments] b on a.department_ID=b.id " +
                    " WHERE (a.enabled = 1) ";//AND (NOT (a.AD_name IS NULL)) AND (NOT (a.state = 'Увольнение'))
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                conn2.Close();

                conn1.Open();
                SqlCommand cmd = new SqlCommand("", conn1);
                cmd.CommandTimeout = 270;

                cmd.CommandText = "TRUNCATE TABLE [dbo].[_user_1c];";
                foreach (DataRow r2 in dt2.Rows)
                {
                    string name = r2["fio"].ToString().Replace("оглы", "").Replace("Оглы", "").Trim();
                    name = Regex.Replace(name, @"\s{2,}", " ", RegexOptions.IgnoreCase);
                    string[] fio = name.Split(' ');
                    string sname = "";
                    if (fio.Length == 3 && fio[0].Trim().Length > 0 && fio[1].Trim().Length > 0 && fio[2].Trim().Length > 0)
                        sname = fio[0] + " " + fio[1][0] + "." + fio[2][0] + ".";
                    else
                        sname = name;
                    cmd.CommandText +=
                        "INSERT INTO[dbo].[_user_1c] ([ID_1C], [fio_full], [fio], [department_ID], [ID_1C_NP], [organization_id], [state]) " +
                        "VALUES('" + r2["ID_1C"].ToString() + "','" + r2["fio"].ToString() + "','" + sname + "','" + r2["department_ID"].ToString() + "','" + r2["ID_1C_NP"].ToString() + "','" + r2["organization_id"].ToString() + "','" + r2["state"].ToString() + "')";
                }
                cmd.ExecuteNonQuery();
                cmd.CommandTimeout = 270;
                conn1.Close();
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
            Response.Clear();
            Response.Write(CompareTables());
            Response.End();
        }
    }
}