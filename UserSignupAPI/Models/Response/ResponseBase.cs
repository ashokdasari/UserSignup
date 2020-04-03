using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSignup.Models.Response
{
    public class ResponseBase
    {

        public bool Success { get; set; }
        public UnmetExpectation[] UnmetExpectations { get; set; }

        public ResponseBase()
        {
            Success = true;
            UnmetExpectations = new UnmetExpectation[] { };
        }
    }
}
