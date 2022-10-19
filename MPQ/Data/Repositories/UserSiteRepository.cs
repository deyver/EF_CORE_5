using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class UserSiteRepository : IUserSiteRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<UserSite> _dbset;

        public UserSiteRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<UserSite>();
        }

        public void Add(UserSite userSite)
        {
            _dbset.Add(userSite);
        }

        public void Add(List<UserSite> userSite)
        {
            _dbset.AddRange(userSite);
        }

        public void Update(UserSite userSite)
        {
            _dbset.Update(userSite);
        }

        public void Update(List<UserSite> userSite)
        {
            _dbset.UpdateRange(userSite);
        }

        public void Delete(UserSite userSite)
        {
            _dbset.Remove(userSite);
        }

        public void Delete(List<UserSite> userSite)
        {
            _dbset.RemoveRange(userSite);
        }

        public async Task<List<UserSite>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<UserSite> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<UserSite>> GetByUserIdAsync(int userId)
        {
            return await _dbset
                .Include(p => p.Site)
                .Include(p => p.User)
                .Where(p => p.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<List<UserSite>> GetBySiteIdAsync(int id)
        {
            var teste = await _dbset
                .Include(p => p.Site)
                .Where(p => p.SiteId == id)
                .AsNoTracking()
                .ToListAsync();
            return teste;

        }
    }
}