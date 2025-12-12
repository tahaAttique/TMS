using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace TMS.EmailSendingJob
{
    [Queue("email-sending")]
    public class TicketEmailSendingJob(IConfiguration configuration) : AsyncBackgroundJob<EmailSendingArgs>, ITransientDependency
    {
        private readonly IConfiguration _configuration = configuration;

        public override async Task ExecuteAsync(EmailSendingArgs args)
        {
            var smtpHost = _configuration["Settings:Abp.Mailing.Smtp.Host"] ?? throw new UserFriendlyException("SMTP host not set in appsettings");
            var smtpPort = Convert.ToInt32(_configuration["Settings:Abp.Mailing.Smtp.Port"]);
            var smtpUser = _configuration["Settings:Abp.Mailing.Smtp.UserName"] ?? throw new UserFriendlyException("SMTP username not set in appsettings");
            var smtpPassword = _configuration["Settings:Abp.Mailing.Smtp.Password"] ?? throw new UserFriendlyException("SMTP password not set in appsettings");

            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUser, "Support Team"),
                    Subject = args.Subject,
                    Body = $"Dear {args.Name},<br>" + args.Body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(new MailAddress(args.EmailAddress, args.Name));
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
