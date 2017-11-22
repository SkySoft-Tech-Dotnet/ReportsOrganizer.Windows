using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsOrganizer.DAL.Extensions
{
    internal static class EntityExtensions
    {
        public static Task AddAsync<TEntity>(this DbSet<TEntity> entities, TEntity entity,
            CancellationToken cancellationToken) where TEntity : class
        {
            return new Task(() =>
            {
                entities.Add(entity);
            }, cancellationToken);
        }

        public static async Task AddAsync<TEntity>(this ApplicationDbContext dbContext, TEntity entity,
            CancellationToken cancellationToken) where TEntity : class
        {
            await AddAsync(dbContext.Set<TEntity>(), entity, cancellationToken);
        }

        public static Task DeleteAsync<TEntity>(this DbSet<TEntity> entities, TEntity entity,
            CancellationToken cancellationToken) where TEntity : class
        {
            return new Task(() =>
            {
                entities.Remove(entity);
            }, cancellationToken);
        }

        public static Task DeleteAsync<TEntity>(this ApplicationDbContext dbContext, TEntity entity,
            CancellationToken cancellationToken) where TEntity : class
        {
            return DeleteAsync(dbContext.Set<TEntity>(), entity, cancellationToken);
        }

        public static Task DeleteAsync<TEntity>(this DbSet<TEntity> entities, IEnumerable<TEntity> entity,
            CancellationToken cancellationToken) where TEntity : class
        {
            return new Task(() =>
            {
                entities.RemoveRange(entity);
            }, cancellationToken);
        }

        public static Task DeleteAsync<TEntity>(this ApplicationDbContext dbContext, IEnumerable<TEntity> entity,
            CancellationToken cancellationToken) where TEntity : class
        {
            return DeleteAsync(dbContext.Set<TEntity>(), entity, cancellationToken);
        }
    }
}
