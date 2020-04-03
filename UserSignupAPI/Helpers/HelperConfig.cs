using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSignup.Helpers
{
    public class HelperConfig: IHelperConfig
    {
        private readonly IConfiguration _config;

        public HelperConfig(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string FromAddress
        {
            get
            {
                return _config.GetValue<string>("FromAddress");
            }
        }

        public string Subject
        {
            get
            {
                return _config.GetValue<string>("Subject");
            }
        }

        public string Body
        {
            get
            {
                return _config.GetValue<string>("Body");
            }
        }

        public string FromEmailAddress
        {
            get
            {
                return _config.GetValue<string>("FromEmailAddress");
            }
        }

        public string SmtpServer
        {
            get
            {
                return _config.GetValue<string>("SmtpServer");
            }
        }

        public int SmtpPort
        {
            get
            {
                return _config.GetValue<int>("SmtpPort");
            }
        }

        public string FromEmailPassword
        {
            get
            {
                return _config.GetValue<string>("FromEmailPassword");
            }
        }

        public int PasswordMinLength
        {
            get
            {
                return int.Parse(_config.GetValue<string>("PasswordMinLength"));
            }
        }

        
    }
}
