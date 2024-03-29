﻿using Core.Application.Dtos.Email;
using Core.Application.Interfaces.Services;
using Core.Domain.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        private MailSettings _mailSettings { get; }

        public EmailService(IOptions<MailSettings> options) 
        {
            _mailSettings = options.Value;
        }

        public async Task SendEmail(EmailRequest request)
        {
            try
            {
                MimeMessage email = new();
                email.Sender = MailboxAddress.Parse($"{_mailSettings.DisplayName} <{_mailSettings.EmailFrom}>");
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                BodyBuilder bodyBuilder = new();
                bodyBuilder.HtmlBody = request.Body;
                email.Body = bodyBuilder.ToMessageBody();


                using SmtpClient smtp = new();
                smtp.ServerCertificateValidationCallback = (s,c,h,e) => true;
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.SmtpUser,_mailSettings.StmPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
