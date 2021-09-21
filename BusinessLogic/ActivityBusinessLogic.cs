using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Helper;

namespace BusinessLogic
{
    public class ActivityBusinessLogic : BusinessLogicBase<Activity>
    {

        #region Public
        public override async Task<Message<Activity>> AddAsync(Activity entidad)
        {
            //Rules:           
            Message<Activity> respuesta = new Message<Activity>();
            respuesta.IsSuccess = true;

            string mensajeFecha = string.Empty;
            if (entidad != null)
            {
                respuesta.Data = entidad;
                
                //No se pueden crear actividades si una Propiedad está desactivada           
                Property propiedad = _Context.Property.Find(entidad.Property_Id);
                if (propiedad == null)
                {
                    //throw new Exception($"No se ha asignado una propiedad válida.");
                    respuesta.ReturnMessage = "No se ha asignado una propiedad válida";
                    respuesta.IsSuccess = false;
                    return respuesta;
                }

                if (propiedad.Disabled_At.HasValue == true)
                {
                    respuesta.ReturnMessage = $"No se pueden crear actividades si una Propiedad está desactivada";
                    respuesta.IsSuccess = false;
                    return respuesta;
                }

                if (EsFechaValida(entidad, out mensajeFecha) == false)
                {
                    respuesta.ReturnMessage = mensajeFecha;
                    respuesta.IsSuccess = false;
                    return respuesta;
                    //TODO: Validar el comportamiento de intervalos de 1 hora por actividad. Posible ajuste de la tabla Activity para agregar campos Schedule_Start, Schedule_End
                }

                if (respuesta.IsSuccess)
                {
                    entidad.Update_At = DateTime.Now;
                    _Context.Add(entidad);
                    await this.SaveAsync();
                }
               
            }
            return respuesta;
        }

        public async Task<Message<Activity>> ReagendarAsync(Activity activity)
        {
            string mensajeValidacion = string.Empty;
            Message<Activity> resultado = new Message<Activity>();
            
            if (activity != null)
            {
                if (EsFechaValida(activity, out mensajeValidacion) == false)
                {
                    
                    resultado.ReturnMessage = mensajeValidacion;
                    return resultado;
                }

                if (string.Compare(activity.Status, "CANCELADA") == 0)
                {
                    resultado.ReturnMessage = "No se pueden re-agendar actividades canceladas";
                    return resultado;
                }

                activity.Update_At = DateTime.Now;
                await this.SaveAsync();
                resultado.IsSuccess = true;
                resultado.Data = activity;
                resultado.ReturnMessage = "Se ha re-agendado la actividad";
            }
            return resultado;
        }

        public async Task<Message<Activity>> CancelAsync(int idActividad)
        {
            Message<Activity> respuesta = new Message<Activity>();
            Activity activity = this._Context.Activity.Find(idActividad);

            if (activity == null)
            {
                respuesta.ReturnMessage = "No se pudo recuperar la información de la actividad";
            }

            if (activity != null)
            {
                activity.Status = "CANCELADA";
                activity.Update_At = DateTime.Now;
                this.Update(activity);
                await this.SaveAsync();
                respuesta.IsSuccess = true;
                respuesta.ReturnMessage = "Se ha cambiado el estatus de la actividad";
                respuesta.Data = activity;
            }
            return respuesta;
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
                propiedad.Activities = _Context.Activity.Where(x => x.Property_Id == propiedad.Id).ToList();
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
