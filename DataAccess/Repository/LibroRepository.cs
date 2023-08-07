using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ILibroRepository
    {
        IEnumerable<Libro> ObtenerLibros();
        IEnumerable<Libro> ObtenerLibrosConStock();
        Libro ObtenerLibro(long libroId);
        void AgregarLibro(Libro libro);
        void EliminarLibro(long libroId);
        void ActualizarLibro(long libroId, Libro libro);
        Libro ObtenerLibro(string libroNombre);
        bool ExisteLibro(string libroNombre);
        int CantidadLibros(string libroNombre);
    }
    public class LibroRepository : ILibroRepository
    {
        private readonly LibreriaDBEntities _context;
        public LibroRepository(LibreriaDBEntities context)
        {
            _context = context;
        }
        public void ActualizarLibro(long libroId, Libro libro)
        {
            _context.ActualizarLibro(libroId, libro.Nombre, libro.Autor, libro.Cantidad, libro.Precio);
            _context.SaveChanges();
        }

        public void AgregarLibro(Libro libro)
        {
            _context.AgregarLibro(libro.Nombre,libro.Autor,libro.Cantidad,libro.Precio);
            _context.SaveChanges();
        }

        public int CantidadLibros(string libroNombre)
        {
            return _context.Libro.Count(l => l.Nombre.ToLower().Trim() == libroNombre.ToLower().Trim());
        }

        public void EliminarLibro(long libroId)
        {
            _context.EliminarLibro(libroId);
            _context.SaveChanges();
        }

        public bool ExisteLibro(string libroNombre)
        {
            var libro = ObtenerLibro(libroNombre);
            if (libro == null)
                return false;
            return true;
        }

        public Libro ObtenerLibro(long libroId)
        {
            var result = _context.ObtenerLibro(libroId);
            var libro = result.Select(r => new Libro
            {
                Id = r.Id,
                Nombre = r.Nombre,
                Cantidad = r.Cantidad,
                Precio = r.Precio,
                Autor = r.Autor,
            }).FirstOrDefault();
            return libro;
            //return _context.Libro.FirstOrDefault(l=>l.Id==libroId);
        }

        public Libro ObtenerLibro(string libroNombre)
        {
            return _context.Libro.FirstOrDefault(l => l.Nombre.ToLower().Trim() == libroNombre.ToLower().Trim());
        }

        public IEnumerable<Libro> ObtenerLibros()
        {
            var result = _context.ObtenerLibros();
            var libros = result.Select(r => new Libro
            {
                Id = r.Id,
                Nombre = r.Nombre,
                Cantidad = r.Cantidad,
                Precio = r.Precio,
                Autor = r.Autor,
            });
            return libros;
            //return _context.Libro.ToList();
        }

        public IEnumerable<Libro> ObtenerLibrosConStock()
        {
            return _context.Libro.Where(l => l.Cantidad > 0);
        }
    }
}
