using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IGoalStfRepository
    {
        Task<GoalStf> GetByIdAsync(int id);
        Task<List<GoalStf>> GetAllAsync();
        void Add(GoalStf goalStf);
        void Add(List<GoalStf> goalStf);
        void Update(GoalStf goalStf);
        void Update(List<GoalStf> goalStf);
        void Delete(GoalStf goalStf);
        void Delete(List<GoalStf> goalStf);
        bool Save();
    }
}