using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Application.Services
{
    public class EmailService
    {
        private static string _environment;
        private static string _login;
        private static string _password;
        private static string _provider;
        private static int _port;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _environment = _configuration["ApplicationInfo:Environment"];
            _login = _configuration["Email:Login"];
            _password = _configuration["Email:Password"];
            _provider = _configuration["Email:Provider"];
            _port = Convert.ToInt32(_configuration["Email:Port"]);
        }

        public void Send(List<string> emails, string subject, string body)
        {
            var loginInfo = new NetworkCredential(_login, _password);

            MailMessage msg = CreateMailMessageWithSender(_login, _environment);

            foreach (var email in emails)
            {
                msg.To.Add(email);
            }

            msg.Subject = subject;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = body;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;

            var client = new SmtpClient(_provider);
            ServicePointManager.ServerCertificateValidationCallback = (obj, certificate, chain, errors) => true;

            client.EnableSsl = true;
            client.Port = _port;
            client.UseDefaultCredentials = false;
            client.Credentials = loginInfo;
            client.Send(msg);

            Log.Information(">>> E-mail's been sent");
        }

        /// <summary>
        ///  Criação de mensagem com destinatário em função do ambiente de desenvolvimento.
        ///  Em localhost é usado mailtrap
        /// </summary>
        public static MailMessage CreateMailMessageWithSender(string login, string environment)
        {
            string mailAddress = environment.ToLower() != "local" ? login : $"searcher@mailtrap.io";
            return new MailMessage { From = new MailAddress(mailAddress, "Seacher") };
        }
    }
}
