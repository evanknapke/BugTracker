using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BugTracker.Models;
using BugTracker.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace BugTracker
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
            // // Setup JWT
            // services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));

            // services.AddEntityFrameworkSqlServer()
            //   .AddDbContext<UserService>(opt => opt.UseNpgsql(Configuration["MongoDB:ConnectionString"]));

            // services.AddIdentity<IdentityUser, IdentityRole>()
            //   .AddEntityFrameworkStores<UserDbContext>();

            // auth0 Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            });
            services.AddControllers();

            // Setup Database
            services.Configure<BugTrackerDBSettings>(
                Configuration.GetSection(nameof(BugTrackerDBSettings)));
            
            services.Configure<BugTrackerDBSettings>(settings =>
            {
                settings.DatabaseName = "bugtracker-db";
                settings.ConnectionString = Configuration["MongoDB:ConnectionString"];
            });
            
            services.AddSingleton<IBugTrackerDBSettings>(sp =>
                sp.GetRequiredService<IOptions<BugTrackerDBSettings>>().Value);
            
            services.AddSingleton<UserService>();
            services.AddSingleton<IssueService>();
            
            services.AddControllers()
                .AddNewtonsoftJson(options => options.UseMemberCasing());;

            // services.AddControllersWithViews();
            // // In production, the Angular files will be served from this directory
            // services.AddSpaStaticFiles(configuration =>
            // {
            //     configuration.RootPath = "ClientApp/dist";
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            
            // auth0 Authentication
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // app.UseAuthentication();
            // app.UseAuthorization();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllers();
            // });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
