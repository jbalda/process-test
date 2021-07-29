using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.EmailProvider
{
    public interface IEmailSender
    {
        public System.Threading.Tasks.Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
