using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiRest.Models
{
    public class VentaCompletaDTO
    {
        public string PuntoVenta { get; set; }
        public long Cliente { get; set; }
        public IEnumerable<DetalleDTO> Detalles { get; set; }
        public VentaCompletaDTO()
        {
            Detalles = new List<DetalleDTO>();
        }
    }
}