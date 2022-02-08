using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sync.Infraestructure.Persistance.Models
{
    public class Articulos
    {
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public string SKU { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalImpuestos { get; set; }
        public decimal Total { get; set; }
        public string Nombre { get; set; }
    }
}
