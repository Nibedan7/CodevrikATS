using CdplATS.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace CdplATS.UI.Helpers
{
    public class CommonService
    {
        private readonly EmailConfig _emailConfig;

        public CommonService(IOptions<EmailConfig> emailConfig)
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

        [NonAction]
        public async Task<bool> SendAttachmentEmail(EmailEntity emailEntity)
        {
            try
            {
                string smtpServer = _emailConfig.SmtpServer;
                int smtpPort = _emailConfig.SmtpPort;
                string fromEmail = _emailConfig.HostEmail;
                string emailPassword = _emailConfig.HostEmailAppPassword;

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(fromEmail, "Cement Digital"),
                    Subject = emailEntity.Subject,
                    Body = emailEntity.Body,
                    IsBodyHtml = true
                };

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

                if (emailEntity.AttachmentBytes != null && !string.IsNullOrWhiteSpace(emailEntity.AttachmentFileName))
                {
                    using (var attachmentStream = new MemoryStream(emailEntity.AttachmentBytes))
                    {
                        var attachment = new Attachment(attachmentStream, emailEntity.AttachmentFileName);
                        mail.Attachments.Add(attachment);

                        using (var smtp = new SmtpClient(smtpServer, smtpPort)
                        {
                            Credentials = new NetworkCredential(fromEmail, emailPassword),
                            EnableSsl = true
                        })
                        {
                            await smtp.SendMailAsync(mail);
                        }
                    }
                }
                else
                {
                    using (var smtp = new SmtpClient(smtpServer, smtpPort)
                    {
                        Credentials = new NetworkCredential(fromEmail, emailPassword),
                        EnableSsl = true
                    })
                    {
                        await smtp.SendMailAsync(mail);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
