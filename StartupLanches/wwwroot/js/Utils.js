$(function () {
    $(document).ajaxStart(function () {
        $("body").append("<div class='carregando'></div>");
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
            //eval("var ex = " + xhr.responseText + ";");
            var ex = JSON.parse(xhr.responseText);
            alert(ex.message + " - " + ex.type);
        }
        catch (err) {
            console.log(err.message);
            console.log(xhr);
        }
    });
    //plugin bootstrap minus and plus
    //http://jsfiddle.net/laelitenetwork/puJ6G/

    $("div.input-group").MinusPlusInputify();
});

var StartupLanches =
    {
        Utils:
            {
                Confirm: function (titulo, mensagem, callback_nao, callback_sim) {
                    var modal = $("#modal-message");
                    modal.find(".modal-footer button").hide();
                    modal.modal("show");

                    modal.find(".modal-title").html(titulo);
                    modal.find("#modal-body-mensagem p").html(mensagem);

                    modal.find("#btNao").click(function () {
                        if (callback_nao)
                            callback_nao();
                        modal.modal("hide");
                    }).show();

                    modal.find("#btSim").click(function () {
                        if (callback_sim)
                            callback_sim();
                        modal.modal("hide");
                    }).show();

                    modal.find(".modal-footer button:first()").focus();
                },

                Ok: function (titulo, mensagem, callback_ok) {
                    var modal = $("#modal-message");
                    modal.find(".modal-footer button").hide();
                    modal.modal("show");

                    modal.find(".modal-title").html(titulo);
                    modal.find("#modal-body-mensagem p").html(mensagem);
                    
                    modal.on('hidden.bs.modal', function () {
                        if (callback_ok)
                            callback_ok();
                    });

                    modal.find(".modal-footer button:first()").focus();
                }
            }
    };

jQuery.fn.extend({
    MinusPlusInputify: function () {
        return this.each(function () {
            var context = $(this);
            $(this).find('.btn-number').click(function (e) {
                e.preventDefault();

                fieldName = $(this).attr('data-field');
                type = $(this).attr('data-type');
                var input = $(context).find("input[name='" + fieldName + "']");
                var currentVal = parseInt(input.val());
                if (!isNaN(currentVal)) {
                    if (type == 'minus') {

                        if (currentVal > input.attr('min')) {
                            input.val(currentVal - 1).change();
                        }
                        if (parseInt(input.val()) == input.attr('min')) {
                            $(this).attr('disabled', true);
                        }

                    } else if (type == 'plus') {

                        if (currentVal < input.attr('max')) {
                            input.val(currentVal + 1).change();
                        }
                        if (parseInt(input.val()) == input.attr('max')) {
                            $(this).attr('disabled', true);
                        }

                    }
                } else {
                    input.val(0);
                }
            });

            $(this).find('.input-number').focusin(function () {
                $(this).data('oldValue', $(this).val());
            });
            $(this).find('.input-number').change(function () {

                minValue = parseInt($(this).attr('min'));
                maxValue = parseInt($(this).attr('max'));
                valueCurrent = parseInt($(this).val());

                name = $(this).attr('name');
                if (valueCurrent >= minValue) {
                    $(".btn-number[data-type='minus'][data-field='" + name + "']").removeAttr('disabled')
                } else {
                    $(this).val($(this).data('oldValue'));
                }
                if (valueCurrent <= maxValue) {
                    $(".btn-number[data-type='plus'][data-field='" + name + "']").removeAttr('disabled')
                } else {
                    $(this).val($(this).data('oldValue'));
                }


            });
            $(this).find(".input-number").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
                    // Allow: Ctrl+A
                    (e.keyCode == 65 && e.ctrlKey === true) ||
                    // Allow: home, end, left, right
                    (e.keyCode >= 35 && e.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
        });
    }
});

var preparar_inputs = function (inputGroups) {
    $(inputGroups).each(function () {
        $(this).find('.btn-number').click(function (e) {
            e.preventDefault();

            fieldName = $(this).attr('data-field');
            type = $(this).attr('data-type');
            var input = $("input[name='" + fieldName + "']");
            var currentVal = parseInt(input.val());
            if (!isNaN(currentVal)) {
                if (type == 'minus') {

                    if (currentVal > input.attr('min')) {
                        input.val(currentVal - 1).change();
                    }
                    if (parseInt(input.val()) == input.attr('min')) {
                        $(this).attr('disabled', true);
                    }

                } else if (type == 'plus') {

                    if (currentVal < input.attr('max')) {
                        input.val(currentVal + 1).change();
                    }
                    if (parseInt(input.val()) == input.attr('max')) {
                        $(this).attr('disabled', true);
                    }

                }
            } else {
                input.val(0);
            }
        });

        $(this).find('.input-number').focusin(function () {
            $(this).data('oldValue', $(this).val());
        });
        $(this).find('.input-number').change(function () {

            minValue = parseInt($(this).attr('min'));
            maxValue = parseInt($(this).attr('max'));
            valueCurrent = parseInt($(this).val());

            name = $(this).attr('name');
            if (valueCurrent >= minValue) {
                $(".btn-number[data-type='minus'][data-field='" + name + "']").removeAttr('disabled')
            } else {
                alert('Sorry, the minimum value was reached');
                $(this).val($(this).data('oldValue'));
            }
            if (valueCurrent <= maxValue) {
                $(".btn-number[data-type='plus'][data-field='" + name + "']").removeAttr('disabled')
            } else {
                alert('Sorry, the maximum value was reached');
                $(this).val($(this).data('oldValue'));
            }


        });
        $(this).find(".input-number").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
                // Allow: Ctrl+A
                (e.keyCode == 65 && e.ctrlKey === true) ||
                // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });
    });
}