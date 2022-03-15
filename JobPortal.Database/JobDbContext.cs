using JobPortal.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Database
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role{ get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<UserOtp> UserOtp { get; set; }
    }
}
