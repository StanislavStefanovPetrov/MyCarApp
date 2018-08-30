function inportDataToDropDownList(elementFromId, elementToId,urlRoute, antifogeryToken) {
    var makeId = $("#" + elementFromId+" option:selected").val();
    $.ajax({
        async: true,
        url: urlRoute,
        method: "GET",
        data: { makeId: makeId },
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