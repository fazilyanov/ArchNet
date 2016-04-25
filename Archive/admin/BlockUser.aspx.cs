using System;

namespace WebArchiveR6.Admin
{
    public partial class BlockUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string act = Page.RouteData.Values["act"].ToString();
            string user_login = Request.QueryString["user_login"];

            switch (act)
            {
                case "b":
                    Application["BlockedUser"] = user_login;
                    break;

                case "u":
                    Application["BlockedUser"] = null;
                    break;

                default:
                    break;
            }
            Response.Clear();
            Response.Write((Application["BlockedUser"] ?? "").ToString());
        }
    }
}