using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IClienteRepository
    {
        void CrearCliente(Cliente cliente);
        void EliminarCliente(long clienteId);
        void ActualizarCliente(long clienteId, Cliente cliente);
        IEnumerable<Cliente> ObtenerClientes();
        Cliente ObtenerCliente(long clienteId);
        bool ExisteCliente(string documentoId);
        int CantidadClientes(string documentoId);

    }
    public class ClienteRepository : IClienteRepository
    {
        private readonly LibreriaDBEntities _context;

        public ClienteRepository(LibreriaDBEntities context)
        {
            _context = context;
        }

        public void ActualizarCliente(long clienteId, Cliente cliente)
        {
            _context.ActualizarCliente(clienteId, cliente.Cedula, cliente.Nombres, cliente.Telefono, cliente.Direccion);
            _context.SaveChanges();
        }

        public int CantidadClientes(string documentoId)
        {
            return _context.Cliente.Count(c => c.Cedula == documentoId);
        }

        public void CrearCliente(Cliente cliente)
        {
            _context.AgregarCliente(cliente.Cedula, cliente.Nombres, cliente.Telefono, cliente.Direccion);
            _context.SaveChanges();
        }

        public void EliminarCliente(long clienteId)
        {
            _context.EliminarCliente(clienteId);
            _context.SaveChanges();
        }

        public bool ExisteCliente(string documentoId)
        {
            var cliente = _context.Cliente.FirstOrDefault(c => c.Cedula == documentoId);
            if (cliente == null)
                return false;
            return true;
        }

        public Cliente ObtenerCliente(long clienteId)
        {
            var result = _context.ObtenerCliente(clienteId);
            var cliente = result.Select(r => new Cliente
            {
                Id = r.Id,
                Nombres = r.Nombres,
                Cedula = r.Cedula,
                Direccion = r.Direccion,
                Telefono = r.Telefono,
            }).FirstOrDefault();
            return cliente;
            //return _context.Cliente.FirstOrDefault(c=>c.Id == clienteId);
        }

        public IEnumerable<Cliente> ObtenerClientes()
        {
            var result = _context.ObtenerClientes();
            var clientes = result.Select(r => new Cliente
            {
                Id = r.Id,
                Nombres = r.Nombres,
                Cedula = r.Cedula,
                Direccion = r.Direccion,
                Telefono = r.Telefono,
            });
            return clientes;
            //return _context.Cliente.ToList();
        }
    }
}
