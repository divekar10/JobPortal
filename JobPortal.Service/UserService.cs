using JobPortal.Database.Repo;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Add(User entity)
        {
            var user = new User();
            user.Name = entity.Name;
            user.Email = entity.Email;
            user.Password = entity.Password;
            user.RoleId = entity.RoleId;
            user.CreatedAt = DateTime.Now;
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
    }
}
