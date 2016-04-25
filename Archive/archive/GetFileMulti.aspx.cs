using Ionic.Zip;
using System;
using System.Data;
using System.IO;
using System.Net;

namespace WebArchiveR6
{
    public partial class GetFileMulti : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //string _files = (Request.QueryString["f"] ?? "").ToString().Trim();
            string cur = (Request.QueryString["cur"] ?? "").ToString();
            string _b = (Request.QueryString["b"] ?? "").ToString().Trim();
            string _down = (Request.QueryString["down"] ?? "").ToString().Trim();
            string _f_main = "";
            string _f_alt = "";
            string _f_sp = "";
            string _f_complect = "";
            //
            if (_down.Length > 0)
            {
                //application/zip, application/octet-stream
                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(_down);
                Response.ContentType = "application/zip";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.AddHeader("content-disposition", "attachment; filename=files_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".zip");
                Response.BinaryWrite(buffer);
                Response.End();
            }
            else if (Session[cur] != null)
            {
                DataTable dt = (Session[cur] as DataTable);
                //string[] ArrFiles = _files.Split('|');
                string zipname = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "_" + Context.Session["user_login"].ToString();
                using (ZipFile zip = new ZipFile(System.Text.Encoding.GetEncoding("cp866")))
                {
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        _f_main = Path.Combine(Properties.Settings.Default.filepath, _b, "archive", row["file"].ToString());
                        _f_alt = Path.Combine(Properties.Settings.Default.filepathalt, _b, "archive", row["file"].ToString());
                        _f_complect = Path.Combine(Properties.Settings.Default.filepath, "complectfiles", row["file"].ToString());
                        _f_sp = Path.Combine(Properties.Settings.Default.filepath, "temp_sp", row["file"].ToString());

                        if (!File.Exists(_f_main))
                            if (File.Exists(_f_alt))
                                _f_main = _f_alt;
                            else if (File.Exists(_f_sp))
                                _f_main = _f_sp;
                            else if (File.Exists(_f_complect))
                                _f_main = _f_complect;
                            else
                                _f_main = "";

                        if (_f_main != "")
                        {
                            zip.AddFile(_f_main, zipname);
                            i++;
                        }
                        else Response.Write("ID " + row["id"].ToString() + " - " + (row["file"].ToString() == "ad" ? "Доступ к файлу запрещен " : " Файл не найден. (" + row["file"].ToString() + ")") + " <br/>");
                    }

                    zipname = Path.Combine(Properties.Settings.Default.filepath, "tempfiles", zipname) + ".zip";
                    zip.Save(zipname);
                    Response.Write("Архив файлов успешно сформирован<br/>");
                    Response.Write("В архив добавлено файлов: " + i + " из " + dt.Rows.Count + "<br/><br/>");
                    Response.Write("<a href='/Archive/GetFileMulti.aspx?down=" + zipname + "'>Скачать</a>");
                }
            }
        }
    }
}