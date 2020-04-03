using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSignup.Models.Response
{
    public class ValidateUserResponse : ResponseBase
    {
        public int UserId { get; set; }
    }
}
