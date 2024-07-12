using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.Career.Repositories;
using UCR.ECCI.IS.TECH_EVALUATION_1.Domain.Career.Repositories;

namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure;

/// <summary>
/// InfrastructureLayerDependencyInjection class
/// </summary>
public static class InfrastructureLayerDependencyInjection
{

    /// <summary>
    /// AddInfrastructureLayerServices method
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructureLayerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ICareerRepository, SqlCareerRepository>();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
