using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sync.Infraestructure.Persistance.Models
{
    public class Orden
    {
        public int OrdenId { get; set; }
        public int NumeroOrden { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalImpuestos { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaOrden { get; set; }
    }
}
