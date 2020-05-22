$(document).ready(function () {
    var nomSelected = document.querySelector("#NomSelector");
    loadPartial();
    GetPrice(nomSelected);
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
//Удаление.
function DeleteGood(id) {
    $.ajax({
        type: 'GET',
        url: '/Shipdocs/Delete/' + id,
        success: function (result) {
            if (result != "OK") alert("Ошибка");
            // заменяем содержимое присланным частичным представлением.
            loadPartial();
        }
    });
}
//Работа с событием добавления товара.
function AddForm() {
    var item = $("#GoodAddForm").serialize();
    var quantityPlace = document.querySelector("#QuantityPlace");
    $.ajax({
        type: 'POST',
        url: '/Shipdocs/Create/',
        data: item,
        success: function (result) {
            // Уведомление об ошибке.
            //if (result != "OK") alert("Ошибка");
            // обновляем представление.
            loadPartial();
            quantityPlace.value = "";
        }
    });
}
//Получение цены.
function GetPrice(obj) {
    var nomid = obj.value;
    var shipmentId = document.querySelector("#ShipmentId").innerHTML;
    var pricePlace = document.querySelector("#PricePlace");
    var quantityPlace = document.querySelector("#QuantityPlace");
    var data = {
        shipmentid: shipmentId,
        nomid: nomid
    }
    $.ajax({
        type: 'Get',
        url: '/Shipdocs/GetPrice',
        data: data,
        success: function (result) {
            // устанавливаем цену.
            pricePlace.value = result;
            quantityPlace.value = "";
        }
    });
}
function SendForm() {
    loadPartial();
}

