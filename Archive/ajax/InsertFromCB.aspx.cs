using System;
using System.IO;
using System.Web;
using System.Data;

namespace WebArchiveR6
{
    public partial class InsertFromCB : System.Web.UI.Page
    {
        public string _path = "", buf = "", cur = "", id = "", sc = "", sd = "", _ret = "";

        protected override void OnInit(EventArgs e) {
            cur = (Request.QueryString["cur"] ?? "").ToString();
            id = (Request.QueryString["id"] ?? "").ToString();
            sc = (Session["complectnew_cursor_sidx"]??"").ToString();
            sd = (Session["complectnew_cursor_sord"]??"").ToString();
            string buf;
            Response.Clear();
            using (var reader = new StreamReader(Request.InputStream)) {
                buf = reader.ReadToEnd();
            }
            if (buf.Length > 0 && cur != "") {
                string[] stringSeparators = new string[] { "\r\n" };
                string[] lines = buf.Split(stringSeparators, StringSplitOptions.None);
                DataTable dt = (Session[cur] as DataTable);

                DataView dv = dt.DefaultView;
                dv.Sort = sc + " " + sd;
                DataTable dt_s = dv.ToTable();
                dt_s.PrimaryKey = new DataColumn[] { dt_s.Columns["id"] };
                try {
                    //DataRow row = dt.Rows.Find(e.RowKey);

                    int i = 0;
                    bool paste = false;
                    string bc = "";
                    int vi = 0;
                    foreach (DataRow r in dt_s.Rows) {
                        if (r["id"].ToString() == id || paste) {
                            bc = lines[i++].Trim();
                            if (bc.Length > 0 ) {
                                if (Int32.TryParse(bc, out vi)) {
                                    if (vi > 1000000000 && vi < Int32.MaxValue) {
                                        r["barcode"] = vi;
                                        r["status"] = r["status"].ToString() != "1" ? "2" : r["status"];
                                    }
                                    else _ret = "Значение «" + bc + "» не является штрихкодом (Строка " + i + "). Вставка значений отменена ";
                                }
                                else _ret = "Значение «" + bc + "» не является числом. (Строка " + i + "). Вставка значений отменена";
                            }
                            paste = true;
                        }
                        if (i >= lines.Length || _ret.Length > 0) break;
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
                Response.Write("В буфере обмена нет данных.");

            Response.End();
            //}

        }
    }
}