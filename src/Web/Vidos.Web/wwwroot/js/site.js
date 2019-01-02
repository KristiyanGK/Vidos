// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.getElementById("toTopBtn").style.display = "block";
    } else {
        document.getElementById("toTopBtn").style.display = "none";
    }
}

function openNav() {
    document.getElementById("AllProductsSideNav").style.width = "250px";
    document.getElementById("mainWithSideNav").style.marginLeft = "250px";
}

function closeNav() {
    document.getElementById("AllProductsSideNav").style.width = "0";
    document.getElementById("mainWithSideNav").style.marginLeft = "0";
}

function scrollToTheTop() {
    $("html, body").animate({ scrollTop: 0 }, "fast");
}

$(document).ready(function () {
    $("#toTopBtn").click(function() {
        scrollToTheTop();
    });

    $("#AllOrdersAdminTable").DataTable({
        "aoColumnDefs": [
            {
                "bSortable": false, "aTargets": [7]
            }
        ],
        "language": {
            "lengthMenu": "Покажи _MENU_ записки на страницата",
            "zeroRecords": "Няма намерени записки",
            "info": "показване на страница _PAGE_ от _PAGES_",
            "infoEmpty": "Няма записки",
            "infoFiltered": "(филтрирани от _MAX_ записки)",
            "search": "Потърси",
            "processing": "изпълняване...",
            "paginate": {
                "first": "Първи",
                "last": "Последен",
                "next": "Следващ",
                "previous": "Предишен"
            }
        }
    });
});
