using PracticasApis.Context;
using PracticasApis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticasApis.Operaciones
{
    public class AlumnoDAO
    {
        public ProyectoContext contexto = new ProyectoContext();

        public List<Alumno> seleccionarTodos()
        {
            var alumnos = contexto.Alumnos.ToList<Alumno>();
            return alumnos; 
        }

        public Alumno seleccionarId(int id)
        {
            var alumno = contexto.Alumnos.Where(a => a.Id == id).FirstOrDefault();
            return alumno;
        }

        public bool insertar(string dni, string nombre, string direccion, int edad, string email)
        {
            try
            {
                Alumno alumno = new Alumno();
                alumno.Dni = dni;
                alumno.Nombre = nombre;
                alumno.Direccion = direccion;
                alumno.Edad = edad;
                alumno.Email = email;

                contexto.Alumnos.Add(alumno);
                contexto.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool actualizar(int id, string dni, string nombre, string direccion, int edad, string email)
        {
            try
            {
                var alumno = seleccionarId(id);
                if (alumno == null)
                {
                    return false;
                }
                else
                {
                    alumno.Dni = dni;
                    alumno.Nombre = nombre;
                    alumno.Direccion = direccion;
                    alumno.Edad = edad;
                    alumno.Email = email;

                    contexto.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool eliminar(int id)
        {
            try
            {
                var alumno = seleccionarId(id);
                if (alumno == null)
                {
                    return false;
                }
                else
                {
                    contexto.Alumnos.Remove(alumno);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            { 
                return false; 
            }

        }
        public List<AlumnoAsignatura> seleccionarAlumnosAsignaturas()
        {
            var query = from a in contexto.Alumnos
                        join m in contexto.Matriculas on a.Id equals m.AlumnoId
                        join asig in contexto.Asignaturas on m.AsignaturaId equals asig.Id
                        select new AlumnoAsignatura
                        {
                            NombreAlumno = a.Nombre,
                            NombreAsignatura = asig.Nombre
                        };

            return query.ToList();
        }

        public List<AlumnoProfesor> seleccionarAlumnosProfesor(string usuario)
        {
            var query = from a in contexto.Alumnos
                        join m in contexto.Matriculas on a.Id equals m.AlumnoId
                        join asig in contexto.Asignaturas on m.AsignaturaId equals asig.Id
                        where asig.Profesor == usuario
                        select new AlumnoProfesor
                        {
                            Id = a.Id,
                            Dni = a.Dni,
                            Nombre = a.Nombre,
                            Direccion = a.Direccion,
                            Edad = a.Edad,
                            Email = a.Email,
                            Asignatura = a.Nombre
                        };
            return query.ToList();
        }

        public Alumno seleccionarPorDni(string dni)
        {
            var alumno = contexto.Alumnos.Where(a => a.Dni.Equals(dni)).FirstOrDefault();
            return alumno;
        }

        public bool insertarYMatricular(
            string dni,
            string nombre,
            string direccion,
            int edad,
            string email,
            int id_asig)
        {

            try
            {
                var existe = seleccionarPorDni(dni);
                if (existe == null)
                {
                    insertar(dni, nombre, direccion, edad, email);
                    var insertado = seleccionarPorDni(dni);
                    Matricula matricula = new Matricula();
                    matricula.AlumnoId = insertado.Id;
                    matricula.AsignaturaId = id_asig;
                    contexto.Matriculas.Add(matricula);
                    contexto.SaveChanges();
                }
                else
                {
                    Matricula matricula = new Matricula();
                    matricula.AlumnoId = existe.Id;
                    matricula.AsignaturaId = id_asig;
                    contexto.Matriculas.Add(matricula);
                    contexto.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Eliminar Alumno
        public bool eliminarAlumno(int id)
        {
            try
            {
                var alumno = contexto.Alumnos.Where(a => a.Id == id).FirstOrDefault();
                if (alumno != null)
                {
                    //Se cicla por todas las calificaciones y elimina a todos con el id del alumno,al igual que las matriculas
                    var matriculas = contexto.Matriculas.Where(m => m.AlumnoId == id);
                    foreach (Matricula m in matriculas)
                    {
                        var calificaciones = contexto.Calificacions.Where(c => c.MatriculaId == m.Id);
                        contexto.Calificacions.RemoveRange(calificaciones);
                    }
                    contexto.Matriculas.RemoveRange(matriculas);
                    contexto.Alumnos.Remove(alumno);
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
