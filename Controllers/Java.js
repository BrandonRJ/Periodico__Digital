
// Periódico Virtual InfoWeb - JS Principal
// Autores: Brandon & Ramses


document.addEventListener("DOMContentLoaded", function () {
    console.log("InfoWeb cargado correctamente.");
    inicializarEventos();
});

// 1. Inicialización de eventos dinámicos
function inicializarEventos() {
    // Resaltar noticias al pasar el mouse 
    const tarjetasNoticias = document.querySelectorAll('.card-noticia');
    tarjetasNoticias.forEach(card => {
        card.addEventListener('mouseenter', () => {
            card.style.boxShadow = "0 8px 16px rgba(0,0,0,0.2)";
            card.style.transform = "translateY(-5px)";
            card.style.transition = "all 0.3s ease";
        });
        card.addEventListener('mouseleave', () => {
            card.style.boxShadow = "none";
            card.style.transform = "translateY(0)";
        });
    });
// 2. Validación del Módulo de Autores 
// Verifica el formato del correo antes de enviarlo al servidor
function validarFormularioAutor() {
    const txtNombre = document.querySelector('[id*="txtNombreAutor"]');
    const txtEmail = document.querySelector('[id*="txtEmailAutor"]');
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (txtNombre.value.trim() === "") {
        alert("El nombre del autor es obligatorio.");
        txtNombre.focus();
        return false;
    }

    if (!emailRegex.test(txtEmail.value)) {
        alert("Por favor, ingrese un correo electrónico válido.");
        txtEmail.focus();
        return false;
    }

    return true; // Si todo está bien, permite el PostBack
}

// 3. Confirmación de Eliminación 
// Se usa en los botones de "Eliminar" de los GridViews
function confirmarEliminacion(tipoEntidad) {
    return confirm("¿Está seguro de que desea eliminar este " + tipoEntidad + "? Esta acción no se puede deshacer.");
}

// 4. Validación de Categorías 
// Asegura que nombre y descripción no estén vacíos
function validarCategoria() {
    const nombre = document.querySelector('[id*="txtNombreCat"]').value;
    const desc = document.querySelector('[id*="txtDescripcionCat"]').value;

    if (nombre.trim() === "" || desc.trim() === "") {
        alert("Los campos de nombre y descripción son obligatorios para la categoría.");
        return false;
    }
    return true;
}

// 5. Utilidad: Limpiar formularios
function limpiarCampos() {
    const inputs = document.querySelectorAll('input[type="text"], textarea');
    inputs.forEach(input => input.value = "");
}