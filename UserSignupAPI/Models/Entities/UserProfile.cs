using System.ComponentModel.DataAnnotations.Schema;

namespace UserSignup.Models.Entities
{
    public class UserProfile
    {
        
        public int UserProfileId { get; set; }

        public string FullName { get; set; }

        public string ContactNumber { get; set; }

        public string Address { get; set; }

        public bool SendPromotions { get; set; }

        [ForeignKey("SignedUpUser")]
        public int SignedupUserId { get; set; }
        public virtual SignedupUser SignedupUser { get; set; }
    }
}
