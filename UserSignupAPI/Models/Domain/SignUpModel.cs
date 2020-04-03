namespace UserSignup.Models.Domain
{
    public class SignUpModel
    {
        public int UserId { get; set; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string PasswordSalt { set; get; }
        public string VerificationCode { get; set; }
    }
}
