using JobPortal.Database.Repo;
using JobPortal.Service;
using JobPortal.Service.Caching;
using JobPortal.Service.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JobPortal.Api.Service
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IJobRepository, JobRepository>()
                .AddScoped<IApplicantRepository, ApplicantRepository>()
                .AddScoped<IOtpRepository, OtpRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IEmailTemplateRepository, EmailTemplateRepository>()
                .AddScoped<IPermissionRoleMappingRepository, PermissionRoleMappingRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IJobService, JobService>()
                .AddScoped<IApplicantService, ApplicantService>()
                .AddScoped<IEmailSender, Notification>()
                .AddScoped<IOtpService, OtpService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IRecruiterService, RecruiterService>()
                .AddScoped<ICacheManager, MemoryCacheManager>()
                .AddScoped<IPermissionRoleMappingService, PermissionRoleMappingService>()
                .AddScoped<IPermissionService, PermissionService>(); 
        }
    }
}
