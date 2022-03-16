using JobPortal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IUserService
    {
        Task<User> GetUser(string email, string password);
        Task<User> Add(User entity);
        Task<IEnumerable<User>> AddUsers(List<User> entities);
        Task<IEnumerable<User>> GetUsers(PagedParameters pagedParameters);
        Task<IEnumerable<User>> GetCandidates(PagedParameters pagedParameters);
        Task<User> Update(User entity);
        Task<bool> Delete(int id);
        Task<User> GetUserByMail(string email);
        Task<bool> ForgotPassword(string email);
        Task<User> ResetPassword(int otp, string newPassword, string confirmPassword);
        Task<IEnumerable<AppliedJobDto>> GetMyAllJobsApplied(int userId, PagedParameters pagedParameters);
        Task<bool> IsEmailAlreadyExist(string email);
    }
}
