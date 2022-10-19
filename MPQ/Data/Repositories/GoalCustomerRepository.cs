using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class GoalCustomerRepository : IGoalCustomerRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<GoalCustomer> _dbset;

        public GoalCustomerRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<GoalCustomer>();
        }

        public GoalCustomer Add(GoalCustomer goalCustomer)
        {
            return _dbset.Add(goalCustomer).Entity;
        }

        public void Add(List<GoalCustomer> goalCustomer)
        {
            _dbset.AddRange(goalCustomer);
        }

        public void Update(GoalCustomer goalCustomer)
        {
            _dbset.Update(goalCustomer);
        }

        public void Update(List<GoalCustomer> goalCustomer)
        {
            _dbset.UpdateRange(goalCustomer);
        }

        public void Delete(GoalCustomer goalCustomer)
        {
            _dbset.Remove(goalCustomer);
        }

        public void Delete(List<GoalCustomer> goalCustomer)
        {
            _dbset.RemoveRange(goalCustomer);
        }

        public async Task<List<GoalCustomer>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GoalCustomer> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}