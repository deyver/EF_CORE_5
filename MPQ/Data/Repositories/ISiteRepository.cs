using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
   public interface ISiteRepository
    {
        Task<Site> GetByIdAsync(int Id);
        Task<List<Site>> GetAllAsync();
        void Add(Site site);
        void Update(Site site);
        void Delete(Site site);
        bool Save();
    }
}
