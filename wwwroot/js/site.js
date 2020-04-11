// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#addCommentBtn").click(function () {
    if (!$("input[name='rating']:checked").val()) {
        alert("Rating is required");
        return;
    }
    else
    {
        $("#reviewForm").submit();
    }
});