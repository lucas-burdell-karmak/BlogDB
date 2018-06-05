using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlogDB.Core;
using Microsoft.AspNetCore.Http;

namespace The_Intern_MVC
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
            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Login/Index");
                    options.AccessDeniedPath = new PathString("/NullPost/Index");
                });
            services.AddSingleton<IAuthorRepo, SQLAuthorRepo>();
            services.AddSingleton<IPostValidator, PostValidator>();
            services.AddSingleton<IPostRepo, SQLPostRepo>();
            services.AddSingleton<IPostDataAccess, PostDataAccess>();
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

            app.UseStaticFiles();
            app.UseAuthentication(); // used to invoke Authentication Middleware that sets the HttpContext.User property
            var cookiePolicy = new CookiePolicyOptions(); // use default options
            app.UseCookiePolicy(cookiePolicy);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "Home Default",
                    template: "{controller=Home}/{action=Index}");
                routes.MapRoute(
                    name: "Login",
                    template: "{controller=Login}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "NullPost Default",
                    template: "{controller=NullPost}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "Register Default",
                    template: "{controller=Register}/{action=Index}/{id?}");
            });
        }
    }
}
