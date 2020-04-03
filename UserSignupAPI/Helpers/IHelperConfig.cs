using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSignup.Helpers
{
    public interface IHelperConfig
    {
        string FromAddress { get; }
        string Subject { get; }
        string Body { get; }
        int SmtpPort { get; }
        string SmtpServer { get; }
        string FromEmailAddress { get; }
        string FromEmailPassword { get; }
        int PasswordMinLength { get; }
    }
}
