using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs.Genero;
using WebAPI.DTOs.Personaje;

namespace WebAPI.DTOs.Pelicula
{
    public record DetallePeliculaSerieResponseDTO
    {
        public int Id { get; init; }

        public string Titulo { get; init; }

        public DateTime FechaCreacion { get; init; }
 
        public int Calificacion { get; init; }  

        public ICollection<ItemPersonajeResponseDTO> Personajes { get; init; }

        public ItemGeneroResponseDTO Genero { get; set; }
    }
}
