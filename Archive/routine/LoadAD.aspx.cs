using System;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;

namespace WebArchiveR6
{
    public partial class loadAD : System.Web.UI.Page
    {
        public string sReport = "";

        //protected void ClearTable()
        //{
        //    SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand("TRUNCATE TABLE [dbo].[_ad]",conn);
        //    cmd.ExecuteNonQuery();
        //    conn.Close();
        //}

        protected void CheckUser(string p_login, string p_name, string p_mail)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[_user] WHERE login=@p_login ", conn);
            cmd.Parameters.AddWithValue("@p_login", p_login);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                // update?
                if ((dt.Rows[0]["name"].ToString() != p_name) || (dt.Rows[0]["mail"].ToString() != p_mail))
                {
                    UpdateUser(p_login, p_name, p_mail);
                    sReport += p_login + " ( Обновлен ) " + dt.Rows[0]["name"].ToString() + "(" + dt.Rows[0]["mail"].ToString() + ") -> " + p_name + "(" + p_mail + ")\n";
                    faFunc.UpdateUserSName(p_login, p_name);
                }
            }
            else
            {
                // add
                InsertUser(p_login, p_name, p_mail);
                sReport += p_login + " ( Добавлен ) " + p_name + "(" + p_mail + ")\n";
                faFunc.UpdateUserSName(p_login, p_name);
            }
        }

        protected void UpdateUser(string p_login, string p_name, string p_mail)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();
            string _sql = "UPDATE [dbo].[_user] SET [name] = @p_name, [mail] =@p_mail WHERE [login] = @p_login";
            SqlCommand cmd = new SqlCommand(_sql, conn);
            cmd.Parameters.AddWithValue("@p_login", p_login);
            cmd.Parameters.AddWithValue("@p_name", p_name);
            cmd.Parameters.AddWithValue("@p_mail", p_mail);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected int InsertUser(string p_login, string p_name, string p_mail)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();

            string _sql =
                "IF NOT EXISTS(SELECT * FROM [dbo].[_user] WHERE login=@p_login) " +
                "BEGIN " +
                "   INSERT INTO [dbo].[_user] ([login],[name],[mail]) VALUES (@p_login,@p_name,@p_mail) " +
                "END ";

            SqlCommand cmd = new SqlCommand(_sql, conn);
            cmd.Parameters.AddWithValue("@p_login", p_login);
            cmd.Parameters.AddWithValue("@p_name", p_name);
            cmd.Parameters.AddWithValue("@p_mail", p_mail);

            int _ret = cmd.ExecuteNonQuery();
            conn.Close();
            return _ret == -1 ? 0 : _ret; ;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Response.Clear();
            if (Session["common_access_admin_ad"] != null)
                Response.Write(LoadUsersFromAD("LDAP://zao"));
            else
                Response.Write("Доступа нет!");
            Response.End();
        }

        protected string LoadUsersFromAD(string ldap)
        {
            DirectoryEntry dom;
            DirectorySearcher ds;
            SearchResultCollection src;
            string _login;
            string _name;
            string _company;
            string _mail;

            int i = 0;
            try
            {
                dom = new DirectoryEntry(ldap);
            }
            catch (Exception ex)
            {
                Response.Write("Err002:" + ex.Message);
                return "";
            }
            try
            {
                ds = new DirectorySearcher(dom,
                    "(&(objectClass=user)(!(objectClass=computer)))",
                    new string[] { "samaccountname", "displayname" },
                    SearchScope.Subtree
                    );
            }
            catch (Exception ex)
            {
                Response.Write("Err003:" + ex.Message);
                return "";
            }
            try
            {
                ds.PageSize = 100;
                src = ds.FindAll();
            }
            catch (Exception ex)
            {
                Response.Write("Err004:" + ex.Message);
                return "";
            }

            foreach (SearchResult sr in src)
            {
                DirectoryEntry objDirEnt = sr.GetDirectoryEntry();

                try
                {
                    i++;
                    _login = _name = _company = _mail = "";
                    try
                    {
                        string Key = "displayName";
                        foreach (Object objValue in objDirEnt.Properties[Key])
                        {
                            _name = objValue.ToString().Trim();
                        }
                        Key = "sAMAccountName";

                        foreach (Object objValue in objDirEnt.Properties[Key])
                        {
                            _login = objValue.ToString().Trim();
                        }

                        Key = "mail";
                        foreach (Object objValue in objDirEnt.Properties[Key])
                        {
                            _mail = objValue.ToString().Trim();
                        }
                        if (_login == "a.baryshkova")
                        {
                            _login.Trim();
                        }
                        if (_mail.Trim() == "")
                        {
                            Key = "userPrincipalName";
                            foreach (Object objValue in objDirEnt.Properties[Key])
                            {
                                _mail = objValue.ToString().Trim();
                            }
                        }

                        _login = (_login.Length > 100 ? _login.Remove(100) : _login);
                        _name = (_name.Length > 250 ? _name.Remove(250) : _name);
                        _mail = (_mail.Length > 100 ? _mail.Remove(100) : _mail);

                        CheckUser(_login, _name, _mail);
                    }
                    catch (Exception Ex)
                    {
                        Response.Write("Err005:" + Ex.Message);
                        return "";
                    }
                }
                catch (Exception Ex)
                {
                    Response.Write("Err006:" + Ex.Message);
                    return "";
                }
            }
            return sReport == "" ? "Нет изменений" :  sReport;
        }
    }
}