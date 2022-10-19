using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class ComparisonOperatorRepository : IComparisonOperatorRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<ComparisonOperator> _dbset;

        public ComparisonOperatorRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<ComparisonOperator>();
        }

        public void Add(ComparisonOperator comparisonOperator)
        {
            _dbset.Add(comparisonOperator);
        }

        public void Add(List<ComparisonOperator> comparisonOperator)
        {
            _dbset.AddRange(comparisonOperator);
        }

        public void Update(ComparisonOperator comparisonOperator)
        {
            _dbset.Update(comparisonOperator);
        }

        public void Update(List<ComparisonOperator> comparisonOperator)
        {
            _dbset.UpdateRange(comparisonOperator);
        }

        public void Delete(ComparisonOperator comparisonOperator)
        {
            _dbset.Remove(comparisonOperator);
        }

        public void Delete(List<ComparisonOperator> comparisonOperator)
        {
            _dbset.RemoveRange(comparisonOperator);
        }

        public async Task<List<ComparisonOperator>> GetAllAsync()
        {
            return await _dbset
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ComparisonOperator> GetByIdAsync(int id)
        {
            return await _dbset
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}