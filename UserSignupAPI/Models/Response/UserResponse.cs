using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSignup.Models.Response
{
    public class UserResponse
    {
        public int UserId { get; set; }

        public string Email { set; get; }

        public string Password { set; get; }

        public string PasswordSalt { set; get; }

        public string VerificationCode { get; set; }

        public UserProfileResponse UserProfile { get; set; }
    }
}
