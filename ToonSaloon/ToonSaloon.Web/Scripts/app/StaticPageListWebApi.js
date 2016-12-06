var uri2 = '/api/StaticPageList';

$(document)
    .ready(function() {
        GetPages();
    });

function GetPages() {
    $.getJSON(uri2)
        .done(function (data) {
            $.each(data,
                function (index, pages) {
                    $('<li><a href="/StaticPage/View/' + pages.Id + '">' + pages.Name + '</a></li>').appendTo($('#staticList'));
                });
        });
};