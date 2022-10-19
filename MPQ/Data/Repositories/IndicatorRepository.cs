using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class IndicatorRepository : IIndicatorRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Indicator> _dbset;

        public IndicatorRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<Indicator>();
        }

        public void Add(Indicator indicator)
        {
            _dbset.Add(indicator);
        }

        public void Update(Indicator indicator)
        {
            _dbset.Update(indicator);
        }

        public void Delete(Indicator indicator)
        {
            _dbset.Remove(indicator);
        }

        public async Task<List<Indicator>> GetAllAsync()
        {
            return await _dbset
                .ToListAsync();
        }

        public async Task<Indicator> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.GoalStfs)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<List<Indicator>> GetBySiteIdAsync(string NomeSite)
        {
            var teste = await _dbset
                .Include(p => p.GoalStfs)
                 .Where(p => p.Name == NomeSite)
                .AsNoTracking()
                .ToListAsync();
            return teste;
        }
    }
}