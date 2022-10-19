using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IProducedPartRepository
    {
        void Add(ProducedPart producedPart);


        void Add(List<ProducedPart> producedPart);


        void Update(ProducedPart producedPart);


        void Update(List<ProducedPart> producedPart);


        void Delete(ProducedPart producedPart);


        void Delete(List<ProducedPart> producedPart);


        Task<List<ProducedPart>> GetAllAsync();


        Task<ProducedPart> GetByIdAsync(int id);

        bool Save();


    }
}
