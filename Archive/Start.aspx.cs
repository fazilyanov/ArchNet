using System;

namespace ArchNet.Util
{
    public partial class Start : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            //Session["user_login"] = null;
            Response.Redirect(GetRouteUrl("default", new { p_base = "dbselect" }));
        }
    }
}