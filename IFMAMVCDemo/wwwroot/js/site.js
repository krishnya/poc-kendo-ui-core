// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#sibeBarCollapseBtn").on("click", function (event) {
        event.preventDefault();
        $("#dashMainWrapper").toggleClass("sidebarCollapse");
        console.log("test")
    })
})
