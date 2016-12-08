$(document)
    .ready(function() {
        $('#addPage')
            .validate({
                rules: {
                    Name: {
                        required: true
                    },
                    Body: {
                        required: true
                    },
                    Category: {
                        required: true
                    },
                    messages: {
                        Name: {
                            required: "Who is posting this?"
                        },
                        Body: {
                            required: "What is this static page about?"
                        },
                        Category: {
                            required: "Please choose one if its something specfic! (default is none)"
                        }
                    }

                }
            });
    });
