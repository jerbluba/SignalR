<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="SignalR.Chat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>挑戰區</title>
   
    




    <script type="text/javascript">
        
        //建立與Server端的Hub的物件，注意Hub的開頭字母一定要為小寫
        
        $(function () {

            var fontSize2 = $("#answer").css('height').replace('px', '') / 16;
            $("#answertable").css({
                    'text-align': 'center',
                    'font-size': fontSize2
                });


            

            var chat = $.connection.codingChatHub;
            //registerClientMethods(chat);  // 向後端的Hub，註冊 Clinet端（前端）的 function
            //建立連線後，我們接著來定義client端的function來讓Server端的hub呼叫。

            var userID = $('#noBox').val();//遊戲編號
            var userName = $('#idBox').val();//玩家帳號
           
            var fontSize = $('#event').css('font-size').replace('px','')/5;//log視窗字型大小
            
            //遮罩畫面的訊息
            chat.client.message = function (message, message2) {
                $('#messageUI').html(message + userName + message2);
            }
            //色子
            chat.client.dice = function (your, open) {
                var dice1 = setInterval(function () {
                    randomDice($('#dice1'),dice1,your);
                }, 50);
                var dice2 = setInterval(function () {
                    randomDice($('#dice2'), dice2, open);
                }, 50);

            }
            //根據不同的still讓玩家可以對遮罩面下達不同的指令
            chat.client.touch = function (still) {
                switch (still) {
                    case 0:
                        $('#blockUI').click(function () {
                            $(this).css({
                                'display': 'none',
                            });
                            chat.server.gameStart(userID);//點下去會依照骰子大小修改你的still

                            chat.server.setRotate(userID);
                        });
                        break;
                    case 1:
                        $('#blockUI').click(function () {
                            $(this).css({
                                'display': 'none',
                            });
                        });
                        chat.server.setRotate(userID);
                        break;

                    case 2:
                    case 3:
                        $('#blockUI').click(function () {
                            $(this).css({
                                'display': 'none',
                            });
                        });
                        break;
                    case 4:
                        history.go(-1);
                        break;
                   
                }
            }
            //遮罩面的除錯
            chat.client.debug = function (message) {
                $('#debug').html("<li>" + message + "</li>");
            }
            //畫面資料
            chat.client.setScore = function (id, message) {
                if (id == userID) {
                    id = "";
                }
                $('#' + id + "score").val(message);
            }

            chat.client.setIcon = function (id,selector) {
                if (id == userID) {
                    id = "";
                }
                var xStart = 4;
                var yStart = 3;
                var xNum = 4;
                var yNum = 3;
                var xLength = 115;
                var yLength = 120;
                $('#' + id + 'icon').css({

                    'background-position-x': -(xStart + Math.floor(selector / xNum) * xLength),
                    'background-position-y': -(yStart + (selector % yNum) * yLength)
                });
                //   alert('doCss'+id);

            }
            chat.client.setData = function (id, message) {
                if (id == userID) {
                    id = "";
                }
                $('#' + id + "data").text(message);

            }
            chat.client.setEvent = function (message) {
                // console.log(fontSize);
                $('#event').append("</br>" + message).css({ 'font-size': fontSize, }).scrollTop(9999999);
                 
            }
            chat.client.setInput = function (id,val) {
                $('#' + id).val(val);
            }
            //互動觸發

            chat.client.rotateFunction = function (angle,id) {  //旋轉輪盤
                $('#lotteryBtn').stopRotate();
                $("#lotteryBtn").rotate({
                    angle: 0,
                    duration: 5000,
                    animateTo:  1440+angle, //指針角度
                    callback: function () {
                        //   alert(angle);
                        if(id==userID)
                            chat.server.startCount(userID);
                    }
                });
            };


         
            //console debug
            chat.client.console = function (message) {//consolo debug
                console.log(message);
            }

            chat.client.sendTable = function () {//寄table給對手
                var sID = userID+1;
                if (sID % 2 == 0) {
                    sID -= 2;
                }

                chat.server.sendTable(sID,$('#answertable').html());
             
            }
            chat.client.setTable = function (table) {//填入table
                $('#answertable').html(table);
            }
            chat.client.setSingle = function (str, type,index) {
                $('.' + type).eq(index).val(str);
               
            }

            chat.client.showKeyBoard = function () {

                var w = $('#keyboard').parent().css('width');
                var h = $('#keyboard').parent().css('height');
                $('#keyboard').css({
                    'display': 'block',
                    'width': w,
                    'height': h,
                     
                }).html(newTable(buffer.attr('class'), w, h));

            }
            chat.client.hideKeyBoard=function(){
                $('#keyboard').html('').css({
                    'display': 'none',
                });
            }
            chat.client.showAnswer = function (message, x, y) {//填入國字
                $("#GridView1_" + x + "plus" + y + "_" + x).append("<li>" + message + "</li>");
            }
            //顯示所有同注音的欄位 並且計算分數
            chat.client.setAll = function (str,type) {// TODO 算分
                $('.' + type).each(function (index) {

                    chat.server.showAnswer(str, $(this).attr('id'), $(this).attr('class'), $('#question').text(),index, userID);
                });

            }

            init=function (id) {
                /** Initialize buffer for clicked bpmf and textarea for chosen Chinese characetrs */
                buffer = $('#' + id);
                chat.server.keyBoard(userID);

            }

            kb_on_click = function (str) {//點擊鍵盤的觸發
                var seq = "";
                buffer.val(str.charAt(0));
                //檢查注音
                chat.server.tryAnswer(str, buffer.attr('id'), buffer.attr('class'), $('#question').text(), userID);

                //檢查國字
                var x = buffer.attr('id').replace('GridView1_', '').split('plus');
                var y = x[1].split('_')[0];
          
                x = x[0];
                var s = x;
                switch (x % 4) {
                    case 1:
                        s  -= 1;
                        break;
                    case 2:
                        s -= 2;
                        break;
                    case 3:
                        s -= 3;
                        break;
                }
                x = [s, s + 1, s + 2, s + 3];
                var str = "",val="";
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
                    chat.server.showAnswer($('#question').text(), str,userID, x[0] / 4, y);
                }

                //關閉鍵盤並清空時間
                $('#keyboard').html('').css({
                    'display': 'none',
                });
            }


            //將連線打開
            $.connection.hub.start().done(function () {
                //當連線完成後，呼叫Server端的hello方法，並傳送使用者姓名給Server
                //    alert("open");
                chat.server.userConnected(userID);
                //  alert("succse");
            });

            $('#answer').css({
                'overflow': 'auto',
            });
            $("#answertable").html($("#GridView1").html());
            $("#answertable tr").css({
                'height': '25px'
            });

            //點擊呼叫輪盤
            $("#lotteryBtn").rotate({
                bind:
                  {
                      click: function () {
                          chat.server.setRotate(userID);
                      }
                  }
            });
            $('#exit').click( function(){
                $('#Button1').click();

        });
         
        });

        //骰子動畫
        var XL = [-6, -54, -100, -148, -196, -243];
        var XL2 = [-12, -68, -105];
        var YL = [-14, -64, -116];
        function randomDice(target,interval,answer) {
            var x = Math.floor(Math.random() * 10);
            switch (x) {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    target.css({
                        'background-position': XL[x] + " " + YL[0]
                    });
                    break;
                default:
                    target.css({
                        'background-position': XL[answer-1] + " " + YL[2]
                    });
                    clearInterval(interval);
                    break;
                case 6:
                case 7:
                case 8:
                    target.css({
                        'background-position': XL[x - 6] + " " + YL[1]
                    });
                    break;
            } 
        }
        
       
