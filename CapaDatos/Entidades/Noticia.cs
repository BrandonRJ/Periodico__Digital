using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Periodico_Digital.CapaDatos.Entidades
{
    [Table("Noticias")]
    public class Noticia
    {
        [Key]
        [Column("IdNoticia")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El contenido no puede estar vacío")]
        public string Contenido { get; set; }

        [Column("FechaPublicacion")]
        public DateTime FechaPublicacion { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } // Publicada, Borrador, Archivada

        // Relación con Autor
        [Column("IdAutor")]
        public int AutorId { get; set; }

        [ForeignKey("AutorId")]
        public virtual Autor Autor { get; set; }

        // Relación con Categoria
        [Column("IdCategoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }
    }
}