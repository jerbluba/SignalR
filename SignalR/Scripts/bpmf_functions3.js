//global variables for two text input objects
var buffer=null;
var textpad=null;
var list=Array();
var choice=null;
var cursorPos=1;



//查出該字音轉成鍵盤上的值
function xbpmf(str){
/** Convert bpmf in buffer to the key for the character table. */
		var key="";
		for (i=0; i<str.length; i++){
			//alert(i+ " " + str.charAt(i)+ ":" + bpmf[str.charAt(i)]);
			//document.write(bpmf[str.charAt(i)]);
			key +=bpmf[str.charAt(i)];}
	//		alert("key is: " +key);
		return key;
	}
//轉出所有選項
function cbpmf(key){
/**Search characters for keyined bpmf with Regular Expression */
	var pattern=new RegExp("\\n"+ key +" \.", "g");
	//var pattern=new RegExp("\\n"+ key +" .{2}", "g");
	//alert("RE pattern for "+key+": "+ escape(pattern.source));
	//alert("RE test for "+ pattern.source+": "+ pattern.test(cin));
	//var tmp=new Array();
	var pos=key.length+2;
	var ars=cin.match(pattern);
	if (ars==null){
		alert("No such pronuciation in the dictionary");
	//	buffer.value="";
		return;}
	var pmt=": ";
	for (i=0; i<ars.length; i++){
		list[i]=ars[i].charAt(pos);
		pmt += i+"."+ list[i]+ " ";}
	return pmt;
    //以下兩行是原始輸入法用的
    //buffer.value += pmt;
	//choice.select();

    //var sel=window.prompt("Select the character: ", "0");
	//var sel=window.prompt(pmt, "0");
	//alert("Character chosen is: "+tmp[sel]);
	//return tmp[sel];
}

function append_word(){
	//alert(buffer.value);
	if ((buffer.value).indexOf(":")==-1) {
		//alert("You forgot tone mark! Try it again!");
		kb_on_click(' ');
		return;}
	i=parseInt(choice.value);
	//alert("Select "+i +"-th word");
	//textpad.value +=list[i];  	//Append Chinese character
	//insertString(list[i]);
	insertAtCursor(textpad, list[i]);
	buffer.value="";      		//Empty buffer and ready for next input
	choice.value="0";
}


function newTable(type,width,height){
    var answer='<p>鍵盤區</p><table style="font-size:24pt;width:'+width+';height:'+height+';"><tr align="left"><th>';
    var first = [ "˙" ];
    var second = [ "ㄅ", "ㄆ", "ㄇ", "ㄈ", "ㄉ", "ㄊ", "ㄋ", "ㄏ", "ㄍ", "ㄎ", "ㄑ", "ㄔ", "ㄘ", "ㄒ", "ㄕ", "ㄙ", "ㄌ", "ㄖ", "ㄐ", "ㄓ", "ㄗ", ];
    var third = [ "ㄧ", "ㄨ", "ㄩ", ];
    var forth = [ "ㄚ", "ㄞ", "ㄢ", "ㄦ", "ㄛ", "ㄟ", "ㄣ", "ㄜ", "ㄠ", "ㄤ", "ㄝ", "ㄡ", "ㄥ", ];
    var fifth = ["ˊ", "ˇ", "ˋ", ];
    var array=[];
    switch(type){
        case 'first':
            array=first;
            break;
            
        case 'second':
            array=second;
            break;
            
        case 'third':
            array=third;
            break;
            
        case 'forth':
            array=forth;
            break;
            
        case 'fifth':
            array=fifth;
            break;
    
    }
    for(var i=0;i<array.length;i++){
        if(i%10==9)answer+=    '</th></tr><tr align="left"><th>';
        answer += '<input value="' + array[i] + '" onclick="kb_on_click(this.value);" type="button" style="font-size:24pt;">';
            
    }

    answer += '</th></tr></table>';
  
    return answer;
}



function insertAtCursor(obj, text) {
	var head = obj.value.substring(0,cursorPos);
	var tail = obj.value.substring(cursorPos, obj.value.length);
	obj.value=head+text+tail;
	//alert("head: "+head.length+" text: "+text.length);
	cursorPos=head.length+text.length;
	//alert("head/text/tail: "+head.length+"/"+text.length+"/"+tail.length);
	//alert("Current cursor at (insert) "+cursorPos);
	}

function setCursorPos(obj) {
	if (document.selection){
		obj.focus();
		var objRange = document.selection.createRange();
		var oldRange = objRange.text;
		var weirdString = '#%~';

		objRange.text = oldRange + weirdString;  //insert the weirdstring at the cursor position
		objRange.moveStart('character', (0 - oldRange.length - weirdString.length));

		var newText = obj.value; //save off the new string with the weirdstring in it
		objRange.text = oldRange;  //set the actual text value back to how it was
		for (i=0; i <= newText.length; i++) {
			var temp = newText.substring(i, i + weirdString.length);
			if (temp == weirdString) {
				cursorPos = (i - oldRange.length);
				break;		
			}
		}
	} else if(obj.selectionStart){
		cursorPos=obj.selectionStart;
	} else {cursorPos=obj.value.length;} 
	//alert("Current cursor at (change) "+cursorPos);
 }
