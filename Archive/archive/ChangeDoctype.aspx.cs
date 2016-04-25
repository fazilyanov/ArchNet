using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace WebArchiveR6
{
    public partial class ChangeDoctype : System.Web.UI.Page
    {
        public string list = "";

        protected void Page_PreRender(object sender, EventArgs e)
        {
            string _id_doctree = (Request.QueryString["id_doctree"] ?? "").ToString().Trim();
            string _id_archive = (Request.QueryString["id_archive"] ?? "").ToString().Trim();
            string _act = (Request.QueryString["act"] ?? "").ToString().Trim();
            if (_id_doctree.Length > 0 && _id_archive.Length > 0)
            {
                string _page = faFunc.GetDocTypeByIdDoctree(_id_doctree);
                if (_page != "none")
                    Response.Redirect(GetRouteUrl("archive", new { p_base = Master.cur_basename, p_page = _page }) + "?id=" + _id_archive + "&act=" + _act + "&s=1");
                else
                {
                    Response.Clear();
                    Response.Write("Не удается определить тип документа.");
                }
            }
            else
            {
                string _tmp;
                string list_item = "<a href=\"{0}\" class=\"list-group-item\"  style=\"font-size: 20px;\"><span class=\"{2}\"></span>&nbsp;&nbsp;{1}</a>";
                string[] _p = { "acc", "dog", "ord", "oth", "empl", "ohs", "tech", "bank", "norm" };
                foreach (string name in _p)
                {
                    _tmp = faFunc.GetDocTypeName(name);
                    if (Session[Master.cur_basename + "_access_archive_" + name + "_view"] != null || Session[Master.cur_basename + "_access_archive_" + name + "_edit"] != null)
                    {
                        list += String.Format(list_item, GetRouteUrl("archive", new { p_base = Master.cur_basename, p_page = name }) + "?id=0&act=add", _tmp, "gi gi-blank");
                    }
                }
            }

        }
    }
}