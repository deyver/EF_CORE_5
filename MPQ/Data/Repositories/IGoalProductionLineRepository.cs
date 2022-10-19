using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IGoalProductionLineRepository
    {
        Task<GoalProductionLine> GetByIdAsync(int id);
        Task<List<GoalProductionLine>> GetBySiteIdAsync(int id);
        Task<List<GoalProductionLine>> GetByCustomerIdAsync(int id);
        Task<List<GoalProductionLine>> GetByProjectIdAsync(int? id);
        Task<List<GoalProductionLine>> GetByProductionLineIdAsync(int? id);
        Task<List<GoalProductionLine>> GetAllAsync();
        void Add(GoalProductionLine goalProductionLine);
        void Add(List<GoalProductionLine> goalProductionLine);
        void Update(GoalProductionLine goalProductionLine);
        void Update(List<GoalProductionLine> goalProductionLine);
        void Delete(GoalProductionLine goalProductionLine);
        void Delete(List<GoalProductionLine> goalProductionLine);
        bool Save();
    }
}