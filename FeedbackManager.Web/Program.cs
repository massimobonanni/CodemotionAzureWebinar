using FeedbackManager.CognitiveServices.Services;
using FeedbackManager.Core.Implementations;
using FeedbackManager.Core.Interfaces;
using FeedbackManager.CosmosDB.Services;

namespace FeedbackManager.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add app settings file to process for the configuration
            builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile(
                     "appsettings.json", optional: false, reloadOnChange: true);
                config.AddJsonFile(
                    "appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                config.AddJsonFile(
                    "appsettings.local.json", optional: true, reloadOnChange: true);
                config.AddJsonFile(
                    "appsettings.local.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            });

            // Add services to the container.
            builder.Services.AddScoped<IFeedbackAnalyzer, CognitiveAnalyzer>();
            //builder.Services.AddScoped<IFeedbackPersistence, CosmosDBFeedbackPersistance>();
            builder.Services.AddScoped<IFeedbackPersistence, NoPersistanceService>();
            builder.Services.AddControllersWithViews();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}