using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<User> _dbset;

        public UserRepository(ApplicationContext context) 
        {
            _context = context;
            _dbset = _context.Set<User>();
        }

        public void Add(User user)
        {
            _dbset.Add(user);
        }

        public void Update(User user)
        {
            _dbset.Update(user);
        }

        public void Delete(User user)
        {
            _dbset.Remove(user);
        }

        public async Task<List<User>> GetAllNoTrackingAsync()
        {
            return await _dbset
                .Include(p => p.DefaultSite)
                .Include(p => p.UserGroups)
                .Include(p => p.UserSites)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.DefaultSite)
                .Include(p => p.UserGroups)
                .Include(p => p.UserSites)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.DefaultSite)
                .Include(p => p.UserGroups)
                .Include(p => p.UserSites)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _dbset
                .Include(p => p.DefaultSite)
                .Include(p => p.UserGroups)
                .Include(p => p.UserSites)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Login == login);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

    }
}