using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IGoalRepository
    {
        Task<Goal> GetByIdAsync(int Id);
        Task<List<Goal>> GetAllAsync();
        void Add(Goal goal);
        void Update(Goal goal);
        void Delete(Goal goal);
        bool Save();
    }
}