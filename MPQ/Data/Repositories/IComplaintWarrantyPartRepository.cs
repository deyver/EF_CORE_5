using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IComplaintWarrantyPartRepository
    {
        Task<ComplaintWarrantyPart> GetByIdAsync(int Id);
        Task<List<ComplaintWarrantyPart>> GetAllAsync();
        Task<List<ComplaintWarrantyPart>> GetBySiteIdAsync(int id);
        void Add(ComplaintWarrantyPart complaintWarrantyPart);
        void Add(List<ComplaintWarrantyPart> complaintWarrantyPart);
        void Update(ComplaintWarrantyPart complaintWarrantyPart);
        void Update(List<ComplaintWarrantyPart> complaintWarrantyPart);
        void Delete(ComplaintWarrantyPart complaintWarrantyPart);
        void Delete(List<ComplaintWarrantyPart> complaintWarrantyPart);
        bool Save();
    }
}