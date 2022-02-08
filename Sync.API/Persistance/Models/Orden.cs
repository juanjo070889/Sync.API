using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sync.API.Persistance.Models
{
    public class Orden
    {
        public int OrdenId { get; set; }
        public long NumeroOrden { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalImpuestos { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaOrden { get; set; }
    }
}
