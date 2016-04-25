using System;
using System.Data;
using System.Data.SqlClient;

namespace WebArchiveR6
{
    public partial class Rating : System.Web.UI.Page
    {
        public string _js = "";
        public string _from = "";
        public string _to = "";
        public string _m = "", _y = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _from = (Request.QueryString["cph_when_begin"] ?? "").ToString();
            _to = (Request.QueryString["cph_when_end"] ?? "").ToString();
            if (_from == "" && _to == "")
            {
                DateTime now = DateTime.Now;
                DateTime _f, _t;
                _f = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
                _t = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 0);
                _from = _f.ToShortDateString() + " " + _f.ToShortTimeString();
                _to = _t.ToShortDateString() + " " + _t.ToShortTimeString();
                _m = now.Month.ToString();
                _y = now.Year.ToString();
            }
            else
            {
                try
                {
                    _m = _y = (_from != "" ? _from : _to);
                    _m = DateTime.Parse(_m).Month.ToString();
                    _y = DateTime.Parse(_y).Year.ToString();
                }
                catch 
                {
                    _from = _to = "";
                }
                
            }
            jqUserScore.AppearanceSettings.Caption = "Админ / Эффективность (Последнее обновление:" + DateTime.Now.ToShortTimeString() + " МСК)";
        }
       

        protected DataTable GetData()
        {
            SqlCommand cmd;
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.constr);
            conn.Open();
            // Пользователи 
            string _sql =
                "SELECT DISTINCT [id], [name] , 0 as score1, 0 as score2, 0 as score3, 0 as score_all, 0 as workhour, 0 as min, 0 as max, 0 as max_score " +
                "FROM [_user] a " +
                "WHERE a.del=0 AND a.watch=2";

            cmd = new SqlCommand(_sql, conn);
            cmd.CommandTimeout = 600;
            
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            

            if (dt.Rows.Count > 0)
            {
                int _min=0,_max=0;
                cmd = new SqlCommand("SELECT [min],[max] FROM [_norm_hour] a where a.del=0 and a.[year]=" + _y + " and a.[month]=" + _m + " ", conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                sda.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    _min = (int)dt1.Rows[0]["min"];
                    _max = (int)dt1.Rows[0]["max"];
                }
                cmd.Dispose();

                // Для каждого считаем 
                string id_user = "";
                foreach (DataRow row in dt.Rows)
                {
                    id_user = row["id"].ToString();

                    // Основные баллы
                    cmd = new SqlCommand("SELECT sum(score) FROM [_journal] a where id_user=" + id_user + " and [when]>= CONVERT(DATETIME,'" + _from + "',104) AND [when]<= CONVERT(DATETIME,'" + _to + "',104) AND id_base<>19 AND (id_table=1 OR id_table=3) group by id_user", conn);
                    var res1 = cmd.ExecuteScalar();
                    row["score1"] = res1 is DBNull || res1 == null ? 0 : (int)res1;
                    cmd.Dispose();

                    // Дополнительные баллы
                    cmd = new SqlCommand("SELECT sum(score) FROM [_user_score] a where id_user=" + id_user + " and id_scoretype=2  and [when]>= CONVERT(DATETIME,'" + _from + "',104) AND [when]<= CONVERT(DATETIME,'" + _to + "',104) group by id_user", conn);
                    var res2 = cmd.ExecuteScalar();
                    row["score2"] = res2 is DBNull || res2 == null ? 0 : (int)res2;
                    cmd.Dispose();

                    // Штрафные баллы
                    cmd = new SqlCommand("SELECT sum(score) FROM [_user_score] a where id_user=" + id_user + " and id_scoretype=3 and [when]>= CONVERT(DATETIME,'" + _from + "',104) AND [when]<= CONVERT(DATETIME,'" + _to + "',104) group by id_user", conn);
                    var res3 = cmd.ExecuteScalar();
                    row["score3"] = res3 is DBNull || res3 == null ? 0 : (int)res3;
                    cmd.Dispose();

                    // Итог
                    row["score_all"] = int.Parse(row["score1"].ToString()) + int.Parse(row["score2"].ToString()) - int.Parse(row["score3"].ToString());

                    // Часов отработано
                    cmd = new SqlCommand("SELECT hour FROM [_user_workhour] a where a.del=0 and id_user=" + id_user + " and year=" + _y + " and month=" + _m , conn);
                    var res4 = cmd.ExecuteScalar();
                    row["workhour"] = res4 is DBNull || res4 == null ? 0 : (int)res4;
                    cmd.Dispose();
                    
                    //
                    row["min"] = _min;
                    row["max"] = _max;
                    row["max_score"] = (int)row["workhour"] > 0 ? ((int)row["score_all"] / (int)row["workhour"]) * _max : 0;
                }
            }


            conn.Close();




            //if (Session[_data] == null)
            //{
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("id", typeof(int));
            //    dt.Columns.Add("docpack", typeof(string));
            //    dt.Columns.Add("date_doc", typeof(string));
            //    dt.Columns.Add("doctree", typeof(string));
            //    dt.Columns.Add("frm", typeof(string));
            //    dt.Columns.Add("num_doc", typeof(string));
            //    dt.Columns.Add("summ", typeof(string));
            //    dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
            //    Session[_data] = dt;
            //}
            return dt;
        }

        // Запрос данных 
        protected void jqUserScore_DataRequesting(object sender, Trirand.Web.UI.WebControls.JQGridDataRequestEventArgs e)
        {
            jqUserScore.DataSource = GetData();
            jqUserScore.DataBind();
        }
    }
}


//string _sql =
//    "SELECT DISTINCT [id_user],b.name as [user], 0 as score1, 0 as score2, 0 as score3, 0 as score_all, 0 as workhour " +
//    "FROM [_journal] a " +
//    "LEFT JOIN _user b on b.id=a.id_user " +
//    "WHERE [when]>= CONVERT(DATETIME,'" + _from + "',104) AND [when]<= CONVERT(DATETIME,'" + _to + "',104) "+
//    " AND id_base<>19 AND (id_table=1 OR id_table=3)";
//"SELECT [id],[when],[id_user],[id_edittype],[id_base],[id_table],[change_id] FROM [Archive].[dbo].[_user_score] " +