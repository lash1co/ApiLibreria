using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using WebApiRest.Factory;
using WebApiRest.Models;

namespace WebApiRest.Services
{
    public interface IVentaService
    {
        Response AgregarVenta(Venta venta);
        Response AgregarVentaDTO(VentaDTO ventaDTO);
        Response AgregarVentaCompletaDTO(VentaCompletaDTO ventaCompletaDTO);
        Response ActualizarVenta(long ventaId, Venta venta);
        Response EliminarVenta(long ventaId);
        IEnumerable<Venta> ObtenerVentas();
        Venta ObtenerVenta(long ventaId);
        Venta ObtenerVenta(string codVenta);
        long UltimaVenta();
    }
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IClienteRepository _clienteRepository;

        public VentaService(IFactory factory)
        {
            _ventaRepository = factory.CreateVentaRepository();
            _clienteRepository = factory.CreateClienteRepository();
        }

        public Response ActualizarVenta(long ventaId, Venta ventaActualizar)
        {
            if (ventaId != ventaActualizar.Id)
                return new Response(HttpStatusCode.BadRequest, "Los Ids no coinciden", ventaActualizar);
            var venta = _ventaRepository.ObtenerVenta(ventaId);
            if (venta == null)
                return new Response(HttpStatusCode.NotFound, "La venta no existe", venta);
            if (ventaActualizar.CodVenta.Trim() == "" || ventaActualizar.Cliente < 1 || ventaActualizar.PuntoVenta.Trim() == "" || ventaActualizar.Total < 0)
                return new Response(HttpStatusCode.BadRequest, "Parametro(s) no valido(s)", ventaActualizar);
            var cliente = _clienteRepository.ObtenerCliente(ventaActualizar.Cliente);
            if (cliente == null)
                return new Response(HttpStatusCode.NotFound, "El cliente no existe", ventaActualizar);
            if (_ventaRepository.CantidadCodVenta(ventaActualizar.CodVenta) > 0 && venta.CodVenta.ToLower().Trim() != ventaActualizar.CodVenta.ToLower().Trim())
                return new Response(HttpStatusCode.BadRequest, "El código de venta ya se ha usado", ventaActualizar);
            try 
            {
                _ventaRepository.ActualizarVenta(ventaId, ventaActualizar);
                return new Response(HttpStatusCode.OK, "Venta actualizada", ventaActualizar);
            }
            catch(Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, ex.Message, ventaActualizar);
            }
        }

        public Response AgregarVenta(Venta venta)
        {
            if (venta.CodVenta.Trim() == "" || venta.Cliente < 1 || venta.PuntoVenta.Trim() == "" || venta.Total < 0 )
                return new Response(HttpStatusCode.BadRequest, "Parametro(s) no valido(s)", venta);
            if (_ventaRepository.ExisteCodVenta(venta.CodVenta))
                return new Response(HttpStatusCode.BadRequest, "El código de venta ya existe", venta);
            var cliente = _clienteRepository.ObtenerCliente(venta.Cliente);
            if (cliente == null)
                return new Response(HttpStatusCode.NotFound, "El cliente no existe", venta);
            try
            {
                _ventaRepository.AgregarVenta(venta);
                return new Response(HttpStatusCode.Created, "Venta creada", venta);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, ex.Message, venta);
            }
        }

        public Response AgregarVentaCompletaDTO(VentaCompletaDTO ventaCompletaDTO)
        {
            VentaDTO ventaDTO = new VentaDTO();
            ventaDTO.Cliente = ventaCompletaDTO.Cliente;
            ventaDTO.PuntoVenta = ventaCompletaDTO.PuntoVenta;
            var response = AgregarVentaDTO(ventaDTO);
            return response;
        }

        public Response AgregarVentaDTO(VentaDTO ventaDTO)
        {
            var lastId = UltimaVenta();
            var codVenta = "FAC-" + lastId + 1;
            Venta venta = new Venta();
            venta.CodVenta = codVenta;
            venta.PuntoVenta = ventaDTO.PuntoVenta;
            venta.Cliente = ventaDTO.Cliente;
            venta.Fecha = DateTime.Now;
            venta.Total = 0;
            var response = AgregarVenta(venta);
            return response;
        }

        public Response EliminarVenta(long ventaId)
        {
            var venta = _ventaRepository.ObtenerVenta(ventaId);
            if (venta == null)
                return new Response(HttpStatusCode.NotFound, "No existe la venta", venta);
            try
            {
                _ventaRepository.EliminarVenta(ventaId);
                return new Response(HttpStatusCode.OK, "Venta eliminada", venta);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, ex.Message, venta);
            }
        }

        public Venta ObtenerVenta(long ventaId)
        {
            return _ventaRepository.ObtenerVenta(ventaId);
        }

        public Venta ObtenerVenta(string codVenta)
        {
            return _ventaRepository.ObtenerVenta(codVenta);
        }

        public IEnumerable<Venta> ObtenerVentas()
        {
            return _ventaRepository.ObtenerVentas();
        }

        public long UltimaVenta()
        {
            return _ventaRepository.UltimoIdVenta();
        }
    }
}