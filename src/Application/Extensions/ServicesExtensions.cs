using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Abstractions.Extensions;
using TRS.FinalPlantasy.Application.Abstractions.Infrastructure;
using TRS.FinalPlantasy.Application.Infrastructure;

namespace TRS.FinalPlantasy.Application.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddTransient<IApplicationIdProvider, ApplicationIdProvider>();

        services.AddApplicationOptions(configuration);

        return services;
    }
}
