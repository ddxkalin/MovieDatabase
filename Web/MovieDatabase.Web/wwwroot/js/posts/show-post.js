(function (movieDbApp, $, undefined) {

    //Public Property
    movieDbApp.showPostConfig = {
        rateMovieUrl: '',
        addToWishlistUrl: '',
        removeFromWishlist: '',
    };

    //Public Method
    movieDbApp.init = function () {
        initRatedEvents();
        initWishlistEvents();
    };

    //Private Method
    function rateMovie(rating) {
        var postId = $("#postId").val();
        var url = movieDbApp.showPostConfig.rateMovieUrl;
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: url,
            type: "post",
            data: { __RequestVerificationToken: token, postId: postId, rating: rating },
            success: function (data) {
                $('.rating-value').html(data.overallRating.toFixed(1));
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    }

    function addToWishlist() {
        var postId = $("#postId").val();
        var url = movieDbApp.showPostConfig.addToWishlistUrl;
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: url,
            type: "post",
            data: { __RequestVerificationToken: token, postId: postId},
            success: function (data) {
                if (data.success === true) {
                    toastr.success(data.message);
                    $('.wishlist-buttons a').toggle()
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    }

    function removeFromWishlist() {
        var postId = $("#postId").val();
        var url = movieDbApp.showPostConfig.removeFromWishlistUrl;
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: url,
            type: "post",
            data: { __RequestVerificationToken: token, postId: postId },
            success: function (data) {
                if (data.success === true) {
                    toastr.success(data.message);
                    $('.wishlist-buttons a').toggle();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    }

    function initWishlistEvents() {
        $('.add-to-wishlist').on('click', function () {
            addToWishlist();
        });

        $('.remove-from-wishlist').on('click', function () {
            removeFromWishlist();
        });
    }

    function initRatedEvents() {
        $(document).on('movieRated', function (e, ratingValue) {
            rateMovie(ratingValue);
        });
    }

}(window.movieDbApp = window.movieDbApp || {}, jQuery));