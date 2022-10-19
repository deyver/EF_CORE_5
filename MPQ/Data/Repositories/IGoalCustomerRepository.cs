using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IGoalCustomerRepository
    {
        Task<GoalCustomer> GetByIdAsync(int id);
        Task<List<GoalCustomer>> GetAllAsync();
        GoalCustomer Add(GoalCustomer goalCustomer);
        void Add(List<GoalCustomer> goalCustomer);
        void Update(GoalCustomer goalCustomer);
        void Update(List<GoalCustomer> goalCustomer);
        void Delete(GoalCustomer goalCustomer);
        void Delete(List<GoalCustomer> goalCustomer);
        bool Save();
    }
}