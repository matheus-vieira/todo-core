// Write your JavaScript code.
$(document).ready(function () {
    $('#add-item-button')
        .on('click', x());
});

function addItem() {
    $('#add-item-error').hide();
    var newTitle = $('#add-item-title').val();
    var newDueAt = $('#add-item-due-at').val();
    $.post(
        '/ToDo/AddItem',
        { title: newTitle, dueAt: newDueAt },
        () => window.location = '/ToDo'
    ).fail(function (data) {
        var error = data.statusText;
        if (data.responseJSON) {
            var key = Object.keys(data.responseJSON)[0];
            error = data.responseJSON[key];
        }
        $('#add-item-error').text(error);
        $('#add-item-error').show();
    });
}

function x() {
    var $itemErro = $('#add-item-error');
    var $newTitle = $('#add-item-title');
    var $newDueAt = $('#add-item-due-at');
    return function () {
        $itemErro.hide();
        $.post(
            '/ToDo/AddItem',
            {
                title: $newTitle.val(),
                dueAt: $newDueAt.val()
            },
            () => window.location = '/ToDo'
        ).fail(function (data) {
            var error = data.statusText;
            if (data.responseJSON) {
                var key = Object.keys(data.responseJSON)[0];
                error = data.responseJSON[key];
            }
            $itemError.text(error).show();
        });
    }
}