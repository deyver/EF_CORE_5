using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Group> _dbset;

        public GroupRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<Group>();
        }

        public void Add(Group group)
        {
            _dbset.Add(group);
        }

        public void Add(List<Group> group)
        {
            _dbset.AddRange(group);
        }

        public void Update(Group group)
        {            
            _dbset.Update(group).State = EntityState.Modified;
        }

        public void Update(List<Group> group)
        {
            _dbset.UpdateRange(group);
        }

        public void Delete(Group group)
        {
            _dbset.Remove(group);
        }

        public void Delete(List<Group> group)
        {
            _dbset.RemoveRange(group);
        }

        public async Task<List<Group>> GetAllAsync()
        {
            return await _dbset                
                .Include(p => p.UserGroups)
                .Include(p => p.GroupMenus)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.UserGroups)
                .Include(p => p.GroupMenus)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Save()
        {
            int result = _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return result > 0;
        }
    }
}