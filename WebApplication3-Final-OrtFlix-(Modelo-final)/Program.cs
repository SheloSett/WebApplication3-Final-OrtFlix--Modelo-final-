using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3_Final_OrtFlix__Modelo_final_.Context;
using WebApplication3_Final_OrtFlix__Modelo_final_.Models;
using WebApplication3_Final_OrtFlix__Modelo_final_.Services;

namespace WebApplication3_Final_OrtFlix__Modelo_final_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<OrtflixDatabaseContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString:OrtflixDBConnection"]));
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<OrtflixDatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OrtflixDBConnection")));
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/IniciarSesion";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(
                    new ResponseCacheAttribute
                    {
                        NoStore = true,
                        Location = ResponseCacheLocation.None,
                    }
                    );
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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
                name: "default",
                pattern: "{controller=Login}/{action=IniciarSesion}/{id?}");

            app.Run();
        }
    }
}