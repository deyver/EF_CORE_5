using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class GoalProductionLineRangeRepository : IGoalProductionLineRangeRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<GoalProductionLineRange> _dbset;

        public GoalProductionLineRangeRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<GoalProductionLineRange>();
        }

        public void Add(GoalProductionLineRange goalProductionLineRange)
        {
            _dbset.Add(goalProductionLineRange);
        }

        public void Add(List<GoalProductionLineRange> goalProductionLineRange)
        {
            _dbset.AddRange(goalProductionLineRange);
        }

        public void Update(GoalProductionLineRange goalProductionLineRange)
        {
            _dbset.Update(goalProductionLineRange);
        }

        public void Update(List<GoalProductionLineRange> goalProductionLineRange)
        {
            _dbset.UpdateRange(goalProductionLineRange);
        }

        public void Delete(GoalProductionLineRange goalProductionLineRange)
        {
            _dbset.Remove(goalProductionLineRange);
        }

        public void Delete(List<GoalProductionLineRange> goalProductionLineRange)
        {
            _dbset.RemoveRange(goalProductionLineRange);
        }

        public async Task<List<GoalProductionLineRange>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.GoalProductionLine).ThenInclude(p => p.ProductionLine)
                .Include(p => p.Operator)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GoalProductionLineRange> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.GoalProductionLine).ThenInclude(p => p.ProductionLine)
                .Include(p => p.Operator)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<GoalProductionLineRange>> GetByGoalProductionLineIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.GoalProductionLine).ThenInclude(p => p.ProductionLine)
                .Include(p => p.Operator)
                .Where(p => p.GoalProductionLineId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}