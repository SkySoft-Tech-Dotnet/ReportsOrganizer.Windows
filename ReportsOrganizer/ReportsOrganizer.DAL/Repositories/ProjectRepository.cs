using ReportsOrganizer.DAL.Abstractions;
using ReportsOrganizer.Models;

namespace ReportsOrganizer.DAL.Repositories
{
    public interface IProjectRepository : IBaseRepository<Project>
    {

    }

    internal class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _applicationContext;

        public ProjectRepository(ApplicationDbContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

    }
}
