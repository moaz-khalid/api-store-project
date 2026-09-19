using api_course_project.MiddleWares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Core.Entities.identity;
using Store.Repository.Data;
using Store.Repository.Data.Contexts;
using Store.Repository.identity;
using Store.Repository.identity.Contexts;

namespace api_course_project.Helper
{
    public static class ConfigureMiddleWare
    {
        public static async Task<WebApplication> ConfigureMiddleWareAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;  // create a scope to resolve scoped services like DbContext
            var context = services.GetRequiredService<StoreDbContext>();     // inject the StoreDbContext to apply migrations and seed data
            var identityContext = services.GetRequiredService<StoreIdentityDbContext>();    // inject the StoreIdentityDbContext to apply migrations and seed data
            var userManager = services.GetRequiredService<UserManager<AppUser>>();    // inject the UserManager to seed users
            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // inject the loggerFactory to log any errors during migration

            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
                await identityContext.Database.MigrateAsync();
                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "there are problems during apply migration !");
            }

            app.UseMiddleware<ExceptionMiddleWare>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();


            return app;
        }

    }
} 
