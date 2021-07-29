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
using WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using WebAPI.DTOs.Genero;

namespace WebAPI.Controllers.api
{
    [Route("/genres")]
    [ApiController]
    [Authorize]
    public class GenerosController : ControllerBase
    {
        private readonly AppContexto _context;
        private readonly IMapper mapper;

        public GenerosController(AppContexto context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Generos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemGeneroResponseDTO>>> GetGenero()
        {
           return await _context.Generos.Select(d => mapper.Map<ItemGeneroResponseDTO>(d)).ToListAsync();
        }

        // GET: api/GeneroDTOs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemGeneroResponseDTO>> GetGenero(int id)
        {
            var genero = await _context.Generos.FindAsync(id);

            if (genero == null)
            {
                return NotFound();
            }

            return mapper.Map<ItemGeneroResponseDTO>(genero);
        }

        // PUT: api/Generos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenero(int id, ModificacionGeneroRequestDTO generoRequestDTO)
        {
            if (id != generoRequestDTO.Id)
            {
                return BadRequest();
            }
            var genero = mapper.Map<Genero>(generoRequestDTO);
            _context.Entry(genero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(id))
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

        // POST: api/Generos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AltaGeneroResponseDTO>> PostGenero(AltaGeneroRequestDTO generoRequestDTO)
        {
            var genero = mapper.Map<Genero>(generoRequestDTO);
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();
            var generoResponseDTO = mapper.Map<AltaGeneroResponseDTO>(genero);
            return CreatedAtAction("GetGenero", new { id = generoResponseDTO.Id }, generoResponseDTO);
        }

        // DELETE: api/Generos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }

            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeneroExists(int id)
        {
            return _context.Generos.Any(e => e.Id == id);
        }
    }
}
