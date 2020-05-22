$(document).ready(function () {
    var groupSelected = document.querySelector("#group");
    NomSelect(groupSelected);
});
function NomSelect(obj) {
    // получаем выбранный id
    var id = obj.value;
    Change(id);
};     
function Change(id) {
        $.ajax({
            type: 'GET',
            url: '/GetNomencl/' + id,
            success: function (data) {

                // заменяем содержимое присланным частичным представлением
                $('#nomencl').replaceWith(data);
            }
        });
}
