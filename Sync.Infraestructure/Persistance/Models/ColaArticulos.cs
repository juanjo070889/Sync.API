using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sync.Infraestructure.Persistance.Models
{
    public class ColaArticulos
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool Sincronizado { get; set; }
    }
}
