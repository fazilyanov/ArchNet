using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Net;

namespace WebArchiveR6
{
    public partial class Torg31 : System.Web.UI.Page
    {
        public string _path = "", cur = "", id = "";

        protected override void OnInit(EventArgs e)
        {
            try
            {
                cur = (Request.QueryString["cur"] ?? "").ToString();
                id = (Request.QueryString["id"] ?? "").ToString();
                if (cur != "" && Session[cur] != null)
                {
                    DataTable dt = (Session[cur] as DataTable);
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();

                        using (var pck = new ExcelPackage())
                        {
                            string p = Server.MapPath("~/templates/torg31.xlsx");
                            using (var stream = File.OpenRead(p))
                            {
                                pck.Load(stream);
                            }
                            ExcelWorksheet ws = pck.Workbook.Worksheets[1];

                            int cur_row_index = 16;

                            if (dt.Rows.Count > 1)
                            {
                                ws.InsertRow(17, dt.Rows.Count - 1, 16);

                                for (int i = 1; i < dt.Rows.Count; i++)
                                {
                                    ws.Cells[16, 1, 16, 80].Copy(ws.Cells[cur_row_index + i, 1, cur_row_index + i, 80]);
                                }
                            }

                            ws.Cells[11, 63].Value = id;
                            ws.Cells[11, 72].Value = DateTime.Now.ToString("dd.MM.yyyy");

                            int sheet_count = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                ws.Cells[cur_row_index, 1].Value = row["frm_contr"].ToString();
                                ws.Cells[cur_row_index, 27].Value = row["name"].ToString();
                                ws.Cells[cur_row_index, 41].Value = row["num_doc"].ToString();
                                ws.Cells[cur_row_index, 51].Value = row["date_doc"].ToString() != "" ? ((DateTime)row["date_doc"]).ToString("dd.MM.yyyy") : "";
                                ws.Cells[cur_row_index, 61].Value = row["sheets"].ToString();
                                ws.Cells[cur_row_index, 70].Value = row["prim"].ToString();

                                sheet_count += (int)row["sheets"];
                                cur_row_index++;
                            }
                            ws.Cells[cur_row_index, 61].Value = sheet_count;
                            //

                            string name = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "_" + Context.Session["user_login"].ToString() + "_torg31.xlsx";
                            string fullpath = Path.Combine(Properties.Settings.Default.filepath + @"tempfiles\", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "_" + Context.Session["user_login"].ToString() + "_torg31.xlsx");
                            Stream stream1 = File.Create(fullpath);
                            pck.SaveAs(stream1);
                            stream1.Close();

                            WebClient client = new WebClient();
                            Byte[] buffer = client.DownloadData(fullpath);
                            Response.ContentType = "application/vnd.ms-excel";
                            Response.AddHeader("content-length", buffer.Length.ToString());
                            Response.AddHeader("content-disposition", "attachment; filename=" + name);
                            Response.BinaryWrite(buffer);
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}