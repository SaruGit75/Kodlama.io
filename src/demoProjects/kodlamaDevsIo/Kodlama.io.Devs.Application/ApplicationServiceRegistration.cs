using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Core.Application.Pipelines.Authorization;
using FluentValidation;
using MediatR;
using Core.Application.Pipelines.Validation;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Authentication.Rules;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Rules;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Features.ProgrammingTechnologies.Rules;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Kodlama.io.Devs.Application;

public static class ApplicationServiceRegister
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<ProgrammingLanguageBusinessRules>();
        services.AddScoped<ProgrammingTechnologyBusinessRules>();
        services.AddScoped<AuthenticationBusinessRules>();
        services.AddScoped<GithubProfileBusinessRules>();



        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        return services;
    }
}