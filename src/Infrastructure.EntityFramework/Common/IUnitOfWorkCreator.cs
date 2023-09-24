using Microsoft.Extensions.DependencyInjection;
using TRS.FinalPlantasy.Application.Abstractions.Repositories;

namespace TRS.FinalPlantasy.Infrastructure.EntityFramework.Common;

/// <summary>
/// Create a unit of work from the given service scope
/// </summary>
/// <typeparam name="TUnitOfWork"></typeparam>
internal interface IUnitOfWorkCreator<TUnitOfWork>
    where TUnitOfWork : IUnitOfWork
{
    Task<TUnitOfWork> CreateAsync(IServiceScope scope, CancellationToken cancellationToken);
}
