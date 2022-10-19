using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class MetaRepository : IMetaRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Goal> _dbset;

        public MetaRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<Goal>();
        }

        public void Add(Goal indicator)
        {
            _dbset.Add(indicator);
        }

        public void Update(Goal indicator)
        {
            _dbset.Update(indicator);
        }

        public void Delete(Goal indicator)
        {
            _dbset.Remove(indicator);
        }

        public async Task<List<Goal>> GetAllAsync()
        {
            return await _dbset
                .ToListAsync();
        }

        public async Task<Goal> GetByIdAsync(int id)
        {
            return await _dbset
                //.Include(p => p.MetaSTF)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<List<Goal>> GetBySiteIdAsync(string NomeSite)
        {
            var teste = await _dbset
                //.Include(p => p.GoalSTFs)
                 .Where(p => p.Name == NomeSite)
                .AsNoTracking()
                .ToListAsync();
            return teste;
        }
    }
}