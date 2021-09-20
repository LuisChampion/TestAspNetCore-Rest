using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic
{
    public class PropertyBusinessLogic: BusinessLogicBase<Entities.Property>
    {
        public override Property Add(Property entidad)
        {
            var newEntidad = new Property();
            newEntidad.Id = 2;
            newEntidad.Title = "Algo";
            newEntidad.Address = "xx";
            newEntidad.Description = "cxx";
            newEntidad.Created_At = new DateTime(2021, 1, 1);
            newEntidad.Disabled_At = new DateTime(2021, 1, 1);
            newEntidad.Update_At = new DateTime(2021, 1, 1);
            newEntidad.Status = "Creado";
            _Context.Add(newEntidad);
            _Context.SaveChanges();
            return newEntidad;
        }
    }
}
