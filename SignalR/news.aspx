<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news.aspx.cs" Inherits="SignalR.news" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript"  > 

        function exit() {
            $('#Button1').click();

        }

        $(function () {
            var fontSize = $('#GridView1').css('height').replace('px', '') / 6;
            $("#GridView1 td").each(function () {
                $(this).css({
                    'text-align': 'center',
                    'font-size':fontSize
                });

            });
            $("#GridView1 th").each(function () {
                $(this).css({
                    'font-size': fontSize
                });

            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:TextBox ID="idBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="pwBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="cBox" runat="server" CssClass="hide"></asp:TextBox>        
        <asp:TextBox ID="welBox" runat="server" CssClass="hide" Text="true" ></asp:TextBox>
        <asp:TextBox ID="iconBox" runat="server"  CssClass="hide"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="exit" CssClass="hide" />
        <asp:GridView ID="GridView1" runat="server" CssClass="show" AutoGenerateColumns="true" AllowSorting="false" PageSize="5" ShowHeader="false">
           


        </asp:GridView>
    </form>
</body>
</html>
