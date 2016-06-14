<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SignalR.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
   
    <script type="text/javascript"  > 

        function checkID() {
            var firstItem = {};
            firstItem["id"] = $('#id').val();
            firstItem["pw"] = $('#pw').val();
            $.ajax({
                type: "POST",
                url: "Default.aspx/checkID",
                data: JSON.stringify(firstItem),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    // Replace the div's content with the page method's return.
                    $('#cBox').val(msg.d);
                    var bool=checkLabel(illegalCharCheck($('#id').val()) && !$('#id').val() == "", $('#noid'))&& checkLabel(illegalCharCheck($('#pw').val()) && !$('#pw').val() == "", $('#nopw'))&& checkLabel(($('#cBox').val()!="false"), $('#wrong'));
                   // alert(bool);
                    if (bool) {
                        $('#Button1').click();
                    }

                    return msg.d;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    // Replace the div's content with the page method's return.
                    console.log("Status: " + textStatus);
                    console.log("Error: " + errorThrown);
                    console.log("XMLr: " + XMLHttpRequest.readyState);
                    console.log("XMLr: " + XMLHttpRequest.responseText);
                    console.log("XMLr: " + XMLHttpRequest.responseXML);
                    console.log("XMLr: " + XMLHttpRequest.status);
                    console.log("XMLr: " + XMLHttpRequest.statusText);
                    $('#cBox').val("false");
                    if (checkLabel(illegalCharCheck($('#id').val()) && !$('#id').val() == "", $('#noid'))
                 && checkLabel(illegalCharCheck($('#pw').val()) && !$('#pw').val() == "", $('#nopw'))
                 && checkBool($('#cBox').val(), $('#wrong'))
                 ) {
                        $('#Button1').click();
                    }
                    return false;
                },
            });
        }
        function enter(id) {
            $('#enterBox').val(id);
            $('#Button2').click();

        }
        function edit(id) {
            $('#table').val(id);
            $('#Button4').click();

        }


        function exit() {
            $('#Button3').click();

        }
        $(window).load(function () {

            var img2 = new Image();
            var h2;
            var w2;
            img2.src = $('#welpic').attr('src');
            img2.onload = function () {
           //     w2 = $('body').css('width').replace('px', '') / this.width;
             //   h2 = $('body').css('height').replace('px', '') / this.height;
            }

            if ($('#welBox').val()=="true") {
                $('#welpic').css({
                    'min-height': '100%',
                    'min-width': '100%',
                    'max-height': '100%',
                    'max-width': '100%',
                    'position': 'absolute',
                    'z-index': '100',
                    'margin': 'auto',
                    'top': '0'

                });

            } else {
                $('#welpic').css({
                    'display': 'none',
                    'position': 'absolute',
                    'z-index': '100',
                    'margin': 'auto',
                    'top': '0'
                });

            }

         
            $('#welpic').click(function () {
               
                $('#main').css({
                    'display': 'block',
                });
                $(this).css({
                    'display': 'none',
                });
                $('#welBox').val("false");
            });
            $('#login').click(function () { //Done
                $('input').each(function (index) {
                    $('#' + $(this).attr('id') + "Box").val($(this).val());
                });
                checkID();
            });

            if ($("#edit").val() != "") {
                $("#GridView1_LinkButton2_" + $("#edit").val()).attr('class', 'hide');
                $("#GridView1_LinkButton3_" + $("#edit").val()).attr('class', '');
            }

        });
        

    </script>
