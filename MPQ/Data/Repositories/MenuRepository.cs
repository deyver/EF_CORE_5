using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Menu> _dbset;

        public MenuRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<Menu>();
        }

        public void Add(Menu menu)
        {
            _dbset.Add(menu);
        }

        public void Add(List<Menu> menu)
        {
            _dbset.AddRange(menu);
        }

        public void Update(Menu menu)
        {
            _dbset.Update(menu);
        }

        public void Update(List<Menu> menu)
        {
            _dbset.UpdateRange(menu);
        }

        public void Delete(Menu menu)
        {
            _dbset.Remove(menu);
        }

        public void Delete(List<Menu> menu)
        {
            _dbset.RemoveRange(menu);
        }

        public async Task<List<Menu>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.GroupMenus)
                .AsNoTracking()
                .OrderBy(p => p.Sequence)
                .ThenBy(p => p.Level)
                .ToListAsync();
        }

        public async Task<Menu> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.GroupMenus)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Menu>> GetByUrlAsync(string url)
        {
            return await _dbset
                .Include(p => p.GroupMenus)
                .AsNoTracking()
                .OrderBy(p => p.Sequence)
                .ThenBy(p => p.Level)
                .Where(p => p.Url.ToLower() == url.ToLower())
                .ToListAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}