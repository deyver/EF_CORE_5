using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IMenuRepository
    {
        Task<Menu> GetByIdAsync(int Id);
        Task<List<Menu>> GetByUrlAsync(string Login);
        Task<List<Menu>> GetAllAsync();
        void Add(Menu menu);
        void Add(List<Menu> menu);
        void Update(Menu menu);
        void Update(List<Menu> menu);
        void Delete(Menu menu);
        void Delete(List<Menu> menu);
        bool Save();
    }
}