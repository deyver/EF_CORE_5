using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IGoalCustomerRangeRepository
    {
        Task<GoalCustomerRange> GetByIdAsync(int id);
        Task<List<GoalCustomerRange>> GetAllAsync();
        Task<List<GoalCustomerRange>> GetByGoalCustomerIdAsync(int id);
        void Add(GoalCustomerRange goalCustomerRange);
        void Add(List<GoalCustomerRange> goalCustomerRange);
        void Update(GoalCustomerRange goalCustomerRange);
        void Update(List<GoalCustomerRange> goalCustomerRange);
        void Delete(GoalCustomerRange goalCustomerRange);
        void Delete(List<GoalCustomerRange> goalCustomerRange);
        bool Save();
    }
}