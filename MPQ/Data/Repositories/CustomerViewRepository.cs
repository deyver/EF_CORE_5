using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class CustomerViewRepository : ICustomerViewRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<CustomerView> _dbset;

        public CustomerViewRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<CustomerView>();
        }

        public void Add(CustomerView customerView)
        {
            _dbset.Add(customerView);
        }

        public void Add(List<CustomerView> customerView)
        {
            _dbset.AddRange(customerView);
        }

        public void Update(CustomerView customerView)
        {
            _dbset.Update(customerView);
        }

        public void Update(List<CustomerView> customerView)
        {
            _dbset.UpdateRange(customerView);
        }

        public void Delete(CustomerView customerView)
        {
            _dbset.Remove(customerView);
        }

        public void Delete(List<CustomerView> customerView)
        {
            _dbset.RemoveRange(customerView);
        }

        public async Task<List<CustomerView>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.BusinessUnit)
                .Include(p => p.Customer)
                .Include(p => p.Indicator)
                .Include(p => p.Site)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CustomerView> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.BusinessUnit)
                .Include(p => p.Customer)
                .Include(p => p.Indicator)
                .Include(p => p.Site)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<CustomerView>> GetBySiteIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.BusinessUnit)
                .Include(p => p.Customer)
                .Include(p => p.Indicator)
                .Include(p => p.Site)
                .Where(p => p.SiteId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}