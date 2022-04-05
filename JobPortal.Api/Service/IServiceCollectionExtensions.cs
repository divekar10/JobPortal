using JobPortal.Database.Repo;
using JobPortal.Service;
using JobPortal.Service.Notifications;
using Microsoft.Extensions.DependencyInjection;

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
                .AddScoped<IRoleRepository, RoleRepository>();
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
                .AddScoped<IRecruiterService, RecruiterService>();
        }
    }
}
