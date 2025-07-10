using System;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharedService.ExceptionHandling;

namespace SharedService;

public static class SharedRegistrationExtension
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services, Assembly assembly)
    {
        //Fluent Validation
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(assembly);

        //Automapper
        services.AddAutoMapper(assembly);

        //MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        return services;
    }

    public static IApplicationBuilder UseSharedMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
