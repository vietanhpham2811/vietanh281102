$(document).ready(function () {

    $(".search").mouseenter(function () {

    });





    $("#sub-menu-filter").click(function () {
        $(".sub-menu-filter1,sub-menu-filter2").fadeToggle();
    })


    array_slide = new Array("Image_dalis/cat-banner-1.jpg", "Image_dalis/content-img.png", "Image_dalis/content-img2.jpg", "Image_dalis/content-img3.jpg");
    var n = 0;
    setInterval(function () {
        $(".slide_show").fadeToggle(2000, function () {
            n++;

            $(".slide_show").fadeToggle();
            $(".slide_show").attr("src", array_slide[n]);

            if (n == array_slide.length) {
                return n = 0;
            }
        });
    });


});

// slide-show

function change_img(id) {
    var style = document.getElementById(id).getAttribute("src");
    document.getElementById("slide-show").setAttribute("src", style)

    document.getElementById("img1").removeAttribute("style")
    document.getElementById("img2").removeAttribute("style")

    document.getElementById("img3").removeAttribute("style")

    document.getElementById("img4").removeAttribute("style")

    document.getElementById("img5").removeAttribute("style")
    document.getElementById(id).setAttribute("style", "border:1px solid red")
}


function onload() {
    document.getElementById("img1").setAttribute("style", "border;1px solid red")

}

$(document).ready(function () {
    $("#img-check1").click(function () {
        $("#check1").fadeIn(0.001, function () {
            $("#check2").fadeOut();
        });
        $(".img-slide2").fadeOut(0.001,function(){
            $(".img-slide1").fadeIn()
        })
    });
    $("#img-check2").click(function () {
        $("#check2").fadeIn(0.001, function () {
            $("#check1").fadeOut()
        })
         $(".img-slide1").fadeOut(0.001,function(){
            $(".img-slide2").fadeIn()
        })
    });

});



$(document).ready(function(){
    $("#s-b1").click(function(){
        $(".sub-describe2").fadeOut(1,function(){
            $(".sub-describe1").fadeIn()
            $("#s-b1").css("border-bottom","4px solid red")
            $("#s-b2").css("border-bottom","none")
           
        })
    })

    $("#s-b2").click(function(){
        $(".sub-describe1").fadeOut(1,function(){
            $(".sub-describe2").fadeIn()
             $("#s-b2").css("border-bottom","4px solid red")
             $("#s-b1").css("border-bottom","none")
        })
    })
    
   
})




$(document).ready(function(){
    var n=0;
    $("#btnAdd").click(function(){
       $("#quantity").val.innerHTML=n;
    });
        
});
