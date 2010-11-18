<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfoTrabControl.ascx.cs" Inherits="Stprm.Web.InfoTrabControl" %>

<asp:Panel runat="server" Width="100%">
<table width="100%">
    <tr>
        <td colspan="2">
        <asp:Label runat="server" ID="_lbl_cab" Text="Información de trabajador" Font-Size="Large" width="100%" />
        </td>
    </tr>
    <tr>
        <td><asp:label runat="server" ID="_lbl_ficha" Text="Ficha" /></td>
        <td>
        <asp:TextBox runat="server" ID="_txt_ficha" ReadOnly="true" />
        <asp:Button runat="server" Text="Seleccionar" ID="_btn_seleccionar" />
        </td>
    </tr>
    <tr>
        <td width="100px"> <asp:Label runat="server" ID="_lbl_nombre" Text="Nombre" /> </td>
        <td><asp:Textbox runat="server" ID="_txt_trab_nom" ReadOnly="true" Width="100%" /> </td>
    </tr>
    <tr>
        <td width="100px"><asp:Label runat="server" ID="_lbl_tra_cat_actual" Text="Categoría actual" /></td>
        <td><asp:Textbox runat="server" ID="_txt_tra_cat_actual" ReadOnly="true" width="100%"/></td>
    </tr>
    <tr>
        <td width="100px"><asp:Label runat="server" ID="_lbl_tra_cat_motivo" Text="Motivo" /></td>
        <td><asp:TextBox runat="server" ID="_txt_tra_cat_motivo" ReadOnly="true" Width="100%" /></td>
    </tr>
    <tr>
        <td width="100px"><asp:Label runat="server" ID="_lbl_tra_cat_base" Text="Puesto base" /></td>
        <td><asp:TextBox runat="server" ID="_txt_tra_cat_base" ReadOnly="true" Width="100%" /></td>
    </tr>
</table>

</asp:Panel>
