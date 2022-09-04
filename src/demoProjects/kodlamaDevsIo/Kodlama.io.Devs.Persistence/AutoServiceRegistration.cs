using Kodlama.io.Devs.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kodlama.io.Devs.Persistence;

public static class AutoServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<KodlamaIoContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("ProgrammingLanguagesDbConnection")));
        //assembly name'ler ile service registration yapılacak.

        var allRepoInterfaces = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(t => t.FullName.Contains("Kodlama.io.Devs.Application"))?.GetTypes()
            .Where(t => t.Namespace != null && (t.Namespace.Contains("Repositories")) && t.IsInterface)
            .ToList();

        var allRepoConcretes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.Namespace != null && (t.Namespace.Contains("Repositories"))).ToList();

        foreach (var intrfc in allRepoInterfaces)
        {
            var impl = allRepoConcretes?.FirstOrDefault(t => t.IsClass && intrfc.Name.Substring(1) == t.Name);
            if (impl != null) services.AddScoped(intrfc, impl);
        }

        return services;
    }
}