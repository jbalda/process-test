using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entidades
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NombreCompleto { get; set; }
        [Required]

        public string NombreUsuario { get; set; }
        [Required]
        public string Contrasena { get; set; }

        public string Email { get; set; }

    }
}
