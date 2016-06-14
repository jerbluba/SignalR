<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="SignalR.register"  EnableSessionState="True" EnableViewStateMac="False" Debug="False" EnableViewState="False" ViewStateMode="Disabled" ViewStateEncryptionMode="Never" EnableEventValidation="False" ValidateRequest="False" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        #selector {
            max-height: 100%;
            max-width: 100%;
            min-height: 100%;
            min-width: 100%;
            position:absolute;
            background:url(pic/icons.jpg);
            background-repeat:no-repeat;
            background-size:contain;
            z-index:101;
        }
        #selector:hover {
            cursor:pointer;
        }
    </style>
    <script>
        
        function checkID(id) {
            var firstItem = {};
            firstItem["id"] = id.val();
            $.ajax({
                type: "POST",
                url: "register.aspx/checkID",
                data: JSON.stringify(firstItem),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    // Replace the div's content with the page method's return.
                    $('#cBox').val(msg.d);
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
                 
                 },
            });
        }

        $(window).load(function () {
            $('#icon').click(function () {
                 $('#selector').css({
                    'display':'block',
                });

            });

            $('#selector').click(function (e) {
               // var b = $('#selector').css('background-image');
                var tmpImg = new Image();
                tmpImg.src = "pic/icons.jpg"; //or  document.images[i].src;
                var orgWidth = tmpImg.width;
                var orgHeight = tmpImg.height;
                
                var xA = orgWidth, yA = orgHeight, xL = $('#selector').css('width').replace('px', ''), yL = $('#selector').css('height').replace('px', '');
                var x = Math.floor((4 * e.pageX) / (xL));
                var y = Math.floor((3 * e.pageY) / (yL));
            if(xA*yL>xL*yA){//
                //yL = yA;
                y = Math.floor((3 * e.pageY * xA) / (xL * yA));
            } else if (xA * yL < xL * yA) {//
                //xL = xA;
                x = Math.floor((4 * e.pageX * yA) / (xA * yL));
            }

                
                var se = 3 *x + y;
                //alert(e.pageX + " " + $('#selector').css('width').replace('px', ''));
                icon(se);
                $('#selector').css({
                    'display': 'none',
                });
            });
            $('#id').val($('#idBox').val());
            $('#pw').val($('#pwBox').val());
            $('#backdiv').click(function () {
                $('#Button3').click();

            });
            $('#nextdiv').click(function () {
                var custom = "";
                $('input').each(function (index) {

                    $('#' + $(this).attr('id') + "Box").val($(this).val());
                });
                checkID($('#newId'));
                if (checkLabel(illegalCharCheck($('#newId').val()) && !$('#newId').val() == "", $('#noid'))
                    && checkLabel(illegalCharCheck($('#pw').val()) && !$('#pw').val() == "", $('#nopw'))


                    ) {
                    //$('#dataRows').html("<input name='confirm' type='text' value='true' />" + custom);
                    //alert($('#dataRows').html());
                    $('#cBox').val('true');
                    // $('#form1').attr('action', 'register.aspx');
                    //$('#form1').submit();
                    $('#Button2').click();
                }

            });
            $('#sure').click(function () { //Done
                var pwcheck = true;
                var custom = "";
                $('input').each(function (index) {

                    if ($(this).attr('id') === "pwc") {
                        if ($(this).val() != $('#pw').val()) {
                            pwcheck = false;
                        }
                    }

                    $('#' + $(this).attr('id') + "Box").val($(this).val());
                });
                checkID($('#id'));
                if (checkLabel(illegalCharCheck($('#name').val()) && !$('#name').val() == "", $('#noname'))
                    && checkLabel(illegalCharCheck($('#id').val()) && !$('#id').val() == "", $('#noid'))
                    && checkLabel(illegalCharCheck($('#pw').val()) && !$('#pw').val() == "", $('#nopw'))
                    && checkLabel(illegalCharCheck($('#pwc').val()) && !$('#pwc').val() == "", $('#nopwc'))
                    && checkLabel(pwcheck, $('#wrongpwc'))
                    && checkLabel($('#cBox').val(), $('#reid'))

                    ) {
                    //$('#dataRows').html("<input name='confirm' type='text' value='true' />" + custom);
                    //alert($('#dataRows').html());
                    $('#cBox').val('true');
                    // $('#form1').attr('action', 'register.aspx');
                    //$('#form1').submit();
                    $('#Button1').click();
                }




            });


        });


       
    </script>
</head>
<body>
    <form id="form1" runat="server">
       
        
        <asp:TextBox ID="nameBox" runat="server"  CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="idBox" runat="server"  CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="newIdBox" runat="server"  CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="pwBox" runat="server"   CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="pwcBox" runat="server"   CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="cBox" runat="server"   CssClass="hide" ></asp:TextBox>
        <asp:TextBox ID="iconBox" runat="server"  CssClass="hide"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" CssClass="hide" PostBackUrl="~/register.aspx" />
        <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" CssClass="hide"/>
        <asp:Button ID="Button3" runat="server" Text="Button" CssClass="hide" OnClick="login" />
        
         </form>
</body>
</html>
