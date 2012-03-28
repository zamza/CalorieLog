/// <reference path="jquery-1.5.1.js" />
$('#calendar').fullCalendar({
    dayClick: function (onSuccess) {
        var hi = $.ajax({
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "/JsonServices/SayHi",
            data: "{}",
            dataType: "json",
            success: onSuccess
        });
        alert(hi);
    }
});


   