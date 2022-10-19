using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Data.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> GetByIdAsync(int id);
        Task<List<Project>> GetAllAsync();
        void Add(Project project);
        void Add(List<Project> project);
        void Update(Project project);
        void Update(List<Project> project);
        void Delete(Project project);
        void Delete(List<Project> project);
        bool Save();
    }
}