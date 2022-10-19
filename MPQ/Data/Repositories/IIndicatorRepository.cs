using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IIndicatorRepository
    {
        Task<Indicator> GetByIdAsync(int Id);
        Task<List<Indicator>> GetAllAsync();
        Task<List<Indicator>> GetBySiteIdAsync(string NomeSite);
        void Add(Indicator company);
        void Update(Indicator company);
        void Delete(Indicator company);
        bool Save();
    }
}