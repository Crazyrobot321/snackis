using Microsoft.EntityFrameworkCore;
using Snackis.Application.Interfaces;
using Snackis.Application.Interfaces.Api;
using Snackis.Application.Services;
using Snackis.Application.Services.Api;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;
using Snackis.Infrastructure.Repositories;

namespace Snackis.API
{
    public static class AppServiceRegistration
    {
        public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            string ConnectionUri = "https://erikssnackisforum.azurewebsites.net/";

            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IPrivateMessageService, PrivateMessageService>();
            services.AddScoped<IReportService, ReportService>();

            services.AddScoped<IPostReposistory, PostReposistory>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IPrivateMessageRepository, PrivateMessageRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();

            services.AddScoped<IReportServiceApi, ReportServiceApi>();
            services.AddScoped<ITopicServiceApi, TopicServiceApi>();
            services.AddScoped<ICategoryServiceApi, CategoryServiceApi>();
            services.AddScoped<ISubCategoryServiceApi, SubCategoryServiceApi>();
            services.AddScoped<IPostServiceApi, PostServiceApi>();
            services.AddScoped<IPrivateMessageServiceApi, PrivateMessageServiceApi>();

            services.AddHttpClient<ITopicServiceApi, TopicServiceApi>(client =>
            {
                client.BaseAddress = new Uri(ConnectionUri);
            });
            services.AddHttpClient<ICategoryServiceApi, CategoryServiceApi>(client =>
            {
                client.BaseAddress = new Uri(ConnectionUri);
            });
            services.AddHttpClient<ISubCategoryServiceApi, SubCategoryServiceApi>(client =>
            {
                client.BaseAddress = new Uri(ConnectionUri);
            });
            services.AddHttpClient<IPostServiceApi, PostServiceApi>(client =>
            {
                client.BaseAddress = new Uri(ConnectionUri);
            });
            services.AddHttpClient<IPrivateMessageServiceApi, PrivateMessageServiceApi>(client =>
            {
                client.BaseAddress = new Uri(ConnectionUri);
            });
            services.AddHttpClient<IReportServiceApi, ReportServiceApi>(client =>
            {
                client.BaseAddress = new Uri(ConnectionUri);
            });
            services.AddControllers();
        }
    }
}
