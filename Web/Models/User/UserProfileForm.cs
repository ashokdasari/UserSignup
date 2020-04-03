using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.User
{
    public class UserProfileForm
    {
        public int UserId { set; get; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
    }
}