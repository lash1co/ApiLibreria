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
    public interface ILibroService
    {
        Response AgregarLibro(Libro libro);
        Response ActualizarLibro(long libroId, Libro libro);
        Response EliminarLibro(long libroId);
        IEnumerable<Libro> ObtenerLibros();
        IEnumerable<Libro> ObtenerLibrosConStock();
        Libro ObtenerLibro(long libroId);
        Libro ObtenerLibro(string libroNombre);
    }
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;
        public LibroService(IFactory factory)
        {
            _libroRepository = factory.CreateLibroRepository();
        }
        public Response ActualizarLibro(long libroId, Libro libroActualizar)
        {
            if (libroId != libroActualizar.Id)
                return new Response(HttpStatusCode.BadRequest, "Los Ids no coinciden", libroActualizar);
            var libro = _libroRepository.ObtenerLibro(libroId);
            if (libro == null)
                return new Response(HttpStatusCode.NotFound, "No existe el libro", libro);
            if (libroActualizar.Precio == 0.0m || libroActualizar.Autor.Trim() == "" || libroActualizar.Nombre.Trim() == "")
                return new Response(HttpStatusCode.BadRequest, "Parametro(s) no valido(s)", libroActualizar);
            if (_libroRepository.CantidadLibros(libroActualizar.Nombre) > 0 && libro.Nombre.ToLower().Trim() != libroActualizar.Nombre.ToLower().Trim())
                return new Response(HttpStatusCode.BadRequest, "El nombre de libro ya se ha usado", libroActualizar);
            try
            {
                _libroRepository.ActualizarLibro(libroId, libroActualizar);
                return new Response(HttpStatusCode.OK, "Libro actualizado", libroActualizar);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.NotModified, ex.Message, libroActualizar);
            }
        }

        public Response AgregarLibro(Libro libro)
        {
            if (libro.Precio == 0.0m || libro.Autor.Trim() == "" || libro.Nombre.Trim() == "")
                return new Response(HttpStatusCode.BadRequest, "Parametro(s) no valido(s)", libro);
            if (_libroRepository.ExisteLibro(libro.Nombre))
                return new Response(HttpStatusCode.BadRequest, "El nombre de libro ya existe", libro);
            try
            {
                _libroRepository.AgregarLibro(libro);
                return new Response(HttpStatusCode.Created, "Libro creado", libro);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, ex.Message, libro);
            }
        }

        public Response EliminarLibro(long libroId)
        {
            var libro = _libroRepository.ObtenerLibro(libroId);
            if (libro == null)
                return new Response(HttpStatusCode.NotFound, "No existe el libro", libro);
            try
            {
                _libroRepository.EliminarLibro(libroId);
                return new Response(HttpStatusCode.OK, "Libro eliminado", libro);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, ex.Message, libro);
            }
        }

        public Libro ObtenerLibro(long libroId)
        {
            return _libroRepository.ObtenerLibro(libroId);
        }

        public Libro ObtenerLibro(string libroNombre)
        {
            return _libroRepository.ObtenerLibro(libroNombre);
        }

        public IEnumerable<Libro> ObtenerLibros()
        {
            return _libroRepository.ObtenerLibros();
        }

        public IEnumerable<Libro> ObtenerLibrosConStock()
        {
            return _libroRepository.ObtenerLibrosConStock();
        }
    }
}