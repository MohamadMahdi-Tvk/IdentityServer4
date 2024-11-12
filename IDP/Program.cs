using IdentityServer4.Models;
using IDP.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace IDP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string cs = @"Data Source=.;Initial Catalog=IdentityServer;Integrated Security=true; TrustServerCertificate=true";
            var miAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddTestUsers(ConfigIdentityServer.GetUsers())
                .AddConfigurationStore(option =>
                {
                    option.ConfigureDbContext = b =>
                     b.UseSqlServer(cs, sql => sql.MigrationsAssembly(miAssembly));
                })
                .AddOperationalStore(option =>
                {
                    option.ConfigureDbContext = b =>
                    b.UseSqlServer(cs, sql => sql.MigrationsAssembly(miAssembly));
                    option.EnableTokenCleanup = true;
                });

            //.AddInMemoryIdentityResources(ConfigIdentityServer.GetIdentityResources())
            //.AddInMemoryApiResources(ConfigIdentityServer.GetApiResources())
            //.AddInMemoryClients(ConfigIdentityServer.GetClients())
            //.AddInMemoryApiScopes(ConfigIdentityServer.GetApiScopes());

            //IdentityServer4.EntityFramework.DbContexts.ConfigurationDbContext
            //IdentityServer4.EntityFramework.DbContexts.PersistedGrantDbContext

            var app = builder.Build();


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
