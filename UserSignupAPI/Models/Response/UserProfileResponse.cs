using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSignup.Models.Response
{
    public class UserProfileResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public bool SendPromotions { get; set; }
    }
}
