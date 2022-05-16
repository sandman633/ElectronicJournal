using ElectronicJournal.Web.Repositories.Implementations;
using ElectronicJournal.Web.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicJournal.Web.Repositories.Config
{
    public static class RepositoryConfig
    {
        public static void RepositoryConfiguration(this IServiceCollection services)
        {
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ICourseRequestRepository, CourseRequestRepository>();
            services.AddTransient<ICourseStatusRepository, CourseStatusRepository>();
            services.AddTransient<ICourseTypeRepository, CourseTypeRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();
            services.AddTransient<IUserCourseRepository, UserCourseRepository>();
        }
    }
}
