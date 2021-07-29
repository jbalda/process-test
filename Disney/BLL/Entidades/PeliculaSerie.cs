using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entidades
{
    public class PeliculaSerie
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Imagen { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [Range(1,5)]
        public int Calificacion { get; set; }
        [Required]
        public ICollection<Personaje> Personajes { get; set; }
        public Genero Genero { get; set; }

        [Required]
        public int GeneroId { get; set; }
    }
}
