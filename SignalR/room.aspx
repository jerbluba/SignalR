<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="room.aspx.cs" Inherits="SignalR.room" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="no" DataSourceID="SqlDataSource1" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="true" />
                <asp:BoundField DataField="no" HeaderText="no" InsertVisible="True" 
                    ReadOnly="True" SortExpression="no" />
                <asp:BoundField DataField="name" HeaderText="名稱" 
                    SortExpression="name" />
                <asp:BoundField DataField="id" HeaderText="帳號" 
                    SortExpression="id" />
                <asp:BoundField DataField="pw" HeaderText="密碼" 
                    SortExpression="pw" />
                <asp:BoundField DataField="icon" HeaderText="圖檔" 
                    SortExpression="icon" />
                <asp:BoundField DataField="score" HeaderText="歷史高分" 
                    SortExpression="score" />
                <asp:BoundField DataField="room" HeaderText="所屬社團" 
                    SortExpression="room" />
                
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:gameMDF %>" 
            SelectCommand="SELECT * FROM [users]" 
            DeleteCommand="DELETE FROM [users] WHERE [no] = @no" 
            InsertCommand="INSERT INTO [users] ([name], [id], [pw], [icon], [score], [room]) VALUES (@name, @id, @pw, @icon, @score, @room)" 
            UpdateCommand="UPDATE [users] SET [name] = @name, [id] = @id, [pw] = @pw, [icon] = @icon, [score] = @score, [room] = @room WHERE [id] = @id">
            <DeleteParameters>
                <asp:Parameter Name="no" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="id" Type="String" />
                <asp:Parameter Name="pw" Type="String" />
                  <asp:Parameter Name="icon" Type="Int32" />
                <asp:Parameter Name="score" Type="Int32" />
                <asp:Parameter Name="room" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                  <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="id" Type="String" />
                <asp:Parameter Name="pw" Type="String" />
                  <asp:Parameter Name="icon" Type="Int32" />
                <asp:Parameter Name="score" Type="Int32" />
                <asp:Parameter Name="room" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
         </form>

 
</body>
</html>
