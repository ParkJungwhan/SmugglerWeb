using Microsoft.FluentUI.AspNetCore.Components;
using SmugglerWeb.Components;

namespace SmugglerWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();
            //builder.Services.AddFluentUIComponents();
            builder.Services.AddFluentUIComponents(options =>
            {
                options.ValidateClassNames = false;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            // 이거는 일단 고정해야 도커에서 계속 갱신해도 문제없이 동작함
#if DEBUG
            app.Run("https://localhost:8001");
#else
            app.Run("http://*:5000");
#endif
        }
    }
}