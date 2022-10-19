using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IGoalProductionLineRangeRepository
    {
        Task<GoalProductionLineRange> GetByIdAsync(int id);
        Task<List<GoalProductionLineRange>> GetByGoalProductionLineIdAsync(int id);
        Task<List<GoalProductionLineRange>> GetAllAsync();
        void Add(GoalProductionLineRange goalProductionLineRange);
        void Add(List<GoalProductionLineRange> goalProductionLineRange);
        void Update(GoalProductionLineRange goalProductionLineRange);
        void Update(List<GoalProductionLineRange> goalProductionLineRange);
        void Delete(GoalProductionLineRange goalProductionLineRange);
        void Delete(List<GoalProductionLineRange> goalProductionLineRange);
        bool Save();
    }
}