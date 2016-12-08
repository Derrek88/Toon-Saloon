$(document)
    .ready(function() {
        $('#addPost')
            .validate({
                rules: {
                    Body: {
                        required: true
                    },
                    Headline: {
                        required: true
                    },
                    Subtitle: {
                        required: true
                    },
                    AuthorName: {
                        required: true
                    },
                    Category: {
                        required: true
                    },
                    TagPlaceHolder: {
                        required: true
                    },
                    messages: {
                        Body: {
                            required: "Post need a body!"
                        },
                        Headline: {
                            required: "Please add a headline!"
                        },
                        Subtitle: {
                            required: "Add a subtitle please!"
                        },
                        AuthorName: {
                            required: "Who is posting this?"
                        },
                        Category: {
                            required: "Please choose one!"
                        },
                        TagPlaceHolder: {
                            required: "Please tag this post!"
                        }
                    }
                }
            });
    });