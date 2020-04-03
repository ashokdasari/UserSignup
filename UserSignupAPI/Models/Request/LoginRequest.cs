namespace UserSignup.Models.Request
{
    public class LoginRequest
    {
        public string Email { set; get; }
        public string Password { set; get; }
        public string VerificationCode { set; get; }
    }
}
