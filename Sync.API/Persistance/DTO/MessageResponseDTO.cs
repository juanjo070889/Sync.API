using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sync.API.Persistance.DTO
{
    public class MessageResponseDTO
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Data { get; set; }
    }
}
