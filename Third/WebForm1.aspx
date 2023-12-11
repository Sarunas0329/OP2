<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Antras.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="~/StyleSheet.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
            <asp:FileUpload ID="FileUpload2" runat="server" />
        <asp:Button ID="Start" runat="server" Height="26px" OnClick="Start_Click" Text="Start" Width="160px"/>
        <asp:Table ID="startingData" runat="server" BorderStyle="Solid" BorderWidth="2px" Height="293px" Width="637px" BorderColor="Red">
        </asp:Table>
        &nbsp;<asp:Label ID="Label1" runat="server" Text="Rezultatai"></asp:Label>
        <asp:Table ID="results" runat="server" BorderStyle="Solid" BorderWidth="2px" Height="641px" Width="637px" BorderColor="Red">
        </asp:Table>
        <br />
        Iveskite leidinio kodą ir mėnesį (skaičiais)<br />
        <asp:TextBox ID="codeText" runat="server" Width="231px" Height="22px">Leidinio kodas</asp:TextBox>
        <br />
        <asp:TextBox ID="monthText" runat="server" Width="231px">Mėnesis</asp:TextBox>
        <br />
        <asp:Button ID="createListButton" runat="server" OnClick="createListButton_Click" Text="Sudaryti sąrašą" Width="147px" />
        <br />
        <asp:Table ID="newListTable" runat="server" Height="227px" Width="637px" BorderStyle="Solid" BorderWidth="2px" BorderColor="Red">
        </asp:Table>
        </div>
    </form>
    <p>
        &nbsp;</p>
    
</body>
</html>
