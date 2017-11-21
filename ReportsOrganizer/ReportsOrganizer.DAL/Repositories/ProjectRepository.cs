using Microsoft.EntityFrameworkCore;
using ReportsOrganizer.DAL.Abstractions;
using ReportsOrganizer.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsOrganizer.DAL.Repositories
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<IEnumerable<Project>> ToListAsync(CancellationToken cancellationToken);
    }

    internal class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _applicationContext;

        public ProjectRepository(ApplicationDbContext applicationContext)
            : base(applicationContext) => _applicationContext = applicationContext;

        public async Task<IEnumerable<Project>> ToListAsync(CancellationToken cancellationToken)
        {
            return await _applicationContext.Projects.ToListAsync(cancellationToken);
        }
    }
}
