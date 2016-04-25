using System;
using System.Data;

namespace WebArchiveR6.ajax
{
    public partial class copyses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _from = Request.QueryString["from"] ?? "";
            string _to = Request.QueryString["to"] ?? "";
            string _new = Request.QueryString["new"] ?? "";
            if (_new.Length > 0 && Session[_to] != null)
            {
                DataTable dt1 = Session[_to] as DataTable;
                foreach (DataRow row_new in dt1.Rows) row_new["status"] = 3;


                DataTable dt2 = Session[_from] as DataTable;



                int i = 0;
                foreach (DataRow improw in dt2.Rows)
                {
                    DataRow newrow = dt1.NewRow();
                    foreach (DataColumn c in improw.Table.Columns)
                    {
                        newrow[c.ColumnName] = improw[c.ColumnName];
                    } newrow["id"] = --i;
                    newrow["status"] = 1;
                    dt1.Rows.InsertAt(newrow, 0);
                }
            }
            else
                Session[_to] = Session[_from];
        }
    }
}