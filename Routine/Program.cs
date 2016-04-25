using System;
using System.IO;
using System.Net;

namespace Routine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string res = "";
            string host = "http://sky-sp1.stg.lan:8013";  //"http://localhost:7373";
            string[] urls =
                {
                  "/routine/UpdateUser1C.aspx",
                  "/routine/LoadAD.aspx",
                  "/routine/DeleteOldFiles.aspx",
                  "/routine/UpdateDepartment1C.aspx"
                  };
            string[] text =
               {
                  "[routine/UpdateUser1C.aspx] \nОбновление пользователей в таблице [dbo].[_user_1c]..",
                  "[routine/LoadAD.aspx] \nСинхронизация пользователей c Active Directory.. ",
                  "[routine/DeleteOldFiles.aspx] \nУдаление файлов обработанных комплектов..",
                  "[routine/UpdateDepartment1C.aspx] \nОбновление таблицы [dbo].[_department_1c].."
                  };
            int i = 0;
            string buf;
            foreach (string u in urls)
            {
                Console.Write(text[i]);
                res += text[i];
                buf = "";
                try
                {
                    WebRequest request = WebRequest.Create(host + u);
                    request.Timeout = 5 * 60 * 1000;
                    request.Credentials = CredentialCache.DefaultCredentials;
                    WebResponse response = request.GetResponse();

                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        string line;
                        while ((line = stream.ReadLine()) != null)
                            buf += line + "\n";
                    }
                }
                catch (Exception ex)
                {
                    buf += "Ошибка: " + ex.Message;
                }
                buf = "\n" + buf + "\n";
                Console.WriteLine();
                Console.WriteLine(buf);
                res += buf;
                i++;
            }

            buf = "Удаление временных файлов.. ";
            Console.Write(buf);
            res += "\n" + buf;
            buf = DeleteFiles();
            Console.WriteLine(buf);
            res += buf;

            Console.WriteLine();
            Console.WriteLine(SendMailAdmin(res));
        }

        private static string DeleteFiles()
        {
            string[] files = Directory.GetFiles(@"\\SKY-SP-SQL1.STG.LAN\ArchiveScanFiles$\tempfiles\");
            int i = 0;
            foreach (string file in files)
            {
                if (DateTime.Now - File.GetCreationTime(file) > TimeSpan.FromDays(2d))
                {
                    i++;
                    //File.Delete(file);
                }
            }
            return "!!!!!!Удалено временных файлов : " + i;
        }

        private static string SendMailAdmin(string msg, string subject = "Архив: Регламентная операция")
        {
            string ret = "Письмо успешно отправлено";
            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
            mm.From = new System.Net.Mail.MailAddress("archive@stg.ru");
            mm.To.Add(new System.Net.Mail.MailAddress("a.fazilyanov@stg.ru"));
            mm.Subject = subject;
            mm.IsBodyHtml = false;
            mm.Body = msg;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("SKY-EX-CAS1.stg.lan", 587);
            client.Credentials = new System.Net.NetworkCredential("archive", "dywicaso");
            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }
    }
}