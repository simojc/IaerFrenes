/// <reference path="jquery-ui-1.12.1.js" />
/// <reference path="jquery.validate-vsdoc.js" />

$(document).ready(function () {

    $(":input[data-autocomplete]").each(function () {
        $(this).autocomplete({ source: $(this).attr("data-autocomplete") });
    });

    $(":input[data-datepicker]").datepicker();


})