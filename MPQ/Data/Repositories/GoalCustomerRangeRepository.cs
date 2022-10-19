using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class GoalCustomerRangeRepository : IGoalCustomerRangeRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<GoalCustomerRange> _dbset;

        public GoalCustomerRangeRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<GoalCustomerRange>();
        }

        public void Add(GoalCustomerRange goalCustomerRange)
        {
            _dbset.Add(goalCustomerRange);
        }

        public void Add(List<GoalCustomerRange> goalCustomerRange)
        {
            _dbset.AddRange(goalCustomerRange);
        }

        public void Update(GoalCustomerRange goalCustomerRange)
        {
            _dbset.Update(goalCustomerRange);
        }

        public void Update(List<GoalCustomerRange> goalCustomerRange)
        {
            _dbset.UpdateRange(goalCustomerRange);
        }

        public void Delete(GoalCustomerRange goalCustomerRange)
        {
            _dbset.Remove(goalCustomerRange);
        }

        public void Delete(List<GoalCustomerRange> goalCustomerRange)
        {
            _dbset.RemoveRange(goalCustomerRange);
        }

        public async Task<List<GoalCustomerRange>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.GoalCustomer)
                .Include(p => p.Operator)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GoalCustomerRange> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.GoalCustomer)
                .Include(p => p.Operator)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<GoalCustomerRange>> GetByGoalCustomerIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.GoalCustomer)
                .Include(p => p.Operator)
                .Where(g => g.GoalCustomerId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}