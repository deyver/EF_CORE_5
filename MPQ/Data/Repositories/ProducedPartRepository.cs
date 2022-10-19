using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class ProducedPartRepository : IProducedPartRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<ProducedPart> _dbset;

        public ProducedPartRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<ProducedPart>();
        }

        public void Add(ProducedPart producedPart)
        {
            _dbset.Add(producedPart);
        }

        public void Add(List<ProducedPart> producedPart)
        {
            _dbset.AddRange(producedPart);
        }

        public void Update(ProducedPart producedPart)
        {
            _dbset.Update(producedPart);
        }

        public void Update(List<ProducedPart> producedPart)
        {
            _dbset.UpdateRange(producedPart);
        }

        public void Delete(ProducedPart producedPart)
        {
            _dbset.Remove(producedPart);
        }

        public void Delete(List<ProducedPart> producedPart)
        {
            _dbset.RemoveRange(producedPart);
        }

        public async Task<List<ProducedPart>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.ProductionArea)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProducedPart> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.ProductionArea)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
