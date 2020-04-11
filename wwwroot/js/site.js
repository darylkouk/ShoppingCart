// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#input').prop('disabled', true);
    $('#plus').click(function () {
        $('#input').val(parseInt($('#input').val()) + 1);
    });
    $('#minus').click(function () {
        $('#input').val(parseInt($('#input').val()) - 1);
        if ($('#input').val() == 0) {
            $('#input').val(1); 
        }
    });
});
//$.ajax(
            //        {
            //            type: "POST", //HTTP POST Method
            //            url: "Home/Cart", // Controller/View
            //            data: { //Passing data
            //                Count1: $('#input').val(parseInt($('#input').val()) + 1),
            //                Count2: $('#input').val(parseInt($('#input').val()) - 1);//Reading text box values using Jquery
            //                //City: $("#txtAddress").val(),
            //                //Address: $("#txtcity").val()
            //            }
            //        });


                
                
                
                
                
                
