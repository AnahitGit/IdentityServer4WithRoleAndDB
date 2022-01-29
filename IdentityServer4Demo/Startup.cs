
using IdentityServer4Demo.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Entities;
using Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentityServer4Demo
{
    public class Startup
    {
        public IConfiguration _configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddIdentityServer()               
            //    .AddInMemoryApiScopes(InMemoryConfig.GetApiScopes())
            //    .AddInMemoryApiResources(InMemoryConfig.GetApiResources())
            //    .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
            //    .AddTestUsers(InMemoryConfig.GetUsers())
            //    .AddInMemoryClients(InMemoryConfig.GetClients())
            //    .AddDeveloperSigningCredential(); //method to set temporary signing credentials


            var connection = _configuration.GetConnectionString("sqlConnection");
            services.AddDbContextPool<CompEmpIdentityServerContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IUserRepository, UserRepository>();

            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                //.AddTestUsers(InMemoryConfig.GetUsers())
                .AddDeveloperSigningCredential() //not something we want to use in a production environment
                .AddConfigurationStore(opt =>
                {
                    opt.ConfigureDbContext = c => c.UseSqlServer(connection,
                        sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(opt =>
                {
                    opt.ConfigureDbContext = o => o.UseSqlServer(connection,
                        sql => sql.MigrationsAssembly(migrationAssembly));
                })
                ;
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
