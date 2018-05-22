/// <reference path="../_references.js" />
$.validator.setDefaults({
    errorPlacement: function (error, element) {
        var errorText = $(error).text();
        layer.tips(errorText, element, { tipsMore: true, tips: [2, '#78BA32'] });
    },
    highlight: function (element, errorClass, validClass) {
        $(element).css('border-color', '#f00');
    },
    unhighlight: function (element, errorClass) {
        $(element).css('border-color', '');
    },
    ignore: '.valid-ignore',
});