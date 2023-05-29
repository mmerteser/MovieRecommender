using Microsoft.Extensions.Logging;
using MovieRecommender.Application;
using MovieRecommender.Application.AbstractServices;
using MovieRecommender.Application.Models;
using MovieRecommender.Application.Repositories;
using MovieRecommender.Application.Utilities.Result;
using MovieRecommender.Core.Entities;
using System.Net;
using System.Net.Mail;

namespace MovieRecommender.Business.ConcreteServices
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IUserRepository _userRepository;
        public EmailService(ILogger<EmailService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public IResult SendEmail(string toEmail, string mailBody)
        {
            try
            {
                EmailOptions emailOptions = Configuration.EmailOptions;

                SmtpClient client = new SmtpClient();
                client.Port = emailOptions.Port;
                client.Host = emailOptions.Host;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(emailOptions.FromEmail, emailOptions.FromEmailPassword);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailOptions.FromEmail);
                mail.To.Add(toEmail);
                mail.Subject = "Film Önerisi";
                mail.Body = mailBody;
                client.Send(mail);

                return new SuccessResult();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"SendMail Error: {ex.Message}");
                return new ErrorResult();
            }
        }
    }
}
