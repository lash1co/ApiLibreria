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
    public class LibroController : ApiController
    {
        private readonly ILibroService _libroService;
        public LibroController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet]
        public IHttpActionResult ObtenerLibros()
        {
            return Ok(_libroService.ObtenerLibros());
        }

        [HttpGet]
        public IHttpActionResult ObtenerLibro(long id)
        {
            return Ok(_libroService.ObtenerLibro(id));
        }

        [HttpPost]
        public IHttpActionResult CrearLibro(Libro libro)
        {
            if(ModelState.IsValid)
            {
                var resultado = _libroService.AgregarLibro(libro);
                if (resultado.StatusCode == HttpStatusCode.BadRequest)
                    return BadRequest(JsonConvert.SerializeObject(resultado));
                return Ok(resultado);
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(new Response(HttpStatusCode.BadRequest, "Modelo no valido", ModelState)));
            }
        }

        [HttpPut]
        public IHttpActionResult ActualizarLibro(long id, Libro libro)
        {
            if (ModelState.IsValid)
            {
                var resultado = _libroService.ActualizarLibro(id, libro);
                if (resultado.StatusCode != HttpStatusCode.OK)
                    return BadRequest(JsonConvert.SerializeObject(resultado));
                return Ok(resultado);
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(new Response(HttpStatusCode.BadRequest, "Modelo no valido", ModelState)));
            }
        }

        [HttpDelete]
        public IHttpActionResult EliminarLibro(long id)
        {
            var resultado = _libroService.EliminarLibro(id);
            if (resultado.StatusCode != HttpStatusCode.OK)
                return BadRequest(JsonConvert.SerializeObject(resultado));
            return Ok(resultado);
        }
    }
}
