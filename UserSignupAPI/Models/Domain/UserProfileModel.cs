namespace UserSignup.Models.Domain
{
    public class UserProfileModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public bool SendPromotions { get; set; }
    }
}
