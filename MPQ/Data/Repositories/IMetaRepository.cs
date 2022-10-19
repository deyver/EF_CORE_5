using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IMetaRepository
    {
        Task<Goal> GetByIdAsync(int Id);
        Task<List<Goal>> GetAllAsync();
        Task<List<Goal>> GetBySiteIdAsync(string NomeSite);
        void Add(Goal goal);
        void Update(Goal goal);
        void Delete(Goal goal);
        bool Save();
    }
}