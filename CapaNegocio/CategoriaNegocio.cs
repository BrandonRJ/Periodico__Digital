using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Periodico_Digital.CapaDatos;
using Periodico_Digital.CapaDatos.Entidades;
using System.Data.Entity;

namespace Periodico_Digital.CapaNegocio
{
    public class CategoriaNegocio
    {
        // 1. LISTAR  noticias para el conteo)
        public List<Categoria> ListarCategorias()
        {
            using (var db = new PeriodicoContext())
            {
                // El .Include("Noticias") es lo que hace que funcione el campo 'TotalNoticias'
                return db.Categorias.Include(c => c.Noticias).ToList();
            }
        }

        // 2. INSERTAR 
        public bool Insertar(string descripcion)
        {
            using (var db = new PeriodicoContext())
            {
                db.Categorias.Add(new Categoria { Descripcion = descripcion });
                return db.SaveChanges() > 0;
            }
        }

        // 3. ELIMINAR (Para completar la gestión)
        public bool Eliminar(int id)
        {
            using (var db = new PeriodicoContext())
            {
                var cat = db.Categorias.Find(id);
                if (cat != null)
                {
                    db.Categorias.Remove(cat);
                    return db.SaveChanges() > 0;
                }
                return false;
            }
        }
    }
}