using Microsoft.EntityFrameworkCore;

namespace TRS.FinalPlantasy.Infrastructure.SqlServer.Common;

internal abstract class GenericRepository<T>
    where T : class
{ 
    protected DbContext Context { get; }

    protected GenericRepository(DbContext context)
    {
        Context = context;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await Context.Set<T>()
            .AddAsync(entity, cancellationToken);
    }
}