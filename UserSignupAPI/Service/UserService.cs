using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserSignup.Helpers;
using UserSignup.Models.Domain;
using UserSignup.Models.Request;
using UserSignup.Models.Response;
using UserSignup.Repository.Interfaces;
using UserSignup.Service.Interfaces;
using UserSignup.Utilities;
using UserSignup.Utilities.Interfaces;

namespace UserSignup.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IEmailUtility _emailUtility;
        private readonly IHelperConfig _configHelper;
        public UserService(IUserRepository userRepo, IEmailUtility emailUtility, IHelperConfig configHelper)
        {
            _userRepo = userRepo;
            _emailUtility = emailUtility;
            _configHelper = configHelper;
        }
        public async Task<ResponseBase> Signup(SignupRequest signup)
        {
            var res = new ResponseBase();

            // Basic validation
            var unmetExceptions = new List<UnmetExpectation>();
            if (signup.Email == "")
            {
                unmetExceptions.Add(new UnmetExpectation("Email", "Email Required"));
            }
            else if (!_emailUtility.IsValidEmail(signup.Email))
            {
                unmetExceptions.Add(new UnmetExpectation("Email", "Email is not in Correct format"));
            }

            if (signup.Password == "")
            {
                unmetExceptions.Add(new UnmetExpectation("Password", "Password Required"));
            }
            else if (signup.Password.Length < _configHelper.PasswordMinLength)
            {
                unmetExceptions.Add(new UnmetExpectation("Password", $"Password should be minimum of {_configHelper.PasswordMinLength} characters"));
            }

            if (unmetExceptions.Count > 0)
            {
                res.Success = false;
                res.UnmetExpectations = unmetExceptions.ToArray();
                return res;
            }

            //Check if user already exists
            var existingUser = _userRepo.GetSignedupUser(signup.Email);
            if (existingUser != null)
            {
                res.Success = false;
                res.UnmetExpectations = new UnmetExpectation[]
                {
                    new UnmetExpectation("Email",  "User already exists with the given email")
                };

                return res;
            }


            try
            {
                var salt = Encoding.UTF8.GetBytes("@!#@YourSalt Goes Here@!#@#");
                var saltedHash = CommonUtility.GenerateSaltedHash(Encoding.UTF8.GetBytes(signup.Password), salt);
                var verificationCode = CommonUtility.GenerateVerificationCode();
                var user = new SignUpModel()
                {
                    Email = signup.Email,
                    Password = Convert.ToBase64String(saltedHash),
                    VerificationCode = verificationCode.ToString(),
                    PasswordSalt = Convert.ToBase64String(salt)
                };
                await _userRepo.Signup(user);

                _emailUtility.SendEmail(signup.Email, _configHelper.Subject, string.Format(_configHelper.Body, verificationCode), string.Empty);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.UnmetExpectations = new UnmetExpectation[]
                {
                    new UnmetExpectation("",ex.Message)
                };
            }

            return res;
        }

        public ValidateUserResponse Validate(LoginRequest loginRequest)
        {

            var res = new ValidateUserResponse();
            var unmetExceptions = new List<UnmetExpectation>();

            if (loginRequest.Email == "")
            {
                unmetExceptions.Add(new UnmetExpectation("Email", "Email Required"));
            }
            else if (!_emailUtility.IsValidEmail(loginRequest.Email))
            {
                unmetExceptions.Add(new UnmetExpectation("Email", "Email is not in correct format"));
            }

            if (loginRequest.Password == "")
            {
                unmetExceptions.Add(new UnmetExpectation("Password", "Password Required"));
            }


            if (loginRequest.VerificationCode == "")
            {
                unmetExceptions.Add(new UnmetExpectation("VerificationCode", "VerificationCode Required"));
            }

            if (unmetExceptions.Count > 0)
            {
                res.Success = false;
                res.UnmetExpectations = unmetExceptions.ToArray();
                return res;
            }

            var existingUser = _userRepo.GetSignedupUser(loginRequest.Email);
            if (existingUser == null)
            {
                res.Success = false;
                res.UnmetExpectations = new UnmetExpectation[]
                {
                    new UnmetExpectation("Email",  "User does not exists the given Email")
                };

                return res;
            }
            res.UserId = existingUser.UserId;
            var salt = Convert.FromBase64String(existingUser.PasswordSalt);
            var encryptedPassWord = Convert.ToBase64String(CommonUtility.GenerateSaltedHash(Encoding.UTF8.GetBytes(loginRequest.Password), salt));

            if (encryptedPassWord != existingUser.Password)
            {
                res.Success = false;
                res.UnmetExpectations = new UnmetExpectation[]
                {
                    new UnmetExpectation("Password",  "Password doesn't match with the password set during the signup")
                };


            }
            else if (loginRequest.VerificationCode != existingUser.VerificationCode)
            {
                res.Success = false;
                res.UnmetExpectations = new UnmetExpectation[]
                {
                    new UnmetExpectation("VerificationCode",  "Access doesn't match")
                };


            }
            return res;
        }

        public async Task<ResponseBase> SaveUserProfile(UserProfileRequest req)
        {
            var res = new ResponseBase();

            var unmetExceptions = new List<UnmetExpectation>();
            if (req.FullName == "")
            {
                unmetExceptions.Add(new UnmetExpectation("FullName", "FullName Required"));
            }

            if (req.Address == "")
            {
                unmetExceptions.Add(new UnmetExpectation("Address", "Address Required"));
            }

            if (req.ContactNumber == "")
            {
                unmetExceptions.Add(new UnmetExpectation("ContactNumber", $"ContactNumber is required"));
            }

            if (unmetExceptions.Count > 0)
            {
                res.Success = false;
                res.UnmetExpectations = unmetExceptions.ToArray();
                return res;
            }

            var user = _userRepo.GetSignedupUser(req.UserId);
            if (user == null)
            {
                res.Success = false;
                res.UnmetExpectations = new UnmetExpectation[]
                {
                    new UnmetExpectation("", "Invalid Operation. Make sure to login and update Profile")
                };
                return res;
            }

            UserProfileModel userProfile = new UserProfileModel
            {
                FullName = req.FullName,
                Address = req.Address,
                ContactNumber = req.ContactNumber,
                SendPromotions = req.SendPromotions,
                UserId = req.UserId
            };
            await _userRepo.SaveUserProfile(userProfile);
            return res;
        }
    }
}
