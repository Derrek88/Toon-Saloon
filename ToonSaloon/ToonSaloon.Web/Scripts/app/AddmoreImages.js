$(document).ready(function () {
    $("#images2").hide();
    $("#images3").hide();


    $("#addImage2")
        .on("click",
            function () {
                $("#images2").show();
            });

    $("#addImage3")
        .on("click",
            function () {
                $("#images3").show();
            });


    $("#removeImage2")
        .on("click",
            function () {
                $("#images2").hide();
                $("#image2Title input").val('');
                $("#image2Description input").val('');
                $("#image2Source input").val('');
            });

    $("#removeImage3")
        .on("click",
            function () {
                $("#images3").hide();
                $("#image3Title input").val('');
                $("#image3Description input").val('');
                $("#image3Source input").val('');

            });

});