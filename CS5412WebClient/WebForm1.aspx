<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CS5412WebClient.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Arial; height: 329px;">
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Get Message" 
        onclick="Button1_Click" />
    <br />
    <asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>
    <br />
        <br />
        <br />
        <br />
        <br />
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <asp:Button ID="Button2" runat="server" Text="Get/Set" 
    onclick="Button2_Click" Height="61px" Width="100px" />
    <asp:Label ID="Label2" runat="server" Font-Bold="true"></asp:Label>
    <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Font-Bold="true"></asp:Label>
    </div>
    </form>

</body>
</html>
