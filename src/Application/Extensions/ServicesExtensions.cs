using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Abstractions.Extensions;
using TRS.FinalPlantasy.Application.Abstractions.Infrastructure;
using TRS.FinalPlantasy.Application.Abstractions.Planning;
using TRS.FinalPlantasy.Application.Infrastructure;
using TRS.FinalPlantasy.Application.Planning;
using TRS.FinalPlantasy.Domain.Extensions;

namespace TRS.FinalPlantasy.Application.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddMediatR(configure => configure.RegisterServicesFromAssembly(typeof(ServicesExtensions).Assembly));

        services.AddTransient<IApplicationIdProvider, ApplicationIdProvider>();

        services.AddApplicationOptions(configuration);

        services.AddTransient<IAddPlanApplicationService, AddPlanApplicationService>();
        services.AddTransient<IUpdatePlanApplicationService, UpdatePlanApplicationService>();
        services.AddTransient<IDeletePlanApplicationService, DeletePlanApplicationService>();
        services.AddTransient<IValidator<PlanEntryModel>, PlanEntryModelValidator>();

        services.AddPlanningDomain();

        return services;
    }
}
