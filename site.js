// Color rating crows.

function colorCrows() {
    var rating;
    var target;

    $(".movie-rating").each(function () {
        rating = parseFloat($(this).html().replace(',','.'));
        rating = (rating * 100 / 5) + "%";
        target = $(this).attr("data-target");
        $(target).css("width", rating);
    });
}

$(document).ready(colorCrows());

// Bootstrap Datepicker

$('#signUpDateOfBirth').datepicker({
    format: "yyyy-mm-dd",
    autoclose: true,
    startView: 2
});


function nextPage(pager) {
    var metadata = JSON.parse($(pager).attr('metadata'));
    var target = $(pager).attr('target');
    var action = $(pager).attr('action');

    var options = {
        url: action,
        type: "GET",
        data: { resourceUrl: metadata.nextPageLink },
        success: function (data, status, jqXHR) {
            $(target).replaceWith(data);
            metadata = JSON.parse(jqXHR.getResponseHeader('X-Pagination'));
            $(pager).find("span").html(metadata.currentPage + ' / ' + metadata.totalPages);
            validatePagerButtons(pager, metadata.currentPage, metadata.totalPages);
            $(pager).attr('metadata', JSON.stringify(metadata));
            colorCrows();
        }
    };

    $.ajax(options);
    return false;
}

function previousPage(pager) {
    var metadata = JSON.parse($(pager).attr('metadata'));
    var target = $(pager).attr('target');
    var action = $(pager).attr('action');

    var options = {
        url: action,
        type: "GET",
        data: { resourceUrl: metadata.previousPageLink },
        success: function (data, status, jqXHR) {
            $(target).replaceWith(data);
            metadata = JSON.parse(jqXHR.getResponseHeader('X-Pagination'));
            $(pager).find("span").html(metadata.currentPage + ' / ' + metadata.totalPages);
            validatePagerButtons(pager, metadata.currentPage, metadata.totalPages);
            $(pager).attr('metadata', JSON.stringify(metadata));
            colorCrows();
        }
    };

    $.ajax(options);
    return false;
}

function validatePagerButtons(pager, page, total) {
    if (page === 1) {
        $(pager).find('.btn-previous').attr('disabled', 'disabled');
        $(pager).find('.btn-next').removeAttr('disabled');
    }

    if (page === total) {
        $(pager).find('.btn-next').attr('disabled', 'disabled');
        $(pager).find('.btn-previous').removeAttr('disabled');
    }
}

$(document).ready(validatePagerButtons('#reviewsPager', 1));

function onSearchInput(searchBox, value, pager) {
    if (value !== "" && value.length < 3) {
        return;
    }

    var target = $(searchBox).attr('target');
    var action = $(searchBox).attr('action');

    var options = {
        url: action,
        type: "GET",
        data: { searchWord: value },
        success: function (data, status, jqXHR) {
            $(target).replaceWith(data);
            metadata = JSON.parse(jqXHR.getResponseHeader('X-Pagination'));
            $(pager).find("span").html(metadata.currentPage + ' / ' + metadata.totalPages);
            validatePagerButtons(pager, metadata.currentPage, metadata.totalPages);
            $(pager).attr('metadata', JSON.stringify(metadata));
        }
    };

    $.ajax(options);
}

function toggleComment(element, trigger) {
    $(element).toggle();

    if ($(trigger).html() === 'Cancel') {
        $(trigger).html('Comment');
    } else {
        $(trigger).html('Cancel');
    }
}

$(document).on('submit', '#likeReview', function () {
    var form = $(this);
    var options = {
        url: $(form).attr('action'),
        type: "POST",
        data: $(form).serialize(),
        success: function (data) {
            span = $(form).find('span');
            if ($('#like').val() === "true") {
                $(span).removeClass('glyphicon-heart-empty');
                $(span).addClass('glyphicon-heart');
                $('#like').val("false");
            } else {
                $(span).removeClass('glyphicon-heart');
                $(span).addClass('glyphicon-heart-empty');
                $('#like').val("true");
            }
            if ($(data.peopleWhoLikedReview).length === 0) {
                $('#likedBy').html('Liked by: ');
            } else {
                var likedBy = 'Liked by: ';
                $(data.peopleWhoLikedReview).each(function (index, user) {
                    likedBy += user + ' ';
                });
                $('#likedBy').html(likedBy);
            }
        }
    };
    $.ajax(options);
    return false;
});

function sortReviewsBy(trigger) {
    var genre = $('#reviewsFilterSelect').find(':selected').val();
    var options = {
        url: "Reviews/ReviewsAjax",
        type: "GET",
        data: {
            sortBy: $(trigger).val(),
            movieGenre: genre
        },
        success: function (data, status, jqXHR) {
            $('#listOfReviews').replaceWith(data);
            metadata = jqXHR.getResponseHeader('X-Pagination');
            $('#reviewsPager').attr('metadata', metadata);
            switch ($(trigger).val()) {
                case 'time_asc':
                    $(trigger).val('time_desc');
                    break;
                case 'time_desc':
                    $(trigger).val('time_asc');
                    break;
                case 'rating_asc':
                    $(trigger).val('rating_desc');
                    break;
                case 'rating_desc':
                    $(trigger).val('rating_asc');
                    break;
                case 'movie_asc':
                    $(trigger).val('movie_desc');
                    break;
                case 'movie_desc':
                    $(trigger).val('movie_asc');
                    break;
                case 'reviewer_asc':
                    $(trigger).val('reviewer_desc');
                    break;
                case 'reviewer_desc':
                    $(trigger).val('reviewer_asc');
                    break;
            }
            colorCrows();
        }
    };
    $.ajax(options);
}