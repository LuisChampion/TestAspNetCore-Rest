using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IBusinessLogic<TEntidad> where TEntidad: class
    {
        TEntidad Add(TEntidad entidad);
        int Save();
        void Update(TEntidad entidad);
        void Delete(TEntidad entidad);
        List<TEntidad> Get();  
    }
}
