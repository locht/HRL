
$(document).mouseup(function (e) {
    var container = $(".box23");
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        $(".box23").css("display", "none");
    }
});

$(document).ready(function () {
    $(".box48").click(function () {
        var data = $(this).attr("data");

        if ($("#collapse-" + data + "").is(":visible")) {
            $("#collapse-" + data + "").slideUp();
            $(this).children("i").removeClass("fa fa-chevron-up");
            $(this).children("i").addClass("fa fa-chevron-down");
        }
        else {
            $("#collapse-" + data + "").slideDown();
            $(this).children("i").removeClass("fa fa-chevron-down");
            $(this).children("i").addClass("fa fa-chevron-up");
        }
    });
    //show thongbao
    $('.box3').click(function () {
        $(".box23").show();
    });
});



  


