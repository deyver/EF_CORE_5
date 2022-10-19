using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(int Id);
        Task<List<Customer>> GetAllAsync();
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
        bool Save();
    }
}