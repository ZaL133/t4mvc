using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using t4mvc.core;
using t4mvc.data;
using t4mvc.data.Services;
using t4mvc.web.Controllers;
using t4mvc.web.core.Infrastructure;
using t4mvc.web.core.ViewModels;
using t4mvc.web.core.ViewModelServices;

namespace t4mvc.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<t4DbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<t4mvcUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<t4mvcRole>()
                .AddEntityFrameworkStores<t4DbContext>();
            builder.Services.AddControllersWithViews().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null)
                    .AddRazorOptions(options =>
                    {
                        options.ViewLocationExpanders.Add(new t4mvcViewLocationExpander());
                    });

            // Add automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                var mappingProfile = new t4mvcMappingProfile();

                // add in any custom maps
                mappingProfile.CreateMap<t4mvcUser, UserViewModel>().ReverseMap();

                mc.AddProfile(mappingProfile);
            });
            var mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ISearchViewModelService, SearchViewModelService>();
            builder.Services.AddScoped<IContextHelper, ContextHelper>();
            builder.Services.AddScoped<It4mvcApiController, t4mvcApiController>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserViewModelService, UserViewModelService>();
            

            builder.Services.AddScoped<IUrlHelper>(x => {
                var actionContext   = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory         = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            // Add the code generated services 
            ServiceConfig.AddCodeGen(builder.Services);

            // Log4net
            builder.Logging.AddLog4Net("log4net.config");

            var app = builder.Build();

            Current.Configure(app.Services);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.MapControllerRoute(
                name: "MyArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}