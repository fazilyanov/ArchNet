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
    public partial class setses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string notreplace = Request.QueryString["notreplace"];

            string _key = Request.QueryString["key"];
            string _value = Request.QueryString["value"];
            string _key1 = Request.QueryString["key1"];
            string _value1 = Request.QueryString["value1"];
            if (_key != null && _value != null)
                if (notreplace == null) Session[_key] = _value.Trim();
                else if (Session[_key] == null) Session[_key] = _value.Trim();
            if (_key1 != null && _value1 != null)
                if (notreplace == null) Session[_key1] = _value1.Trim();
                else if (Session[_key1] == null) Session[_key1] = _value1.Trim();
        }
    }
}