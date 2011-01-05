<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrarFichaControl.ascx.cs" Inherits="Stprm.Web.RegistrarFichaControl" %>

<table>
    <tr>
        <td width="20%"><strong>Ficha</strong></td>
        <td width="80%"><asp:TextBox runat="server" ID="_txt_ficha" Width="100%" /></td>
    </tr>
    <tr>
        <td width="20%">Nombre</td>
        <td width="80%"><asp:TextBox runat="server" ID="_txt_nombre" Width="100%" /></td>
    </tr>
    <tr>
        <td width="20%">Regimen</td>
        <td width="80%"><asp:TextBox runat="server" ID="_txt_regimen" Width="100%" /></td>
    </tr>
    <tr>
        <td width="20%">Depto </td>
        <td width="80%"><asp:TextBox runat="server" ID="_txt_depto"  Width="100%"/></td>
    </tr>
    <tr>
        <td colspan="2" align="right"><asp:Button runat="server" ID="_btn_ok" 
                Text="Guardar" onclick="_btn_ok_Click" /></td>
    </tr>
</table>
