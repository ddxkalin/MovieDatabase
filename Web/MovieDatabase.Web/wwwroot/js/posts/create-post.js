(function (movieDbApp, $, undefined) {

    var addActorHtml = "<div class='form-group row cast'>" +
                            '<div class="col-xs-12 col-md-6" >' +
                                '<div class="md-input">' +
                                    '<input type="text" class="md-input-control typeahead">' +
                                '</div>' +
                                '$actorIdValidation$' +
                            '</div>' +
                            '<div class="col-xs-12 col-md-6">' +
                                '<div class="md-input movie-actor">' +
                                    '$characterNameInput$' +
                                    '$actorIdInput$' +
                                '</div>' +
                                '$characterNameValidation$' +
                            '</div>' +
                        '</div>';

    var addAwardHtml = "<div class='form-group row award'>" +
                            '<div class="col-xs-12">' +
                                '<div class="md-input">' +
                                    '$awardInput$'+
                                '</div>' +
                            '</div>' +
                        '</div>';

    //Public Property
    movieDbApp.createPostConfig = {
        getActorsUrl: '',
        getImdbRatingUrl: '',
        actorHtml: {
            characterNameInput: '',
            actorIdInput: '',
            characterNameValidation: '',
            actorIdValidation: ''
        },
        awardHtml: {
            awardInput: '',
        }
    };

    //Public Method
    movieDbApp.init = function () {
        $.validator.setDefaults({ ignore: ':disabled' });

        typeahead_initialize();

        $('form').on('typeahead:selected typeahead:autocomplete', '.typeahead', function (e, datum) {
            var $input = $(e.target);
            var $movieActorContainer = $input.closest('.cast').find('.movie-actor');

            //fire .focusout() so the validator can update
            $movieActorContainer.find('.actor-id').attr('value', datum.id).focusout();
        });

        initActorButtonEvents();
        initAwardButtonEvents();
        initGetImdbRatingEvent();
        initRatedEvents();
    };

    //Private Method
    function typeahead_initialize() {
        var actors = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            remote: {
                url: movieDbApp.createPostConfig.getActorsUrl,
                wildcard: 'QUERY'
            }
        });

        $('.typeahead').typeahead(null, {
            limit: 10,
            name: 'actors',
            source: actors,
            display: 'name',
            //value: 'id',
            templates: {
                empty: [
                    '<div class="empty-message">',
                    'Nothing found',
                    '</div>'
                ].join('\n'),
            }
        });
    }

    function initActorButtonEvents() {
        $('#actor-add-btn').on('click', function (e) {
            var nextId = $('.cast').length;

            var html = addActorHtml
                .replace('$characterNameInput$', movieDbApp.createPostConfig.actorHtml.characterNameInput)
                .replace('$actorIdInput$', movieDbApp.createPostConfig.actorHtml.actorIdInput)
                .replace('$actorIdValidation$', movieDbApp.createPostConfig.actorHtml.actorIdValidation)
                .replace('$characterNameValidation$', movieDbApp.createPostConfig.actorHtml.characterNameValidation)
                .replace(/0/g, nextId);


            $('.cast').last().after(html);

            $('.typeahead').typeahead('destroy');
            typeahead_initialize();

            resetFormValidation();

            if ($('#actor-remove-btn').hasClass('hidden')) {
                $('#actor-remove-btn').removeClass('hidden');
            }
        });

        $('#actor-remove-btn').on('click', function (e) {
            var length = $('.cast').length;

            if (length > 1) {
                if (length == 2) {
                    $(this).addClass('hidden');
                }

                $('.cast').last().remove();
            }
        });
    }

    function initAwardButtonEvents() {
        $('#award-add-btn').on('click', function (e) {
            var nextId = $('.award').length;

            var html = addAwardHtml
                .replace('$awardInput$', movieDbApp.createPostConfig.awardHtml.awardInput)
                .replace(/0/g, nextId);

            $('.award').last().after(html);
            resetFormValidation();

            if ($('#award-remove-btn').hasClass('hidden')) {
                $('#award-remove-btn').removeClass('hidden');
            }
        });

        $('#award-remove-btn').on('click', function (e) {
            var length = $('.award').length;

            if (length > 1) {
                if (length == 2) {
                    $(this).addClass('hidden');
                }

                $('.award').last().remove();
            }
        });
    }

    function initGetImdbRatingEvent() {
        $('.movie-title').on('focusout', function (e) {
            //if (!$('.imdb-rating').hasClass('hidden')) {
            //    return;
            //}

            var movieTitle = $(this).val();
            var url = movieDbApp.createPostConfig.getImdbRatingUrl;
            var token = $('input[name="__RequestVerificationToken"]').val();

            $.ajax({
                url: url,
                type: "post",
                data: { __RequestVerificationToken: token, title: movieTitle },
                success: function (data) {
                    var rating = data['imdbRating'];

                    $('.rating-value').html(rating);
                    $('.imdb-rating').removeClass('hidden');

                    //set the hidden input val
                    $('#imdb-rating').attr('value', rating);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);

                    if (!$('.imdb-rating').hasClass('hidden')) {
                        $('.imdb-rating').addClass('hidden');
                    }
                }
            });

        });
    }

    function initRatedEvents() {
        $(document).on('movieRated', function (e, ratingValue) {
            $('#rating').attr('value', ratingValue);
        });
    }

    function resetFormValidation() {
        $("form").removeData("validator").removeData("unobtrusiveValidation");
        //Parse the form again
        $.validator.unobtrusive.parse($("form"));
    }

}(window.movieDbApp = window.movieDbApp || {}, jQuery));