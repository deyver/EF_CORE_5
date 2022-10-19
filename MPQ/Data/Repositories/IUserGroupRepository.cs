using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
   public interface IUserGroupRepository
    {
        Task<UserGroup> GetByIdAsync(int Id);
        Task<List<UserGroup>> GetByUserIdAsync(int Id);
        Task<List<UserGroup>> GetByGroupIdAsync(int id);
        Task<List<UserGroup>> GetAllAsync();
        void Add(UserGroup userGroup);
        void Add(List<UserGroup> userGroup);
        void Update(UserGroup userGroup);
        void Update(List<UserGroup> userGroup);
        void Delete(UserGroup userGroup);
        void Delete(List<UserGroup> userGroup);
        bool Save();
    }
}