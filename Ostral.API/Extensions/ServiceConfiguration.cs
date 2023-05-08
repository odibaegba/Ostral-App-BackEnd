using Ostral.Core.Implementations;
using Ostral.Core.Interfaces;
using Ostral.Infrastructure.EmailService;
using Ostral.Infrastructure.Repository;

namespace Ostral.API.Extensions
{
    public static class ServiceConfiguration
    {
        public static void AddServicesExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDigitTokenService, DigitTokenService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmailService, SmtpEmailService>();
            
            services.AddScoped<IIdentityService, IdentityService>();
                
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseService, CourseService>();
            
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            
            services.AddScoped<ITutorRepository, TutorRepository>();
            services.AddScoped<ITutorService, TutorService>();

            services.AddScoped<ITutorCourseRepository, TutorCourseRepository>();
            services.AddScoped<ITutorCourseService, TutorCourseService>();
            
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();
            
            services.AddScoped<ICategoryCourseRepository, CategoryCourseRepository>();
            services.AddScoped<ICategoryCourseService, CategoryCourseService>();

            services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
            services.AddScoped<IStudentCourseService, StudentCourseService>();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IContentRepository, ContentRespository>();
            services.AddScoped<IContentService, ContentService>();
        }
    }
}