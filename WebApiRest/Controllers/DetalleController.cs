using DataAccess.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiRest.Models;
using WebApiRest.Services;

namespace WebApiRest.Controllers
{
    public class DetalleController : ApiController
    {
        private readonly IDetalleVentaService _detalleService;

        public DetalleController(IDetalleVentaService detalleVentaService) 
        {
            _detalleService = detalleVentaService;
        }

        [HttpGet]
        public IHttpActionResult ObtenerDetallesXId(long id)
        {
            return Ok(_detalleService.ObtenerDetalleVenta(id));
        }

        [HttpGet]
        [Route("api/Detalle/Venta/{ventaId}")]
        public IHttpActionResult ObtenerDetallesXVenta(long ventaId)
        {
            return Ok(_detalleService.ObtenerDetallesVentaPorVenta(ventaId));
        }

        [HttpPost]
        public IHttpActionResult CrearDetalle(DetalleVenta detalleVenta)
        {
            if(ModelState.IsValid)
            {
                var response = _detalleService.CrearDetalleVenta(detalleVenta);
                if (response.StatusCode != HttpStatusCode.OK)
                    return BadRequest(JsonConvert.SerializeObject(response));
                return Ok(response);
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(new Response(HttpStatusCode.BadRequest, "Modelo no valido", ModelState)));
            }
        }

        [HttpDelete]
        public IHttpActionResult EliminarDetalle(long id)
        {
            var response = _detalleService.EliminarDetalleVenta(id);
            if (response.StatusCode != HttpStatusCode.OK)
                return BadRequest(JsonConvert.SerializeObject(response));
            return Ok(response);
        }

        [HttpPut]
        public IHttpActionResult ActualizarDetalle(long id, DetalleVenta detalleActualizar) 
        {
            if (ModelState.IsValid)
            {
                var response = _detalleService.ActualizarDetalleVenta(id, detalleActualizar);
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
