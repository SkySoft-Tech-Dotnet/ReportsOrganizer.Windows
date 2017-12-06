using ReportsOrganizer.Core.Abstractions;
using ReportsOrganizer.DAL.Repositories;
using ReportsOrganizer.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Services
{
    public interface IProjectService : IBaseService<Project>
    {

        Task<Project> FindById(int id, CancellationToken cancellationToken);
        Task<Project> FindByShortName(string shortName, CancellationToken cancellationToken);
        Task<IEnumerable<Project>> ToListAsync(CancellationToken cancellationToken);
        Task<BindingList<Project>> ToBindingListAsync(CancellationToken cancellationToken);
    }

    internal class ProjectService : BaseService<Project>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private BindingList<Project> _cacheProjects;

        public ProjectService(IProjectRepository projectRepository) : base(projectRepository)
            => _projectRepository = projectRepository;

        public Task<Project> FindById(int id, CancellationToken cancellationToken)
            => _projectRepository.FindById(id, cancellationToken);

        public Task<Project> FindByShortName(string shortName, CancellationToken cancellationToken)
            => _projectRepository.FindByShortName(shortName.ToUpper())
                .FirstOrDefaultAsync(cancellationToken);

        public Task<IEnumerable<Project>> ToListAsync(CancellationToken cancellationToken)
            => _projectRepository.ToListAsync(cancellationToken);
        
        public async Task<BindingList<Project>> ToBindingListAsync(CancellationToken cancellationToken)
        {
            var projectList = await _projectRepository.ToListAsync(cancellationToken);
            _cacheProjects = new BindingList<Project>(projectList.ToList());
            return _cacheProjects;
        }

        public override Task AddAsync(Project entity, CancellationToken cancellationToken)
        {
            _cacheProjects?.Add(entity);
            return base.AddAsync(entity, cancellationToken);
        }

        public override Task AddOrUpdateAsync(Project entity, CancellationToken cancellationToken)
        {
            if (entity.Id == 0)
                _cacheProjects?.Add(entity);
            else
                _cacheProjects?.ResetBindings();
            return base.AddOrUpdateAsync(entity, cancellationToken);
        }

        public override Task DeleteAsync(ICollection<Project> entity, CancellationToken cancellationToken)
        {
            entity.ToList().ForEach(e=>_cacheProjects?.Remove(e));
            return base.DeleteAsync(entity, cancellationToken);
        }

        public override Task DeleteAsync(Project entity, CancellationToken cancellationToken)
        {
            _cacheProjects?.Remove(entity);
            return base.DeleteAsync(entity, cancellationToken);
        }

        public override Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            _cacheProjects?.ResetBindings();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
