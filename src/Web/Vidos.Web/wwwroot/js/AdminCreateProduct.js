$(document).ready(function() {
    var dropDown = $("#BrandsSelector");

    $.getJSON("/api/Brand", function (result) {
        $.each(result, function (key, entry) {
            dropDown.append($("<option></option>").attr("value", entry).text(entry));
        });
    });
});
