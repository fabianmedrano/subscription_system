
$(function () {

    $('input[data-mask]').each(function () {
        var mask = $(this).data('mask');

        if (mask === 'currency') {
            $(this).inputmask({
                alias: "currency",
                prefix: $(this).data('prefix'),
                groupSeparator: $(this).data('group-separator'),
                autoGroup: true,
                digits: $(this).data('digits'),
                digitsOptional: false,
                placeholder: "0.00",
                rightAlign: $(this).data('right-align')
            });
        } else if (mask === 'months') {
            console.log("here")
            $(this).inputmask({
                alias: "numeric",
                min: 1,
                max: 12,
                placeholder: "MM",
                digits: 0,
                rightAlign: false,
                autoGroup: true,
                allowMinus: false,
                allowPlus: false,
            });
        }else {
            $(this).inputmask(mask); // Aplica cualquier otra máscara
        }
    });

});
