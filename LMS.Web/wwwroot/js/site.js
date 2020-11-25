// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.querySelectorAll(".CourseName").forEach(item => {
    item.addEventListener("click", function () {
        document.querySelector("#CourseDocumentButton").style.display = "block";
    })
});

document.querySelector("#CourseDocumentButton").addEventListener("click", function () {
    document.querySelector("documentLink").cklick();
});

/*document.querySelectorAll(".CourseName").forEach(item => {
    item.addEventListener("click", function () {
        document.querySelector("#CourseDocumentButton").style.display = "block";

    })*/

    