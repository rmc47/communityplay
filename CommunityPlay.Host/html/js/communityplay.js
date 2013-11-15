$(function () {
    var viewModel = {
        title: ko.observable("CommunityPlay @ Cambridge 105"),
        searchTerm: ko.observable("Search..."),
        mediaResults: ko.observableArray(),
        cartButtons: ko.observableArray()
    };

    ko.applyBindings(viewModel);

    $.getJSON("/api/medialibrary/list", function (data) {
        for (var i = 0; i < data.length; i++) {
            var media = data[i];
            var mediaItem = {
                text: media.Name,
                play: function() { 
                    var id = media.ID;
                    return function () {                    
                        $.get("/api/audio/play/" + id);
                }}()
            };
            viewModel.mediaResults.push(mediaItem);
            viewModel.cartButtons.push(mediaItem);
        }
    });
});