<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeNew.aspx.cs" Inherits="Stprm.Web.EmployeeNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ajax:ToolkitScriptManager runat="server" />
    <div>
        <table>
            <tr>
                <td>Ficha</td>
                <td>
                    <asp:TextBox runat="server" ID="_txt_id" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="_txt_id" Text="*" SetFocusOnError="true" />
                    <asp:RangeValidator ID="RangeValidator1" runat="server" MinimumValue="1" MaximumValue="9999999" Text="*" SetFocusOnError="true" ControlToValidate="_txt_id" />
                </td>
            </tr>

            <tr>
                <td>Nombres</td>
                <td>
                 <asp:TextBox runat="server" ID="_txt_firstname" />
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_txt_firstname" Text="*" SetFocusOnError="true" />
                </td>
            </tr>

            <tr>
                <td>Ape. Paterno</td>
                <td>
                <asp:TextBox runat="server" ID="_txt_middlename" />
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="_txt_middlename" Text="*" SetFocusOnError="true" />
                </td>
            </tr>

            <tr>
                <td>Ape. Materno</td>
                <td>
                 <asp:TextBox runat="server" ID="_txt_lastname" />
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="_txt_lastname" Text="*" SetFocusOnError="true" />
                </td>
            </tr>

            <tr>
                <td>Situacion Contractual</td>
                <td>
                <ajax:ComboBox runat="server" ID="_cmb_arrangement"  DropDownStyle="DropDownList">
                    <asp:ListItem Text="Planta" Value="plant" />
                    <asp:ListItem Text="Transitorio" Value="trans" />
                </ajax:ComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                <asp:Label runat="server" ID="_lbl_msg" Text="" ForeColor="Red" />
                <asp:Button runat="server" ID="_btn_save" Text="Guardar" OnClick="btn_saveClick" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
