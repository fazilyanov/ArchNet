using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebArchiveR6.ajax
{
    public partial class setopt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["texttree"] = "";
            if (Request.QueryString["texttree"]!=null && !String.IsNullOrEmpty(Request.QueryString["texttree"].ToString().Trim()) )
            {
                Session["texttree"] = Request.QueryString["texttree"].ToString().Trim();
            }
            Session["structurtext"] = "";
            if (Request.QueryString["structurtext"] != null && !String.IsNullOrEmpty(Request.QueryString["structurtext"].ToString().Trim()))
            {
                Session["structurtext"] = Request.QueryString["structurtext"].ToString().Trim();
            }
            
        }
    }
}