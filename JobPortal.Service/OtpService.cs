using JobPortal.Database.Repo;
using JobPortal.Model;
using Serilog;
using System;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IOtpService
    {
        Task<UserOtp> Add(UserOtp entity);
        Task<UserOtp> Validate(int otp);
        Task<bool> IsOtpUnique(int otp);
    }
    public class OtpService : IOtpService
    {
        private readonly IOtpRepository _otpRepository;
        public OtpService(IOtpRepository otpRepository)
        {
            _otpRepository = otpRepository;
        }
        public async Task<UserOtp> Add(UserOtp entity)
        {
            try
            {
                return await _otpRepository.AddAsync(entity);
            }
            catch(Exception ex)
            {
                Log.Error("Error occurred : {0}", ex);
            }
            return null;
        }

        public async Task<UserOtp> Validate(int otp)
        {
            try
            {
                return await _otpRepository.GetDefault(x => x.Otp == otp && x.ExpireAt >= DateTime.Now);
            }
            catch(Exception ex)
            {
                Log.Error("Error occurred : {0}", ex);
            }
            return null;
        }

        public async Task<bool> IsOtpUnique(int otp)
        {
            var isUnique = await _otpRepository.GetDefault(x => x.Otp == otp);
            return isUnique == null ? true : false;
        }
    }
}
