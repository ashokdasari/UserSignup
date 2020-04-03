using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSignup.Utilities.Interfaces
{
    public interface IEmailUtility
    {
        public bool SendEmail(string toAddresses, string subject, string body, string ccAddresses);

        bool IsValidEmail(string email);
    }
}
