<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="study.aspx.cs" Inherits="SignalR.study" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
   <script type="text/javascript">
       function enter(id) {
           $('#enterBox').val(id);
           $('#Button1').click();

       }

   </script>
</head>
<body>
    <form id="form1" runat="server">

             <asp:TextBox ID="idBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="pwBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="cBox" runat="server" CssClass="hide"></asp:TextBox>        
        <asp:TextBox ID="welBox" runat="server" CssClass="hide" Text="true" ></asp:TextBox>
        <asp:TextBox ID="iconBox" runat="server"  CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="enterBox" runat="server" CssClass="hide"></asp:TextBox>
          <asp:Button ID="Button1" runat="server" Text="Button" CssClass="hide" OnClick="enter" />
    </form>
    
</body>
</html>
