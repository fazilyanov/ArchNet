using System;
using System.IO;
using System.Net;

namespace WebArchiveR6
{
    public partial class GetFile : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            #region Если указана прямая ссылка на файл

            string _down = (Request.QueryString["down"] ?? "").ToString().Trim();
            if (_down.Length > 0)
            {
                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(_down);
                switch (Path.GetExtension(_down).ToUpper())
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

                    case ".XLS":
                        Response.ContentType = "application/vnd.ms-excel";
                        break;

                    case ".XLSX":
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        break;

                    case ".ZIP":
                        Response.ContentType = "application/zip";
                        break;

                    case ".CSV":
                        Response.ContentType = "text/csv";
                        break;
                }
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.AddHeader("content-disposition", "attachment; filename=" + Path.GetFileName(_down));
                Response.BinaryWrite(buffer);
                Response.End();
                return;
            }

            #endregion Если указана прямая ссылка на файл

            string _f = (Request.QueryString["f"] ?? "").ToString().Trim();

            if (_f == "ad")
            {
                Response.Write("<span style='color:red;'>Доступ к файлу документа запрещен</span>");
                return;
            }

            string _f_main = "";
            string _f_alt = "";
            string _f_sp = "";
            string _f_complect = "";
            string _f_ntd = ""; string _f_mon = "";
            string _id = (Request.QueryString["id"] ?? "").ToString().Trim();
            string _k = (Request.QueryString["k"] ?? "").ToString().Trim();
            string _b = (Request.QueryString["b"] ?? "").ToString().Trim();
            //string _b_alt = "";

            if (faFunc.GetMD5Hash(_id) == _k)
            {
                /*
                 * switch (_b) {
                     case "asm": _b_alt = "ASM";
                         break;

                     case "region": _b_alt = "OOO_AG";
                         break;

                     case "sib": _b_alt = "OOO_APS_east";
                         break;

                     case "ngm": _b_alt = "OOO_APS_NGM";
                         break;

                     case "north": _b_alt = "OOO_APS_north";
                         break;

                     case "west": _b_alt = "OOO_APS_WEST";
                         break;

                     case "pvs": _b_alt = "OOO_APVS";
                         break;

                     case "energo": _b_alt = "OOO_ASM";
                         break;

                     case "autotrans": _b_alt = "OOO_STG_Autotrans";
                         break;

                     case "complect": _b_alt = "stg_complectation";
                         break;

                     case "logistic": _b_alt = "stg_logistic";
                         break;

                     case "zao_stg": _b_alt = "ZAO_STG";
                         break;

                     case "south": _b_alt = "zao_aps_south";
                         break;
                 }
                 */

                _f_main = Path.Combine(Properties.Settings.Default.filepath, _b, "archive", _f);
                _f_alt = Path.Combine(Properties.Settings.Default.filepathalt, _b, "archive", _f);
                _f_complect = Path.Combine(Properties.Settings.Default.filepath, "complectfiles", _f);
                _f_sp = Path.Combine(Properties.Settings.Default.filepath, "temp_sp", _f);
                _f_ntd = Path.Combine(Properties.Settings.Default.filepath, "ntd", _f);
                _f_mon = Path.Combine(Properties.Settings.Default.filepath, "monitor", _f);

                if (!File.Exists(_f_main))
                    if (File.Exists(_f_alt))
                        _f_main = _f_alt;
                    else if (File.Exists(_f_sp))
                        _f_main = _f_sp;
                    else if (File.Exists(_f_complect))
                        _f_main = _f_complect;
                    else if (File.Exists(_f_ntd))
                        _f_main = _f_ntd;
                    else if (File.Exists(_f_mon))
                        _f_main = _f_mon;
                    else
                        _f_main = "";

                if (_f_main != "")
                {
                    Session["last_open_file"] = _f_main;
                    WebClient client = new WebClient();
                    Byte[] buffer = client.DownloadData(_f_main);

                    switch (Path.GetExtension(_f_main).ToUpper())
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

                        case ".XLS":
                            Response.ContentType = "application/vnd.ms-excel";
                            break;

                        case ".XLSX":
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            break;
                    }
                    //
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                    Response.End();
                }
                else Response.Write("Файл не найден");
            }
            else
                //Response.Redirect(GetRouteUrl("error", new { p_base = Master.cur_basename, p_error = "filenotfound" }));
                Response.Write("Переданы неверные параметры");
        }
    }
}