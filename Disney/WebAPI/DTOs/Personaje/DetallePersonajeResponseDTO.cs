using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs.Pelicula;

namespace WebAPI.DTOs.Personaje
{
    public record DetallePersonajeResponseDTO
    {
        public int Id { get; init; }

        public string Nombre { get; init; }

        public int Edad { get; init; }

        public float Peso { get; init; }

        public string Historia { get; init; }

        public string Imagen { get; init; }
        public ICollection<ItemPeliculaSerieResponseDTO> PeliculasYSeries { get; init; }
    }
}
