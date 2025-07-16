using CdplATS.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace CdplATS.API.Services
{
    public class SendEmailCommonAPIService
    {
        private readonly EmailConfig _emailConfig;

        public SendEmailCommonAPIService(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        [NonAction]
        public async Task<bool> SendEmail(EmailEntity emailEntity)
        {
            try
            {
                string smtpServer = _emailConfig.SmtpServer;
                int smtpPort = _emailConfig.SmtpPort;
                string fromEmail = _emailConfig.HostEmail;
                string emailPassword = _emailConfig.HostEmailAppPassword;

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(fromEmail, _emailConfig.SenderName);
                if (!string.IsNullOrEmpty(emailEntity.Email))
                {
                    var emails = emailEntity.Email.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var email in emails)
                    {
                        string trimmedEmail = email.Trim();
                        if (!string.IsNullOrWhiteSpace(trimmedEmail))
                        {
                            mail.Bcc.Add(trimmedEmail);
                        }
                    }
                }
                mail.Subject = emailEntity.Subject;
                mail.Body = emailEntity.Body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(fromEmail, emailPassword),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(mail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
