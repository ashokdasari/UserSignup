using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSignup.Models.Response
{
    public class UnmetExpectation
    {
        public string Key
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }

        public UnmetExpectation(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}
