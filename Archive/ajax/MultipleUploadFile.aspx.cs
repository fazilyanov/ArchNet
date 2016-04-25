using System;
using System.IO;
using System.Web;
using System.Data;

namespace WebArchiveR6
{
    public partial class MultipleUploadFile : System.Web.UI.Page
    {
        public string _path = "", cur = "";
        protected override void OnInit(EventArgs e)
        {
            string _ret = "";
            int _ok = 0;
            cur = (Request.QueryString["cur"] ?? "").ToString();
            if (Request.Files.Count > 0 && cur != "")
            {
                DataTable dt = (Session[cur] as DataTable);
                Response.Clear();
                try
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFile file = Request.Files[i];
                        string ext = Path.GetExtension(file.FileName);
                        string file_orig = Path.GetFileName(file.FileName);
                        ext = ext.ToLower() == ".jpeg" ? ".jpg" : ext;
                        var _date = DateTime.Now;
                        _path = Properties.Settings.Default.filepath + @"tempfiles\";
                        if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
                        _path = Path.Combine(_path, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss_" + i) + "_" + Context.Session["user_login"].ToString() + ext);
                        //if (File.Exists(_path)) File.Delete(_path);
                        file.SaveAs(_path);
                        if (File.Exists(_path))
                        {
                            _ok++;
                            DataRow newrow = dt.NewRow();
                            int min_id = 0;
                            foreach (DataRow r in dt.Rows)
                                min_id = Math.Min((int)r["id"], min_id);
                            min_id--;
                            newrow["id"] = min_id;
                            newrow["status"] = 1;
                            newrow["file"] = _path;
                            newrow["file_orig"] = file_orig;
                            newrow["id_archive"] = 0;
                            newrow["barcode"] = 0;
                            newrow["file_archive"] = "";
                            //newrow["id_quality"] = 1;
                            //newrow["id_quality_name_text"] = "Соответствует";
                            newrow["file_archive"] = "";

                            dt.Rows.Add(newrow);
                        }
                        else
                        {
                            _ret += "Файл: '" + file.FileName + "' не загружен!\r\n ";
                        }
                    }
                    Session[cur] = dt;
                    Response.Write("Загружено файлов: " + _ok + " из " + Request.Files.Count);
                }
                catch (Exception ex)
                {
                    Response.StatusCode = 400;
                    Response.Write(faFunc.GetExceptionMessage(ex));
                }
                Response.End();
            }
        }
    }
}