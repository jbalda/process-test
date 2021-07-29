using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DTOs.Pelicula
{
    public class AltaPeliculaSerieResponseDTO
    {

        public int Id { get; set; }

        public string Titulo { get; set; }


        public string Imagen { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int Calificacion { get; set; }

        public int GeneroId { get; set; }
    }
}
