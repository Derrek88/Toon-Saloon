$(document)
    .ready(function() {
        var $anchor = $('.gallery');
        $anchor.each(function() {
            $(this).unitegallery({
                theme_enable_play_button: true,
                slider_videoplay_button_type: "round"
            });
        });

    });