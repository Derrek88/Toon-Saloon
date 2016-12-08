$(document)
    .ready(function() {
        $('#addToon')
            .validate({
                rules: {
                    Author: {
                        required: true
                    },
                    ImgUrl: {
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
                        ImgUrl: {
                            required: "Please add a picture!"
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