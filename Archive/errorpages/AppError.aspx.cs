using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ArchNet.ErrorPages
{
    public partial class AppError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["ErrorExceptionCount"] == null) Session["ErrorExceptionCount"] = 0;
        }
        private void SendMail()
        {
            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
            mm.From = new System.Net.Mail.MailAddress(Properties.Settings.Default.ArchiveMail);
            mm.To.Add(new System.Net.Mail.MailAddress(Properties.Settings.Default.AdminMail));
            mm.Subject = "Ошибка в Веб Архиве";
            mm.IsBodyHtml = true;//письмо в html формате (если надо)
            string _str = "";
            _str = "<b>Пользователь:</b> " + (Session["user_name"] ?? Context.User.Identity.Name.Trim()).ToString() + "<br/>";
            _str += (Session["user_login"] == null ? "" : " Логин: " + Session["user_login"].ToString()) + "<br/>";
            _str += (Session["user_winlogin"] == null ? "" : " WinLogin: " + Session["user_winlogin"].ToString()) + "<br/>";
            _str += (Session["user_mail"] == null ? "" : " E-mail: " + Session["user_mail"].ToString()) + "<br/>";
            _str += (Session["user_location"] == null ? "" : " Место: " + Session["user_location"].ToString()) + "<br/>";
            _str += "Ошибка:<br/><p>" + Session["ErrorException"].ToString() + "</p><br/><br/>";

            _str += faFunc.GetSessionValues();
            //long totalSessionBytes = 0;

            //_str += "<b>Активных сессий:</b>  " + (Application["ActiveSession"] ?? 0).ToString() + " <br/>";
            ////_str += "<b>Активныe пользователи:</b> <br/>";
            ////foreach (string item in (Application["ActiveUser"] as List<string>))
            ////    _str += "&nbsp;&nbsp;&nbsp;" + item + "<br/>";
            //_str += "<br/><br/><table ><caption>Переменные сессии</caption><tr><th>Ключ</th><th>Значение</th><th>Размер</th></tr>";
            //BinaryFormatter b = new BinaryFormatter();
            ////MemoryStream m;
            //foreach (string item in Session.Contents) {
            //    var m = new MemoryStream();
            //    b.Serialize(m, Session[item]);
            //    totalSessionBytes += m.Length;
            //    _str += "<tr><td>" + item + "</td><td>" + (!item.Contains("drop_down_list") && !item.Contains("list") && !item.Contains("menu") ? Session[item].ToString() : "html") + "</td><td>" + m.Length + " байт</td></tr>";
            //}
            //_str += "</table><br/><br/>";

            mm.Body = _str;

            faFunc.SendMail(mm);
            faFunc.ToLog(8, "Об общей ошибке приложения");
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["ErrorException"] != null )
            {
                Label1.Text = (Session["ErrorException"] ?? "").ToString();
                SendMail();
            }
            Session.Clear();
            ViewState.Clear();
        }

    }
}