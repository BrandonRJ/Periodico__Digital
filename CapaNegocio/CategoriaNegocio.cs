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
        public bool Insertar(string nombreCategoria)
        {
            using (var db = new PeriodicoContext())
            {
                // Validación de redundancia: ¿Existe ya este nombre?
                if (db.Categorias.Any(c => c.Nombre.ToLower() == nombreCategoria.Trim().ToLower()))
                {
                    throw new Exception("Redundancia detectada: La categoría '" + nombreCategoria + "' ya existe.");
                }

                db.Categorias.Add(new Categoria
                {
                    Nombre = nombreCategoria.Trim(),
                    Descripcion = nombreCategoria.Trim()
                });
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
        public bool Actualizar(int id, string nuevoNombre)
        {
            using (var db = new PeriodicoContext())
            {
                // Validar redundancia: ¿Hay OTRA categoría (diferente ID) con este mismo nombre?
                bool redundante = db.Categorias.Any(c =>
                    c.Nombre.ToLower() == nuevoNombre.Trim().ToLower() && c.Id != id);

                if (redundante)
                {
                    throw new Exception("No se puede actualizar: Ya existe otra categoría llamada '" + nuevoNombre + "'.");
                }

                var cat = db.Categorias.Find(id);
                if (cat != null)
                {
                    cat.Nombre = nuevoNombre.Trim();
                    cat.Descripcion = nuevoNombre.Trim();
                    return db.SaveChanges() > 0;
                }
                return false;
            }
        }
    }
}