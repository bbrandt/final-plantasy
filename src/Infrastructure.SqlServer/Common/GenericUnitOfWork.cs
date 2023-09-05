using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Common;

internal abstract class GenericUnitOfWork
{
    private readonly IServiceScope _serviceScope;
    private readonly DbContext _context;

    public GenericUnitOfWork(
        IServiceScope serviceScope,
        DbContext context)
    {
        _serviceScope = serviceScope;
        _context = context;
    }

    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _serviceScope?.Dispose();
    }
}
