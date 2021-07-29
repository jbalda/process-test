using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.Personaje
{
    public record BusquedaFiltroPersonajeDTO
    {
        public string Name { get; init; }
        public int? age { get; init; }
        public int? movies { get; init; }
    }
}
