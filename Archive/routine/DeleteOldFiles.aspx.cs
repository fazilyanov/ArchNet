using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace WebArchiveR6
{
    public partial class DeleteOldFiles : System.Web.UI.Page
    {
        public string sReport = "";

        protected string CompareFiles(string file1, string file2, string _b)
        {
            string res = "";

            string _f_complect = Path.Combine(Properties.Settings.Default.filepath, "complectfiles", file1);
            string _f_archive = "";
            string _f_main = Path.Combine(Properties.Settings.Default.filepath, _b, "archive", file2);
            string _f_alt = Path.Combine(Properties.Settings.Default.filepathalt, _b, "archive", file2);

            if (File.Exists(_f_complect))
            {
                _f_archive = File.Exists(_f_main) ? _f_main : (File.Exists(_f_alt) ? _f_alt : "");
                if (_f_archive != "")
                {
                    FileInfo f1 = new FileInfo(_f_complect);
                    FileInfo f2 = new FileInfo(_f_archive);
                    if (f1.Length == f2.Length)
                    {
                        res = "Удален";
                    }
                    else
                    {
                        res = "Разные размеры:" + f1.Length.ToString() + " - " + f2.Length.ToString() + "(" + ((int)((f1.Length - f2.Length) / 1024)).ToString() + ")";
                    }
                }
                else
                {
                    res = "Нет файла архива";
                }
            }
            else
            {
                res = "Нет файла комплекта";
            }

            return res;
        }

        protected string CheckDel()
        {
            string res = "";
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText =
                " SELECT b.id, b.id_complectnew,b.id_archive,a.date_reg, c.name as base, c.namerus as baserus," +
                " b.[file], b.file_archive" +
                " FROM [dbo].[_complectnew] a " +
                " left join [dbo].[_complectnew_list] b on a.id=b.id_complectnew " +
                " left join _base c on a.id_base=c.id " +
                "where (a.del=1 or b.del=1) and b.accept=1 and a.date_reg < CONVERT(DATETIME,'" + DateTime.Now.ToShortDateString() + "',104);";
            //
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();
            res = "Удалено файлов из удаленных комплектов : ";
            int delcount = 0;
            if (dt.Rows.Count > 0)
            {
                //res += "<table border='1' cellpadding='5' style='border-collapse: collapse;'>"+
                //     "<tr><th>ID</th><th>ID Комплекта</th><th>ID Архива</th><th>Дата создания</th><th>База данных</th><th>Результат</th></tr>";
                string _f = "";

                SqlCommand cmd2 = new SqlCommand("UPDATE [dbo].[_complectnew_list] SET [accept] = 2 WHERE id=@id", conn);

                foreach (DataRow row in dt.Rows)
                {
                    if (row["file"].ToString().Trim().Length > 0)
                    {
                        _f = Path.Combine(Properties.Settings.Default.filepath, "complectfiles", row["file"].ToString());
                        if (File.Exists(_f))
                        {
                            //delete file
                            File.Delete(_f);
                        }
                    }
                    conn.Open();
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.AddWithValue("@id", row["id"].ToString());
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    delcount++;
                    //res += "<tr><td>" + row["id"].ToString() + "</td><td>" + row["id_complectnew"].ToString() + "</td><td>" + row["id_archive"].ToString() + "</td><td>" + row["date_reg"].ToString() + "</td><td>" + row["baserus"].ToString() + "</td><td>" + delcount + "</td></tr>";
                }
                //res += "</table>";
            }
            return res + delcount;
        }

        protected string Pugacheva()
        {
            string res = "";
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText =
                " SELECT b.id, b.id_complectnew,b.id_archive,b.barcode,a.date_reg, c.name as base, c.namerus as baserus," +
                " b.[file], b.file_archive" +
                " FROM [dbo].[_complectnew] a " +
                " left join [dbo].[_complectnew_list] b on a.id=b.id_complectnew " +
                " left join _base c on a.id_base=c.id " +
                "where a.del=0 and b.del=0 and b.accept=1 and b.id_archive>0 and b.file_archive='' order by b.id  OFFSET 27000 ROWS FETCH NEXT 3000 ROWS ONLY;";//;
            //
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();
            res = "";
            int dfile = 0;
            if (dt.Rows.Count > 0)
            {
                res += "<table border='1' cellpadding='5' style='border-collapse: collapse;'>" +
                     "<tr><th>ID</th><th>ID Комплекта</th><th>ID Архива</th><th>Штрихкод</th><th>Дата создания</th><th>База данных</th><th>Результат</th></tr>";
                string ret = "";

                SqlCommand cmd2 = new SqlCommand("", conn);
                SqlCommand cmd3 = new SqlCommand("UPDATE [dbo].[_complectnew_list] SET [file_archive]=@file_archive WHERE id=@id", conn);

                foreach (DataRow row in dt.Rows)
                {
                    cmd2.Parameters.Clear();
                    if ((int)row["barcode"] > 0)
                    {
                        cmd2.CommandText = "SELECT a.[file] FROM [dbo].[" + row["base"].ToString() + "_docversion] as a where a.barcode=@barcode and a.del=0 order by a.id desc";
                        cmd2.Parameters.AddWithValue("@barcode", row["barcode"]);
                    }
                    else if ((int)row["id_archive"] > 0)
                    {
                        cmd2.CommandText = "SELECT a.[file] FROM [dbo].[" + row["base"].ToString() + "_docversion] as a where a.id_archive=@id_archive and a.del=0 order by a.id desc";
                        cmd2.Parameters.AddWithValue("@id_archive", row["id_archive"]);
                    }

                    conn.Open();
                    var qres = cmd2.ExecuteScalar();
                    string nfile = (qres ?? "").ToString();
                    conn.Close();

                    ret = nfile != "" ? CompareFiles(row["file"].ToString(), nfile, row["base"].ToString()) : " ненайден в архиве";

                    if (ret == "Удален")
                    {
                        dfile++;

                        conn.Open();
                        cmd3.Parameters.Clear();
                        cmd3.Parameters.AddWithValue("@id", row["id"].ToString());
                        cmd3.Parameters.AddWithValue("@file_archive", nfile);
                        cmd3.ExecuteNonQuery();
                        conn.Close();
                    }
                    res += "<tr><td>" + row["id"].ToString() + "</td><td>" + row["id_complectnew"].ToString() + "</td><td>" + row["id_archive"].ToString() + "</td><td>" + row["barcode"].ToString() + "</td><td>" + row["date_reg"].ToString() + "</td><td>" + row["baserus"].ToString() + "</td><td>" + ret + "</td></tr>";
                }
                res += "</table>";
            }
            return dfile + " / " + dt.Rows.Count + "<br/>" + res;
        }

        protected string Check()
        {
            string res = "";
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText =
                " SELECT top 5000 b.id, b.id_complectnew,b.id_archive,a.date_reg, c.name as base, c.namerus as baserus," +
                " b.[file], b.file_archive" +
                " FROM [dbo].[_complectnew] a " +
                " left join [dbo].[_complectnew_list] b on a.id=b.id_complectnew " +
                " left join _base c on a.id_base=c.id " +
                "where b.accept=1 and b.file_archive!='' ";//and b.date_reg< CONVERT(DATETIME,'" + DateTime.Now.AddMonths(-3).ToShortDateString() + "',104)";
            //
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            conn.Close();
            // res = "";
            int delcount = 0;
            if (dt.Rows.Count > 0)
            {
                //  res += "<table border='1' cellpadding='5' style='border-collapse: collapse;'>" +
                //       "<tr><th>ID</th><th>ID Комплекта</th><th>ID Архива</th><th>Дата создания</th><th>База данных</th><th>Результат</th></tr>";
                string ret = "";

                SqlCommand cmd2 = new SqlCommand("UPDATE [dbo].[_complectnew_list] SET [accept] = @accept, [file]=[file_archive] WHERE id=@id", conn);

                foreach (DataRow row in dt.Rows)
                {
                    ret = CompareFiles(row["file"].ToString(), row["file_archive"].ToString(), row["base"].ToString());
                    if (ret == "Удален")
                    {
                        delcount++;
                        File.Delete(Path.Combine(Properties.Settings.Default.filepath, "complectfiles", row["file"].ToString()));
                        conn.Open();
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("@id", row["id"].ToString());
                        cmd2.Parameters.AddWithValue("@accept", 2);
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("@id", row["id"].ToString());
                        cmd2.Parameters.AddWithValue("@accept", 3);
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                    }
                    //  res += "<tr><td>" + row["id"].ToString() + "</td><td>" + row["id_complectnew"].ToString() + "</td><td>" + row["id_archive"].ToString() + "</td><td>" + row["date_reg"].ToString() + "</td><td>" + row["baserus"].ToString() + "</td><td>" + ret + "</td></tr>";
                }
                //res += "</table>";
            }
            return "Удалено файлов: " + delcount + " из " + dt.Rows.Count; //+ res;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Write(Check());
            Response.End();
        }
    }
}