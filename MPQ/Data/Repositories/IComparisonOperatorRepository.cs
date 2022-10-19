using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IComparisonOperatorRepository
    {
        Task<ComparisonOperator> GetByIdAsync(int id);
        Task<List<ComparisonOperator>> GetAllAsync();
        void Add(ComparisonOperator comparisonOperator);
        void Add(List<ComparisonOperator> comparisonOperator);
        void Update(ComparisonOperator comparisonOperator);
        void Update(List<ComparisonOperator> comparisonOperator);
        void Delete(ComparisonOperator comparisonOperator);
        void Delete(List<ComparisonOperator> comparisonOperator);
        bool Save();
    }
}