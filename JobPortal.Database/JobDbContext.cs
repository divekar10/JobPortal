using JobPortal.Model;
using JobPortal.Model.Email;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Database
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions options) : base(options)
        {

        }
        public JobDbContext()
        {
           
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role{ get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<UserOtp> UserOtp { get; set; }
        public DbSet<ExceptionLog> ExceptionLog { get; set; }
        public DbSet<EmailTemplate> EmailTemplate { get; set; }
    }
}
