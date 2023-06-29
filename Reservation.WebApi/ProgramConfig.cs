using Reservation.Infrastructure.Abstracts;
using Reservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Serilog;

namespace Reservation.WebApi
{
    public static class ProgramConfig
    {
        public static void AddWebApiProject(this WebApplicationBuilder builder)
        {
            // Add EntityFramework core database
            builder.Services.AddDbContext<IDataContext, DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));

                if (builder.Environment.IsDevelopment())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                    options.ConfigureWarnings(warningOptions =>
                    {
                        warningOptions.Log(new EventId[] {
                        CoreEventId.FirstWithoutOrderByAndFilterWarning,
                        CoreEventId.RowLimitingOperationWithoutOrderByWarning
                    });
                    });
                }
            });

            // Add Serilog
            var logConf = new LoggerConfiguration();

            logConf.WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose);

            if (!builder.Environment.IsDevelopment())
            {
                logConf.WriteTo.EventLog("Application", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning);
            }

            Log.Logger = logConf.CreateLogger();

            builder.Host.UseSerilog();

            // Add Web API authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration, "AzureAd");
        }
    }
}
