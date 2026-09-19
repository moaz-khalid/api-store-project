using api_course_project.Errors;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Store.Core;
using Store.Core.Mapping.Baskets;
using Store.Core.Mapping.Products;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Repository;
using Store.Repository.Data.Contexts;
using Store.Repository.Repositories;
using Store.Service.Services.Products;
using Store.Service.Services.Caches;
using Store.Repository.identity.Contexts;
using Store.Core.Entities.identity;
using Microsoft.AspNetCore.Identity;

namespace api_course_project.Helper
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInService(); 
            services.AddSwaggerService();
            services.AddDbContextService(configuration);
            services.AddUserDefinedService();
            services.AddAutoMapperService(configuration);
            services.ConfigureInvalidModelStateResponseService();
            services.AddRedisService(configuration);
            services.AddIdentityService();

            return services;
        }


        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }

        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });






            return services;
        }

        private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
        {

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IBasketRepository,BasketRepository>();
            return services;
        }

        private static IServiceCollection AddAutoMapperService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new BasketProfile()));
            return services;
        }

        private static IServiceCollection ConfigureInvalidModelStateResponseService(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {

                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                            .SelectMany(p => p.Value.Errors)
                                            .Select(E => E.ErrorMessage)
                                            .ToArray();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }

        private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
            {
                var connection = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });


            return services;
        }

        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }





    }
}