</script>
</head>
<body>

        <form id="form1" runat="server">
            <div id="question" class="hide">
            <asp:GridView ID="GridView1" runat="server" ShowHeader="false" CssClass="hide"></asp:GridView>
                

            </div> 
                <asp:TextBox ID="iconBox" runat="server"  CssClass="hide"></asp:TextBox>
                <asp:TextBox ID="idBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="pwBox" runat="server" CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="cBox" runat="server" CssClass="hide"></asp:TextBox>        
        <asp:TextBox ID="welBox" runat="server" CssClass="hide"  ></asp:TextBox>
        <asp:TextBox ID="noBox" runat="server"  CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="stillBox" runat="server"  CssClass="hide"></asp:TextBox>
        <asp:TextBox ID="enterBox" runat="server" CssClass="hide"></asp:TextBox>
<asp:Button ID="Button1" runat="server" Text="Button" CssClass="hide" OnClick="Button1_Click" />
    </form
    >
    <div id="blockUI" >
        <div id="messageUI"></div>
        <div class="middle">
            <label >你擲出：</label>
        <div id="dice1" class="dice"></div>
        


        </div>
        <div class="middle">
            <label >對手擲出：</label>
        <div id="dice2" class="dice"></div>


        </div>
        
        <div class="middle">
            <div id="debug" ></div>
        

        </div>
         
    </div>
 


  

</body>
</html>
