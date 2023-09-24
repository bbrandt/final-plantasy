using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Abstractions.Planning.Repositories;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Common;
using TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning;

namespace TRS.FinalPlantasy.Infrastructure.EntityFramework.Planning.Repositories;

internal class PlanEntryUnitOfWorkCreator : IUnitOfWorkCreator<IPlanEntryUnitOfWork>
{
    public Task<IPlanEntryUnitOfWork> CreateAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        var context = scope.ServiceProvider.GetRequiredService<PlanningContext>();
        var repository = scope.ServiceProvider.GetRequiredService<IPlanEntryRepository>();

        IPlanEntryUnitOfWork unitOfWork = new PlanEntryUnitOfWork(scope, context, repository);

        return Task.FromResult(unitOfWork);
    }
}
