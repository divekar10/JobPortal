﻿using JobPortal.Model;
using JobPortal.Model.Email;
using JobPortal.Model.Menu;
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
        public DbSet<PermissionRoleMapping> PermissionRoleMapping { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<MenuMaster> MenuMaster { get; set; }
        public DbSet<SubMenuMaster> SubMenuMaster { get; set; }
        public DbSet<SubMenuChildMaster> SubMenuChildMaster { get; set; }
        public DbSet<UserMenuMapping> UserMenuMapping { get; set; }
    }
}
