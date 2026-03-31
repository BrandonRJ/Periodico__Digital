using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Periodico_Digital.CapaNegocio;
using Periodico_Digital.CapaDatos.Entidades;

namespace Periodico__Digital.CapaPresentacion.Views
{
    public partial class Autor : System.Web.UI.Page
    {
        private AutorNegocio autorNegocio = new AutorNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarLista();
            }
        }
        protected void gvAutores_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  Obtenemos la fila que se seleccionó
            GridViewRow fila = gvAutores.SelectedRow;

            //  Pasamos los textos a los TextBox de arriba
            // Nota: HttpUtility.HtmlDecode limpia caracteres raros como &nbsp;
            txtNombreAutor.Text = Server.HtmlDecode(fila.Cells[0].Text);
            txtEmailAutor.Text = Server.HtmlDecode(fila.Cells[1].Text);

            //  Guardamos el ID en el ViewState para saber que estamos EDITANDO y no CREANDO
            ViewState["IdAutorEdicion"] = gvAutores.SelectedDataKey.Value;

            //  Cambiamos el texto del botón para que el usuario sepa qué está haciendo
            btnGuardar.Text = "Actualizar Autor";
        }

        private void CargarLista()
        {
            try
            {
               
                gvAutores.DataSource = autorNegocio.ObtenerReporteAutores();
                gvAutores.DataBind();
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error al cargar: " + ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //  Primero verificamos que los validadores (Email obligatorio) estén OK
            if (Page.IsValid)
            {
                try
                {
                    //  Capturamos los datos de la pantalla
                    string nombre = txtNombreAutor.Text.Trim();
                    string email = txtEmailAutor.Text.Trim();

                    //  Verificamos si estamos EDITANDO o REGISTRANDO NUEVO
                    if (ViewState["IdAutorEdicion"] != null)
                    {
                        // --- MODO EDICIÓN ---
                        int id = Convert.ToInt32(ViewState["IdAutorEdicion"]);

                        if (autorNegocio.ActualizarAutor(id, nombre, email))
                        {
                            MostrarAlerta("Autor actualizado con éxito.");
                            FinalizarEdicion(); // Este método debería limpiar campos y el ViewState
                        }
                    }
                    else
                    {
                        // --- MODO REGISTRO NUEVO ---
                        if (autorNegocio.RegistrarAutor(nombre, email))
                        {
                            MostrarAlerta("Autor registrado con éxito.");
                            FinalizarEdicion(); // Limpia el formulario para el siguiente registro
                        }
                    }
                }
                catch (Exception ex)
                {
                    //  Si el correo está duplicado o falta un dato, el error saltará aquí
                    MostrarAlerta("Error: " + ex.Message);

                  
                }
            }
        }

        // Método auxiliar para limpiar todo después de guardar/actualizar
        private void FinalizarEdicion()
        {
            txtNombreAutor.Text = "";
            txtEmailAutor.Text = "";
            btnGuardar.Text = "Guardar Autor";
            ViewState["IdAutorEdicion"] = null; // Limpiamos el ID para que el próximo sea nuevo
            gvAutores.SelectedIndex = -1;       // Quitamos la selección de la tabla
            CargarLista();                      // ¡ESTO ES LO QUE REFRESCARÁ LA LISTA!
        }
        protected void gvAutores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvAutores.DataKeys[e.RowIndex].Value);

                if (autorNegocio.EliminarAutor(id))
                {
                   
                    {
                        gvAutores.SelectedIndex = -1; // Limpia cualquier selección previa
                        CargarLista();
                        MostrarAlerta("Autor eliminado correctamente.");
                    }
                }
                else
                {
                    MostrarAlerta("No se encontró el autor para eliminar.");
                }
            }
            catch (Exception ex)
            {
                //  Solo culpar a las noticias si el error viene de la base de datos
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE"))
                {
                    MostrarAlerta("No se puede eliminar: el autor tiene noticias asociadas.");
                }
                else
                {
                    // Esto te dirá si el error es de conexión, de nulos, etc.
                    MostrarAlerta("Error técnico: " + ex.Message);
                }
            }
        }
        private void MostrarAlerta(string mensaje)
        {
            string script = $"alert('{mensaje.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
        }
    }
}