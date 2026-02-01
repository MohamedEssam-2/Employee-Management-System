using System.Net;
using System.Net.Mail;

namespace Demo.PL.Utilities
{
    public static class EmailSetting
    {
        public static bool SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("mido8786essam2@gmail.com", "vkmb hxyy fjfc dxta");
                client.Send("mido8786essam2@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
