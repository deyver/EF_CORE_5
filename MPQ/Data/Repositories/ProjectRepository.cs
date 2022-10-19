using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Project> _dbset;

        public ProjectRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<Project>();
        }

        public void Add(Project project)
        {
            _dbset.Add(project);
        }

        public void Add(List<Project> project)
        {
            _dbset.AddRange(project);
        }

        public void Update(Project project)
        {
            _dbset.Update(project);
        }

        public void Update(List<Project> project)
        {
            _dbset.UpdateRange(project);
        }

        public void Delete(Project project)
        {
            _dbset.Remove(project);
        }

        public void Delete(List<Project> project)
        {
            _dbset.RemoveRange(project);
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.Customer)
                .Include(p => p.Site)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Customer)
                .Include(p => p.Site)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}