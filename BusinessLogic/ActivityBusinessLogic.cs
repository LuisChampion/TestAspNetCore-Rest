using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace BusinessLogic
{
    public class ActivityBusinessLogic : BusinessLogicBase<Activity>
    {

        #region Public
        public override async ValueTask<Activity> AddAsync(Activity entidad)
        {
            //Rules:           
            string mensajeFecha = string.Empty;
            if (entidad != null)
            {
                //No se pueden crear actividades si una Propiedad está desactivada           
                Property propiedad = _Context.Property.Find(entidad.Property_Id);
                if (propiedad == null)
                {
                    throw new Exception($"No se ha asignado una propiedad válida.");
                }

                if (propiedad.Disabled_At.HasValue == true)
                {
                    throw new Exception($"No se pueden crear actividades si una Propiedad está desactivada");
                }

                if (EsFechaValida(entidad, out mensajeFecha) == false)
                {
                    throw new Exception(mensajeFecha);
                    //TODO: Validar el comportamiento de intervalos de 1 hora por actividad. Posible ajuste de la tabla Activity para agregar campos Schedule_Start, Schedule_End
                }

                await _Context.AddAsync(entidad);
                await this.SaveAsync();
            }
            return entidad;
        }

        public async Task<int> ReagendarAsync(int idActividad, DateTime fecha)
        {
            string mensajeValidacion = string.Empty;

            Activity activity = this._Context.Activity.Find(idActividad);
            if (activity != null)
            {
                if (EsFechaValida(activity, out mensajeValidacion) == false)
                {
                    throw new Exception($"{mensajeValidacion}");
                }

                if (string.Compare(activity.Status, "CANCELADA") == 0)
                {
                    mensajeValidacion = "No se pueden re-agendar actividades canceladas";
                    throw new Exception($"{mensajeValidacion}");
                }
                return await this.SaveAsync();
            }
            return await Task.FromResult(0);
        }

        public Task<bool> CancelaAsync(int idActividad)
        {
            bool resultado = false;
            Activity activity = this._Context.Activity.Find(idActividad);
            
            if (activity != null)
            {
                this.Update(activity);
                this.Save();
                resultado = true;
            }
            return Task.FromResult(resultado);
        }

        
        #endregion

        #region Private
        private bool EsFechaValida(Activity activity, out string mensaje)
        {
            mensaje = string.Empty;
            if (activity == null)
            {
                mensaje = "No se ha inicializado la instancia de Actividad";
            }

            Property propiedad = null;
            if (activity.Property == null)
            {
                propiedad = _Context.Property.Find(activity.Property_Id);
            }
            else
            {
                propiedad = activity.Property;
            }

            if (propiedad != null)
            {
                //No se pueden crear actividades en la misma fecha y hora(para la misma propiedad), tomando en cuenta que cada actividad debe durar máximo una hora.
                List<Activity> actividadesConMismaFechaHora = propiedad.Activities.Where(x => x.Schedule == activity.Schedule).ToList();
                if (actividadesConMismaFechaHora != null && actividadesConMismaFechaHora.Count == 0)
                {
                    return true;
                }
                else
                {
                    mensaje = "No se pueden crear actividades en la misma fecha y hora";
                }
            }
            return false;
        }
        #endregion





    }
}
