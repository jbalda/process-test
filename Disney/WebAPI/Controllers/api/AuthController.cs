using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL.Entidades;
using WebAPI.Data;
using WebAPI.Interfaces;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using WEbAPI.DTOs;
using WEbAPI.DTOs.Usuario;
using WebAPI.EmailProvider;

namespace WebAPI.Controllers.api
{
    [Route("/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppContexto _context;
        private readonly IPasswordService _passwordservice;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public AuthController(AppContexto context, IPasswordService passwordService, IConfiguration configuration,IMapper mapper, IEmailSender emailSender )
        {
            _context = context;
            _passwordservice = passwordService;
            _configuration = configuration;
            _mapper = mapper;
            _emailSender = emailSender;
        }



 

 
        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDTO login)
        {
            //Si es usuario válido 
            var validacion = await IsValidUser(login);
            if (validacion.EsValido)
            {
                var token = GenerarToken(validacion.Usuario);
                return Ok(new { token });
            }
            return NotFound();
        }


        private async Task<(bool EsValido,Usuario Usuario)> IsValidUser(UsuarioLoginDTO login)
        {
            var usuarios = await _context.Usuarios.ToListAsync();


            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == login.NombreUsuario);
             if(usuario == null)
            {
                return (false, null);
            }
            bool esValido = _passwordservice.Check(usuario.Contrasena, login.Contrasena);
            return (esValido, usuario);
        }

        private string GenerarToken(Usuario usu)
        {
            //Header
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usu.NombreCompleto)
            };

            //Payload
            var payload = new JwtPayload
            (
               _configuration["Authentication:Issuer"],
               _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(2)
             );

            //Creación token
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioRegistroDTO usuarioDTO)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == usuarioDTO.NombreUsuario);
            if (usuario != null)
            {
                ModelState.AddModelError("NombreUsuario", "Ya existe el nombre de usuario");
                return BadRequest(ModelState);
            }
             usuario = _mapper.Map<Usuario>(usuarioDTO);
            usuario.Contrasena = _passwordservice.Hash(usuario.Contrasena);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            await _emailSender.SendEmailAsync(usuario.Email, "Usuario creado", "Confirmación usuario creado");
            return Ok(new { NombreUsuario=usuarioDTO.NombreUsuario, NombreCompleto=usuarioDTO.NombreCompleto });
        }


        
    }
}
