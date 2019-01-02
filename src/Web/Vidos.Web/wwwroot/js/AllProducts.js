var priceDefault = 0;
var brandDefault = "Всички";
var productsCountDefault = 9; 

var url = "/Shopping/Products/AllPartial";
var brand = brandDefault;
var price = priceDefault;
var productsCount = productsCountDefault;

function hidePage() {
    document.getElementById("loader").style.display = "block";
    document.getElementById("AllProductsBody").style.display = "none";
}

function showPage() {
    document.getElementById("loader").style.display = "none";
    document.getElementById("AllProductsBody").style.display = "block";
}

function fetchProducts(page) {
    $.get(
        url,
        {
            pageNumber: page,
            brandName: brand,
            priceSort: price,
            productsOnPage: productsCount
        },
        function (result) {
            $("#AllProductsBody").html(result);
        });

    scrollToTheTop();
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

$(document).ready(function () {
    var dropDown = $("#BrandsSelector");

    $.getJSON("/api/Brand", function (result) {
        $.each(result, function (key, entry) {
            dropDown.append($("<option></option>").attr("value", entry).text(entry));
        });
    });

    var initialPage = 1;

    var pageNumberParam = getParameterByName("pageNumber");
    var brandNameParam = getParameterByName("brandName");
    var priceSortParam = getParameterByName("priceSort");
    var productsOnPageParam = getParameterByName("productsOnPage");

    if (pageNumberParam != null) {
        initialPage = pageNumberParam;
    }

    if (brandNameParam != null) {
        brand = brandNameParam;
        $("#BrandsSelector").val(brand);
    }

    if (priceSortParam != null) {
        price = priceSortParam;
        $("#PriceSelector").val(price);
    }

    if (productsOnPageParam != null) {
        productsCount = productsOnPageParam;
        $("#ItemsOnPageSelector").val(productsCount);
    }

    fetchProducts(initialPage);
});

$("#btnClear").on("click", function () {
    brand = brandDefault;
    price = priceDefault;
    productsCount = productsCountDefault;

    $("#PriceSelector").val(price);
    $("#BrandsSelector").val(brand);
    $("#ItemsOnPageSelector").val(productsCount);

    fetchProducts(1);
});

$(document).on({
    ajaxStart: function () { hidePage() },
    ajaxStop: function () { showPage() }
});

$(document).on("click", ".page-link", function () {
    $.each($(this), function (key, value) {
        var nextPage = $(value).val();
        fetchProducts(nextPage);
    });
});

$("#BrandsSelector").change(function () {
    brand = $("#BrandsSelector option:selected").val();
    fetchProducts(1);
});

$("#ItemsOnPageSelector").change(function () {
    productsCount = $("#ItemsOnPageSelector option:selected").val();
    fetchProducts(1);
});

$("#PriceSelector").change(function () {
    price = $("#PriceSelector option:selected").val();
    fetchProducts(1);
});