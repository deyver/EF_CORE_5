using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Customer> _dbset;

        public CustomerRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<Customer>();
        }

        public void Add(Customer customer)
        {
            _dbset.Add(customer);
        }

        public void Update(Customer customer)
        {
            _dbset.Update(customer);
        }

        public void Delete(Customer customer)
        {
            _dbset.Remove(customer);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _dbset
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
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