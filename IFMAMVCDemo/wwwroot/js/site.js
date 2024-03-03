// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#sibeBarCollapseBtn").on("click", function (event) {
        event.preventDefault();
        $("#dashMainWrapper").toggleClass("sidebarCollapse");
    })
})
$(function () {
    var current = location.pathname;
    if (current === '/') {
        $('.sideBar li:first-child a').addClass('active');
    } else {
        $('.sideBar li a').each(function () {
            var $this = $(this);
            // if the current path is like this link, make it active
            if ($this.attr('href').indexOf(current) !== -1) {
                $this.addClass('active');
            }
        });
    }
    
});