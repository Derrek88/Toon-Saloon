var uri = '../../api/';

$(document)
    .ready(function() {
        GetToon();
    });

function GetToon() {
    $.getJSON(uri)
           .done(function (data) {
               
               // data contains a list of contacts
               // we need to iterate each one and add it to the table
            
           });
};
    
