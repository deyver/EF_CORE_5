using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int Id);
        Task<User> GetByLoginAsync(string Login);
        Task<List<User>> GetAllAsync();
        Task<List<User>> GetAllNoTrackingAsync();
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        bool Save();
    }
}