using PracticasApis.Context;
using PracticasApis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticasApis.Operaciones
{
    public class ClientesDao
    {
        public readonly ProyectoContext _contexto = new ProyectoContext();

        // Seleccionar todos los clientes
        public List<Cliente> SeleccionarTodos()
        {
            return _contexto.Clientes.ToList();
        }

        // Seleccionar cliente por ID
        public Cliente? SeleccionarPorId(int idCliente)
        {
            return _contexto.Clientes.FirstOrDefault(c => c.IdCliente == idCliente);
        }

        // Insertar un nuevo cliente
        public bool Insertar(string nombre, string apellido, string? email, string? telefono)
        {
            try
            {
                Cliente cliente = new Cliente
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Email = email,
                    Telefono = telefono
                };

                _contexto.Clientes.Add(cliente);
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Actualizar un cliente existente
        public bool Actualizar(int idCliente, string nombre, string apellido, string? email, string? telefono)
        {
            try
            {
                var cliente = SeleccionarPorId(idCliente);
                if (cliente == null) return false;

                cliente.Nombre = nombre;
                cliente.Apellido = apellido;
                cliente.Email = email;
                cliente.Telefono = telefono;

                _contexto.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Eliminar un cliente por ID
        public bool Eliminar(int idCliente)
        {
            try
            {
                var cliente = SeleccionarPorId(idCliente);
                if (cliente == null) return false;

                _contexto.Clientes.Remove(cliente);
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Seleccionar clientes con sus órdenes
        public List<Cliente> SeleccionarClientesConOrdenes()
        {
            return _contexto.Clientes
                .Where(c => c.Ordenes.Any()) // Filtra solo clientes con órdenes
                .ToList();
        }
    }
}
