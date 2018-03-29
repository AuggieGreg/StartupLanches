$(function () {
    $("#table-lanches tbody tr").click(function () {
        $("#content-montagem").load(Url.TableMontagemLanche, { idLanche: $(this).attr("data-id") }, function (res) {
            if (res) {

            }
        });
    });
}); 