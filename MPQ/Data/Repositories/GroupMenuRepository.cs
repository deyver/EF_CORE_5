using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class GroupMenuRepository : IGroupMenuRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<GroupMenu> _dbset;

        public GroupMenuRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<GroupMenu>();
        }

        public void Add(GroupMenu groupMenu)
        {
            _dbset.Add(groupMenu);
        }

        public void Add(List<GroupMenu> groupMenu)
        {
            _dbset.AddRange(groupMenu);
        }

        public void Update(GroupMenu groupMenu)
        {
            _dbset.Update(groupMenu);
        }

        public void Update(List<GroupMenu> groupMenu)
        {
            _dbset.UpdateRange(groupMenu);
        }

        public void Delete(GroupMenu groupMenu)
        {
            _dbset.Remove(groupMenu).State = EntityState.Deleted;
        }

        public void Delete(List<GroupMenu> groupMenu)
        {
            _dbset.RemoveRange(groupMenu);
        }

        public async Task<List<GroupMenu>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.Group).AsNoTracking()
                .Include(p => p.Menu).AsNoTracking()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GroupMenu> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Group).AsNoTracking()
                .Include(p => p.Menu).AsNoTracking()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<GroupMenu>> GetByGroupIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Group).AsNoTracking()
                .Include(p => p.Menu).AsNoTracking()
                .Where(p => p.GroupId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<GroupMenu>> GetByMenuIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Group).AsNoTracking()
                .Include(p => p.Menu).AsNoTracking()
                .Where(p => p.MenuId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public bool Save()
        {
            int result = _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return result > 0;
        }
    }
}