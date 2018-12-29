﻿// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.getElementById("toTopBtn").style.display = "block";
    } else {
        document.getElementById("toTopBtn").style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
}

function openNav() {
    document.getElementById("AllProductsSideNav").style.width = "250px";
    document.getElementById("mainWithSideNav").style.marginLeft = "250px";
}

function closeNav() {
    document.getElementById("AllProductsSideNav").style.width = "0";
    document.getElementById("mainWithSideNav").style.marginLeft = "0";
}

$(document).ready(function () {
    var dropDown = $("#BrandsSelector");

    $.getJSON("/Shopping/Brand/All", function (result) {
        $.each(result, function (key, entry) {
            dropDown.append($("<option></option>").attr("value", entry).text(entry));
        });
    });

    $("#btnFilter").click(function() {

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
