using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Goal> _dbset;

        public GoalRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<Goal>();
        }

        public void Add(Goal goal)
        {
            _dbset.Add(goal);
        }

        public void Delete(Goal goal)
        {
            _dbset.Remove(goal);
        }

        public async Task<List<Goal>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.GoalStfs)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Goal> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.GoalStfs)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(Goal goal)
        {
            _dbset.Update(goal);
        }
    }
}
