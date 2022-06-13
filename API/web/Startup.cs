using Core.Middleware;
using Core.Models;
using Core.Repository;
using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Core.Constants;

namespace web
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
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                config.Cookie.HttpOnly = true;
                config.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasAccessToDataManagement",
                    policy => policy.RequireRole(ApplicationRoles.SystemAdmin));
            });

            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IFilmRepository, FilmRepository>();

            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IFilmService, FilmService>();
            services.AddTransient<IAuthService, AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(b =>
            {
                b.AllowAnyMethod();
                b.AllowAnyHeader();
                b.SetIsOriginAllowed(origin => true);
                b.AllowCredentials();
            });

            app.UseMiddleware<FilterExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
