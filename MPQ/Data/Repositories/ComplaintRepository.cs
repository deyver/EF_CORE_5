using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Complaint> _dbset;

        public ComplaintRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<Complaint>();
        }

        public void Add(Complaint complaint)
        {
            _dbset.Add(complaint);
        }

        public void Add(List<Complaint> complaint)
        {
            _dbset.AddRange(complaint);
        }

        public void Update(Complaint complaint)
        {
            _dbset.Update(complaint);
        }

        public void Update(List<Complaint> complaint)
        {
            _dbset.UpdateRange(complaint);
        }

        public void Delete(Complaint complaint)
        {
            _dbset.Remove(complaint);
        }

        public void Delete(List<Complaint> complaint)
        {
            _dbset.RemoveRange(complaint);
        }

        public async Task<List<Complaint>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.BusinessUnit)
                .Include(p => p.Customer)
                .Include(p => p.Indicator)
                .Include(p => p.Site)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Complaint> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.BusinessUnit)
                .Include(p => p.Customer)
                .Include(p => p.Indicator)
                .Include(p => p.Site)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Complaint>> GetBySiteIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.BusinessUnit)
                .Include(p => p.Customer)
                .Include(p => p.Indicator)
                .Include(p => p.Site)                
                .Where(p => p.SiteId == id)
                .AsNoTracking()
                .ToListAsync();
        }
        
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}