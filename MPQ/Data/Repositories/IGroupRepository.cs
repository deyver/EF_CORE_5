using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
   public interface IGroupRepository
    {
        Task<Group> GetByIdAsync(int Id);
        Task<List<Group>> GetAllAsync();
        void Add(Group group);
        void Add(List<Group> group);
        void Update(Group group);
        void Update(List<Group> group);
        void Delete(Group group);
        void Delete(List<Group> group);
        bool Save();
    }
}