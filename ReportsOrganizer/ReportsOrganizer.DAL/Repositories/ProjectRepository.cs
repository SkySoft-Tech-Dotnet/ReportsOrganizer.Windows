using ReportsOrganizer.DAL.Abstractions;
using ReportsOrganizer.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsOrganizer.DAL.Repositories
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<Project> FindById(int id, CancellationToken cancellationToken);
        Task<Project> FindByShortNameAsync(string shortName, CancellationToken cancellationToken);
        Task<IEnumerable<Project>> ToListAsync(CancellationToken cancellationToken);
    }

    internal class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
            => _dbContext = dbContext;

        public async Task<Project> FindById(int id, CancellationToken cancellationToken)
            => await _dbContext.Projects.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        public async Task<Project> FindByShortNameAsync(string shortName, CancellationToken cancellationToken)
            => await _dbContext.Projects.FirstOrDefaultAsync(e => e.ShortName == shortName, cancellationToken);

        public async Task<IEnumerable<Project>> ToListAsync(CancellationToken cancellationToken)
            => await _dbContext.Projects.ToListAsync(cancellationToken);
    }
}
