using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<UserGroup> _dbset;

        public UserGroupRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<UserGroup>();
        }

        public void Add(UserGroup userGroup)
        {
            _dbset.Add(userGroup);
        }

        public void Add(List<UserGroup> userGroup)
        {
            _dbset.AddRange(userGroup);
        }

        public void Update(UserGroup userGroup)
        {
            _dbset.Update(userGroup);
        }

        public void Update(List<UserGroup> userGroup)
        {
            _dbset.UpdateRange(userGroup);
        }

        public void Delete(UserGroup userGroup)
        {
            _dbset.Remove(userGroup);
        }

        public void Delete(List<UserGroup> userGroup)
        {
            _dbset.RemoveRange(userGroup);
        }

        public async Task<List<UserGroup>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.Group)
                .Include(p => p.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<UserGroup> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Group)
                .Include(p => p.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<UserGroup>> GetByUserIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Group)
                .Include(p => p.User)
                .Where(p => p.UserId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<UserGroup>> GetByGroupIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Group)
                .Include(p => p.User)
                .Where(p => p.GroupId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}