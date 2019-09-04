using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Empite.PitchReady.Service
{
    public class EmailHelper : IEmailHelper
    {
        private readonly EmailSettings _emailSettings;

        public EmailHelper(
            IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, Dictionary<string, string> properties, string templateName)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));
                mimeMessage.To.Add(new MailboxAddress(email));
                mimeMessage.Subject = subject;

                var builder = new StringBuilder();

                //using (var reader = File.OpenText($"Templates\\{templateName}"))
                //{
                //    builder.Append(reader.ReadToEnd());
                //}

                foreach (KeyValuePair<string, string> property in properties)
                {
                    builder.Replace(property.Key, property.Value);
                }

                mimeMessage.Body = new TextPart("html")
                {
                    Text = "<html><h1>Test</h1>" + builder + "</html>" //builder.ToString() //"<html><h1>Test</h1>" +message+"</html>"
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(_emailSettings.MailServer);

                    //if (_env.IsDevelopment())
                    //{
                    //    // The third parameter is useSSL (true if the client should make an SSL-wrapped
                    //    // connection to the server; otherwise, false).
                    //    await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, true);
                    //}
                    //else
                    //{
                    //    await client.ConnectAsync(_emailSettings.MailServer);
                    //}

                    // Note: only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
