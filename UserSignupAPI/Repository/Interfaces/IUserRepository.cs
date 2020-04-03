using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSignup.Models.Domain;

namespace UserSignup.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task Signup(SignUpModel signup);
        SignUpModel GetSignedupUser(string Email);
        SignUpModel GetSignedupUser(int UserId);
        Task SaveUserProfile(UserProfileModel userProfile);
    }
}
