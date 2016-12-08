$(document)
    .ready(function () {
        $('#editToon')
            .validate({
                rules: {
                    Author: {
                        required: true
                    },
                    ShowName: {
                        required: true
                    },
                    Season: {
                        required: true
                    },
                    Episode: {
                        required: true
                    },
                    messages: {
                        Author: {
                            required: "Who is posting this?"
                        },
                        ShowName: {
                            required: "What show is it?"
                        },
                        Season: {
                            required: "What season is it?"
                        },
                        Episode: {
                            required: "What episode is it?"
                        }
                    }
                }
            });
    });