<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ladder.aspx.cs" Inherits="SignalR.ladder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    


        <script type="text/javascript">






            $(function () {

                $('#answer').css({
                    'overflow': 'auto',
                });


                $("#laddertable").html($("#GridView1").html()).css({
                    'width': '64%',
                    //'font-size': '64%',
                });

                var fontSize = $("#ladder").css('height').replace('px','')/11;
                $("#laddertable td").each(function () {
                    $(this).css({
                        'text-align': 'center',
                        'font-size':fontSize
                });

                    
                });

                $("#laddertable th").each(function () {
                    $(this).css({
                        'text-align': 'center',
                        'font-size': fontSize
                    });

                });
            });
            function enter() {
                $('#enterBox').val("default");
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
        <asp:GridView ID="GridView1" runat="server" CssClass="hide"></asp:GridView>
        <asp:Button ID="Button1" runat="server" Text="Button" CssClass="hide" OnClick="enter" />
    </form>
</body>
</html>
