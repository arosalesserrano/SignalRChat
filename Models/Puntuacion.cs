using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Models
{
    public class Puntuacion
    {
        public int ID { get; set; }
        public DateTime timestamp { get; set; }
        public decimal Puntuaciones { get; set; }
        public string Dorsal { get; set; }
        public string Rol {get; set; }
    }
}
