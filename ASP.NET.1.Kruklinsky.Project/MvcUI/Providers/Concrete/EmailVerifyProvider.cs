using MvcUI.Providers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace MvcUI.Providers.Concrete
{
    public class EmailVerifyProvider : IVerifyProvider
    {
        private Regex emailValidationRegex = new Regex(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");

        private string verifyUrl = "http://localhost:3868//Verify/Verify";

        public EmailVerifyProvider(string verifyUrl)
        {
            this.verifyUrl = verifyUrl;
        }

        public EmailVerifyProvider(string emailRegularExpression, string verifyUrl)
        {
            this.verifyUrl = verifyUrl;
            this.emailValidationRegex = new Regex(emailRegularExpression);
        }

        #region IVerifyProvider

        public bool IsEmail(string email)
        {
            bool result = true;
            if (string.IsNullOrWhiteSpace(email))
            {
                result = false;
            }
            if (!this.emailValidationRegex.IsMatch(email))
            {
                result = false;
            }
            return result;
        }
        public bool IsVerifying(string email)
        {
            bool result = false;
            if (IsEmail(email))
            {
                var user = Membership.GetUser(email);
                if (user != null)
                {
                    if (!user.IsApproved)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        public bool IsApproved(string email)
        {
            bool result = false;
            if (IsEmail(email))
            {
                var user = Membership.GetUser(email);
                if (user != null)
                {
                    if (user.IsApproved)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        public void SendVerifyEmail (string email)
        {
            if (this.IsEmail(email) && this.IsVerifying(email))
            {
                SendEmail(email);
            }
        }
        public bool Verify(string email, string secretCode)
        {
            bool result = false;
            if (this.IsEmail(email) && this.IsVerifying(email))
            {
                var user = Membership.GetUser(email);
                if (user != null)
                {
                    if (!user.IsApproved && user.ProviderUserKey.ToString() == secretCode)
                    {
                        user.IsApproved = true;
                        Membership.Provider.UpdateUser(user);
                        result = this.IsApproved(email);
                    }
                }
            }
            return result;
        }

        #endregion

        #region Private methods

        private void SendEmail(string username)
        {
            var user = Membership.GetUser(username);
            string link = this.verifyUrl +
                            "/" +
                            user.UserName +
                            "/" +
                            user.ProviderUserKey.ToString();

            MailMessage sendingMessage = new MailMessage
            (
                "BNTU.SocialNetwork@gmail.com",
                user.Email,
                "Activation",
                "<a href = \"" + link + "\"> Activate account</a>"
            );

            sendingMessage.IsBodyHtml = true;
            SendMail(sendingMessage, "smtp.gmail.com", "bntu.socialnetwork@gmail.com", "DFyufhtibnNJ");
        }

        /// <summary>
        /// Sends a message with using SMTP Server.
        /// </summary>
        /// <param name="message">Message contains: sender's email, addressee's email, mails content.</param>
        /// <param name="host">SMTP Server hostname or IP address.</param>
        /// <param name="username">Username for account on SMTP Server. Defaults sender's email.</param>
        /// <param name="password">Password for account on SMTP Server.</param>
        /// <param name="port">The port used for SMTP transactions.</param>
        private void SendMail(MailMessage message, string host, string username, string password, int port = 587)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Host = host;
                smtpClient.Port = port;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(username, password);
                try
                {
                    smtpClient.Send(message);
                }
                catch (System.Net.Mail.SmtpException e)
                {
                    string errorMessage = e.Message;
                }
            }
        }

        #endregion
    }
}