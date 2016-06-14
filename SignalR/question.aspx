<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="question.aspx.cs" Inherits="SignalR.question" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<script type="text/javascript">
    $(function () {
        
           
        $(".t").change(function () {
            var sen = $(this).attr('id').replace('t','');
                //取得文字長度, select數要相等
                var len = $(this).val().length;
                //移除多餘的select
                var $altBlock = $("#spnAlt" + sen);
                $altBlock.find("select:gt(" + (len - 1) + ")").remove();
                //取現有select數
                var curLen = $altBlock.find("select").length;
                //補足字元數個select
                for (var i = curLen; i < len; i++) {
                    $altBlock.append(
                    "<select id='s" + sen + "' class='s" + i + "plus" + sen + "'><option value='0'>0</option>" +
                    "<option value='1'>1</option>" +
                    "<option value='2'>2</option>" +
                    "<option value='3'>3</option></select>");

                }
                
            }).change();

            //任何欄位變動後就重新顯示
        $("input,select").live("change", function () {
            var sen=$(this).attr('id').replace('s', '').replace('t', '');
            if (sen > 0 && sen < 5) {
                //組成URL
                var url = "ChWordImage.ashx?t=" + escape($("#t" + sen).val()) +
                          "&w=" + $("#w").val() + "&h=" + $("#h").val() +
                          "&bc=" + $("#bc").val() + "&fc=" + $("#fc").val() +
                          "&af=" + getAltFont() + "&fs=" + $("#fs").val();
                //alert(url);
                //設定圖檔產生參數
                $("#preview" + sen).attr("src", url);
                //取得破音字型切換
                function getAltFont() {
                    var v = [];
                    $("#spnAlt" + sen).find("option:selected").each(function () {
                        v.push(this.value);
                    });
                    return v.join('');
                }


            }

        });
   
       
        $("#bc,#fc").change(function () {
            $(this).parent().find(".c").css("background-color", "#" + this.value);
        }).change();
 
      
        
        //觸發初始change
        $("input:first").change();
        if ($('#typeBox').val() === 'edit') {
            $('input').each(function () {
                if ($(this).val() === '刪除') {
                    $(this).css({'display':'none'});

                }
            });
        }


        


    });

    function enter(id) {
        $('#typeBox').val(id);
        $('#Button1').click();

    }

    function exit() {
        $('#Button3').click();

    }
    function up() {
        $('input').each(function (index) {

            $('#' + $(this).attr('id') + "Box").val($(this).val());
        });

       
        var sp = ["s1", "s2", "s3", "s4", ];
        $('select').each(function (index) {

            $('#' + $(this).attr('id') + "Box").val($('#' + $(this).attr('id') + "Box").val() + $(this).val());
        });

        $('#Button4').click();

    }

    $('#nextdiv').click(function () {
        var custom = "";
        $('input').each(function (index) {

            $('#' + $(this).attr('id') + "Box").val($(this).val());
        });

        $('textarea').each(function (index) {

            $('#' + $(this).attr('id') + "Box").val($(this).val());
        });
        var sp = ["s1", "s2", "s3", "s4", ];
        $('select').each(function (index) {

            $('#' + $(this).attr('id') + "Box").val($('#' + $(this).attr('id') + "Box").val()+$(this).val());
        });
      
            $('#Button2').click();
        

    });
</script>
<style type="text/css">
    /*
    body { font-size: 11pt; background-color: #444444; color: yellow; }
    #bc,#fc { width: 50px; }
    #w,#h,#fs { width: 50px; }
    input.c  { border: 1px dotted black; width: 40px; }
    span { margin: 3px; }
    div { padding: 4px; }
        */
