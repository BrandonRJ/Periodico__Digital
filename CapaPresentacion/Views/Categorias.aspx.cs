using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Periodico_Digital.CapaNegocio; 
using Periodico_Digital.CapaDatos.Entidades;

namespace Periodico__Digital.CapaPresentacion.Views
{
    public partial class Categorias : System.Web.UI.Page
    {
        // Instancia de la capa de negocio
        private CategoriaNegocio catNegocio = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarLista();
            }
        }

        // Método para llenar el GridView
        private void CargarLista()
        {
            try
            {
                gvCategorias.DataSource = catNegocio.ListarCategorias();
                gvCategorias.DataBind();
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error al cargar la lista: " + ex.Message);
            }
        }

        // Evento del botón Guardar (Cumple Punto 3.c)
        protected void btnGuardarCat_Click(object sender, EventArgs e)
        {
            // Solo ejecutamos si los validadores (RequiredField) pasaron
            if (Page.IsValid)
            {
                try
                {
                    // 1. Capturamos ambos valores de los TextBox corregidos
                    string nombre = txtNombreCat.Text.Trim();
                    string descripcion = TextDescripcionmat.Text.Trim();

                    // 2. Llamamos al método enviando AMBOS datos
                    // El método 'Insertar' ahora debe recibir (string, string)
                    if (catNegocio.Insertar(nombre, descripcion))
                    {
                        // Solo si se guardó: limpiamos y refrescamos
                        txtNombreCat.Text = "";
                        TextDescripcionmat.Text = "";
                        CargarLista();
                        MostrarAlerta("¡Guardado correctamente!");
                    }
                }
                catch (Exception ex)
                {
                    // Si la pareja ya existe, el 'throw' de la capa de negocio nos trae aquí
                    CargarLista();
                    MostrarAlerta("AVISO: " + ex.Message);
                }
            }
        }
        // Evento para Eliminar (Cumple con 'Gestionar')
        protected void gvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int idCat = Convert.ToInt32(gvCategorias.DataKeys[e.RowIndex].Value);

                if (catNegocio.Eliminar(idCat))
                {
                    CargarLista();
                    MostrarAlerta(" ¡Listo! La categoría se eliminó de la base de datos.");
                }
            }
            catch (Exception ex)
            {
                CargarLista(); // Refrescamos para que la fila no desaparezca visualmente

                // Si el error es por noticias vinculadas (FK)
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE"))
                {
                    // Solo el mensaje directo
                    MostrarAlerta("No se puede eliminar: Esta categoría tiene noticias asociadas.");
                }
                else
                {
                    // Mensaje corto para cualquier otro error
                    MostrarAlerta("Error: No se pudo eliminar el registro.Esta categoría tiene noticias asociadas");
                }
            }
        }
        // Métodos de apoyo
        private void MostrarAlerta(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
        }
    }
}