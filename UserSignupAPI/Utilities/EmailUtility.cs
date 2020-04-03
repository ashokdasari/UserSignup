using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserSignup.Helpers;
using UserSignup.Utilities.Interfaces;

namespace UserSignup.Utilities
{
    public class EmailUtility : IEmailUtility
    {

        private readonly IHelperConfig _config;

        public EmailUtility(IHelperConfig config)
        {
            _config = config;
        }

        public bool SendEmail(string toAddresses, string subject, string body, string ccAddresses)
        {
            // Getting SMTP configurations from appsettings          
            bool smtpssl = true;
            string smtpServer = _config.SmtpServer;
            int smtpPort = _config.SmtpPort;
            string smtpUsername = _config.FromAddress;
            string smtpPassword = _config.FromEmailPassword;
            string fromEmailAddress = _config.FromAddress;
            string fromDisplayName = "No Reply";


            if (string.IsNullOrEmpty(smtpServer))
            {
                throw new Exception("No SMTP Server location is defined.");
            }

            // Creating Mail Message
            MailMessage mail = new MailMessage();


            mail.From = new MailAddress(fromEmailAddress, fromDisplayName);
            mail.IsBodyHtml = true;

            mail.To.Add(toAddresses);


            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            if (ccAddresses != string.Empty)
            {
                mail.CC.Add(ccAddresses);
            }


            SmtpClient smtpClient = new SmtpClient(smtpServer);
            if (string.IsNullOrWhiteSpace(smtpUsername) || string.IsNullOrWhiteSpace(smtpPassword))
            {
                smtpClient.UseDefaultCredentials = true;
            }
            else
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            }

            smtpClient.Port = Convert.ToInt32(smtpPort);


            smtpClient.EnableSsl = smtpssl;

            smtpClient.Send(mail);
            return true;
        }

        /// <summary>
        /// Checks whether email is valid or not
        /// </summary>
        /// <param name="email">email address</param>
        /// <returns>True if valid; false if invalid</returns>
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(emailRegex);
            if (!re.IsMatch(email.Trim()))
            {
                return false;
            }

            return true;
        }
    }




}
