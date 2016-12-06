var uri = '/api/toonofday';

$(document)
    .ready(function() {
        GetToon();
    });

function GetToon() {
    $.getJSON(uri)
        .done(function(data) {
            $('<img id="toondayimg" src="' + data.ImgUrl + '">' +
              '<h3 id="toondayshowtitle">' + data.ShowName + '</h3>' +
              '<h3 id="toonepisode">Season: ' + data.Season +
              ' Episode: ' + data.Episode + '</h3>').appendTo($('#toonday'));
        });
};

 
    
