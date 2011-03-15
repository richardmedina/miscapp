<%@ Page Title="Cambiar contraseña" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="Stprm.Web.Account.ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="body">
    <h2>
        Cambiar contraseña
    </h2>
    <p>
        Use el formulario siguiente para cambiar la contraseña.
    </p>
    <p>
        Las contraseñas nuevas deben tener una longitud mínima de <%= Membership.MinRequiredPasswordLength %> 
        8 caracteres.
    </p>

    <table style="border: solid 0px gray;" width="400px">
        <tr>
        
            <td>Contraseña Actual</td>
            <td><asp:TextBox runat="server" ID="_txt_actual" TextMode="Password" CssClass="passwordEntry" Width="100%" /></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td>Nueva contraseña</td>
            <td><asp:TextBox runat="server" ID="_txt_nuevo" TextMode="Password" CssClass="passwordEntry" Width="100%" /></td>
        </tr>
        <tr>
            <td>Repetir contraseña</td>
            <td><asp:TextBox runat="server" ID="_txt_repetir" TextMode="Password" CssClass="passwordEntry" /></td>
        </tr>
        <tr>
            <td colspan="2" align="right"><asp:Label runat="server" id="_lbl_mensaje" Text="" ForeColor="Red" /> </td>
        </tr>

        <tr>
            <td colspan="2" align="right">
                <asp:Button runat="server" ID="_btn_cancelar" Text="Cancelar" />
                <asp:Button runat="server" ID="_btn_ok" Text="Cambiar Contraseña" />
            </td>
        </tr>
    </table>
   
</asp:Content>
