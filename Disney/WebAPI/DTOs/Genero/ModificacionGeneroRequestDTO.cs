using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.Genero
{
    public record ModificacionGeneroRequestDTO

    {
        [Required]
        public int Id { get; init; }
        [Required]
        public string Nombre { get; init; }
        [Required]
        public string Imagen { get; init; }
    }
}
