using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class ProductionAreaRepository : IProductionAreaRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<ProductionArea> _dbset;

        public ProductionAreaRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<ProductionArea>();
        }

        public void Add(ProductionArea productionArea)
        {
            _dbset.Add(productionArea);
        }

        public void Update(ProductionArea productionArea)
        {
            _dbset.Update(productionArea);
        }

        public void Delete(ProductionArea productionArea)
        {
            _dbset.Remove(productionArea);
        }

        public async Task<List<ProductionArea>> GetAllAsync()
        {
            return await _dbset
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductionArea> GetByIdAsync(int id)
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