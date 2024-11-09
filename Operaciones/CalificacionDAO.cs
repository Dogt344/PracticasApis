using PracticasApis.Context;
using PracticasApis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticasApis.Operaciones
{
    public class CalificacionDAO
    {
        ProyectoContext contexto = new ProyectoContext();

        //Accion para  conseguir calificaciones de una matricula
        public List<Calificacion> seleccionar(int id)
        {
            var calificaciones = contexto.Calificacions.Where(c => c.MatriculaId == id).ToList();
            return calificaciones;
        }

        public bool agregarCalificacion(Calificacion calificacion)
        {
            try
            {
                contexto.Calificacions.Add(calificacion);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Accion para borrar una calificacion
        public bool eliminarCalificacion(int id)
        {
            try
            {
                var calificacion = contexto.Calificacions.Where(c => c.Id == id).FirstOrDefault();
                if (calificacion != null)
                {
                    contexto.Calificacions.Remove(calificacion);
                    contexto.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
