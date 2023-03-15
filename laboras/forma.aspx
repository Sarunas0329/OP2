<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forma.aspx.cs" Inherits="laboras.forma" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Start" />
        </p>
        <p>
            <asp:Table ID="Table1" runat="server" Height="65px" Width="265px">
            </asp:Table>
        </p>
        <p>
            Rezultatai</p>
        <p>
            <asp:Table ID="Table2" runat="server" BackColor="White" ForeColor="Black" Height="288px" Width="265px">
            </asp:Table>
        </p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
