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
        public string Body { get; set; }
        public string To { get; set; }
        public string DisplayName { get; set; }
        public string Subject { get; set; }
        public async Task<Result> Send()
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(this.To, this.DisplayName));
            message.Subject = this.Subject;
            message.Body = this.Body;
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