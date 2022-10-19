using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Site> _dbset;

        public SiteRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<Site>();
        }

        public void Add(Site site)
        {
            _dbset.Add(site);
        }

        public void Update(Site site)
        {
            _dbset.Update(site);
        }

        public void Delete(Site site)
        {
            _dbset.Remove(site);
        }

        public async Task<List<Site>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.Company)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Site> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Company)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}