using Microsoft.EntityFrameworkCore;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Extensions;
using TatBlog.WebApp.Mapster;

namespace TatBlog.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder
                    .ConfigureMvc()
                    .ConfigureServices()
                    .ConfigureMapster();
            };

            var app = builder.Build();
            {
                app.UseRequestPipeLine();
                app.UseBlogRoutes();
                app.UseDataSeeder();
            }

            app.Run();
        }
    }
}