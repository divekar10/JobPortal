using JobPortal.Database.Repo;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IOtpService
    {
        Task<UserOtp> Add(UserOtp entity);
        Task<UserOtp> Validate(int otp);
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
            return await _otpRepository.AddAsync(entity);   
        }

        public async Task<UserOtp> Validate(int otp)
        {
           return await _otpRepository.GetDefault(x => x.Otp == otp && x.ExpireAt <= DateTime.Now.AddMinutes(10));
        }
    }
}
