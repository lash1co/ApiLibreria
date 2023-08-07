using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web;
using WebApiRest.Factory;
using WebApiRest.Models;

namespace WebApiRest.Services
{
    public interface IDetalleVentaService
    {
        Response CrearDetalleVenta(DetalleVenta detalleVenta);
        void AgregarDetalleVenta(long ventaId, DetalleDTO detalleVentaDTO);
        Response EliminarDetalleVenta(long detalleId);
        Response ActualizarDetalleVenta(long detalleId, DetalleVenta detalleVenta);
        DetalleVenta ObtenerDetalleVenta(long detalleId);
        IEnumerable<DetalleVenta> ObtenerDetallesVentaPorVenta(long ventaId);
        void ActualizarVenta(DetalleVenta detalleVenta);
        void DescontarStock(DetalleVenta detalleVenta);
        void SumarStock(DetalleVenta detalleVenta);
    }

    public class DetalleVentaService : IDetalleVentaService
    {
        private readonly IDetalleVentaRepository _detalleRepo;
        private readonly IVentaRepository _ventaRepo;
        private readonly ILibroRepository _libroRepo;
        
        public DetalleVentaService(IFactory factory)
        {
            _detalleRepo = factory.CreateDetalleVentaRepository();
            _ventaRepo = factory.CreateVentaRepository();
            _libroRepo = factory.CreateLibroRepository();
        }

        public Response ActualizarDetalleVenta(long detalleId, DetalleVenta detalleVentaActualizar)
        {
            if (detalleId != detalleVentaActualizar.Id)
                return new Response(HttpStatusCode.BadRequest, "Los Ids no coinciden", detalleVentaActualizar);
            if (detalleVentaActualizar.Venta > 0 || detalleVentaActualizar.Libro > 0)
                return new Response(HttpStatusCode.BadRequest, "Parametro(s) no valido(s)", detalleVentaActualizar);
            var detalle = _detalleRepo.ObtenerDetalleVenta(detalleId);
            if (detalle == null)
                return new Response(HttpStatusCode.NotFound, "El detalle de venta no existe", detalle);
            var libro = _libroRepo.ObtenerLibro(detalleVentaActualizar.Libro);
            if (libro == null)
                return new Response(HttpStatusCode.NotFound, "El libro no existe", detalleVentaActualizar);
            if (detalleVentaActualizar.Cantidad > detalle.Cantidad)
            {
                var limite = detalleVentaActualizar.Cantidad - detalle.Cantidad;
                if (libro.Cantidad < limite)
                    return new Response(HttpStatusCode.BadRequest, "El libro no tiene suficiente stock", detalleVentaActualizar);
            }
            var venta = _ventaRepo.ObtenerVenta(detalleVentaActualizar.Venta);
            if (venta == null)
                return new Response(HttpStatusCode.NotFound, "La venta no existe", detalleVentaActualizar);
            try
            {
                _detalleRepo.ActualizarDetalleVenta(detalleId, detalleVentaActualizar);
                ActualizarVenta(detalleVentaActualizar);
                if(detalleVentaActualizar.Cantidad > detalle.Cantidad)
                    DescontarStock(detalleVentaActualizar);
                if(detalleVentaActualizar.Cantidad < detalle.Cantidad)
                    SumarStock(detalleVentaActualizar);
                return new Response(HttpStatusCode.OK, "Detalle de venta actualizado", detalleVentaActualizar);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, ex.Message, detalleVentaActualizar);
            }
        }

