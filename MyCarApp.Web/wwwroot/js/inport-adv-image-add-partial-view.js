function inportImageAddPartialView(advId, elementToId, urlRoute, antifogeryToken) {
    $.ajax({
        async: true,
        url: urlRoute,
        method: "GET",
        data: { Id: advId, handler:"AddNewImage"},
        headers: { "RequestVerificationToken": antifogeryToken },
        dataType: "html",
        success: function (response) {
            $("#" + elementToId).html(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}