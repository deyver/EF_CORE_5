using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
   public interface IUserSiteRepository
    {
        Task<UserSite> GetByIdAsync(int Id);
        Task<List<UserSite>> GetByUserIdAsync(int userId);
        Task<List<UserSite>> GetBySiteIdAsync(int id);
        Task<List<UserSite>> GetAllAsync();
        void Add(UserSite userSite);
        void Add(List<UserSite> userSite);
        void Update(UserSite userSite);
        void Update(List<UserSite> userSite);
        void Delete(UserSite userSite);
        void Delete(List<UserSite> userSite);
        bool Save();
    }
}