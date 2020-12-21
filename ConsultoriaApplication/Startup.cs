using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultoriaApplication.Data;
using ConsultoriaApplication.Models;
using ConsultoriaApplication.Models.Repository;
using ConsultoriaApplication.Servicos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsultoriaApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddTransient<IServicoEmail, MailService>();
            services.AddTransient<ITwilioService, TwilioService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITaskListRepository, TaskListRepository>();
            services.AddTransient<ICardRepository, CardRepository>();
            services.AddTransient<IScopeRepository, ScopeRepository>();
            services.AddTransient<IUserTasklistRepository, UserTasklistRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddDbContext<ConsultoriaContext>(options => options.UseMySql(Configuration.GetConnectionString("bd")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ·ÈÌÛ˙‚ÍÓÙ˚„ı‚ÍÓÙ˚„ı";
            }).AddEntityFrameworkStores<ConsultoriaContext>().AddDefaultTokenProviders();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",policy=>policy.RequireClaim("Delete Role"));
            }
                );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{arquivo?}");
            });
            //CreateUser(serviceProvider).Wait();
        }
        
        private async Task CreateUser(IServiceProvider serviceProvider)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString() ,
                Nome = "admin",
                UserName = "admin",
                Email="admin@gmail.com"
            };
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            await userManager.CreateAsync(user,"admin");
            //cadastraUserFromStartup();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] rolesNames = { "Admin", "User"};
            IdentityResult result;
            foreach (var namesRole in rolesNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(namesRole);
                if (!roleExist)
                {
                    result = await roleManager.CreateAsync(new IdentityRole(namesRole));
                }
            }
            await userManager.AddToRoleAsync(user,"Admin");
        }

    }
}
