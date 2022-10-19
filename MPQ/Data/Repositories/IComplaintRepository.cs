using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IComplaintRepository
    {
        Task<Complaint> GetByIdAsync(int Id);
        Task<List<Complaint>> GetBySiteIdAsync(int Id);
        Task<List<Complaint>> GetAllAsync();
        void Add(Complaint complaint);
        void Add(List<Complaint> complaint);
        void Update(Complaint complaint);
        void Update(List<Complaint> complaint);
        void Delete(Complaint complaint);
        void Delete(List<Complaint> complaint);
        bool Save();
    }
}