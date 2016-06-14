<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="practice.aspx.cs" Inherits="SignalR.practice" %>

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
       


        init = function (id) {
            /** Initialize buffer for clicked bpmf and textarea for chosen Chinese characetrs */
            buffer = $('#' + id);
            var w = $('#keyboard').parent().css('width');
            var h = $('#keyboard').parent().css('height');
            $('#keyboard').css({
                'display': 'block',
                'width': w,
                'height': h,

            }).html(newTable(buffer.attr('class'), w, h));

        }

        kb_on_click = function (str) {//點擊鍵盤的觸發
            var seq = "";
            buffer.val(str.charAt(0));
            //檢查注音
            var firstItem = {};
            firstItem["str"] = str;
            firstItem["id"] = buffer.attr('id');
            firstItem["className"] = buffer.attr('class');
            firstItem["name"] = $('#question').text();
            $.ajax({
                type: "POST",
                url: "practice.aspx/tryAnswer",
                data: JSON.stringify(firstItem),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    // Replace the div's content with the page method's return.
                
                    if (msg.d == 'True') {
                    } else {
                        buffer.val('');
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
                   
                    return false;
                },
            });
            //檢查國字
            var x = buffer.attr('id').replace('GridView1_', '').split('plus');
            var y = x[1].split('_')[0];

            x = x[0];
            var s = x;
            switch (x % 4) {
                case 1:
                    s -= 1;
                    break;
                case 2:
                    s -= 2;
                    break;
                case 3:
                    s -= 3;
                    break;
            }
            x = [s, s + 1, s + 2, s + 3];
            var str = "", val = "";
            var doCheck = true;
            var y1 = y, y2 = y;

            if (y % 2 == 0) {
                ++y2;
            } else {
                --y1;
            }
            for (var i = 1; i < x.length; i++) {
                val = $('#GridView1_' + x[i] + 'plus' + y1 + "_" + x[i]).val();

                if (val != undefined) {
                    if (val != "") {
                        str += val;
                    } else {
                        doCheck = false;
                    }
                }
            }
            val = $('#GridView1_' + x[0] + 'plus' + y1 + "_" + x[0]).val();

            if (val != undefined) {

                if (val != "") {
                    str += val;
                } else {
                    doCheck = false;
                }
            } else {
                val = $('#GridView1_' + x[2] + 'plus' + y2 + "_" + x[2]).val();
                //alert("tone2:" + '#GridView1_' + x[2] + 'plus' + y2 + "_" + x[2]);
                if (val != undefined) {
                    if (val != "") {
                        str += val;
                    } else {
                        doCheck = false;
                    }
                }
            }

            if (doCheck) {
                seq = xbpmf(str);
                str = cbpmf(seq);
                var firstItem = {};
                firstItem["name"] = $('#question').text();
                firstItem["x"] = x[0] / 4;
                firstItem["y"] = y;
                $.ajax({
                    type: "POST",
                    url: "practice.aspx/showAnswer",
                    data: JSON.stringify(firstItem),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // Replace the div's content with the page method's return.

                        switch (y % 2) {
                            case 0:
                                y -= 4;
                                break;
                            case 1:
                                y -= 5;
                                break;
                        }
                        $("#GridView1_" + (x[0]) + "plus" +y+ "_" + (x[0])).append("<li>" + msg.d+ "</li>");
                        //return msg.d;
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

                        return false;
                    },
                });



            }

            //關閉鍵盤並清空時間
            $('#keyboard').html('').css({
                'display': 'none',
            });
        }
    </script>
    <style>
        td,table{
         border-width:0px;
        }
        span{
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

   <asp:GridView ID="GridView1" runat="server"   ShowHeader="false"
            CssClass="show"       
       >
        </asp:GridView>
     
    </form>
</body>
</html>
