using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Abstractions.Repositories;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Common;

internal abstract class GenericUnitOfWorkFactory<TUnitOfWork, TUnitOfWorkCreator>
    where TUnitOfWork : IUnitOfWork
    where TUnitOfWorkCreator : IUnitOfWorkCreator<TUnitOfWork>
{
    private readonly IServiceProvider _serviceProvider;

    public GenericUnitOfWorkFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TUnitOfWork> CreateAsync(CancellationToken cancellationToken = default)
    {
        /*
        * Unit of work must dispose this scope when it disposes
        */
        var scope = _serviceProvider.CreateScope();

        var creator = scope.ServiceProvider.GetRequiredService<TUnitOfWorkCreator>();

        var unitOfWork = await creator.CreateAsync(scope, cancellationToken);

        return unitOfWork;
    }
}