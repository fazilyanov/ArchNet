using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.IO;

namespace WebArchiveR6
{
    public partial class GetFile1C : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string cb = Request.QueryString["base"];
            string key = Request.QueryString["key"];

            if (!String.IsNullOrEmpty(id) && !String.IsNullOrEmpty(cb) && !String.IsNullOrEmpty(key) && (faFunc.GetMD5Hash(id) == key))
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[GetFilePathById]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_base", cb));
                cmd.Parameters.Add(new SqlParameter("@p_hiden", 1));
                cmd.Parameters.Add(new SqlParameter("@p_id_archive", id));
                string _filepath = cmd.ExecuteScalar().ToString().Trim();
                if (_filepath.Length > 0)
                {
                    _filepath = "\\\\stg.lan\\nfkdata\\ArchiveScanFiles\\" + cb + "\\archive\\" + _filepath;
                    WebClient client = new WebClient();
                    Byte[] buffer = client.DownloadData(_filepath);

                    switch (Path.GetExtension(_filepath).ToUpper())
                    {
                        case ".PDF":
                            Response.ContentType = "application/pdf";
                            break;
                        case ".JPG":
                        case ".JPEG":
                            Response.ContentType = "image/jpeg";
                            break;
                        case ".BMP":
                            Response.ContentType = "image/bmp";
                            break;
                    }
                    //
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                }
                else
                    Response.Write("Файл не найден.");

            }
            else
            {
                Response.Clear();
                Response.Write("Передан неверный параметр");
            }
        }
    }
}