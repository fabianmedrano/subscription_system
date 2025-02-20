
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
                alias: "currency",
                prefix: $(this).data('prefix'),
                groupSeparator: $(this).data('group-separator'),
                autoGroup: true,
                digits: $(this).data('digits'),
                digitsOptional: false,
                placeholder: "0.00",
                rightAlign: $(this).data('right-align')
            });
        }
        else {
            $(this).inputmask(mask); // Aplica cualquier otra máscara
        }
    });

});
