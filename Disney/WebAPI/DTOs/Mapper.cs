using AutoMapper;
using BLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs.Genero;
using WebAPI.DTOs.Pelicula;
using WebAPI.DTOs.Personaje;
using WEbAPI.DTOs.Usuario;

namespace WebAPI.DTOs
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            //Genero
            CreateMap<ItemGeneroResponseDTO, BLL.Entidades.Genero>();
            CreateMap<BLL.Entidades.Genero, ItemGeneroResponseDTO>();
            CreateMap<AltaGeneroRequestDTO, BLL.Entidades.Genero>();
            CreateMap<BLL.Entidades.Genero, AltaGeneroResponseDTO>();
            CreateMap<ModificacionGeneroRequestDTO, BLL.Entidades.Genero>();
            //Pelicula
            
            CreateMap<AltaPeliculaSerieRequestDTO, BLL.Entidades.PeliculaSerie >();
            CreateMap<BLL.Entidades.PeliculaSerie, AltaPeliculaSerieResponseDTO>();
            CreateMap<AltaPeliculaSerieResponseDTO, BLL.Entidades.PeliculaSerie>();
            CreateMap<BLL.Entidades.PeliculaSerie, DetallePeliculaSerieResponseDTO>();
            CreateMap<BLL.Entidades.PeliculaSerie, ItemPeliculaSerieResponseDTO>();
            CreateMap<ModificacionPeliculaSerieRequestDTO, BLL.Entidades.PeliculaSerie>();

            //Personaje
            CreateMap<AltaPersonajeRequestDTO, BLL.Entidades.Personaje>();
            CreateMap<BLL.Entidades.Personaje, AltaPersonajeResponseDTO >();
            CreateMap<ModificacionPersonajeRequestDTO, BLL.Entidades.Personaje>();
            CreateMap<BLL.Entidades.Personaje, DetallePersonajeResponseDTO>();
            CreateMap<BLL.Entidades.Personaje,ItemPersonajeResponseDTO >();
            
            //Usuario
            CreateMap<BLL.Entidades.Usuario, UsuarioRegistroDTO>();
            CreateMap<UsuarioRegistroDTO, BLL.Entidades.Usuario>();
        }
    }
}
