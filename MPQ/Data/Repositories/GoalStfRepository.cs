using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class GoalStfRepository : IGoalStfRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<GoalStf> _dbset;

        public GoalStfRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<GoalStf>();
        }

        public void Add(GoalStf goalStf)
        {
            _dbset.Add(goalStf);
        }

        public void Add(List<GoalStf> goalStf)
        {
            _dbset.AddRange(goalStf);
        }

        public void Update(GoalStf goalStf)
        {
            _dbset.Update(goalStf);
        }

        public void Update(List<GoalStf> goalStf)
        {
            _dbset.UpdateRange(goalStf);
        }

        public void Delete(GoalStf goalStf)
        {
            _dbset.Remove(goalStf);
        }

        public void Delete(List<GoalStf> goalStf)
        {
            _dbset.RemoveRange(goalStf);
        }

        public async Task<List<GoalStf>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Indicator)
                .Include(p => p.Goal)
                .Include(p => p.BusinessUnit)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GoalStf> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Indicator)
                .Include(p => p.Goal)
                .Include(p => p.BusinessUnit)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}