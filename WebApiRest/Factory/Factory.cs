using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiRest.Factory
{
    public interface IFactory
    {
        IVentaRepository CreateVentaRepository();
        IDetalleVentaRepository CreateDetalleVentaRepository();
        ILibroRepository CreateLibroRepository();
        IClienteRepository CreateClienteRepository();
    }

    public class Factory : IFactory
    {
        private readonly LibreriaDBEntities _context;
        public Factory(LibreriaDBEntities context)
        {
            _context = context;
        }
        public IClienteRepository CreateClienteRepository()
        {
            return new ClienteRepository(_context);
        }

        public IDetalleVentaRepository CreateDetalleVentaRepository()
        {
            return new DetalleVentaRepository(_context);
        }

        public ILibroRepository CreateLibroRepository()
        {
            return new LibroRepository(_context);
        }

        public IVentaRepository CreateVentaRepository()
        {
            return new VentaRepository(_context);
        }
    }
}