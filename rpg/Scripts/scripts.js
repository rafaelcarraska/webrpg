$(function () { var attr1 = new SpinBox('attr1', { 'minimum': 0, 'maximum': 20 }); });
$(function () { var attr2 = new SpinBox('attr2', { 'minimum': 0, 'maximum': 20 }); });
$(function () { var attr3 = new SpinBox('attr3', { 'minimum': 0, 'maximum': 20 }); });
$(function () { var attr4 = new SpinBox('attr4', { 'minimum': 0, 'maximum': 20 }); });
$(function () { var attr5 = new SpinBox('attr5', { 'minimum': 0, 'maximum': 20 }); });
$(function () {
    var tbl = $("#vantagens");

    $("#addVant").click(function () {
        $("<tr><td>vantagem lala</td><td>desvantagem qualquer</td></tr>").appendTo(tbl);
    });

    $("#remVant").click(function () {
        //$("<tr><td>vantagem lala</td><td>desvantagem qualquer</td></tr>").remove();
        $("<tr>").remove(":contains('vantagem lala')");
    });

});
