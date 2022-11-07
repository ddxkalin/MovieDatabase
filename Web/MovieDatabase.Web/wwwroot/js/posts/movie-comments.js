(function (movieDbApp, $, undefined) {
    var newCommentHtml = "<div class='row'>" +
                            '<div class="col-xs-12 col-md-12 mb-0p5" >' +
                                '<div class="col-xs-12 col-lg-3 comment-meta">' +
                                    '<span class="comment-author"></span>' +
                                    '<span class="comment-date"></span>' +
                                '</div>' +
                                '<div class="col-xs-12 col-lg-9 comment-text"></div>' +
                            '</div>' +
                        '</div>';

    //Public Property
    movieDbApp.movieCommentsConfig = {
        addCommentUrl: '',
    };

    //Public Method
    movieDbApp.commentsInit = function () {
        initAddCommentEvents();
    };

    //Private method
    function addComment() {
        var postId = $("#postId").val();
        var url = movieDbApp.movieCommentsConfig.addCommentUrl;
        var token = $('input[name="__RequestVerificationToken"]').val();
        var commentText = $('#newComment').val();

        $.ajax({
            url: url,
            type: "post",
            data: { __RequestVerificationToken: token, postId: postId, commentText: commentText},
            success: function (data) {
                if (data.alert.success === true) {
                    toastr.success(data.alert.message);

                    var $newComment = $(newCommentHtml);
                    $newComment.find('.comment-text').html(data.comment);
                    $newComment.find('.comment-date').html(data.createdOn);
                    $newComment.find('.comment-author').html(data.author);

                    $('.comments-box').prepend($newComment);
                    $('#newComment').val('');

                    if ($('.empty-comment').length) {
                        $('.empty-comment').hide();
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    }

    function initAddCommentEvents() {
        $('.add-comment').on('click', function () {
            addComment();
        });
    }

}(window.movieDbApp = window.movieDbApp || {}, jQuery));