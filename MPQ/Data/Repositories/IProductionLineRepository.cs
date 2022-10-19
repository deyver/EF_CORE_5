using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IProductionLineRepository
    {
        Task<ProductionLine> GetByIdAsync(int id);
        Task<List<ProductionLine>> GetByCustomerIdAsync(int id);
        Task<List<ProductionLine>> GetByProjectIdAsync(int id);
        Task<List<ProductionLine>> GetByProductionAreaIdAsync(int id);
        Task<List<ProductionLine>> GetAllAsync();
        void Add(ProductionLine productionLine);
        void Add(List<ProductionLine> productionLine);
        void Update(ProductionLine productionLine);
        void Update(List<ProductionLine> productionLine);
        void Delete(ProductionLine productionLine);
        void Delete(List<ProductionLine> productionLine);
        bool Save();
    }
}