<%@ Page Title="Gestión de Categorías" Language="C#" MasterPageFile="~/CapaPresentacion/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="Periodico__Digital.CapaPresentacion.Views.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row mt-4">
        <div class="col-md-4 card shadow-sm p-3">
            <h3>Gestión de Categorías</h3>
            <hr />
            <div class="mb-3">
                <label class="form-label">Nombre de Categoría:</label>
                <asp:TextBox ID="txtNombreCat" runat="server" CssClass="form-control" placeholder="Ej. Deportes" />

                <asp:RequiredFieldValidator ID="rfvCat" runat="server"
                    ControlToValidate="txtNombreCat"
                    ErrorMessage="La descripción es obligatoria"
                    CssClass="text-danger" Display="Dynamic" />
            </div>

            <asp:Button ID="btnGuardarCat" runat="server" Text="Guardar Categoría"
                OnClick="btnGuardarCat_Click" CssClass="btn btn-success w-100" />
        </div>

        <div class="col-md-8">
            <h3>Lista de Categorías</h3>
            <asp:GridView ID="gvCategorias" runat="server" AutoGenerateColumns="false"
                CssClass="table table-striped table-hover" DataKeyNames="Id"
                OnRowDeleting="gvCategorias_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="Descripcion" HeaderText="Nombre de Categoría" />

                    <asp:BoundField DataField="TotalNoticias" HeaderText="Noticias Vinculadas"
                        ItemStyle-HorizontalAlign="Center" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Eliminar"
                                CommandName="Delete" CssClass="btn btn-sm btn-danger"
                                OnClientClick="return confirm('¿Desea eliminar esta categoría?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
