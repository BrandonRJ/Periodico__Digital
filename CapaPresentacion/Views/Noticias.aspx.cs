using Periodico_Digital.CapaDatos;
using Periodico_Digital.CapaDatos.Entidades;
using Periodico_Digital.CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Periodico__Digital.CapaPresentacion.Views
{
    public partial class Noticias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                CargarGrid();
            }
        }

        private void CargarGrid()
        {
            try
            {
                var service = new NoticiaService();
                // Trae las noticias con sus relaciones (Autor y Categoría)
                gvNoticias.DataSource = service.ListarTodo();
                gvNoticias.DataBind();
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error al cargar la lista: " + ex.Message);
            }
        }

        private void CargarCombos()
        {
            using (var db = new PeriodicoContext())
            {
                // Cargar Autores
                var autores = db.Autores.ToList();
                ddlAutor.DataSource = autores;
                ddlAutor.DataTextField = "Nombre";
                ddlAutor.DataValueField = "Id";
                ddlAutor.DataBind();
                ddlAutor.Items.Insert(0, new ListItem("-- Seleccione Autor --", ""));

                // Cargar Categorías
                var categorias = db.Categorias.ToList();
                ddlCategoria.DataSource = categorias;
                ddlCategoria.DataTextField = "Descripcion";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("-- Seleccione Categoría --", ""));
            }
        }

        protected void btnGuardarNoticia_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación básica de selección
                if (string.IsNullOrEmpty(ddlAutor.SelectedValue) || string.IsNullOrEmpty(ddlCategoria.SelectedValue))
                {
                    MostrarAlerta("Por favor, seleccione un Autor y una Categoría.");
                    return;
                }

                // 1. Creación del objeto con Fecha Automática 
                var nuevaNoticia = new Noticia
                {
                    Titulo = txtNoticia.Text.Trim(),
                    Contenido = txtContenido.Text.Trim(),
                    AutorId = int.Parse(ddlAutor.SelectedValue),
                    CategoriaId = int.Parse(ddlCategoria.SelectedValue),
                    Estado = rblEstado.SelectedValue,
                    FechaPublicacion = DateTime.Now
                };

                // 2. Inserción mediante la Capa de Negocio
                var service = new NoticiaService();
                service.Insertar(nuevaNoticia);

                // 3. CAMBIO: Limpiar el formulario y refrescar
                LimpiarFormulario();
                CargarGrid();

                // 4. CAMBIO: Mostrar alerta de éxito
                MostrarAlerta("Noticia guardada exitosamente.");
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error al guardar la noticia: " + ex.Message);
            }
        }

        // Método para limpiar los campos del formulario
        private void LimpiarFormulario()
        {
            txtNoticia.Text = "";
            txtContenido.Text = "";
            ddlAutor.SelectedIndex = 0;
            ddlCategoria.SelectedIndex = 0;
            rblEstado.SelectedIndex = 0; // Selecciona el primer radio button por defecto
        }

        // Método para mostrar mensajes de alerta al usuario
        private void MostrarAlerta(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
        }
    }
}