var brandNamesUrl = "/Shopping/Brand/AllNames";

$(document).ready(function () {
    var div = $("#BrandsLinks");

    $.getJSON(brandNamesUrl, function (result) {
        $.each(result, function (key, entry) {
            div.append($("<a></a>")
                .addClass("list-group-item text-center")
                .attr("href", "/Shopping/Brand/Details?name=" + entry)
                .text(entry));
        });
    });
});
