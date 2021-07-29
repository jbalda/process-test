using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEbAPI.DTOs.Usuario
{
    public class UsuarioRegistroDTO
    {
     
        [Required(ErrorMessage ="Debe ingresar el nombre de pila del usuario")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "Debe ingresar el nombre de usuario")]
        public string NombreUsuario { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)\S{8,20}$",
         ErrorMessage = "La contraseña debe tener entre 8 y 20 caracteres letras, mayúsculas y minúsculas, y números")]
        public string Contrasena { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
