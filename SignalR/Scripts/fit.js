$(window).load(function () {
    var img = new Image();
    var h;
    var w;
    img.src = $('#mappic').attr('src');
    $('#mappic').css({
        'max-height':'100%',
        'max-width': '100%',
        'min-width': '100%',
        'min-height': '100%',
        'position': 'absolute',
        'z-index':'-1',
    });
    img.onload = function () {
        w = $('body').css('width').replace('px', '') / this.width;
        h = $('body').css('height').replace('px', '') / this.height;

        w = $('#mappic').css('width').replace('px', '') / this.width;
        h = $('#mappic').css('height').replace('px', '') / this.height;
        $('area').each(function () {
            var coords = $(this).attr('coords').split(',');
            var id = $(this).attr('id').replace('space', 'div');
            var type = $(this).attr('class');
          //  console.log($(this).attr('id')+" "+$(this).attr('coords'));


            $(this).attr('coords', Math.round(coords[0] * w) + "," + Math.round(coords[1] * h) + "," + Math.round(coords[2] * w) + "," + Math.round(coords[3] * h));


            //console.log($(this).attr('coords'));
            $('div').each(function () {
              //  alert();
                if ($(this).attr('class') === 'space' && $(this).attr('id')===id) {
                    $(this).css({
                        'margin-left': coords[0] * w,
                        'margin-top': coords[1] * h,
                        'width': (coords[2] - coords[0])*w,
                        'height': (coords[3] - coords[1])*h,
                    });
                    $(this).find(type).css({
                        'width': (coords[2] - coords[0])*w,
                        'height': (coords[3] - coords[1])*h,
                        'font-size': (type != 'textarea' ? (coords[3] - coords[1]) *h* 0.8 : (coords[3] - coords[1]) *h* 0.1),
                    });

                    if(id==='picdiv'){
                        if($(this).css('height').replace('px','')<109||$(this).css('width').replace('px','')<109){
                            
                        }else{
                            $(this).css({
                                'padding-top':((coords[3] - coords[1])-109)/2,
                            });
                        }

                        $('#icon').css({
                            'height': '104px',
                            'width': '112px',
                    
                        });
                        $('.icon').css({
                            'height': '104px',
                            'width': '112px',

                        });
                        if ($('#iconBox').val() != "") {
                            icon($('#iconBox').val());

                        } else {
                            
                            icon(Math.floor(Math.random() * 12));
                           
                        }
                        
                    }

                    if (id === 'showdiv') {
                        var cla = $('#GridView1').attr('class');
                        var cla2 = $('#DetailsView1').attr('class');
                        if(cla==='show'){
                            $('#GridView1').css({
                                'width': (coords[2] - coords[0])*w,
                                'height': (coords[3] - coords[1])*h,
                                'top': coords[1] * h,
                                'left': coords[0] * w,
                            });
                        }
                        
                        if (cla2 === 'show') {
                            $('#DetailsView1').css({
                                'width': (coords[2] - coords[0])*w,
                                'height': (coords[3] - coords[1])*h,
                                'top': coords[1] * h,
                                'left': coords[0] * w,
                            });
                        }

                    }

                    if (id === 'show2div') {
                        var cla = $('#GridView2').attr('class');
                       if (cla === 'show2') {
                            $('#GridView2').css({
                                'width': (coords[2] - coords[0])*w,
                                'height': (coords[3] - coords[1])*h,
                                'top': coords[1] * h,
                                'left': coords[0] * w,
                            });
                        }

                      

                    }
                    if (id === 'wheeldiv') {
                        var bh= $('.ly-plate').css('height').replace('px', '');
                        
                        $('.ly-plate').css({
                            'width':$(this).css('height'),
                            'height': $(this).css('height'),

                        });

                      //  console.log($('.ly-plate').css('width'));
                        $('.rotate-bg').css({
                            'width': $(this).css('height'),
                            'height': $(this).css('height'),

                        });
                        $('.lottery-star').css({
                            'width':$(this).css('height').replace('px','')* $('.lottery-star').css('width').replace('px', '')/bh,
                            'height': $(this).css('height').replace('px', '') * $('.lottery-star').css('height').replace('px', '') / bh,
                            'top': $(this).css('height').replace('px', '') * $('.lottery-star').css('top').replace('px', '') / bh,
                            'left': $(this).css('height').replace('px', '') * $('.lottery-star').css('left').replace('px', '') / bh,
                        });
                        $('#lotteryBtn').css({
                            'width': $(this).css('height').replace('px', '') - 2*$('.lottery-star').css('top').replace('px', ''),
                            'height': $(this).css('height').replace('px', '') - 2 * $('.lottery-star').css('top').replace('px', ''),


                        });
                    }

                }
                
            });

        
           
        });
       
    }
});

function icon(selector) {

    var xStart = 0;
    var yStart = 0;
    var xNum = 4;
    var yNum = 3;
    var xLength = 112;
    var yLength = 104;
    $('#icon').css({
        'background-position-x': -(xStart + Math.floor(selector / xNum) * xLength),
        'background-position-y': -(yStart + (selector % yNum) * yLength)
    });
    $('#iconBox').val(selector);
    
}





function checkLabel(input, label) {//檢查值
    if (input) {
        label.css({ 'display': 'none' });
    } else {
        label.css({ 'display': 'block' });
    }
  
    return input;
}



function illegalCharCheck(value) {//檢查特殊字元
    if (value != value.replace(/[^\a-\z\A-\Z0-9\u4E00-\u9FA5\ ]/g, '')) {
        return false;
    } else {
        return true;
    }

}


