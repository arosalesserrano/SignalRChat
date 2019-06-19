using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Models
{
    public class Juez
    {
        public int ID { get; set; }
        public string NombreCompleto { get; set; }
        public string Club { get; set; }
        public int Activo { get; set; }

        public ICollection<Puntuacion> Puntuaciones { get; set; }
    }
}
