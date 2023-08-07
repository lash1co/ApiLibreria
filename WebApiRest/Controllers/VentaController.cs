using DataAccess.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using WebApiRest.Models;
using WebApiRest.Services;

namespace WebApiRest.Controllers
{
    public class VentaController : ApiController
    {
        private readonly IVentaService _ventaService;
        private readonly IDetalleVentaService _detalleService;
        public VentaController(IVentaService ventaService, IDetalleVentaService detalleService)
        {
            _ventaService = ventaService;
            _detalleService = detalleService;
        }

        [HttpGet]
        public IHttpActionResult ObtenerVentas()
        {
            return Ok(_ventaService.ObtenerVentas());
        }

        [HttpGet]
        public IHttpActionResult ObtenerVenta(long id)
        {
            return Ok(_ventaService.ObtenerVenta(id));
        }

        [HttpGet]
        public IHttpActionResult ObtenerVenta(string codigoVenta)
        {
            return Ok(_ventaService.ObtenerVenta(codigoVenta));
        }

        [HttpPost]
        public IHttpActionResult CrearVenta(VentaDTO ventaDTO)
        {
            if (ModelState.IsValid || ventaDTO.PuntoVenta.Trim() == "" || ventaDTO.Cliente < 1)
            {
                var response = _ventaService.AgregarVentaDTO(ventaDTO);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return BadRequest(JsonConvert.SerializeObject(response));
                return Ok(response);
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(new Response(HttpStatusCode.BadRequest, "Modelo no valido", ModelState)));
            }
        }

        [HttpPost]
        [Route("api/Venta/Detalles")]
        public IHttpActionResult CrearVentaDetalles(VentaCompletaDTO ventaCompletaDTO)
        {
            if (ModelState.IsValid || ventaCompletaDTO.PuntoVenta.Trim() == "" || ventaCompletaDTO.Cliente < 1)
            {
                var response = _ventaService.AgregarVentaCompletaDTO(ventaCompletaDTO);
                if(response.StatusCode != HttpStatusCode.BadRequest)
                {
                    Venta venta = (Venta)response.Data;
                    foreach (var detalle in ventaCompletaDTO.Detalles)
                    {
                        _detalleService.AgregarDetalleVenta(venta.Id, detalle);
                    }
                    return Ok(response);
                }
                return BadRequest(JsonConvert.SerializeObject(response));     
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(new Response(HttpStatusCode.BadRequest, "Modelo no valido", ModelState)));
            }
        }

        [HttpDelete]
        public IHttpActionResult EliminarVenta(long id) 
        {
            var resultado = _ventaService.EliminarVenta(id);
            if (resultado.StatusCode != HttpStatusCode.OK)
                return BadRequest(JsonConvert.SerializeObject(resultado));
            return Ok(resultado);
        }

        [HttpPut]
        public IHttpActionResult ActualizarVenta(long id, Venta ventaActualizada)
        {
            if (ModelState.IsValid)
            {
                var response = _ventaService.ActualizarVenta(id, ventaActualizada);
                if (response.StatusCode != HttpStatusCode.OK)
                    return BadRequest(JsonConvert.SerializeObject(response));
                return Ok(response);
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(new Response(HttpStatusCode.BadRequest, "Modelo no valido", ModelState)));
            }
        }
    }
}
