$(function () {
    $('#supplyer').change(function () {
        // получаем выбранный id
        var id = $(this).val();
        $.ajax({
            type: 'GET',
            url: '@Url.Action("GetNomencl")/' + id,
            success: function (data) {

                // заменяем содержимое присланным частичным представлением
                $('#nomencl').replaceWith(data);
            }
        });
    });
})