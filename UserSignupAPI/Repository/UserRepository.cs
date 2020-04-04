using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSignup.Models.Domain;
using UserSignup.Models.Entities;
using UserSignup.Repository.Interfaces;
using UserSignup.Utilities;

namespace UserSignup.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly UserManagementContext _context;


        public UserRepository(UserManagementContext context)
        {
            _context = context;
        }

        public async Task Signup(SignUpModel signup)
        {
            //SignedupUser user = EncryptPassword(signup.Email, signup.Password)
            //Note: Can use third party library such as AutoMapper to map Domain model to Entity;
            SignedupUser user = new SignedupUser()
            {
                Email = signup.Email,
                Password = signup.Password,
                PasswordSalt = signup.PasswordSalt,
                VerificationCode = signup.VerificationCode
            };

            _context.SignedupUser.Add(user);
            await _context.SaveChangesAsync();
        }

        public SignUpModel GetSignedupUser(string Email)
        {

            var user = _context.SignedupUser.FirstOrDefault<SignedupUser>(u => u.Email == Email);
            if (user == null)
            {
                return null;
            }
            return new SignUpModel
            {
                UserId = user.SignedupUserId,
                Email = user.Email,
                Password = user.Password,
                VerificationCode = user.VerificationCode,
                PasswordSalt = user.PasswordSalt
            };

        }
        public SignUpModel GetSignedupUser(int UserId)
        {

            var user = _context.SignedupUser.FirstOrDefault<SignedupUser>(u => u.SignedupUserId == UserId);
            if (user == null)
            {
                return null;
            }
            return new SignUpModel
            {
                UserId = user.SignedupUserId,
                Email = user.Email,
                Password = user.Password,
                VerificationCode = user.VerificationCode,
                PasswordSalt = user.PasswordSalt
            };
        }

        public async Task SaveUserProfile(Models.Domain.UserProfileModel userProfile)
        {
            var user = _context.SignedupUser.FirstOrDefault<SignedupUser>(u => u.SignedupUserId == userProfile.UserId);

            var existingUerprof = _context.UserProfile.FirstOrDefault<UserProfile>(u => u.SignedupUserId == userProfile.UserId);

            if (existingUerprof != null)
            {
                existingUerprof.Address = userProfile.Address;
                existingUerprof.FullName = userProfile.FullName;
                existingUerprof.ContactNumber = userProfile.ContactNumber;
                existingUerprof.SendPromotions = userProfile.SendPromotions;
                existingUerprof.SignedupUser = user;
                _context.UserProfile.Update(existingUerprof);
            }
            else
            {
                //Note: Can use third party library such as AutoMapper to map Domain model to Entity;
                var userProf = new Models.Entities.UserProfile()
                {
                    Address = userProfile.Address,
                    FullName = userProfile.FullName,
                    ContactNumber = userProfile.ContactNumber,
                    SendPromotions = userProfile.SendPromotions,
                    SignedupUser = user
                };
                _context.UserProfile.Add(userProf);
            }

            await _context.SaveChangesAsync();
        }

        public List<SignUpModel> GetAllUsers()
        {
            var users = _context.SignedupUser;
            foreach (var user in users)
            {
                _context.Entry(user).Reference(u => u.UserProfile).Load();
            }


            return users.ToList().Select(u => new SignUpModel()
            {
                UserId = u.SignedupUserId,
                Email = u.Email,
                Password = u.Password,
                PasswordSalt = u.PasswordSalt,
                VerificationCode = u.VerificationCode,
                UserProfile = new UserProfileModel
                {
                    Address = u.UserProfile != null ? u.UserProfile.Address : "",
                    ContactNumber = u.UserProfile != null ? u.UserProfile.ContactNumber : "",
                    FullName = u.UserProfile != null ? u.UserProfile.FullName : "",
                    SendPromotions = u.UserProfile != null ? u.UserProfile.SendPromotions : false
                }

            }).ToList();
        }

    }
}
