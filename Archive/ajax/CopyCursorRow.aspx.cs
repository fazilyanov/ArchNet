using System;
using System.Data;

namespace WebArchiveR6
{
    public partial class CopyCursorRow : System.Web.UI.Page
    {
        public string _path = "", buf = "", cur = "", id = "", sc = "", sd = "", _ret = "";

        protected override void OnInit(EventArgs e)
        {
            cur = (Request.QueryString["cur"] ?? "").ToString();
            id = (Request.QueryString["id"] ?? "").ToString();

            if (Session[cur] != null && id != "")
            {
                try
                {
                    DataTable dt = (Session[cur] as DataTable);
                    int min = 0;
                    DataRow newrow = dt.NewRow();
                    foreach (DataRow row in dt.Rows)
                    {
                        min = row.Field<int>("id") < min ? row.Field<int>("id") : min;
                        if (row["id"].ToString() == id)
                        {
                            newrow.ItemArray = (object[])row.ItemArray.Clone();
                        }
                    }
                    newrow["id"] = min - 1;
                    newrow["status"] = 1;
                    dt.Rows.Add(newrow);
                }
                catch (Exception ex)
                {
                    Response.StatusCode = 400;
                    Response.Write(faFunc.GetExceptionMessage(ex));
                }
            }
        }
    }
}