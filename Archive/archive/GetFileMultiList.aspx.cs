using System;
using System.IO;
using System.Net;
using Ionic.Zip;

namespace WebArchiveR6
{
    public partial class GetFileMultiList : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // (Request.QueryString["f"] ?? "").ToString().Trim();
            string _b = (Request.QueryString["b"] ?? "").ToString().Trim();
            string _down = (Request.QueryString["down"] ?? "").ToString().Trim();
            string _f_main = "";
            string _f_alt = "";
            string _f_sp = "";
            string _f_complect = "";
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
            else if (Session["filefordown"] == null)
            {
                using (var reader = new StreamReader(Request.InputStream))
                {
                    Session["filefordown"] = reader.ReadToEnd();
                }
            }
            else
            {
                Response.Clear();
                string _files = Session["filefordown"].ToString();

                if (_files.Length > 0)
                {
                    string[] ArrFiles = _files.Split('|');
                    faFunc.ToLog(4, "Записей: " + ArrFiles.Length);

                    string zipname = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "_" + Context.Session["user_login"].ToString();
                    using (ZipFile zip = new ZipFile(System.Text.Encoding.GetEncoding("cp866")))
                    {
                        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
                        int i = 0;

                        foreach (string _t in ArrFiles)
                        {
                            string[] _f = _t.Split('=');
                            _f_main = Path.Combine(Properties.Settings.Default.filepath, _b, "archive", _f[1]);
                            _f_alt = Path.Combine(Properties.Settings.Default.filepathalt, _b, "archive", _f[1]);
                            _f_complect = Path.Combine(Properties.Settings.Default.filepath, _b, "complect", _f[1]);
                            _f_sp = Path.Combine(Properties.Settings.Default.filepath, "temp_sp", _f[1]);

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
                            else Response.Write("ID " + _f[0] + " - " + (_f[1] == "ad" ? "Доступ к файлу запрещен " : " Файл не найден (" + _f[1] + ")") + " <br/>");
                        }
                       
                        zipname = Path.Combine(Properties.Settings.Default.filepath, "tempfiles", zipname) + ".zip";
                        zip.Save(zipname);
                        Response.Write("Архив файлов успешно сформирован<br/>");
                        Response.Write("В архив добавлено файлов: " + i + " из " + ArrFiles.Length + "<br/><br/>");
                        //Response.Redirect("/Archive/GetFileMulti.aspx?down=" + zipname);
                        Response.Write("<a href='/Archive/GetFileMultiList.aspx?down=" + zipname + "'>Скачать</a>");
                        Session["filefordown"] = null;
                        Response.End();
                        
                    }
                }
            }
        }
    }
}