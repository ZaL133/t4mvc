using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using t4mvc.core;
using t4mvc.data;
using t4mvc.data.services;
using t4mvc.web.core.viewmodels;

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
                .AddEntityFrameworkStores<t4DbContext>();
            builder.Services.AddControllersWithViews();

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

            builder.Services.AddScoped<IContextHelper, ContextHelper>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Add the code generated services 
            ServiceConfig.AddCodeGen(builder.Services);

            var app = builder.Build();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}