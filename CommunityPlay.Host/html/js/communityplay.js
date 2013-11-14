$(function () {
    $.getJSON("/api/medialibrary/list", function (data) {
        for (var i = 0; i < data.length; i++) {
            var media = data[i];
            $("#medialist").append("<li><a href=\"/api/audio/play/" + media.ID + "\">" + media.Name + "</a></li>");
        }
    });
});