using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class GoalProductionLineRepository : IGoalProductionLineRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<GoalProductionLine> _dbset;

        public GoalProductionLineRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<GoalProductionLine>();
        }

        public void Add(GoalProductionLine goalProductionLine)
        {
            _dbset.Add(goalProductionLine);
        }

        public void Add(List<GoalProductionLine> goalProductionLine)
        {
            _dbset.AddRange(goalProductionLine);
        }

        public void Update(GoalProductionLine goalProductionLine)
        {
            _dbset.Update(goalProductionLine);
        }

        public void Update(List<GoalProductionLine> goalProductionLine)
        {
            _dbset.UpdateRange(goalProductionLine);
        }

        public void Delete(GoalProductionLine goalProductionLine)
        {
            _dbset.Remove(goalProductionLine);
        }

        public void Delete(List<GoalProductionLine> goalProductionLine)
        {
            _dbset.RemoveRange(goalProductionLine);
        }

        public async Task<List<GoalProductionLine>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .Include(p => p.Project)
                .Include(p => p.ProductionLine)
                .Include(p => p.ProductionArea)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GoalProductionLine> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .Include(p => p.Project)
                .Include(p => p.ProductionLine)
                .Include(p => p.ProductionArea)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<GoalProductionLine>> GetBySiteIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .Include(p => p.Project)
                .Include(p => p.ProductionLine)
                .Include(p => p.ProductionArea)
                .Where(p => p.SiteId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<GoalProductionLine>> GetByCustomerIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .Include(p => p.Project)
                .Include(p => p.ProductionLine)
                .Include(p => p.ProductionArea)
                .Where(p => p.CustomerId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<GoalProductionLine>> GetByProjectIdAsync(int? id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .Include(p => p.Project)
                .Include(p => p.ProductionLine)
                .Include(p => p.ProductionArea)
                .Where(p => p.ProjectId == id)
                .AsNoTracking()
                .ToListAsync();            
        }

        public async Task<List<GoalProductionLine>> GetByProductionLineIdAsync(int? id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .Include(p => p.Project)
                .Include(p => p.ProductionLine)
                .Include(p => p.ProductionArea)
                .Where(p => p.ProductionLineId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}