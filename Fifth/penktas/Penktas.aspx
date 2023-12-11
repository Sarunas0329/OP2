<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Penktas.aspx.cs" Inherits="penktas.Penktas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Start" runat="server" OnClick="Start_Click" Text="Start" Width="113px" />
            <br />
            <asp:Label ID="startException" runat="server" Enabled="False"></asp:Label>
        </div>
        <asp:Button ID="Result" runat="server" OnClick="Result_Click" Text="Results" Width="113px" />
        <br />
        <asp:Label ID="resultException" runat="server" Enabled="False"></asp:Label>
        <br />
        <asp:Label ID="posLabel" runat="server" Text="Position"></asp:Label>
        <br />
        <asp:TextBox ID="PosTextBox" runat="server"></asp:TextBox>
        <asp:Label ID="posException" runat="server" Enabled="False"></asp:Label>
        <br />
        <asp:Label ID="Date1Label" runat="server" Text="Time period, from (yyyy/MM/dd)"></asp:Label>
        <br />
        <asp:TextBox ID="Period1Box" runat="server"></asp:TextBox>
        <asp:Label ID="fromException" runat="server" Enabled="False"></asp:Label>
        <br />
        <asp:Label ID="Date2Label" runat="server" Text="Time period, to (yyyy/MM/dd)"></asp:Label>
        <br />
        <asp:TextBox ID="Period2Box" runat="server"></asp:TextBox>
        <asp:Label ID="toException" runat="server" Enabled="False"></asp:Label>
        <br />
        <asp:Label ID="countLabel" runat="server" Text="Best player count"></asp:Label>
        <br />
        <asp:TextBox ID="PlayerCountBox" runat="server"></asp:TextBox>
        <asp:Label ID="countException" runat="server" Enabled="False"></asp:Label>
        <asp:Table ID="startingData" runat="server" BorderStyle="Solid" BorderWidth="4px" GridLines="Both" Height="500px" Width="750px">
        </asp:Table>
        <asp:Table ID="startingDataPos" runat="server" BorderStyle="Solid" BorderWidth="4px" GridLines="Both" Height="500px" Width="500px">
        </asp:Table>
        <asp:Table ID="resultTable" runat="server" BorderStyle="Solid" BorderWidth="2px" GridLines="Both" Height="250px" Width="750px">
        </asp:Table>
    </form>
</body>
</html>
