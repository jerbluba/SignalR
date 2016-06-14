<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="board.aspx.cs" Inherits="SignalR.board" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript"  >
        function exchange(id) {
            $('#typeBox').val(id);
            $('#ip').val("0,0,0,0");
            $('#show').val($('#content').val());
            $('#Button1').click();
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:TextBox ID="idBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="pwBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="cBox" runat="server" CssClass="hide"></asp:TextBox>        
        <asp:TextBox ID="welBox" runat="server" CssClass="hide" Text="true" ></asp:TextBox>
        <asp:TextBox ID="iconBox" runat="server"  CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="typeBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="ip" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="show" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="enterBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:GridView ID="GridView1" runat="server"  
            CssClass="show" 
            AllowPaging="True" 
            AllowSorting="True"
             OnPageIndexChanging="GridView1_PageIndexChanging" DataSourceID="SqlDataSource1" AutoGenerateColumns="false" PageSize="10" >
                  <Columns>
                <asp:CommandField ButtonType="Button" ShowDeleteButton="False" ShowEditButton="False" ShowInsertButton="false" />
                <asp:BoundField DataField="no" HeaderText="no" InsertVisible="True" 
                    ReadOnly="True" SortExpression="no" Visible="false" />
                <asp:BoundField DataField="id" HeaderText="名稱" 
                    SortExpression="id" />
                <asp:BoundField DataField="show" HeaderText="內文" 
                    SortExpression="show" />
                <asp:BoundField DataField="time" HeaderText="留言日期" 
                    SortExpression="ptime" />
                 <asp:BoundField DataField="ip" HeaderText="ip" InsertVisible="True" 
                    ReadOnly="True" SortExpression="no" Visible="false" />
            </Columns>

        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:gameMDF %>" 
            SelectCommand="SELECT * FROM [board]" ></asp:SqlDataSource>
        <asp:Button ID="Button1" runat="server" Text="Button" CssClass="hide"  OnClick="enter"/>

    </form>
</body>
</html>
