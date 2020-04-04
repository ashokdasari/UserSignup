using System.Collections.Generic;
using System.Threading.Tasks;

using UserSignup.Models.Domain;
using UserSignup.Models.Request;
using UserSignup.Models.Response;

namespace UserSignup.Service.Interfaces
{
    public interface IUserService
    {
        Task<ResponseBase> Signup(SignupRequest signup);
        ValidateUserResponse Validate(LoginRequest signup);
        Task<ResponseBase> SaveUserProfile(UserProfileRequest signup);
        List<UserResponse> GetAllUsers();
    }
}
