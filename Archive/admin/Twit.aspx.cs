using System;


namespace WebArchiveR6
{
    public partial class Twit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["common_access_admin_ad"] == null)
            {
                Response.Write("Доступа нет!");
                Response.End();
            }
            else
            {
                tb1.Text = (Application["message4all"] ?? "").ToString();
            }
        }

        protected void twitthis(object sender, EventArgs e)
        {
            if (tb1.Text.Trim() == "")
                Application["message4all"] = null;
            else
                Application["message4all"] = tb1.Text;
        }
    }
}