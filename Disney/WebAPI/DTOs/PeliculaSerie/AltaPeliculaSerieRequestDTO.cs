using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DTOs.Pelicula
{
    public class AltaPeliculaSerieRequestDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Imagen { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [Range(1,5)]
        public int Calificacion { get; set; }
        [Required]
        public int GeneroId { get; set; }
    }
}
