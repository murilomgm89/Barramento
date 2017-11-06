using System.Net;
using System.Net.Mail;

namespace OiWeb.WebAPI.Handle
{
    public class EmailHandle
    {
        public static void SendEmail(string from,string to, string subject, string message, NetworkCredential credential)
        {
            using (var mm = new MailMessage(from, to))
            {
                mm.Subject = subject;
                mm.Body = message;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credential;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
    }
}