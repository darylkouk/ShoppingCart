// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#addCommentBtn").click(function () {
    if ($(".ratingBtn").val() == null) {
        alert("Rating is required");
        return;
    }
    else {
        $("#reviewForm").submit();
    }
});