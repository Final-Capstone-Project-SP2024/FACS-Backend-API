using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Helpers.EmailHandler
{
    public class SendMailHandler
    {
        private readonly IConfiguration _config;

        public SendMailHandler(IConfiguration config)
        {
            _config = config;
        }


        public async Task SendMail(string? subject = "comsuonhocmon", string? Email = "mandayngu@gmail.com", string securityCode = "XXX_02", string? password ="123456", string? UserName = "Minh Man")
        {

            MailMessage mail = new MailMessage();
            mail.To.Add(Email);
            mail.From = new MailAddress(_config["SendMailAccount:UserName"].ToString(), "From FireDetection_FPT", Encoding.UTF8);
            mail.Subject = subject;
            mail.SubjectEncoding = Encoding.UTF8;
            using (StreamReader reader = new StreamReader("../FireDetection.Backend.API/Template/HTMLPage1.html"))
            {
                mail.Body = reader.ReadToEnd();

            }
            mail.Body = mail.Body.Replace("{userName}", UserName);
            mail.Body = mail.Body.Replace("{securityCode}",securityCode);
            mail.Body = mail.Body.Replace("{password}", password);

            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            var client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(_config["SendMailAccount:UserName"], _config["SendMailAccount:AppPassword"]);
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Send(mail);
        }

        public async Task SendOTP( string? Email = "mandayngu@gmail.com",int Otp = 0)
        {

            MailMessage mail = new MailMessage();
            mail.To.Add(Email);
            mail.From = new MailAddress(_config["SendMailAccount:UserName"].ToString(), "From FireDetection_FPT", Encoding.UTF8);
            mail.Subject = "Reset Password";
            mail.SubjectEncoding = Encoding.UTF8;
            using (StreamReader reader = new StreamReader("../FireDetection.Backend.API/Template/sendotp.html"))
            {
                mail.Body = reader.ReadToEnd();

            }
            mail.Body = mail.Body.Replace("{otp}", Otp.ToString());
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            var client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(_config["SendMailAccount:UserName"], _config["SendMailAccount:AppPassword"]);
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Send(mail);
        }

    }
}
