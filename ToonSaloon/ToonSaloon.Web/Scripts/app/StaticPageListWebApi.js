var uri2 = '/api/StaticPageList';
var uri3 = '/api/PopularTags';

$(document)
    .ready(function() {
        GetPages();
        GetTags();
    });

function GetPages() {
    $.getJSON(uri2)
        .done(function (data) {
            $.each(data,
                function (index, pages) {
                    $('<li><a href="/Home/ViewSearchedStaticPage/' + pages.Id + '">' + pages.Name + '</a></li>').appendTo($('#staticList'));
                });
        });
};

function GetTags() {
    $.getJSON(uri3)
        .done(function(data) {
            $.each(data,
                function(index, tags) {
                    $('<li>' + tags.Name + '</li>').appendTo($('#poptaglist'));
                });
        });
}