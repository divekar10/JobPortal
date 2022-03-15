using JobPortal.Database.Repo;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            user.Password = entity.Password;
            user.RoleId = 2;
            user.CreatedAt = DateTime.Now;
            user.IsActive = true;
            return await _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<User> GetRecruiters()
        {
            return _userRepository.GetRecruiters();
        }
    }
}
