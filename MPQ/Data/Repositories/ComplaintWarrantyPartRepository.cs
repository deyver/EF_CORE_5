using Microsoft.EntityFrameworkCore;
using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public class ComplaintWarrantyPartRepository : IComplaintWarrantyPartRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<ComplaintWarrantyPart> _dbset;

        public ComplaintWarrantyPartRepository(ApplicationContext context)
        {
            _context = context;
            _dbset = _context.Set<ComplaintWarrantyPart>();
        }

        public void Add(ComplaintWarrantyPart complaintWarrantyPart)
        {
            _dbset.Add(complaintWarrantyPart);
        }

        public void Add(List<ComplaintWarrantyPart> complaintWarrantyPart)
        {
            _dbset.AddRange(complaintWarrantyPart);
        }

        public void Update(ComplaintWarrantyPart complaintWarrantyPart)
        {
            _dbset.Update(complaintWarrantyPart);
        }

        public void Update(List<ComplaintWarrantyPart> complaintWarrantyPart)
        {
            _dbset.UpdateRange(complaintWarrantyPart);
        }

        public void Delete(ComplaintWarrantyPart complaintWarrantyPart)
        {
            _dbset.Remove(complaintWarrantyPart);
        }

        public void Delete(List<ComplaintWarrantyPart> complaintWarrantyPart)
        {
            _dbset.RemoveRange(complaintWarrantyPart);
        }

        public async Task<List<ComplaintWarrantyPart>> GetAllAsync()
        {
            return await _dbset
                .Include(p => p.BusinessUnit)
                .Include(p => p.Customer)
                .Include(p => p.Site)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ComplaintWarrantyPart> GetByIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.BusinessUnit)
                .Include(p => p.Customer)
                .Include(p => p.Site)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<ComplaintWarrantyPart>> GetBySiteIdAsync(int id)
        {
            return await _dbset
                .Include(p => p.BusinessUnit)
                .Include(p => p.Customer)
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