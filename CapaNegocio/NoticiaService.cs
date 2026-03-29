using Periodico_Digital.CapaDatos;
using Periodico_Digital.CapaDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
//NoticiaService
namespace Periodico_Digital.CapaNegocio
{
    public class NoticiaService
    {
        // 1. LISTAR TODAS LAS NOTICIAS (Con sus relaciones)
        public List<Noticia> ListarTodo()
        {
            try
            {
                using (var db = new PeriodicoContext())
                {
                    return db.Noticias
                        .Include(n => n.Autor)     // Carga el Autor (Join)
                        .Include(n => n.Categoria) // Carga la Categoría (Join)
                        .OrderByDescending(n => n.FechaPublicacion)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar noticias: " + ex.Message);
            }
        }

        // 2. FILTRAR POR ESTADO "PUBLICADA" (Para la Portada - Punto e)
        public List<Noticia> ListarPublicadas(int? idAutor = null, int? idCat = null)
        {
            using (var db = new PeriodicoContext())
            {
                var query = db.Noticias
                    .Include(n => n.Autor)
                    .Include(n => n.Categoria)
                    .Where(n => n.Estado == "Publicada");

                // Filtros dinámicos (Punto e)
                if (idAutor.HasValue) query = query.Where(n => n.AutorId == idAutor);
                if (idCat.HasValue) query = query.Where(n => n.CategoriaId == idCat);

                return query.ToList();
            }
        }

        // 3. BUSCAR POR ID (Para Transferencia de datos - Punto f)
        public Noticia BuscarPorId(int id)
        {
            using (var db = new PeriodicoContext())
            {
                return db.Noticias
                    .Include(n => n.Autor)
                    .Include(n => n.Categoria)
                    .FirstOrDefault(n => n.Id == id);
            }
        }

        // 4. INSERTAR (Punto d)
        public void Insertar(Noticia noticia)
        {
            using (var db = new PeriodicoContext())
            {
                db.Noticias.Add(noticia);
                db.SaveChanges();
            }
        }

        // 5. ACTUALIZAR
        public void Actualizar(Noticia noticia)
        {
            using (var db = new PeriodicoContext())
            {
                db.Entry(noticia).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // 6. ELIMINAR
        public void Eliminar(int id)
        {
            using (var db = new PeriodicoContext())
            {
                var noticia = db.Noticias.Find(id);
                if (noticia != null)
                {
                    db.Noticias.Remove(noticia);
                    db.SaveChanges();
                }
            }
        }
    }
}