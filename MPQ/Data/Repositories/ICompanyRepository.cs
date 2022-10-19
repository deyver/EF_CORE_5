using MPQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company> GetByIdAsync(int Id);
        //Task<Company> GetByLoginAsync(string Login);
        Task<List<Company>> GetAllAsync();
        Task<List<Company>> GetBySiteIdAsync(string NomeSite);
        void Add(Company company);
        void Update(Company company);
        void Delete(Company company);
        bool Save();
    }
}