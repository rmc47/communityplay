$(function () {
    $.getJSON("/api/medialibrary/list", function (data) {
        for (var i = 0; i < data.length; i++) {
            var media = data[i];
            $("#medialist").append("<li><a href=\"/api/audio/play/" + media.ID + "\">" + media.Name + "</a></li>");

            var cartDiv = $("<div class=\"col-md-4\"></div>");
            var button = $("<button />").text(media.Name);
            button.click(function () {
                var id = media.ID;
                $.get("/api/audio/play/" + id);
            });
            cartDiv.append(button);
            $("#cartsrow").append(cartDiv);
        }
    });
});