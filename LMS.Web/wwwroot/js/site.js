// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.





document.querySelector(this.#courseNamelist).addEventListener("select", assighn());


function assighn() {
    var name = document.querySelector(this.#courseNamelist).value;
    var result = (document.querySelector(this.#dictionary).value)[name];
    var demo = document.querySelector("courseId").setAttribute("value", result);
    document.querySelector(this.#demo).innerHTML = demo;
}









