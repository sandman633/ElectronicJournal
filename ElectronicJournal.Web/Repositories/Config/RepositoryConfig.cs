using ElectronicJournal.Web.Repositories.Implementations;
using ElectronicJournal.Web.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicJournal.Web.Repositories.Config
{
    public static class RepositoryConfig
    {
        public static void RepositoryConfiguration(this IServiceCollection services)
        {
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseRequestRepository, CourseRequestRepository>();
            services.AddScoped<ICourseStatusRepository, CourseStatusRepository>();
            services.AddScoped<ICourseTypeRepository, CourseTypeRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IUserGroupRepository, UserGroupRepository>();
            services.AddScoped<IUserCourseRepository, UserCourseRepository>();
        }
    }
}
