var uri = '/api/toonofday';

$(document)
    .ready(function() {
        GetToon();
    });

function GetToon() {
    $.getJSON(uri);
};


// data contains a list of contacts
               // we need to iterate each one and add it to the table
 
    
