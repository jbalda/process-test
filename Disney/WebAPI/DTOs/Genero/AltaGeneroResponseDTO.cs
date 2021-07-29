using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.Genero
{
    public record AltaGeneroResponseDTO
    {
        public int Id { get; init; }
        public string Nombre { get; init; }

        public string Imagen { get; init; }
    }
}
