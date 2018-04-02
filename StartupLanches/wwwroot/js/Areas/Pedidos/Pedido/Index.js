var calcularIntervalID = null;

$(function () {
    $("#table-cardapio-lanches tbody tr").click(function () {
        var tr = $(this);
        tr.siblings().removeClass("active");
        tr.addClass("active");
        carregar_tbody_montagem_lanche(Url.TableMontagemLanche, { idLanche: $(this).attr("data-id") }, function () {
            $("#content-montagem #table-montagem tfoot #NomeLanche").val(tr.find("#NomeLanche").val());
            calcular_total(0);
            $("#table-montagem").fadeIn('fast');
        });
    });

    $("#selectIngrediente").change(function () {
        if (!$("#txtQtdIngrediente").val()) {
            $("#txtQtdIngrediente").val("1");
        }
    });

    $("#btnAdicionarIngrediente").click(function () {
        if ($("#selectIngrediente").val() && $("#txtQtdIngrediente").val()) {
            $.get(Url.TrMontagemLanche, { idIngrediente: $("#selectIngrediente").val(), quantidade: $("#txtQtdIngrediente").val() }, function (res) {
                if (res) {
                    var tr = $(res);
                    $("#content-montagem #tboMontagem").prepend(tr);
                    bind_tr_montagem_events(tr);
                    $("#txtQtdIngrediente").val("");
                    $("#selectIngrediente").val("");
                    calcular_total(0);
                }
            });
        }
    });

    $("#txtQtdLanche").change(function () {
        calcular_total(0);
    });

    $("#btAdicionarLanche").click(function () {
        if ($("#tboMontagem tr").size() > 0) {
            preparar_grid_montagem_lanche();
            $.get(Url.AdicionarLanchePedido, $("#table-montagem :input:not(button)").serialize(), function (res) {
                if (res) {
                    var tr = $(res);
                    $("#content-pedido #tboPedido").prepend(tr);
                    bind_tr_pedido_events(tr); $("#content-montagem #tboMontagem").empty();
                    $("#txtQtdLanche").val(1);
                    calcular_total(0);
                    calcular_pedido();

                    $("#table-montagem").fadeOut('fast');
                }
            });
        }
    });

    $("#btAbrirPedido").click(function () {
        $(".finalizarPedido").hide();
    });

    $("#btConfirmarPedido").click(function () {
        $(".finalizarPedido").show();
    });

    $("#btLimparPedido").click(function () {
        StartupLanches.Utils.Confirm("Confirmar limpeza de Pedido", "Tem certeza que deseja remover todos os itens do Pedido?", null, function () {
            $("#tboPedido").empty();
            calcular_pedido();
        });
    });

    $("#formPedido").submit(function (e) {
        if ($(this).find("#tboPedido tr[valor]").size() > 0) {
            preparar_grid_pedido_lanche();
            $.post(Url.ConfirmarPedido, $("#formPedido").serialize(), function (res) {
                if (res && res.sucesso === true) {
                    StartupLanches.Utils.Ok("Pedido cadastrado!", "O número do pedido é " + res.numeroPedido + ".", function () {
                        location.reload();
                    });
                }
            });
        }
        else {
            StartupLanches.Utils.Ok("Nenhum lanche no pedido", "Para concluir o pedido é necessário ao menos um lanche.");
        }
        e.preventDefault();
        return false;
    });

});

var bind_tr_montagem_events = function (tr) {
    $(tr).find(".btnRemover").click(function () {
        $(this).parents("tr").fadeOut('fast', function () { $(this).remove(); calcular_total(0); });
    });
    $(tr).find("#Quantidade").change(function () {
        calcular_total();
    });
    $(tr).find("div.input-group").MinusPlusInputify();
}

var bind_tr_pedido_events = function (tr) {
    $(tr).find(".btnRemover").click(function () {
        $(this).parents("tr").fadeOut('fast', function () { $(this).remove(); });
    });
}

var preparar_grid_montagem_lanche = function () {
    $("#tboMontagem tr").each(function (i) {
        var tr = $(this);
        tr.find(":input:not(button)").each(function () {
            $(this).attr("name", "Ingredientes[" + i + "]." + $(this).attr("id").replace(/_/g, '.'));
        });
    });
}

var preparar_grid_pedido_lanche = function () {
    $("#tboPedido tr").each(function (i) {
        var tr = $(this);
        tr.find(":input:not(button)").each(function () {
            $(this).attr("name", $(this).attr("id").replace("_", '[' + i + '].').replace("_", "[").replace("_", "]").replace(/_/g, "."));
        });
    });
}

var carregar_tbody_montagem_lanche = function (url, postObject, callback) {
    $("#content-montagem #tboMontagem").load(url, postObject, function (res) {
        if (res) {
            bind_tr_montagem_events($("#content-montagem #tboMontagem tr"));
            if (callback)
                callback(res);
        }
    });
}

var carregar_tbody_pedido_lanche = function (url, postObject, callback) {
    $("#content-montagem #tboMontagem").load(url, postObject, function (res) {
        if (res) {
            bind_tr_pedido_events($("#content-montagem #tboPedido tr"));
            if (callback)
                callback(res);
        }
    });
}

var calcular_total = function (timeout) {
    if (timeout == undefined)
        timeout = 800;
    if (calcularIntervalID) {
        clearTimeout(calcularIntervalID);
    }
    if ($("#content-montagem #tboMontagem").find("tr[valor]").size() > 0) {
        calcularIntervalID = setTimeout(function () {
            calcularIntervalID = null;
            preparar_grid_montagem_lanche();
            carregar_tbody_montagem_lanche(Url.CalcularPromocoes, $("#tboMontagem :input:not(button)").serialize(), function () {
                var total = 0;
                $("#content-montagem #tboMontagem").find("tr[valor]").each(function () {
                    total += parseFloat($(this).attr("valor").replace(",", "."));
                });

                var quantidadeLanches = parseInt($("#txtQtdLanche").val());
                quantidadeLanches = quantidadeLanches == undefined || !quantidadeLanches ? 1 : quantidadeLanches;
                total = total * quantidadeLanches;

                $("#th-total-lanche").html("R$ " + total.toFixed(2).toString().replace(".", ","));
            });
        }, timeout);
    }
    else {
        $("#th-total-lanche").html("R$ " + 0.00.toFixed(2).toString().replace(".", ","));
    }
}

var calcular_pedido = function () {
    var total = 0;
    $("#content-pedido #tboPedido").find("tr[valor]").each(function () {
        total += parseFloat($(this).attr("valor").replace(",", "."));
    });

    $("#th-total-pedido").html("R$ " + total.toFixed(2).toString().replace(".", ","));
}