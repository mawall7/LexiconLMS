// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#checkbox').click(function () {
        $('form').submit();
    })
})

function removeFormModule() {
    document.querySelector('#formModule').remove();
}
function removeFormActivity() {
    document.querySelector('#formActivity').remove();
}

function remoteForm() {
    $('#createForm').remove();
}


$(document).ready(function () {
    $(".course-section").mouseover(function () {
        $(".course-btn-ed").show();
    });
});
$(document).ready(function () {
    $(".course-section").mouseout(function () {
        $(".course-btn-ed").hide();
    });
});

