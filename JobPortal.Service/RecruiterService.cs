using JobPortal.Database.Repo;
using BCryptNet = BCrypt.Net.BCrypt;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

namespace JobPortal.Service
{
    public class RecruiterService : IRecruiterService
    {
        private readonly IUserRepository _userRepository;
        public RecruiterService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Add(User entity)
        {
            try
            {
            var user = new User();
            user.Name = entity.Name;
            user.Email = entity.Email;
            user.Password = BCryptNet.HashPassword(entity.Password);
            user.RoleId = 2;
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

        public async Task<IEnumerable<User>> GetRecruiters(PagedParameters pagedParameters)
        {
            return await _userRepository.GetRecruiters(pagedParameters);
        }
    }
}
