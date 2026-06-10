using Snackis.Application.Interfaces;
using Snackis.Application.Services;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Repositories;
using Snackis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Snackis.Domain.Entities;

namespace Snackis.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddDbContext<MyDbContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddScoped<IPostService, PostService>();
            //builder.Services.AddScoped<ITopicService, TopicService>();
            //builder.Services.AddScoped<ICategoryService, CategoryService>();
            //builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();

            //builder.Services.AddScoped<IPostReposistory, PostReposistory>();
            //builder.Services.AddScoped<ITopicRepository, TopicRepository>();
            //builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            //builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();

            //builder.Services.AddIdentityCore<ApplicationUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<MyDbContext>();

            //builder.Services.AddAuthentication()
            //    .AddBearerToken(IdentityConstants.BearerScheme);

            //builder.Services.AddAuthorizationBuilder();

            //builder.Services.AddControllers();
            //builder.Services.AddOpenApi();
            //builder.Services.AddSwaggerGen();

            //var app = builder.Build();

            //if (app.Environment.IsDevelopment())
            //{
            //    app.MapOpenApi();
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //app.UseHttpsRedirection();

            //app.UseAuthentication();

            //app.UseAuthorization();

            //app.MapControllers();

            //app.Run();
        }
    }
}
