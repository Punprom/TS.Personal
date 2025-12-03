using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Microsoft.Data.SqlClient;
using TS.Personal.Core.Interfaces;
using TS.Personal.Security.Services;
using Serilog;

namespace TS.Personal.Security;

public static class PersonalSecurityModuleService
{
    public static IServiceCollection AddSecurityModuleServices(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger,
        List<System.Reflection.Assembly> mediatRAssemblies)
    {
        string? connectionString = config.GetConnectionString("DefaultConnection");
        
        services.AddTransient<Func<IDbConnection>>(sp => () => new SqlConnection(connectionString!));
        services.AddScoped<IUserService, UserService>();

        mediatRAssemblies.Add(typeof(PersonalSecurityModuleService).Assembly);
        
        logger.Information("{Module} module services registered", "Security");

        return services;
    }
}
