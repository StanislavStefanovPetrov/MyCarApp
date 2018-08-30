using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCarApp.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using MyCarApp.Web.Areas.Identity.Services;
using MyCarApp.Data;
using MyCarApp.DataSeed;
using AutoMapper;
using MyCarApp.Services.SeedData.Contracts;
using MyCarApp.Services.SeedData;
using SoftUniClone.Services.Admin;
using MyCarApp.Services.Admin.Interfaces;
using MyCarApp.Services.Publisher;
using MyCarApp.Services.Publisher.Interfaces;
using MyCarApp.Web.Pages;
using Microsoft.Extensions.FileProviders;
using System.IO;
using MyCarApp.Services.User;
using MyCarApp.Services.User.Interfaces;
using MyCarApp.Services.Observer.Interfaces;
using MyCarApp.Services.Observer;
using MyCarApp.Services.SearchEngine.Interfaces;
using MyCarApp.Services.SearchEngine;

namespace MyCarApp.Web
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
            services.AddResponseCaching();
            services.AddResponseCompression();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MyCarDbConnection")));
            services
                  .AddIdentity<ApplicationUser, IdentityRole>()
                  .AddDefaultUI()
                  .AddDefaultTokenProviders()
                  .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<EmailOptions>(this.Configuration.GetSection("EmailSettings"));

            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions()
                {
                    RequiredLength = 4,
                    RequiredUniqueChars = 1,
                    RequireDigit = false,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                };

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 4;

                //options.SignIn.RequireConfirmedEmail = true;
            });

            services.AddAutoMapper();

            RegisterServiceLayer(services);

            services.AddMvc(options =>
                 {
                     options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                 })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCaching();
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.SeedDatabase();
                app.SeedVehicleDatabase();
            }

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
                RequestPath = "/Files",
                EnableDirectoryBrowsing = true
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegisterServiceLayer(IServiceCollection services)
        {
            services.AddScoped<ISeedDataService, SeedDataService>();
            services.AddScoped<IAdminVehicleService, AdminVehicleService>();
            services.AddScoped<IPublisherAdvertisementService, PublisherAdvertisementService>();
            services.AddScoped<IPublisherVehicleService, PublisherVehicleService>();
            services.AddScoped<IPictureService, PublisherPictureService>();
            services.AddScoped<IUserAdvertisementService, UserAdvertisementService>();
            services.AddScoped<IObserverAdvertisementService, ObserverAdvertisementService>();
            services.AddScoped<ISearchEngineVehicleService, SearchEngineVehicleService>();
        }
    }
}
