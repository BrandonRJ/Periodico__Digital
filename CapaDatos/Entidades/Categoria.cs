using Periodico__Digital.CapaDatos.Entidades;
using Periodico_Digital.CapaDatos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Periodico_Digital.CapaDatos.Entidades
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        [Column("IdCategoria")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de categoría es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }

        // Relación: Una categoría tiene muchas noticias
        public virtual ICollection<Noticia> Noticias { get; set; }
    }
}