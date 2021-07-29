using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs.Genero;
using WebAPI.DTOs.Personaje;

namespace WebAPI.DTOs.Pelicula
{
    public record ItemPeliculaSerieResponseDTO
    {

        public string Titulo { get; init; }
        public string Imagen { get; init; }
        public DateTime FechaCreacion { get; init; }

      

    }
}
