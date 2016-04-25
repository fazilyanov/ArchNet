using System;
using System.Data;
using System.IO;

namespace WebArchiveR6
{
    public partial class InsertFromScanerComplectNew : System.Web.UI.Page
    {
        public string _path = "", buf = "", cur = "", id = "", idlist = "", sc = "", sd = "", _ret = "";

        protected override void OnInit(EventArgs e)
        {
            id = (Request.QueryString["id"] ?? "").ToString();
            idlist = (Request.QueryString["idlist"] ?? "").ToString();
            cur = (Request.QueryString["cur"] ?? "").ToString();
            
            sc = (Session["_complectnew_reg_cursor_sidx"] ?? "").ToString();
            sd = (Session["_complectnew_reg_cursor_sord"] ?? "").ToString();
            string buf;
            Response.Clear();
            using (var reader = new StreamReader(Request.InputStream))
            {
                buf = reader.ReadToEnd().Trim();
            }
            if (buf.Length > 0 && cur != "")
            {
                DataTable dt = (Session[cur] as DataTable);

                DataView dv = dt.DefaultView;
                DataTable dt_s = dv.ToTable();
                dt_s.PrimaryKey = new DataColumn[] { dt_s.Columns["id"] }; // из вьюшки не приходит ключ

                try
                {
                    int vi = 0;
                    // Если пикается в диалоге "новый"
                    if (idlist == "")
                    {
                        if (Int32.TryParse(buf, out vi))
                        {
                            if (vi > 1000000000 && vi < Int32.MaxValue)
                            {
                                //
                                DataRow r = dt_s.NewRow();
                                int min_id = 0;
                                foreach (DataRow row in dt.Rows)
                                    min_id = Math.Min((int)row["id"], min_id);
                                min_id--;
                                r["id"] = min_id;
                                r["prim"] = vi;
                                r["status"] = "1";
                                dt_s.Rows.Add(r);
                                dt_s.AcceptChanges();
                            }
                            else _ret = "Значение «" + buf + "» не является штрихкодом.";
                        }
                        else _ret = "Значение «" + buf + "» не является числом.";
                    }
                    else
                        foreach (DataRow r in dt_s.Rows)
                        {
                            if (r["id"].ToString() == idlist)
                            {
                                if (buf.Length > 0)
                                {
                                    if (Int32.TryParse(buf, out vi))
                                    {
                                        if (vi > 1000000000 && vi < Int32.MaxValue)
                                        {
                                            r["prim"] = vi;
                                            r["status"] = r["status"].ToString() != "1" ? "2" : r["status"];
                                        }
                                        else _ret = "Значение «" + buf + "» не является штрихкодом.";
                                    }
                                    else _ret = "Значение «" + buf + "» не является числом.";
                                }
                            }
                        }
                    if (_ret.Length == 0)
                    {
                        Session[cur] = dt_s;
                    }
                    else
                        Response.Write(_ret);
                }
                catch (Exception ex)
                {
                    Response.StatusCode = 400;
                    Response.Write(faFunc.GetExceptionMessage(ex));
                }
            }
            else
                Response.Write("Передан пустой параметр.");

            Response.End();
        }
    }
}