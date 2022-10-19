var utilJson = {};

utilJson.parseObj = function (any) {

    if (typeof any == 'string')
        return JSON.parse(any);

    return any;

}

$(function () {

    $("body").delegate(".btn", "click", function (event) {

        //event.preventDefault();

        var myarr = [
            "processar",
            "importar",
            "salvar",
            "confirmar"
        ];

        var label = $(this).html().toLowerCase().trim();
        var exists = false;

        $.each(myarr, function (index, value) {
            if (label.indexOf(value) > -1)
                exists = true;
        });

        if (!$(this).hasAttr("disabled") && exists == true) {

            $(this).attr("disabled", "disabled");
            setTimeout(() => {
                $(this).removeAttr("disabled");
            }, 1500);

        }
    });

    $("body").delegate(".btn", "dblclick", function (event) {
        event.preventDefault();
    });
})

$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};