using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.Personaje
{
    public class AltaPersonajeResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
  
        public int Edad { get; set; }

        public float Peso { get; set; }
  
        public string Historia { get; set; }

        public string Imagen { get; set; }

    }
}
