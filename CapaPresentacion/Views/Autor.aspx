<%@ Page Title="Gestión de Autores" Language="C#" MasterPageFile="~/CapaPresentacion/Site.Master" AutoEventWireup="true" CodeBehind="Autor.aspx.cs" Inherits="Periodico__Digital.CapaPresentacion.Views.Autor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row mt-4">
        <div class="col-md-4 card shadow-sm p-3">
            <h3>Registro de Autor</h3>
            <hr />
            <div class="mb-3">
                <label class="form-label">Nombre:</label>
                <asp:TextBox ID="txtNombreAutor" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombreAutor"
                    ErrorMessage="Nombre requerido" CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="mb-3">
                <label class="form-label">Correo:</label>
                <asp:TextBox ID="txtEmailAutor" runat="server" CssClass="form-control" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmailAutor"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ErrorMessage="Correo inválido" CssClass="text-danger" Display="Dynamic" />
            </div>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Autor" CssClass="btn btn-primary w-100"
                OnClientClick="return validarFormularioAutor();" OnClick="btnGuardar_Click" />
        </div>

        <div class="col-md-8">
            <h3>Lista de Autores</h3>
            <asp:GridView ID="gvAutores" runat="server" CssClass="table table-striped table-hover"
                AutoGenerateColumns="False" OnRowDeleting="gvAutores_RowDeleting" DataKeyNames="IdAutor">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Autor" />
                    <asp:BoundField DataField="Email" HeaderText="Correo" />
                    <asp:BoundField DataField="TotalNoticias" HeaderText="Noticias Registradas" ItemStyle-HorizontalAlign="Center" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Editar" CssClass="btn btn-sm btn-warning"
                                CommandName="Edit" />

                            <asp:LinkButton ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger"
                                OnClientClick="return confirmarEliminacion('Autor');"
                                CommandName="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
