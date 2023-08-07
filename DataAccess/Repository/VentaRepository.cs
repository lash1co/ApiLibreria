using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IVentaRepository
    {
        void AgregarVenta(Venta venta);
        void ActualizarVenta(long ventaId, Venta venta);
        void EliminarVenta(long ventaId);
        Venta ObtenerVenta(long ventaId);
        Venta ObtenerVenta(string codVenta);
        IEnumerable<Venta> ObtenerVentas();
        bool ExisteCodVenta(string codVenta);
        int CantidadCodVenta(string codVenta);
        long UltimoIdVenta();
    }

    public class VentaRepository : IVentaRepository
    {
        private readonly LibreriaDBEntities _context;
        public VentaRepository(LibreriaDBEntities context)
        {
            _context = context;
        }
        public void ActualizarVenta(long ventaId, Venta venta)
        {
            _context.ActualizarVenta(ventaId, venta.CodVenta, venta.Fecha, venta.PuntoVenta, venta.Cliente, venta.Total);
            _context.SaveChanges();
        }

        public void AgregarVenta(Venta venta)
        {
            _context.AgregarVenta(venta.CodVenta, venta.Fecha, venta.PuntoVenta, venta.Cliente, venta.Total);
            _context.SaveChanges();
        }

        public int CantidadCodVenta(string codVenta)
        {
            return _context.Venta.Count(v => v.CodVenta == codVenta);
        }

        public void EliminarVenta(long ventaId)
        {
            _context.EliminarVenta(ventaId); 
            _context.SaveChanges();
        }

        public bool ExisteCodVenta(string codVenta)
        {
            var venta = _context.Venta.FirstOrDefault(v=>v.CodVenta == codVenta);
            if( venta == null )
                return false;
            return true;
        }

        public Venta ObtenerVenta(long ventaId)
        {
            var result = _context.ObtenerVenta(ventaId);
            var venta = result.Select(r => new Venta
            {
                Id = r.Id,
                CodVenta = r.CodVenta,
                Cliente = r.Cliente,
                Fecha = r.Fecha,
                PuntoVenta = r.PuntoVenta,
                Total = r.Total,
            }).FirstOrDefault();
            return venta;
            //return _context.Venta.FirstOrDefault(v=>v.Id == ventaId);
        }

        public Venta ObtenerVenta(string codVenta)
        {
            return _context.Venta.FirstOrDefault(v => v.CodVenta == codVenta);
        }

        public IEnumerable<Venta> ObtenerVentas()
        {
            var result = _context.ObtenerVentas();
            var ventas = result.Select(r => new Venta
            {
                Id = r.Id,
                CodVenta = r.CodVenta,
                Cliente = r.Cliente,
                Fecha = r.Fecha,
                PuntoVenta = r.PuntoVenta,
                Total = r.Total,
            });
            return ventas;
            //return _context.Venta.ToList();
        }

        public long UltimoIdVenta()
        {
            var venta = _context.Venta.LastOrDefault();
            return venta.Id;
        }
    }
}
