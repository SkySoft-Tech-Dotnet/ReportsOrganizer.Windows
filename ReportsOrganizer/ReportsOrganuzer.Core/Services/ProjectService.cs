using ReportsOrganizer.DAL;
using ReportsOrganizer.Models;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using ReportsOrganizer.Core.Abstractions;
using ReportsOrganizer.DAL.Abstractions;
using ReportsOrganizer.DAL.Repositories;

namespace ReportsOrganizer.Core.Services
{
    public interface IProjectService : IBaseService<Project>
    {
    }

    internal class ProjectService : BaseService<Project>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository) : base(projectRepository)
            => _projectRepository = projectRepository;
    }
}
