using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Company> _dbset;

        public CompanyRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<Company>();
        }

        public void Add(Company company)
        {
            _dbset.Add(company);
        }

        public void Update(Company company)
        {
            _dbset.Update(company);
        }

        public void Delete(Company company)
        {
            _dbset.Remove(company);
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _dbset
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Sites)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<List<Company>> GetBySiteIdAsync(string NomeSite)
        {
            var teste = await _dbset
                .Include(p => p.Sites)
                 .Where(p => p.Name == NomeSite)
                .AsNoTracking()
                .ToListAsync();
            return teste;
        }
    }
}