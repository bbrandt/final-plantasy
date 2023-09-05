namespace TRS.FinalPlantasy.Application.Abstractions.Repositories;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Commit the unit of work being tracked
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CompleteAsync(CancellationToken cancellationToken = default);
}
