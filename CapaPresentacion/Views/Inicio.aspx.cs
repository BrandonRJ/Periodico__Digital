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
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CargarCombos();
                cargarGrid();
            }
            
        }

        // Cargar tabla
        private void cargarGrid()
        {
            try
            {
                var service = new NoticiaService();

                gvNoticias.DataSource = service.ListarPublicadas();
                gvNoticias.DataBind();
            }
            catch (Exception ex)
            {

                MostrarAlerta("Error al cargar la lista: " + ex.Message);
            }
        }

        //Datos para las ddl
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
                ddlAutor.Items.Insert(0, new ListItem("-- Filtrar por Autores --", ""));

                // Cargar Categorías
                var categorias = db.Categorias.ToList();
                ddlCategoria.DataSource = categorias;
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("-- Filtrar por categoria --", ""));
            }
        }

        private void CargarNoticias(int? autorId, int? categoriaId) 
        {
            var service = new NoticiaService();
            gvNoticias.DataSource = service.ListarPublicadas(autorId, categoriaId);
            gvNoticias.DataBind();
        }


        //Filtros para los ddl
        private void AplicarFlitros() 
        {
            int? autorId = string.IsNullOrEmpty(ddlAutor.SelectedValue) ? null : (int?)int.Parse(ddlAutor.SelectedValue);
            int? categoriaId = string.IsNullOrEmpty(ddlCategoria.SelectedValue) ? null : (int?)int.Parse(ddlCategoria.SelectedValue);
            CargarNoticias(autorId, categoriaId);
        }

        protected void ddlAutor_SelectedIndexChanged(object sender, EventArgs e) 
        {
            AplicarFlitros();
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFlitros();
        }

        private void MostrarAlerta(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
        }
    }
}
