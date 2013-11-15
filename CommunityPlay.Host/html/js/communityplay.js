$(function () {
    var viewModel = new function() {
        this.title = ko.observable("CommunityPlay @ Cambridge 105");
        this.searchTerm = ko.observable("Search...");
        this.mediaResults = ko.observableArray();
        this.cartButtons = ko.observableArray();

        ko.computed(function () {
            var params = { term: this.searchTerm };
            $.getJSON('/api/medialibrary/search', params, this.mediaResults);
        }, this).extend({ throttle: 500 });
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
            //viewModel.mediaResults.push(mediaItem);
            viewModel.cartButtons.push(mediaItem);
        }
    });
});