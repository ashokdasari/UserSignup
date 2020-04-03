using System.ComponentModel.DataAnnotations;

namespace UserSignup.Models.Entities
{
    public class SignedupUser
    {
        [Key]
        public int SignedupUserId { get; set; }

        public string Email { set; get; }
        
        public string Password { set; get; }

        public string PasswordSalt { set; get; }

        public string VerificationCode { get; set; }
    }
}
