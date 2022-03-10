using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IUserService
    {
        Task<User> GetUser(string email, string password);
        Task<User> Add(User entity);
        Task<IEnumerable<User>> AddUsers(List<User> entities);
        Task<IEnumerable<User>> GetUsers();
        Task<User> Update(User entity);
        Task<bool> Delete(int id); 
    }
}
