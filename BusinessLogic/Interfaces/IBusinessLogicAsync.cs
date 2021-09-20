using Entities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IBusinessLogicAsync<TEntidad> where TEntidad:class
    {
        Task<Message<TEntidad>> AddAsync(TEntidad entidad);
        Task<int> SaveAsync();
        void UpdateAsync(TEntidad entidad);
        void DeleteAsync(TEntidad entidad);
        Task<List<TEntidad>> GetAsync();
        Task<TEntidad> GetAsync(int id);

    }
}
