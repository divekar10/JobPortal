using JobPortal.Database.Repo;
using BCryptNet = BCrypt.Net.BCrypt;
using JobPortal.Model;
using JobPortal.Service.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

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
            try
            {
                var user = new User();
                user.Name = entity.Name;
                user.Email = entity.Email.ToLower();
                user.Password = BCryptNet.HashPassword(entity.Password);
                user.RoleId = 1;
                user.CreatedAt = DateTime.Now;
                user.IsActive = true;
                return await _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred : {0}", ex);
            }
            return null;
        }

        public async Task<IEnumerable<User>> AddUsers(List<User> entities)
        {
            try
            {
                IEnumerable<User> user = await _userRepository.AddAsync(entities);
                return user;
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred : {0}", ex);
            }
            return null;
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                User userId = await _userRepository.GetById(id);
                if (userId != null)
                {
                    _userRepository.Delete(userId);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred : {0}", ex);
            }
            return false;
            
        }

        public async Task<bool> ForgotPassword(string email)
        {
            try
            {
                var user = await GetUserByMail(email);
                if (user != null)
                {
                    regenerate:
                    var otp = Convert.ToInt32(GenerateRandomNo());
                    var isUnique = await _otpService.IsOtpUnique(otp);
                    if (!isUnique)
                        goto regenerate;

                    var to = user.Email;
                    var sub = "OTP";
                    var body = "";
                    body += "<h3>Job Portal</h3>";
                    body += $"<h4 style='font-size:1.1em'>Hi, {user.Name}</h4>";
                    body += "<h5>For Reseting your password, OTP is valid for 10 minutes</h5>";
                    body += $"<h2 style='background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;'>{otp}</h2>";

                    var userOtp = new UserOtp();
                    userOtp.Otp = Convert.ToInt32(otp);
                    userOtp.UserId = user.Id;
                    userOtp.CreatedAt = DateTime.Now;
                    userOtp.ExpireAt = DateTime.Now.AddMinutes(10);

                    await _otpService.Add(userOtp);
                    await _emailSender.SendEmailAsync(to, sub, body);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetCandidates(PagedParameters pagedParameters)
        {
            return await _userRepository.GetCandidates(pagedParameters);
        }

        public async Task<User> GetUser(string email, string password)
        {
            try
            {
                var user = await _userRepository.GetDefault(x => x.Email == email);
                if (user != null)
                {
                    if (!BCryptNet.Verify(password, user.Password))
                    {
                        return null;
                    }
                    return user;
                }
                return user;
            }
            catch (Exception ex)
            {
               Log.Error("Error occurred : {0}", ex);
            }
            return null;
        }

        public async Task<User> GetUserByMail(string email)
        {
            try
            {
                var user = await _userRepository.GetDefault(x => x.Email == email);
                if (user != null)
                {
                    return user;
                }
                return user;
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred : {0}", ex);
            }
            return null;
        }

        public async Task<IEnumerable<User>> GetUsers(PagedParameters pagedParameters)
        {
            //return await _userRepository.Get();
            return await _userRepository.GetUsers(pagedParameters);
        }

        public async Task<User> Update(User entity)
        {
            try
            {
                User _user = await _userRepository.GetById(entity.Id);
                if (_user != null)
                {
                    _user.Name = entity.Name;
                    _user.Email = entity.Email;
                    _user.Password = entity.Password;
                    _user.RoleId = 1;
                    _user.IsActive = entity.IsActive;
                    _userRepository.Update(_user);
                    return _user;
                }
                return _user;
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred : {0}", ex);
            }
            return null;
        }

        private static Random _random = new Random();
        private static string GenerateRandomNo()
        {
            return _random.Next(0, 999999).ToString("D6");
        }

        public async Task<User> ResetPassword(int otp, string newPassword, string confirmPassword)
        {
            try
            {
                var details = await ValidateOtp(otp);
                var updateUser = new User();
                if (details != null)
                {
                    updateUser = await _userRepository.GetById(details.UserId);
                    if (newPassword == confirmPassword)
                    {
                        updateUser.Password = BCryptNet.HashPassword(newPassword);
                        _userRepository.Update(updateUser);
                        return updateUser;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred : {0}", ex);
            }
            return null;
        }

        private async Task<UserOtp> ValidateOtp(int otp)
        {
            return await _otpService.Validate(otp);
        }

        public async Task<IEnumerable<AppliedJobDto>> GetMyAllJobsApplied(int userId, PagedParameters pagedParameters)
        {
            return await _userRepository.GetMyAllJobsApplied(userId, pagedParameters);
        }

        public async Task<bool> IsEmailAlreadyExist(string email)
        {
            var user = await _userRepository.GetDefault(x => x.Email == email);
            if (user != null)
                return true;
            return false;
        }
    }
}
