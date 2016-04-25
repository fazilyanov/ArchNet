using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArchNet.ErrorPages
{
    public partial class FatalError : System.Web.UI.Page
    {
        public string _message = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string _error = (Page.RouteData.Values["p_error"] != null) ? Page.RouteData.Values["p_error"].ToString() : "0";
            switch (_error)
            {
                case "usernotfound":
                    _message = "Пользователь с логином '" + Context.User.Identity.Name.Trim() + "' не найден. В доступе отказано.";
                    break;
                case "accessdenied":
                    _message = "Нет доступа к этому разделу";
                    break;
                case "fatal":
                    _message = Session["ErrorException"].ToString().Replace("\r\n","<br/>");
                    Session["ErrorException"] = null;
                    break;
                case "dumbparam":
                    _message = "Переданы неверные параметры";
                    break;
                case "filenotfound":
                    _message = "Файл не найден";
                    break;
                case "filenoaccess":
                    _message = "Нет доступа к файлу";
                    break;
            }

        }
    }
}