using JobPortal.Database.Repo;
using JobPortal.Model;
using JobPortal.Service.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly IOtpService _otpService;
        public UserService(IUserRepository userRepository, IEmailSender emailSender, IOtpService otpService)
        {
            _userRepository = userRepository;
            _emailSender = emailSender;
            _otpService = otpService;
        }
        public async Task<User> Add(User entity)
        {
            var user = new User();
            user.Name = entity.Name;
            user.Email = entity.Email;
            user.Password = entity.Password;
            user.RoleId = entity.RoleId;
            user.CreatedAt = DateTime.Now;
            user.IsActive = true;
            return await _userRepository.AddAsync(user);
        }

        public async Task<IEnumerable<User>> AddUsers(List<User> entities)
        {
            IEnumerable<User> user = await _userRepository.AddAsync(entities);
            return user;
        }

        public async Task<bool> Delete(int id)
        {
            User userId = await _userRepository.GetById(id);

            if(userId != null)
            {
                _userRepository.Delete(userId);
                return true;
            }
            return false;
        }

        public async Task<bool> ForgotPassword(string email)
        {
            var user = await GetUserByMail(email);
            if(user != null)
            {
                var otp = GenerateRandomNo();
                var to = user.Email;
                var sub = "OTP";
                var body = "OTP is : " + otp;

                var userOtp = new UserOtp();
                userOtp.Otp = Convert.ToInt32(otp);
                userOtp.UserId = user.Id;
                userOtp.CreatedAt = DateTime.Now;
                userOtp.ExpireAt = DateTime.Now.AddMinutes(10);

                await _otpService.Add(userOtp);
                await _emailSender.SendEmailAsync(to,sub,body);
                return true;
            }
            return false;
        }

        public IEnumerable<User> GetCandidates()
        {
            return _userRepository.GetCandidates();
        }

        public IEnumerable<User> GetRecruiters()
        {
            return _userRepository.GetRecruiters();
        }

        public async Task<User> GetUser(string email, string password)
        {
            try
            {
                return await _userRepository.GetDefault(x => x.Email == email && x.Password == password);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<User> GetUserByMail(string email)
        {
            try
            {
                var user = await _userRepository.GetDefault(x => x.Email == email);
                if(user != null)
                {
                    return user;
                }
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.Get();
        }

        public async Task<User> Update(User entity)
        {
            User _user = await _userRepository.GetById(entity.Id);

            if(entity != null)
            {
                _user.Name = entity.Name;
                _user.Email = entity.Email;
                _user.Password = entity.Password;
                _user.IsActive = entity.IsActive;
                _userRepository.Update(_user);

                return _user;
            }
            return entity;
        }

        private static Random _random = new Random();
        private static string GenerateRandomNo()
        {
            return _random.Next(0, 999999).ToString("D6");
        }

        public async Task<User> ResetPassword(int otp, string newPassword, string confirmPassword)
        {
            var details = await ValidateOtp(otp);
            var updateUser = new User();
            if (details != null)
            {
                updateUser = await _userRepository.GetById(details.UserId);

                if (newPassword == confirmPassword)
                {
                    updateUser.Password = newPassword;
                    _userRepository.Update(updateUser);
                    return updateUser;
                }
                
            }
            return null;
        }

        private async Task<UserOtp> ValidateOtp(int otp)
        {
            return await _otpService.Validate(otp);
        }

        public static int GetCurrentUserId()
        {
            
        }
    }
}
