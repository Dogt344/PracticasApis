using PracticasApis.Context;
using PracticasApis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticasApis.Operaciones
{
    public class ProfesorDAO
    {
        public ProyectoContext contexto = new ProyectoContext();

        public Profesor login(string usuario, string pass)
        {
            var profe = contexto.Profesors.Where(p=> p.Usuario == usuario && p.Pass == pass).FirstOrDefault();
            return profe;
        }
    }
}
