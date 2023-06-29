using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShareLib.Behaviors;
using ShareLib.Middleware;

namespace Reservation.Application
{
    public static class ProgramConfig
    {
        public static void AddAppplicationProject(this WebApplicationBuilder builder)
        {
            var assembly = typeof(ProgramConfig).Assembly;

            // add Mediatr classes
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            });

            // add FluentValidation classes
            builder.Services.AddValidatorsFromAssembly(assembly);

            // add requried services
            builder.Services.AddTransient<ExceptionHandlingMiddleware>();
        }

        public static void UseApplicationProject(this WebApplication app)
        {
            // use middleware
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
