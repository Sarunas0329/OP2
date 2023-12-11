<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ketvirtas.aspx.cs" Inherits="Ketvirtas.Ketvirtas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="~/StyleSheet.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True" />
            <asp:Button ID="Start" runat="server" OnClick="Start_Click" Text="Start" Width="140px" />
            <br />
            <asp:Label ID="Label1" runat="server" Enabled="False"></asp:Label>
        </div>
        <p>
            <asp:Table ID="startingData" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" GridLines="Both" Height="550px" Width="600px">
            </asp:Table>
        </p>
            <asp:Table ID="result" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" GridLines="Both" Height="50px" Width="600px">
            </asp:Table>
    </form>
</body>
</html>
