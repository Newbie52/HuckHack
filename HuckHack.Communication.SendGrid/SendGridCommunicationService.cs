using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HuckHack.Domain.Contracts.Services;
using HuckHack.Domain.Entities;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HuckHack.Communication.SendGrid
{
    public class SendGridCommunicationService : ICommunicationService
    {
        private readonly string _apiKey;

        public SendGridCommunicationService(IConfiguration configuration)
        {
            _apiKey = configuration.GetSection("SendGridApiKey").Value;
        }

        public async Task SendInviteEmail(string fromName, string fromUserProfileLink, string toEmail, string coverLetter)
        {
            var client = new SendGridClient(_apiKey);
            var fromEmailAddress = new EmailAddress("notification@huckhack.com");
            var toEmailAddress = new EmailAddress(toEmail);
            await client.SendEmailAsync(MailHelper.CreateSingleTemplateEmail(fromEmailAddress, toEmailAddress,
                "d-cc781e209c04450989a6fde672bfe054", 
                new
                {
                    username = fromName, coverLetter = coverLetter, link = fromUserProfileLink
                }));
        }
    }
}
