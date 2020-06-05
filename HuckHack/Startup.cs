using HuckHack.Communication.SendGrid;
using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Contracts.Services;
using HuckHack.Domain.Services;
using HuckHack.Repositories.Mongo;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HuckHack
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<ICommunicationService, SendGridCommunicationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITeamService, TeamService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRequestRepository, RequestRepository>();
            services.AddTransient<IHackerCardRepository, HackerCardRepository>();
            services.AddTransient<ITeamCardRepository, TeamCardRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<IApplicationRepository, ApplicationRepository>();

            services.AddSingleton<MongoContext>();

            services.AddTransient<IHackerCardService, HackerCardService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/";
                    options.LoginPath = "/";
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
