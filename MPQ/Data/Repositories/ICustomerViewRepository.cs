using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface ICustomerViewRepository
    {
        Task<CustomerView> GetByIdAsync(int Id);
        Task<List<CustomerView>> GetAllAsync();
        Task<List<CustomerView>> GetBySiteIdAsync(int Id);
        void Add(CustomerView customerView);
        void Add(List<CustomerView> customerView);
        void Update(CustomerView customerView);
        void Update(List<CustomerView> customerView);
        void Delete(CustomerView customerView);
        void Delete(List<CustomerView> customerView);
        bool Save();
    }
}