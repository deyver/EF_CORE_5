using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IBusinessUnitRepository
    {
        Task<BusinessUnit> GetByIdAsync(int id);
        Task<List<BusinessUnit>> GetAllAsync();
        void Add(BusinessUnit businessUnit);
        void Add(List<BusinessUnit> businessUnit);
        void Update(BusinessUnit businessUnit);
        void Update(List<BusinessUnit> businessUnit);
        void Delete(BusinessUnit businessUnit);
        void Delete(List<BusinessUnit> businessUnit);
        bool Save();
    }
}