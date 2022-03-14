using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Notifications
{
    public class Notification : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public Notification(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string to, string subject, string message)
        {
            Execute(to, subject, message).Wait();
            return Task.FromResult(0);
        }
        public async Task Execute(string to, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_configuration["MailSettings:Mail"], _configuration["MailSettings:DisplayName"])
                };
                to.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(t => mail.To.Add(new MailAddress(t)));
                //cc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(c => mail.CC.Add(new MailAddress(c)));
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_configuration["MailSettings:Host"], Convert.ToInt32(_configuration["MailSettings:Port"])))
                {
                    smtp.Credentials = new NetworkCredential(_configuration["MailSettings:Mail"], _configuration["MailSettings:Password"]);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
