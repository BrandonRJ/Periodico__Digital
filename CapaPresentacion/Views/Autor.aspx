<%@ Page Title="Gestión de Autores" Language="C#" MasterPageFile="~/CapaPresentacion/Site.Master" AutoEventWireup="true" CodeBehind="Autor.aspx.cs" Inherits="Periodico__Digital.CapaPresentacion.Views.Autor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contenido-wrapper">
        <div class="contenedor-seccion">
            <h3>Registro de Autor</h3>
            <hr />
            <div class="form-group">
                <label>Nombre:</label>
                <asp:TextBox ID="txtNombreAutor" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombreAutor"
                    ErrorMessage="Nombre requerido" CssClass="error" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label>Correo:</label>
                <asp:TextBox ID="txtEmailAutor" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmailAutor" ErrorMessage="¡Atención! El correo no puede quedar vacío." 
                 CssClass="error" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmailAutor" ErrorMessage="El formato del correo es inválido (ejemplo: usuario@dominio.com)."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                     CssClass="error" Display="Dynamic" />
            </div>
            <div class="form-botones">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Autor" CssClass="btn btn-primary"
                    OnClick="btnGuardar_Click" />
            </div>
        </div>

        <div class="contenedor-seccion">
            <h3>Lista de Autores</h3>
            <asp:GridView ID="gvAutores" runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="Id"
                OnRowDeleting="gvAutores_RowDeleting"
                OnSelectedIndexChanged="gvAutores_SelectedIndexChanged"
                CssClass="tabla-inventario"
                GridLines="None">

                <HeaderStyle CssClass="tabla-header" />
                <AlternatingRowStyle CssClass="tabla-row-alt" />

                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Autor" />
                    <asp:BoundField DataField="Email" HeaderText="Correo" />

                    <asp:TemplateField HeaderText="Noticias" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <span class="badge-conteo"><%# Eval("Noticias.Count") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Editar" CssClass="btn-accion edit" CommandName="Select" CausesValidation="false"/>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn-accion delete"
                                OnClientClick="return confirm('¿Desea eliminar este autor?');" CommandName="Delete" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
