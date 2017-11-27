using ReportsOrganizer.Core.Abstractions;
using ReportsOrganizer.DAL.Repositories;
using ReportsOrganizer.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Services
{
    public interface IProjectService : IBaseService<Project>
    {
        Task<Project> FindById(int id, CancellationToken cancellationToken);
        Task<Project> FindByShortName(string shortName, CancellationToken cancellationToken);
        Task<IEnumerable<Project>> ToListAsync(CancellationToken cancellationToken);
    }

    internal class ProjectService : BaseService<Project>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository) : base(projectRepository)
            => _projectRepository = projectRepository;

        public Task<Project> FindById(int id, CancellationToken cancellationToken)
            => _projectRepository.FindById(id, cancellationToken);

        public Task<Project> FindByShortName(string shortName, CancellationToken cancellationToken)
            => _projectRepository.FindByShortNameAsync(shortName, cancellationToken);

        public Task<IEnumerable<Project>> ToListAsync(CancellationToken cancellationToken)
            => _projectRepository.ToListAsync(cancellationToken);
    }
}
