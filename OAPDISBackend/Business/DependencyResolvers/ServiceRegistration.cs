using Business.Utilities.Hangfire;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolvers;

public static class ServiceRegistration
{
    public static void AddBussinessServices(this IServiceCollection services)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("Default")));

        services.AddSingleton<HangfireJobs>();
    }

    [Obsolete("Obsolete")]
    public static void UseDependencyResolvers(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "Hangfire Dashboard",
            AppPath = "/",
            Authorization = new[] {
                new HangfireCustomBasicAuthenticationFilter()
                {
                    Pass = "admin",
                    User = "admin"
                }
            }
        });
        app.UseHangfireServer();
        var hangfireJobs = app.ApplicationServices.GetRequiredService<HangfireJobs>();

        RecurringJob.AddOrUpdate("SendEmail", () => hangfireJobs.SendMeetingReminderEmail(), "0 9 * * *"); // Her gün 00:00'da çalışır
    }
}