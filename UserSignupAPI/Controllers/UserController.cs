using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserSignup.Models.Domain;
using UserSignup.Models.Request;
using UserSignup.Models.Response;
using UserSignup.Service.Interfaces;

namespace UserSignup.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ResponseBase> Signup([FromBody] SignupRequest signup)
        {
            return await _userService.Signup(signup);
        }


        [HttpPost]
        [Route("validate")]
        public ValidateUserResponse Login([FromBody] LoginRequest loginRequest)
        {
            return _userService.Validate(loginRequest);
        }

        [HttpPost]
        [Route("save")]
        public async Task<ResponseBase> SaveProfile([FromBody] UserProfileRequest signup)
        {
            return await _userService.SaveUserProfile(signup);
        }
    }
}
