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
        Task<IEnumerable<Project>> ToListAsync(CancellationToken cancellationToken);
    }

    internal class ProjectService : BaseService<Project>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository) : base(projectRepository)
            => _projectRepository = projectRepository;

        public Task<IEnumerable<Project>> ToListAsync(CancellationToken cancellationToken)
            => _projectRepository.ToListAsync(cancellationToken);
    }
}
