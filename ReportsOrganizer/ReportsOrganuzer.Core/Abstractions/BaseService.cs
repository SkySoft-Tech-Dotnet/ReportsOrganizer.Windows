using ReportsOrganizer.DAL.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Abstractions
{
    public interface IBaseService<TModel>
        where TModel : class
    {
        Task AddAsync(TModel entity, CancellationToken cancellationToken);
        Task AddOrUpdateAsync(TModel entity, CancellationToken cancellationToken);
        Task DeleteAsync(TModel entity, CancellationToken cancellationToken);
        Task DeleteAsync(ICollection<TModel> entity, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }

    internal class BaseService<TModel> : IBaseService<TModel>
        where TModel : class
    {
        private readonly IBaseRepository<TModel> _baseRepository;

        public BaseService(IBaseRepository<TModel> baseRepository)
            => _baseRepository = baseRepository;

        public virtual Task AddAsync(TModel entity, CancellationToken cancellationToken)
        {
            return _baseRepository.AddAsync(entity, cancellationToken);
        }
        public virtual Task AddOrUpdateAsync(TModel entity, CancellationToken cancellationToken)
        {
            return _baseRepository.AddOrUpdateAsync(entity, cancellationToken);
        }

        public virtual Task DeleteAsync(TModel entity, CancellationToken cancellationToken)
        {
            return _baseRepository.DeleteAsync(entity, cancellationToken);
        }

        public virtual Task DeleteAsync(ICollection<TModel> entity, CancellationToken cancellationToken)
        {
            return _baseRepository.DeleteAsync(entity, cancellationToken);
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _baseRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
