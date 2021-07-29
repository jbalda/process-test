using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL.Entidades;
using WebAPI.Data;
using AutoMapper;
using WebAPI.DTOs.Personaje;
using System.Linq.Expressions;


namespace WebAPI.Controllers.api
{
    [Route("/characters")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private readonly AppContexto _context;
        private readonly IMapper _mapper;

        public PersonajesController(AppContexto context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Personajes
        [HttpGet]

        public async Task<ActionResult<IEnumerable<ItemPersonajeResponseDTO>>> GetPersonaje([FromQuery] BusquedaFiltroPersonajeDTO filtroDTO)
        {
            var res = _context.Personajes.AsQueryable();

            if (filtroDTO.movies.HasValue)
            {
                res = res.Where(p => p.PeliculasYSeries.Any(pel => pel.Id == filtroDTO.movies.Value));

            }
            if (filtroDTO.age.HasValue)
            {
                res = res.Where(p => p.Edad == filtroDTO.age.Value);
            }
            if (filtroDTO.Name != null)
            {
                res = res.Where(p => p.Nombre.Contains(filtroDTO.Name));
            }
   
            var personajes = await res.Select(p => _mapper.Map<ItemPersonajeResponseDTO>(p)).ToListAsync();
            //var personajes = await _context.Personajes.Select(p => _mapper.Map<ItemPersonajeResponseDTO>(p)).ToListAsync();
            return personajes;
        }

        // GET: api/Personajes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePersonajeResponseDTO>> GetPersonaje(int id)
        {
            var personaje = await _context.Personajes.Include(p => p.PeliculasYSeries).FirstOrDefaultAsync(p => p.Id == id);

            if (personaje == null)
            {
                return NotFound();
            }
            var personajeDTO = _mapper.Map<DetallePersonajeResponseDTO>(personaje);
            return personajeDTO;
        }

        // PUT: api/Personajes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaje(int id, ModificacionPersonajeRequestDTO personajeDTO)
        {
            if (id != personajeDTO.Id)
            {
                return BadRequest();
            }
            var personaje = _mapper.Map<Personaje>(personajeDTO);
            _context.Entry(personaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonajeExists(id))
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

        // POST: api/Personajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Personaje>> PostPersonaje(AltaPersonajeRequestDTO personajeDTO)
        {
            var personaje = _mapper.Map<Personaje>(personajeDTO);

            //Si fuera necesario recibir con peliculas
            //foreach (var pel in personaje.PeliculasYSeries)
            //{
            //    if (!_context.PeliculaSerie.Local.Any(p => p.Id == pel.Id)){
            //        _context.PeliculaSerie.Attach(pel).State = EntityState.Unchanged;
            //    }
            //}

            _context.Personajes.Add(personaje);
            await _context.SaveChangesAsync();
            var personajeResponseDTO = _mapper.Map<AltaPersonajeResponseDTO>(personaje);
            return CreatedAtAction("GetPersonaje", new { id = personajeResponseDTO.Id }, personajeResponseDTO);
        }

        // DELETE: api/Personajes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaje(int id)
        {
            var personaje = await _context.Personajes.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }

            _context.Personajes.Remove(personaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonajeExists(int id)
        {
            return _context.Personajes.Any(e => e.Id == id);
        }

    }
}
