using ReportsOrganizer.DAL.Abstractions;
using ReportsOrganizer.Models;
using System.Collections.Generic;
using System.Data.Entity;
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
        private readonly ApplicationDbContext _dbContext;

        public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
            => _dbContext = dbContext;

        public async Task<IEnumerable<Project>> ToListAsync(CancellationToken cancellationToken)
            => await _dbContext.Projects.ToListAsync(cancellationToken);
    }
}
