using System;
using System.IO;
using System.Web;

namespace WebArchiveR6
{
    public partial class UploadFile : System.Web.UI.Page
    {
        public string _path = "";
        public string state = "";
        protected override void OnInit(EventArgs e)
        {
            state = (Request.QueryString["state"] ?? "default").ToString();
            if (Request.Files.Count > 0)
            {
                Response.Clear();
                try
                {
                    HttpPostedFile file = Request.Files[0];
                    string ext = Path.GetExtension(file.FileName);
                    ext = ext.ToLower() == ".jpeg" ? ".jpg" : ext;
                    var _date = DateTime.Now;
                    _path = Properties.Settings.Default.filepath + @"tempfiles\";
                    if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
                    _path = Path.Combine(_path, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "_" + Context.Session["user_login"].ToString() + ext);
                    if (File.Exists(_path)) File.Delete(_path);
                    file.SaveAs(_path);
                    Response.Write(_path);
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