</style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="idBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="pwBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="cBox" runat="server" CssClass="hide"></asp:TextBox>        
        <asp:TextBox ID="welBox" runat="server" CssClass="hide" Text="true" ></asp:TextBox>
        <asp:TextBox ID="iconBox" runat="server"  CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="typeBox" runat="server" CssClass="hide"></asp:TextBox>

        <asp:TextBox ID="setypeBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="nameBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="t1Box" runat="server" CssClass="hide"></asp:TextBox>
         <asp:TextBox ID="t2Box" runat="server" CssClass="hide"></asp:TextBox>
         <asp:TextBox ID="t3Box" runat="server" CssClass="hide"></asp:TextBox>
         <asp:TextBox ID="t4Box" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="s1Box" runat="server" CssClass="hide"></asp:TextBox>
         <asp:TextBox ID="s2Box" runat="server" CssClass="hide"></asp:TextBox>
         <asp:TextBox ID="s3Box" runat="server" CssClass="hide"></asp:TextBox>
         <asp:TextBox ID="s4Box" runat="server" CssClass="hide"></asp:TextBox>


        <asp:TextBox ID="explainBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="dataBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" 
            DataKeyNames="no" DataSourceID="SqlDataSource1" BackColor="#DEBA84"  OnPageIndexChanged="GridView1_PageIndexChanged"
            BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" CellSpacing="2" AllowPaging="True" AllowSorting="false" PageSize="10"  CssClass="hide" 
            OnRowDataBound="GridView1_RowDataBound" >
             <Columns>
               <asp:TemplateField ShowHeader="True"><ItemTemplate><asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="if (confirm('Are you sure?') == false) return false;"></asp:LinkButton></ItemTemplate> </asp:TemplateField>
                <asp:BoundField DataField="no" HeaderText="no" InsertVisible="True" 
                    ReadOnly="True" SortExpression="no" Visible="false" />
              

             </Columns>
            


        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server"  AutoGenerateColumns="true" 
            DataKeyNames="no" DataSourceID="SqlDataSource2" BackColor="#DEBA84"  
            BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" AllowPaging="True" AllowSorting="false" PageSize="10"  CssClass="hide" >
             <Columns>
                 <asp:CommandField ButtonType="Button" ShowDeleteButton="false" ShowEditButton="true"  />
                <asp:BoundField DataField="no" HeaderText="no" InsertVisible="True" 
                    ReadOnly="True" SortExpression="no" Visible="false" />
              

             </Columns>


        </asp:GridView>

        <asp:Button ID="Button1" runat="server" Text="Button" CssClass="hide" OnClick="enter" />
        <asp:Button ID="Button2" runat="server" Text="Button" CssClass="hide" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="Button"  CssClass="hide" OnClick="exit" />
        <asp:Button ID="Button4" runat="server" Text="Button" CssClass="hide" OnClick="Button4_Click"/>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:gameMDF %>" 
            SelectCommand="SELECT no,name,id FROM [information]" 
            DeleteCommand="DELETE QUESTION FROM QUESTION q inner join information i on CONVERT(NVARCHAR(MAX), q.name)=CONVERT(NVARCHAR(MAX), i.name) WHERE i.no = @no;DELETE FROM information  WHERE no = @no;" 
            OnDeleted="SqlDataSource1_Deleted"
            >
            <DeleteParameters>
                <asp:Parameter Name="no" Type="Int32" />
                <asp:Parameter Name="name" Type="String" />
            </DeleteParameters>
            
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:gameMDF %>" 
            SelectCommand="SELECT * FROM [question]" 
            UpdateCommand="UPDATE [question] SET [name] = @name, [id] = @id, [w1] = @w1, [w2] = @w2, [w3] = @w3, [w4] = @w4, [w5] = @w5, [w6] = @w6, [w7] = @w7 WHERE [no] = @no" 
            >
            <UpdateParameters>
                <asp:Parameter Name="no" Type="Int32" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="id" Type="String" />
                <asp:Parameter Name="w1" Type="String" />
                <asp:Parameter Name="w2" Type="String" />
                <asp:Parameter Name="w3" Type="String" />
                <asp:Parameter Name="w4" Type="String" />
                <asp:Parameter Name="w5" Type="String" />
                <asp:Parameter Name="w6" Type="String" />
                <asp:Parameter Name="w7" Type="String" />

            </UpdateParameters>

        </asp:SqlDataSource>

    </form>
     <div>
<div class="hide"><span>寬　度:</span><input type="text" id="w"  class="hide" value="500"/></div>
<div class="hide"><span>高　度:</span><input type="text" id="h"  class="hide" value="50"/></div>
<div class="hide"><span>大　小:</span><input type="text" id="fs"  class="hide" value="20" /></div>
<div class="hide"><span>顏　色:</span><input type="text" id="fc"  class="hide" value="ffffff" /><input class='c' readonly /></div>

<div class="hide"><span>底　色:</span><input type="text" id="bc"  class="hide" value="af2f00" /><input class='c' readonly /></div>

</div>
    
</body>
</html>
