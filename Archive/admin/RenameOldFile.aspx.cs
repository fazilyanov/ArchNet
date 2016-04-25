using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace WebArchiveR6
{
    public partial class RenameOldFile : System.Web.UI.Page
    {
        public string _path = "";
        public string state = "";
        protected override void OnInit(EventArgs e)
        {
            string _basename = (Request.QueryString["base"] ?? "").ToString();
            string _ot = (Request.QueryString["ot"] ?? "").ToString();
            string _do = (Request.QueryString["do"] ?? "").ToString();
            if (_basename != "" && _ot != "" && _do != "")
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                string q = @"SELECT a.id, a.id_archive, a.nn, a.[file], a.del From [dbo].[" + _basename + "_docversion] as a where del=0 and id>" + _ot + " and id<" + _do + " order by id";
                DataTable dt = new DataTable();
                try
                {
                    SqlCommand cmd = new SqlCommand(q, conn);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dt);
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(faFunc.GetExceptionMessage(ex));
                    conn.Close();
                    return;
                }

                string good = "", bad = "", ext = "", filename = "", altfilename = "", dest_filename = "", dest_altfilename = "";
                string _ret = "";
                foreach (DataRow row in dt.Rows)
                {
                    good = row["id_archive"] + "." + row["nn"];
                    bad = System.IO.Path.GetFileNameWithoutExtension(row["file"].ToString());
                    ext = System.IO.Path.GetExtension(row["file"].ToString()).ToLower();
                    if (good != bad)
                    {
                        Response.Write(bad + ext + " -> " + good + ext + "\r\n");

                        filename = Path.Combine(Properties.Settings.Default.filepath, _basename, "archive", row["file"].ToString());
                        altfilename = Path.Combine(Properties.Settings.Default.filepathalt, _basename, "archive", row["file"].ToString());
                         dest_filename = Path.Combine(Path.GetDirectoryName(filename), good + ext);
                         dest_altfilename = Path.Combine(Path.GetDirectoryName(altfilename), good + ext);
                        // Response.Write("---------------------------------------------------------------------------<br/>");

                        //if (!File.Exists(filename))
                        //    if (!File.Exists(altfilename))
                        //        _ret += row["id"] + ";" + row["id_archive"] + ";" + row["nn"] + ";" + row["file"] + "\r\n";

                        //if (File.Exists(altfilename))
                        //{
                        //    if (!File.Exists(dest_altfilename))
                        //    {
                        //        File.Move(altfilename, dest_altfilename);
                        //        Response.Write(altfilename + "<br/>");
                        //        Response.Write(dest_altfilename + "<br/>");
                        //    }
                        //    else
                        //    {
                        //        Response.Write(altfilename + "<br/>");
                        //        Response.Write("????????????????????????????????????????????????????????" + dest_altfilename + "<br/><br/>");
                        //    }
                        //}
                        //Response.Write("---------------------------------------------------------------------------<br/><br/>");



                        if (File.Exists(dest_altfilename) || File.Exists(dest_filename))
                        {
                            conn.Open();
                            SqlTransaction trans = conn.BeginTransaction("set_accept_tr");
                            try
                            {
                                SqlCommand cmd = new SqlCommand("", conn, trans);
                                cmd.CommandText = "UPDATE [dbo].[" + _basename + "_docversion] SET [file]='" + row["file"].ToString().Substring(0, row["file"].ToString().LastIndexOf(@"\") + 1) + good + ext + "' where id=" + row["id"].ToString();
                                cmd.ExecuteNonQuery();
                                trans.Commit();
                                conn.Close();
                            }
                            catch (Exception ex)
                            {
                                Response.Write("<br/><br/>" + faFunc.GetExceptionMessage(ex) + "<br/><br/>");
                                try
                                {
                                    trans.Rollback();
                                    conn.Close();
                                }
                                catch (Exception ex2)
                                {
                                    Response.Write("<br/><br/>" + ex2.Message + "<br/><br/>");
                                }
                            }
                        }
                    }
                }

                Response.Write(_ret + "\r\n" + "ОК");
                Response.End();
            }
        }
    }
}