        public Response CrearDetalleVenta(DetalleVenta detalleVenta)
        {
            if (detalleVenta.Venta > 0 || detalleVenta.Libro > 0)
                return new Response(HttpStatusCode.BadRequest, "Parametro(s) no valido(s)", detalleVenta);
            var libro = _libroRepo.ObtenerLibro(detalleVenta.Libro);
            if (libro == null)
                return new Response(HttpStatusCode.NotFound, "El libro no existe", detalleVenta);
            if(libro.Cantidad < detalleVenta.Cantidad)
                return new Response(HttpStatusCode.BadRequest, "El libro no tiene suficiente stock", detalleVenta);
            var venta = _ventaRepo.ObtenerVenta(detalleVenta.Venta);
            if (venta == null)
                return new Response(HttpStatusCode.NotFound, "La venta no existe", detalleVenta);
            try
            {
                _detalleRepo.AgregarDetalleVenta(detalleVenta);
                ActualizarVenta(detalleVenta);
                DescontarStock(detalleVenta);
                return new Response(HttpStatusCode.Created, "Detalle de venta creado", detalleVenta);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, ex.Message, detalleVenta);
            }
        }

        public Response EliminarDetalleVenta(long detalleId)
        {
            var detalle = _detalleRepo.ObtenerDetalleVenta(detalleId);
            if (detalle == null)
                return new Response(HttpStatusCode.NotFound, "No existe el detalle de venta", detalle);
            try
            {
                _detalleRepo.EliminarDetalleVenta(detalleId);
                ActualizarVenta(detalle);
                SumarStock(detalle);
                return new Response(HttpStatusCode.OK, "Detalle de venta eliminado", detalle);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, ex.Message, detalle);
            }
        }

        public DetalleVenta ObtenerDetalleVenta(long detalleId)
        {
            return _detalleRepo.ObtenerDetalleVenta(detalleId);
        }

        public IEnumerable<DetalleVenta> ObtenerDetallesVentaPorVenta(long ventaId)
        {
            return _detalleRepo.ObtenerDetallesxVenta(ventaId);
        }

        public void ActualizarVenta(DetalleVenta detalleVenta)
        {
            var venta = _ventaRepo.ObtenerVenta(detalleVenta.Venta);
            var detalles = _detalleRepo.ObtenerDetallesxVenta(venta.Id);
            decimal total = 0;
            foreach (var detalle in detalles)
            {
                total += decimal.Parse(detalle.Cantidad.ToString()) * detalle.Libro1.Precio;
            }
            venta.Total = total;
            _ventaRepo.ActualizarVenta(venta.Id, venta);
        }

        public void DescontarStock(DetalleVenta detalleVenta)
        {
            var libro = _libroRepo.ObtenerLibro(detalleVenta.Libro);
            if(libro != null && libro.Cantidad >= detalleVenta.Cantidad)
            {
                libro.Cantidad = libro.Cantidad - detalleVenta.Cantidad;
            }
            _libroRepo.ActualizarLibro(libro.Id, libro);
        }

        public void AgregarDetalleVenta(long ventaId, DetalleDTO detalleVentaDTO)
        {
            var libro = _libroRepo.ObtenerLibro(detalleVentaDTO.Libro);
            if (libro != null && detalleVentaDTO.Cantidad > 0 && ventaId > 0 && libro.Cantidad >= detalleVentaDTO.Cantidad) 
            { 
                var detalleVenta = new DetalleVenta();
                detalleVenta.Venta = ventaId;
                detalleVenta.Cantidad = detalleVentaDTO.Cantidad;
                detalleVenta.Libro = libro.Id;
                try
                {
                    _detalleRepo.AgregarDetalleVenta(detalleVenta);
                    ActualizarVenta(detalleVenta);
                    DescontarStock(detalleVenta);
                }
                catch (Exception ex)
                { 
                
                }
            }
        }

        public void SumarStock(DetalleVenta detalleVenta)
        {
            var libro = _libroRepo.ObtenerLibro(detalleVenta.Libro);
            if (libro != null && libro.Cantidad >= detalleVenta.Cantidad)
            {
                libro.Cantidad = libro.Cantidad + detalleVenta.Cantidad;
            }
            _libroRepo.ActualizarLibro(libro.Id, libro);
        }
    }
}