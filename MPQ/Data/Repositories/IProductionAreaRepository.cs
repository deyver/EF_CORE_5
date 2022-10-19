using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IProductionAreaRepository
    {
        Task<ProductionArea> GetByIdAsync(int Id);
        Task<List<ProductionArea>> GetAllAsync();
        void Add(ProductionArea customer);
        void Update(ProductionArea customer);
        void Delete(ProductionArea customer);
        bool Save();
    }
}