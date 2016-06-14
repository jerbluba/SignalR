<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="knowledge.aspx.cs" Inherits="SignalR.knowledge" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript"  > 

         function enter() {//隨機換題目
            $('#Button1').click();

        }
        function edit(id) {//選擇成語或詩詞
            $('#typeBox').val(id);
            $('#Button2').click();

        }


        function exit() {
            $('#Button3').click();

        }
        $(window).load(function () {
            $('table').css({
                'border': '0px',
            });
            $('td').css({
                'border': '0px',
            });
            $('th').css({
                'border': '0px',
            });

        });
    </script>

    <style>
        #GridView1 {
            font-weight:bold;
            
        }

        #GridView1,#GridView2 {
            font-size:300%;
            
        }

    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="idBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="pwBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="cBox" runat="server" CssClass="hide"></asp:TextBox>        
        <asp:TextBox ID="welBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="iconBox" runat="server"  CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="typeBox" runat="server" CssClass="hide"></asp:TextBox>

        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"  CssClass="hide"/>
        <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click"  CssClass="hide"/>
        <asp:Button ID="Button3" runat="server" Text="Button" OnClick="Button3_Click"  CssClass="hide"/>

   <asp:GridView ID="GridView1" runat="server"   
            CssClass="hide"    ShowHeader="false"    
       >
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server"
            
               ShowHeader="false"    
           CssClass="hide">

        </asp:GridView>
      
    </form>
</body>
</html>
