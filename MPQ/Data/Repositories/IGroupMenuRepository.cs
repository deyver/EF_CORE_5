using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
   public interface IGroupMenuRepository
    {
        Task<GroupMenu> GetByIdAsync(int Id);
        Task<List<GroupMenu>> GetByGroupIdAsync(int Id);
        Task<List<GroupMenu>> GetByMenuIdAsync(int id);
        Task<List<GroupMenu>> GetAllAsync();
        void Add(GroupMenu groupMenu);
        void Add(List<GroupMenu> groupMenu);
        void Update(GroupMenu groupMenu);
        void Update(List<GroupMenu> groupMenu);
        void Delete(GroupMenu groupMenu);
        void Delete(List<GroupMenu> groupMenu);
        bool Save();
    }
}