using api_course_project.Helper;
using Microsoft.EntityFrameworkCore;
using Store.Core;
using Store.Core.Mapping.Products;
using Store.Core.Services.Contract;
using Store.Repository;
using Store.Repository.Data;
using Store.Repository.Data.Contexts;
using Store.Service.Services.Products;



namespace api_course_project

{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDependency(builder.Configuration);


            //update-database
            var app = builder.Build();


            await app.ConfigureMiddleWareAsync();


            app.Run();
        }
    }
}