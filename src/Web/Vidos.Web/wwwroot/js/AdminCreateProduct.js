var brandNamesUrl = "/Shopping/Brand/AllNames";

$(document).ready(function () {
    var dropDown = $("#BrandsSelector");

    $.getJSON(brandNamesUrl, function (result) {
        $.each(result, function (key, entry) {
            dropDown.append($("<option></option>").attr("value", entry).text(entry));
        });
    });
});
