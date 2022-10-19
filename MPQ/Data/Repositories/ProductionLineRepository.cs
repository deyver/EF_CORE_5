using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class ProductionLineRepository : IProductionLineRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<ProductionLine> _dbset;

        public ProductionLineRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<ProductionLine>();
        }

        public void Add(ProductionLine productionLine)
        {
            _dbset.Add(productionLine);
        }

        public void Add(List<ProductionLine> productionLine)
        {
            _dbset.AddRange(productionLine);
        }

        public void Update(ProductionLine productionLine)
        {
            _dbset.Update(productionLine);
        }

        public void Update(List<ProductionLine> productionLine)
        {
            _dbset.UpdateRange(productionLine);
        }

        public void Delete(ProductionLine productionLine)
        {
            _dbset.Remove(productionLine);
        }

        public void Delete(List<ProductionLine> productionLine)
        {
            _dbset.RemoveRange(productionLine);
        }

        public async Task<List<ProductionLine>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .Include(p => p.ProductionArea)
                .Include(p => p.Project)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductionLine> GetByIdAsync(int id)
        {
            return await _dbset                
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .Include(p => p.ProductionArea)
                .Include(p => p.Project)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<ProductionLine>> GetBySiteIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .Include(p => p.ProductionArea)
                .Include(p => p.Project)
                .Where(p => p.SiteId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<ProductionLine>> GetByCustomerIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .Include(p => p.ProductionArea)
                .Include(p => p.Project)
                .Where(p => p.CustomerId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<ProductionLine>> GetByProductionAreaIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .Include(p => p.ProductionArea)
                .Include(p => p.Project)
                .Where(p => p.ProductionAreaId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<ProductionLine>> GetByProjectIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.Customer)
                .Include(p => p.ProductionArea)
                .Include(p => p.Project)
                .Where(p => p.ProjectId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;             
        }
    }
}