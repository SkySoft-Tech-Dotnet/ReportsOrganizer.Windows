using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsOrganizer.DAL.Abstractions
{
    public interface IBaseRepository<TModel>
        where TModel : class
    {
        Task AddAsync(TModel entity, CancellationToken cancellationToken);
        Task UpdateAsync(TModel entity, CancellationToken cancellationToken);
        Task DeleteAsync(TModel entity, CancellationToken cancellationToken);
        Task DeleteAsync(ICollection<TModel> entity, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }

    internal class BaseRepository<TModel> : IBaseRepository<TModel>
        where TModel : class
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;

        public virtual async Task AddAsync(TModel entity, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(entity, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync(TModel entity, CancellationToken cancellationToken)
        {
            _dbContext.Update(entity);
            await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(TModel entity, CancellationToken cancellationToken)
        {
            _dbContext.Remove(entity);
            await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(ICollection<TModel> entity, CancellationToken cancellationToken)
        {
            _dbContext.RemoveRange(entity);
            await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
