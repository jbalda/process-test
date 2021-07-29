using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL.Entidades;
using WebAPI.Data;
using WebAPI.DTOs.Pelicula;
using AutoMapper;
using WebAPI.DTOs.PeliculaSerie;

namespace WebAPI.Controllers.api
{
    [Route("/movies")]
    [ApiController]
    public class PeliculaSeriesController : ControllerBase
    {
        private readonly AppContexto _context;
        private readonly IMapper mapper;

        public PeliculaSeriesController(AppContexto context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemPeliculaSerieResponseDTO>>> GetPeliculaSerie([FromQuery] BusquedaFiltroPeliculaDTO filtroDTO)
        {
            var consulta = _context.PeliculaSerie.AsQueryable();
            if (filtroDTO.Genre.HasValue)
            {
                consulta = consulta.Where(p => p.GeneroId == filtroDTO.Genre.Value);
            }
            if (filtroDTO.Name != null)
            {
                consulta = consulta.Where(p => p.Titulo.Contains(filtroDTO.Name));
            }
            if (filtroDTO.Order != null)
            {
                if (filtroDTO.Order.ToUpper().Equals("ASC"))
                {
                    consulta = consulta.OrderBy(p => p.FechaCreacion);
                }
                else if (filtroDTO.Order.ToUpper().Equals("DESC"))
                {
                    consulta = consulta.OrderByDescending(p => p.FechaCreacion);
                }
            }
            var peliculasDTO = await consulta.Select(p => mapper.Map<ItemPeliculaSerieResponseDTO>(p)).ToListAsync();
            return peliculasDTO;
        }

        // GET: api/PeliculaSeries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePeliculaSerieResponseDTO>> GetPeliculaSerie(int id)
        {
            var peliculaSerie = await _context.PeliculaSerie.Include(p => p.Genero).Include(p => p.Personajes).FirstOrDefaultAsync(p => p.Id == id);
            if (peliculaSerie == null)
            {
                return NotFound();
            }
            var perliculaDTO = mapper.Map<DetallePeliculaSerieResponseDTO>(peliculaSerie);
            return perliculaDTO;
        }

        // PUT: api/PeliculaSeries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeliculaSerie(int id, ModificacionPeliculaSerieRequestDTO peliculaSerieDTO)
        {
            if (id != peliculaSerieDTO.Id)
            {
                return BadRequest();
            }
            var peliculaSerie = mapper.Map<PeliculaSerie>(peliculaSerieDTO);
            _context.Entry(peliculaSerie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculaSerieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PeliculaSeries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PeliculaSerie>> PostPeliculaSerie(AltaPeliculaSerieRequestDTO peliculaSerieDTO)
        {
            var peliculaSerie = mapper.Map<PeliculaSerie>(peliculaSerieDTO);
            _context.PeliculaSerie.Add(peliculaSerie);
            await _context.SaveChangesAsync();
            var peliculaResponseDTO = mapper.Map<AltaPeliculaSerieResponseDTO>(peliculaSerie);
            return CreatedAtAction("GetPeliculaSerie", new { id = peliculaResponseDTO.Id }, peliculaResponseDTO);
        }

        // DELETE: api/PeliculaSeries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeliculaSerie(int id)
        {
            var peliculaSerie = await _context.PeliculaSerie.FindAsync(id);
            if (peliculaSerie == null)
            {
                return NotFound();
            }

            _context.PeliculaSerie.Remove(peliculaSerie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Agrega personaje a película
        /// </summary>
        /// <param name="id"> Id de la película</param>
        /// <param name="idPersonaje"> Id del personaje</param>
        /// <returns></returns>
        [HttpPut("{id}/characters/{idPersonaje}")]
        public async Task<IActionResult> AgregarPersonaje(int id, int idPersonaje)
        {
            var pelicula = await _context.PeliculaSerie.FindAsync(id);
            
            if(pelicula == null)
            {
                ModelState.AddModelError("id", "No existe la película con el id");
                return BadRequest();
            }
            var personaje = await _context.Personajes.Include(p=>p.PeliculasYSeries).FirstOrDefaultAsync(p=>p.Id == idPersonaje);
            if(personaje == null)
            {
                ModelState.AddModelError("idPersonaje", "No existe el personaje");
                return BadRequest();
            }
            pelicula.Personajes = pelicula.Personajes ?? new List<Personaje>();
            pelicula.Personajes.Add(personaje);
            await _context.SaveChangesAsync();
            return NoContent();
         }

        private bool PeliculaSerieExists(int id)
        {
            return _context.PeliculaSerie.Any(e => e.Id == id);
        }
    }
}
