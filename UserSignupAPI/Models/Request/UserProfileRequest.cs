namespace UserSignup.Models.Request
{
    public class UserProfileRequest
    {         
        public int UserId { set; get; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public bool SendPromotions { set; get;  }
    }
}
