using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApiRest.Models;
using Newtonsoft.Json;
using System.Web.Helpers;
using WebApiRest.Services;

namespace WebApiRest.Controllers
{
    public class ClienteController : ApiController
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public IHttpActionResult ObtenerClientes()
        {
            return Ok(_clienteService.ObtenerClientes());
        }

        [HttpGet]
        public IHttpActionResult ObtenerCliente(long id)
        {
            return Ok(_clienteService.ObtenerCliente(id));
        }

        [HttpPost]
        public IHttpActionResult CrearCliente(Cliente cliente)
        {
            if(ModelState.IsValid)
            {
                var response = _clienteService.AgregarCliente(cliente);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return BadRequest(JsonConvert.SerializeObject(response));
                return Ok(response);
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(new Response(HttpStatusCode.BadRequest, "Modelo no valido", ModelState)));
            }
        }

        [HttpPut]
        public IHttpActionResult ActualizarCliente(long id, Cliente cliente) 
        {
            if (ModelState.IsValid)
            {
                var response = _clienteService.ActualizarCliente(id, cliente);
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
        public IHttpActionResult EliminarCliente(long id) 
        { 
            var response = _clienteService.EliminarCliente(id);
            if(response.StatusCode != HttpStatusCode.OK)
                return BadRequest(JsonConvert.SerializeObject(response));
            return Ok(response);
        }
    }
}
