$(function () {
    var viewModel = new function() {
        this.title = ko.observable("CommunityPlay @ Cambridge 105");
        this.searchTerm = ko.observable("");
        this.cartButtons = ko.observableArray();

        this.mediaResults = ko.observableArray();
        ko.computed(function () {
            var params = { term: this.searchTerm };
            $.getJSON('/api/medialibrary/search', params, this.mediaResults);
        }, this).extend({ throttle: 500 });
        this.playMediaResult = function (media) {
            $.get(media.PlayUri);
        };

        this.nowPlaying = ko.observableArray();
        this.stopPlaying = function (media) {
            $.get(media.StopUri);
        };
        this.fadePlaying = function (media) {
            $.get(media.FadeUri);
        };
    };

    ko.applyBindings(viewModel);

    var updateCarts = function () {
        $.getJSON("/api/medialibrary/listcarts", function (data) {
            for (var i = 0; i < data.length; i++) {
                var media = data[i];
                var mediaItem = {
                    text: media.Name,
                    play: function () {
                        var id = media.ID;
                        return function () {
                            $.get("/api/audio/play/" + id);
                        }
                    }()
                };
                //viewModel.mediaResults.push(mediaItem);
                viewModel.cartButtons.push(mediaItem);
            }
        });
    };
    updateCarts();
    setInterval(updateCarts, 60000);

    setInterval(function () {
        $.getJSON("/api/nowplaying/list", viewModel.nowPlaying);
    }, 1000);
});