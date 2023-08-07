using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IDetalleVentaRepository
    {
        IEnumerable<DetalleVenta> ObtenerDetallesxVenta(long ventaId);
        DetalleVenta ObtenerDetalleVenta(long id);
        void AgregarDetalleVenta(DetalleVenta detalleVenta);
        void EliminarDetalleVenta(long id);
        void ActualizarDetalleVenta(long id, DetalleVenta detalleVenta);
    }
    public class DetalleVentaRepository : IDetalleVentaRepository
    {
        private readonly LibreriaDBEntities _context;
        public DetalleVentaRepository(LibreriaDBEntities context)
        {
            _context = context;
        }
        public void ActualizarDetalleVenta(long id, DetalleVenta detalleVenta)
        {
            _context.ActualizarDetalleVenta(id, detalleVenta.Venta, detalleVenta.Libro, detalleVenta.Cantidad);
            _context.SaveChanges();
        }

        public void AgregarDetalleVenta(DetalleVenta detalleVenta)
        {
            _context.AgregarDetalleVenta(detalleVenta.Venta, detalleVenta.Libro, detalleVenta.Cantidad);
            _context.SaveChanges();
        }

        public void EliminarDetalleVenta(long id)
        {
            _context.EliminarDetalleVenta(id);
            _context.SaveChanges();
        }

        public DetalleVenta ObtenerDetalleVenta(long id)
        {
            var result = _context.ObtenerDetalleVenta(id);
            var detallesVenta = result.Select(r => new DetalleVenta
            {
                Id = r.Id,
                Venta = r.Venta,
                Libro = r.Libro
            }).FirstOrDefault();
            return detallesVenta;
            //return _context.DetalleVenta.Where(d=>d.Id == id).ToList();
        }

        public IEnumerable<DetalleVenta> ObtenerDetallesxVenta(long ventaId)
        {
            var result = _context.ObtenerDetallesVenta(ventaId);
            var detallesVenta = result.Select(r => new DetalleVenta
            {
                Id = r.Id,
                Venta = r.Venta,
                Libro = r.Libro
            });
            return detallesVenta;
            //return _context.DetalleVenta.Where(d => d.Venta == ventaId).ToList();
        }
    }
}
