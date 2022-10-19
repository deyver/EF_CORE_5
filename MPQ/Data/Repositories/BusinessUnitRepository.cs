using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class BusinessUnitRepository : IBusinessUnitRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<BusinessUnit> _dbset;

        public BusinessUnitRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<BusinessUnit>();
        }

        public void Add(BusinessUnit businessUnit)
        {
            _dbset.Add(businessUnit);
        }

        public void Add(List<BusinessUnit> businessUnit)
        {
            _dbset.AddRange(businessUnit);
        }

        public void Update(BusinessUnit businessUnit)
        {
            _dbset.Update(businessUnit);
        }

        public void Update(List<BusinessUnit> businessUnit)
        {
            _dbset.UpdateRange(businessUnit);
        }

        public void Delete(BusinessUnit businessUnit)
        {
            _dbset.Remove(businessUnit);
        }

        public void Delete(List<BusinessUnit> businessUnit)
        {
            _dbset.RemoveRange(businessUnit);
        }

        public async Task<List<BusinessUnit>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.GoalStfs)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<BusinessUnit> GetByIdAsync(int id)
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
    }
}