using PracticasApis.Context;
using PracticasApis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticasApis.Operaciones
{
    public class OrdeneDao
    {
        private readonly ProyectoContext _contexto = new ProyectoContext();

        // Seleccionar todas las órdenes
        public List<Ordene> SeleccionarTodas()
        {
            return _contexto.Ordenes.ToList();
        }

        // Seleccionar orden por ID
        public Ordene? SeleccionarPorId(int idOrden)
        {
            return _contexto.Ordenes.FirstOrDefault(o => o.IdOrden == idOrden);
        }

        // Insertar una nueva orden
        public bool Insertar(DateOnly fechaOrden, decimal monto, int idCliente)
        {
            try
            {
                Ordene orden = new Ordene
                {
                    FechaOrden = fechaOrden,
                    Monto = monto,
                    IdCliente = idCliente
                };

                _contexto.Ordenes.Add(orden);
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Actualizar una orden existente
        public bool Actualizar(int idOrden, DateOnly fechaOrden, decimal monto, int idCliente)
        {
            try
            {
                var orden = SeleccionarPorId(idOrden);
                if (orden == null) return false;

                orden.FechaOrden = fechaOrden;
                orden.Monto = monto;
                orden.IdCliente = idCliente;

                _contexto.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Eliminar una orden por ID
        public bool Eliminar(int idOrden)
        {
            try
            {
                var orden = SeleccionarPorId(idOrden);
                if (orden == null) return false;

                _contexto.Ordenes.Remove(orden);
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Seleccionar órdenes por cliente
        public List<Ordene> SeleccionarPorCliente(int idCliente)
        {
            return _contexto.Ordenes
                .Where(o => o.IdCliente == idCliente)
                .ToList();
        }
    }
}