</head>
<body>
    <div id="body">
      <img src='pic/welcome.jpg?v=10' alt='' id='welpic'   />
        <form id="form1" runat="server">
  
        <asp:TextBox ID="idBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="pwBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="cBox" runat="server" CssClass="hide"></asp:TextBox>        
        <asp:TextBox ID="welBox" runat="server" CssClass="hide" Text="true" ></asp:TextBox>
        <asp:TextBox ID="iconBox" runat="server"  CssClass="hide"></asp:TextBox>
            <asp:TextBox ID="table" runat="server"  CssClass="hide"></asp:TextBox>
            <asp:TextBox ID="edit" runat="server" CssClass="hide"></asp:TextBox>
            <asp:GridView ID="GridView1" runat="server"   CssClass="hide"  
                AllowPaging="True" 
                AllowSorting="True" 
                AutoGenerateColumns="True"
                PageSize="10"
                Visible="false"
                ShowFooter="false"
                OnRowCommand="GridView1_RowCommand"
                DataKeyNames="no"
             EmptyDataText="目前尚無資料喔" OnRowDataBound="GridView1_RowDataBound"
                >
                <Columns> 
                    
                    <asp:TemplateField ShowHeader="True">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" 
                                OnClientClick="$('#edit').val(this.id.replace('GridView1_LinkButton2_',''));">

                            </asp:LinkButton>

                             <asp:LinkButton ID="LinkButton3" CssClass="hide" runat="server" CausesValidation="False" CommandName="Update" Text="更新" OnClientClick="$('#edit').val('');">

                            </asp:LinkButton>

                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="if (confirm('Are you sure?') == false) return false;">

                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField CommandName="myInsert" Text="新增" ButtonType="Button"
                    HeaderText="新增" />

                    
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

                <EmptyDataTemplate>
                      歡迎您進入「新增（Insert）」模式：<br />
                <br />
                

                </EmptyDataTemplate>
               
            </asp:GridView>

             <asp:DetailsView 
                     ID="DetailsView1" 
                     runat="server" 
                     AutoGenerateRows="true"
                    CellPadding="4" 
                 DataKeyNames="no" 
                    DefaultMode="Insert" 
                    ForeColor="#333333" GridLines="None" Height="50px"
                    Width="125px"
                  
                 >
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <CommandRowStyle BackColor="#FFFFC0" Font-Bold="True" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <FieldHeaderStyle BackColor="#FFFF99" Font-Bold="True" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <Fields>
                       
                        <asp:CommandField ShowInsertButton="True" />
                       
                    </Fields>
                 <EmptyDataTemplate>



                 </EmptyDataTemplate>
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:DetailsView>

                   


            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:gameMDF %>" 
                SelectCommand="SELECT * FROM [users]" 
                DeleteCommand="DELETE FROM [users] WHERE [no] = @no" 
                InsertCommand="INSERT INTO [users] ([name], [id], [pw], [icon], [score], [room]) VALUES (@name, @id, @pw, @icon, @score, @room)" 
                UpdateCommand="UPDATE [users] SET [name] = @name, [id] = @id, [pw] = @pw, [icon] = @icon, [score] = @score, [room] = @room WHERE [no] = @no"
                 
                >
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

            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                ConnectionString="<%$ ConnectionStrings:gameMDF %>" 
            SelectCommand="SELECT * FROM [announce]" 
            DeleteCommand="DELETE FROM [announce] WHERE [no] = @no" 
            InsertCommand="INSERT INTO [announce] ([name], [time]) VALUES (@name, @time)" 
            UpdateCommand="UPDATE [announce] SET [name] = @name, [time] = @time WHERE [no] = @no" 
                >
                 <DeleteParameters>
                <asp:Parameter Name="no" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="time" Type="DateTime" />
            </InsertParameters>
            <UpdateParameters>
                  <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="time" Type="DateTime" />
            </UpdateParameters>

            </asp:SqlDataSource>
        <asp:TextBox ID="enterBox" runat="server" CssClass="hide"></asp:TextBox>

        <asp:Button ID="Button1" runat="server" Text="Button" CssClass="hide" PostBackUrl="~/Default.aspx" />
        <asp:Button ID="Button2" runat="server" Text="Button" CssClass="hide" OnClick="enter" />
        <asp:Button ID="Button3" runat="server" Text="Button" CssClass="hide" OnClick="exit"/>
        <asp:Button ID="Button4" runat="server" Text="Button" CssClass="hide" OnClick="Button4_Click"/>

          </form>
    </div>
    
</body>
</html>
