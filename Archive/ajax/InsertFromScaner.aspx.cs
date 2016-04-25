using System;
using System.IO;
using System.Web;
using System.Data;

namespace WebArchiveR6
{
    public partial class InsertFromScaner : System.Web.UI.Page
    {
        public string _path = "", buf = "", cur = "", id = "", idlist = "", sc = "", sd = "", _ret = "";

        protected override void OnInit(EventArgs e) {
            //(Request.QueryString["cur"] ?? "").ToString();
            id = (Request.QueryString["id"] ?? "").ToString();
            idlist = (Request.QueryString["idlist"] ?? "").ToString();
            cur = (Request.QueryString["cur"] ?? "").ToString();

           // cur = "test_complectnew_list_cursor_" + id;
            sc = (Session["complectnew_cursor_sidx"]??"").ToString();
            sd = (Session["complectnew_cursor_sord"]??"").ToString();
            string buf;
            Response.Clear();
            using (var reader = new StreamReader(Request.InputStream)) {
                buf = reader.ReadToEnd().Trim();
            }
            if (buf.Length > 0 && cur != "") {
                
                DataTable dt = (Session[cur] as DataTable);

                DataView dv = dt.DefaultView;
                dv.Sort = sc + " " + sd;
                DataTable dt_s = dv.ToTable();
                dt_s.PrimaryKey = new DataColumn[] { dt_s.Columns["id"] }; // из вьюшки не приходит ключ

                try {                    
                    int vi = 0;
                    foreach (DataRow r in dt_s.Rows) {
                        if (r["id"].ToString() == idlist) {
                            if (buf.Length > 0 ) {
                                if (Int32.TryParse(buf, out vi)) {
                                    if (vi > 1000000000 && vi < Int32.MaxValue) {
                                        r["barcode"] = vi;
                                        r["status"] = r["status"].ToString() != "1" ? "2" : r["status"];
                                    }
                                    else _ret = "Значение «" + buf + "» не является штрихкодом.";
                                }
                                else _ret = "Значение «" + buf + "» не является числом.";
                            }
                        }
                    }
                    if (_ret.Length == 0) {
                        Session[cur] = dt_s;
                    }
                    else
                        Response.Write(_ret);
                }
                catch (Exception ex) {
                    Response.StatusCode = 400;
                    Response.Write(faFunc.GetExceptionMessage(ex));
                }

            }
            else
                Response.Write("Передан пустой параметр.");




            Response.End();
            //}

        }
    }
}