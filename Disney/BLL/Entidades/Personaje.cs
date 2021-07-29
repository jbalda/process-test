using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BLL.Entidades
{ 
    public class Personaje
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public float Peso { get; set; }
        [Required]
        public string Historia { get; set; }
        [Required]
        public string Imagen { get; set; }
        public ICollection<PeliculaSerie> PeliculasYSeries { get; set; }
    }
}