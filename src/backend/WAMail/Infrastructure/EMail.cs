using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace WAMail.Infrastructure
{
    public class EMail
    {
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string Message { get; set; }
        public async Task<Result> Send()
        {

            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("recipient@gmail.com"));  // replace with valid value  
            message.From = new MailAddress("sender@outlook.com");  // replace with valid value 
            message.Subject = "Your email subject";
            message.Body = string.Format(body, this.FromName, this.FromEmail, this.Message);
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                var result = new Result(false);
                try
                {
                    await smtp.SendMailAsync(message);

                    result.ok = true;
                    return result;
                }
                catch(Exception ex)
                {
                    result.CreateResultException(ex);
                    return result;
                }
            }
        }
    }
}