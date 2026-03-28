using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Periodico_Digital.CapaDatos.Entidades;

namespace Periodico_Digital.CapaDatos
{
    public class PeriodicoContext : DbContext
    {
        // Este nombre debe estar en tu Web.config
        public PeriodicoContext() : base("name=PeriodicoConn")
        {
        }

        // Tablas del Periódico
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}