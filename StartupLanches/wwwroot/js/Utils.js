$(function () {
    $(document).ajaxStart(function () {
        $("body").append("<div class='carregando'>Carregando</div>");
    });

    $(document).ajaxStop(function () {
        $("div.carregando:first").remove();
    });

    $(document).ajaxComplete(function () {
        blockRequest = false;
    });

    $(document).ajaxError(function (e, xhr) {
        $("div.carregando:first").remove();
        try {
            eval("var ex = " + xhr.responseText + ";");
            alert(ex.Message + " - " + ex.Type);
        }
        catch (err) {
            console.log(err.message);
            console.log(xhr);
        }
    });
});