$(document).ready(function () {
    var nomSelected = document.querySelector("#NomSelector");
    loadPartial();
});

//Загрузка товаров в приходный документ.
function loadPartial() {
    var shipmentId = document.querySelector("#ShipmentId").innerHTML;
    $.ajax({
        type: 'GET',
        url: '/Shipdocs/GetGoods/' + shipmentId,
        success: function (data) {

            // заменяем содержимое присланным частичным представлением.
            $("#ListOfGoods").replaceWith(data);
        }
    });
}



