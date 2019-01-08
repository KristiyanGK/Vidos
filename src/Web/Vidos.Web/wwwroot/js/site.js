// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.getElementById("toTopBtn").style.display = "block";
    } else {
        document.getElementById("toTopBtn").style.display = "none";
    }
}

function scrollToTheTop() {
    $("html, body").animate({ scrollTop: 0 }, "fast");
}

$(document).ready(function () {
    $("#toTopBtn").click(function() {
        scrollToTheTop();
    });
});
