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
        IEnumerable<User> GetRecruiters();
        IEnumerable<User> GetCandidates();
        Task<User> Update(User entity);
        Task<bool> Delete(int id);
        Task<User> GetUserByMail(string email);
        Task<bool> ForgotPassword(string email);
        Task<User> ResetPassword(int otp, string newPassword, string confirmPassword);
        Task<IEnumerable<AppliedJobDto>> GetMyAllJobsApplied(int userId);
        Task<bool> IsEmailAlreadyExist(string email);
    }
}
