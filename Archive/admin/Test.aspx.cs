using System;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebArchiveR6.Admin
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            throw new IndexOutOfRangeException();
        }

 

       

        private void check()
        {
            //string _reestr_xls = @"C:\13\Поступление товаров и услуг Казачья2.xlsx";
            //string _reestr_xls = @"C:\13\Реализация товаров и услуг Казачья2.xlsx";
            //string _reestr_xls = @"C:\13\Счета-фактуры выданные Казачья2.xlsx";
            //string _reestr_xls = @"C:\13\Счета-фактуры полученные Казачья2.xlsx";
            //string _reestr_xls = @"C:\13\2015_2.xlsx";
            //string _reestr_xls = @"C:\13\tt.xlsx";
            string _reestr_xls = @"C:\13\СТГ-К на обработку.xlsx";
            int foundcount = 0;
            Excel.Application excelApp = null;
            Excel.Workbook excelWorkBook = null;
            Excel.Worksheet excelWorkSheet = null;
            Excel.Range range;

            try
            {
                excelApp = new Excel.Application();
                excelWorkBook = excelApp.Workbooks.Open(_reestr_xls, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                string date = "", num = "", summ = "", id_archive = "", id_frm_1c = "";// id_doctree = "";

                // string path = "";

                excelWorkSheet = excelWorkBook.Worksheets[1];
                range = excelWorkSheet.UsedRange;

                //object[,] cells = (object[,])excelWorkSheet.UsedRange.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);

                SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
                conn.Open();
                SqlCommand cmd;

                try
                {
                    for (int i = 2; i <= range.Rows.Count; i++)
                    {
                        id_archive = excelWorkSheet.Cells[i, 1].Text ?? "";
                        if (id_archive == "")
                        {
                            num = excelWorkSheet.Cells[i, 2].Text;
                            date = (excelWorkSheet.Cells[i, 3].Text ?? "").ToString().Trim().ToLower();
                            summ = ((excelWorkSheet.Cells[i, 4] as Excel.Range).Value ?? "").ToString().Trim().ToLower();
                            id_frm_1c = (excelWorkSheet.Cells[i, 5].Text ?? "").ToString().Trim().ToLower().PadLeft(9, '0');
                            // id_doctree = (excelWorkSheet.Cells[i, 5].Text ?? "").ToString().Trim().ToLower().PadLeft(9, '0');

                            if (num != "" && date != "" && summ != "" && id_frm_1c != "")//&& id_doctree != ""
                            {
                                string sql =
                                    "SELECT a.[id] FROM [dbo].[complect_archive] a " +
                                    "JOIN [dbo].[_frm] b on b.id = a.id_frm_contr " +
                                    "Where a.del=0  and a.summ=CAST(@summ AS numeric(19, 2))" +
                                    " and a.date_doc = CAST(@date AS Date) and replace(a.num_doc,' ','') = @num"+
                                    " and b.id_1c=@id_frm_1c";  //and id_doctree=@id_doctree and id_doctree in (5022,5086,5565,5568)
                                //" and b.id_1c=@id_frm_1c and id_doctree in (5006,5007,5008,5015,5023,5024,5025,5029,5043,5050,5054,5062,5063,5064,5072,5078,5087,5095,5155,5485,5521,5562,5608)";  //and id_doctree=@id_doctree
                                
                                cmd = new SqlCommand(sql, conn);
                                cmd.Parameters.AddWithValue("@num", num.Replace(" ", ""));
                                cmd.Parameters.AddWithValue("@date", date);
                                cmd.Parameters.AddWithValue("@summ", summ.Replace(",", ".").Replace(" ", "."));
                                cmd.Parameters.AddWithValue("@id_frm_1c", id_frm_1c);

                                id_archive = (cmd.ExecuteScalar() ?? "").ToString();

                                if (id_archive != "")
                                {
                                    excelWorkSheet.Cells[i, 1] = id_archive;
                                    foundcount++;
                                }
                            }
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    
                    Response.Write(ex.Message);
                    conn.Close();
                }
            }
            catch (Exception)
            {
                ReleaseObject(excelWorkSheet);
                ReleaseObject(excelWorkBook);
                ReleaseObject(excelApp);
            }

            try
            {
                Label1.Text = foundcount.ToString();
                //excelWorkBook.SaveAs(_reestr_xls);
                excelApp.Visible = true;
            }
            catch (Exception)
            {
                ReleaseObject(excelWorkSheet);
                ReleaseObject(excelWorkBook);
                ReleaseObject(excelApp);
            }

            //System.Diagnostics.Process.Start(_reestr_xls);
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
                //Log("Unable to release the Object " + ex.ToString(), Color.Orange);
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           // check();
        }
    }
}