using Periodico__Digital.CapaDatos.Entidades;
using Periodico_Digital.CapaDatos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
//UnionRamas
namespace Periodico_Digital.CapaDatos.Entidades
{
    [Table("Autores")]
    public class Autor
    {
        [Key]
        [Column("IdAutor")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo no válido")]
        [StringLength(100)]
        public string Email { get; set; }

        // Relación: Un autor tiene muchas noticias
        public virtual ICollection<Noticia> Noticias { get; set; }
    }
}