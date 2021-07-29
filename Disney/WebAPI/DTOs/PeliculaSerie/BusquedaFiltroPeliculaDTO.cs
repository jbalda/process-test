using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.PeliculaSerie
{
    public record BusquedaFiltroPeliculaDTO
    {
        public string Name { get; init; }
        public int? Genre { get; init; }
        public string Order { get; init; }
    }
}
