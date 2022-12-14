using Microsoft.EntityFrameworkCore;

namespace CoreBussiness.UnitOfWork;

public interface IUnitOfWork
{
    DbSet<TEntity> Set<TEntity>() where TEntity:class;
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken  = new CancellationToken() );
}