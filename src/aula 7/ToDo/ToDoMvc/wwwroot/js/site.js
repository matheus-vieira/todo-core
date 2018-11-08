// Write your JavaScript code.
$(document).ready(function () {
    $('#add-item-button').on('click', addItem());
    $('.done').on('click', markDone);

    /**
     * IIFE - Imediately Invoked Function Expression
    *  Ou seja, função executada imediatamente
                                 "automagicamente"
     */
    var postError = (function () {
        var $itemError = $('#add-item-error');
        function erroOnPost(data) {
            var error = data.statusText;
            if (data.responseJSON) {
                var key = Object
                    .keys(data.responseJSON)[0];
                error = data.responseJSON[key];
            }
            $itemError.text(error).show();
        }
        return {
            hide: () => $itemError.hide(),
            onError: erroOnPost
        };
    })();

    function addItem() {
        var $newTitle = $('#add-item-title');
        var $newDueAt = $('#add-item-due-at');
        return function () {
            postError.hide();
            $.post('/ToDo/AddItem',
                {
                    title: $newTitle.val(),
                    dueAt: $newDueAt.val()
                },
                () => window.location = '/ToDo'
            ).fail(postError.onError);
        };
    }
    function markDone(ev) {
        // ev.target === button
        ev.target.disabled = true;
        postError.hide();
        $.post('/ToDo/MarkDone',
            { id: ev.target.name },
            function () {
                var row = ev.target
                    .parentElement  // td
                    .parentElement; // tr
                row.classList.add('done');
            }
        ).fail(postError.onError);
    }
});

/**
 * Passos para Editar um item:
 *
 * Adicionar um botão de editar
 *
 * (
 *   Alterar a interface para remoção do checkbox
 *   Adição de uma coluna Actions com botões
 *   para Editar (Edit) e Marcar como pronto (Done)
 * )
 *
 * Ajustar a funcionalidade Marcar como Pronto:
 *   Remover o checkbox da primeira coluna
 *   Adicionar um botão com o texto "Done"
 *   Adicionar no botão criado a classe "done"
 *   Alterar no javascript para adicionar o evento de click
 *     na classe "done", não mais na classe "done-checkbox"
 *   Testar as alterações
 *
 * Criar a funcionalidade Editar:
 *   Adicionar um botão com o texto "Edit"
 *   Adicionar no botão criado a classe "edit"
 *   Adicionar no javascript o evento de click
 *     na classe "edit"
 *   Adicionar no javascript uma função para edição do item
 *     Função será chamada "edit"
 *       A função recebe como parametro um objeto com as informações do evento
 *       Requisição para:
 *          url: "/ToDo/GetItem"
 *         Objeto: { id: ev.target.name }.
 *         Sucesso:
 *            A função recebe como parametro um objeto com as informações do item
 *              Item: { id: guid, title: string, dueAt: datetime }
 *            Mudar o valor do campo com o id: "edit-item-title" com a propriedade "title"
 *            Mudar o valor do campo com o id: "edit-item-due-at" com a propriedade "dueAt"
 *            Mudar o valor do campo (hidden) com o id: "edit-item-id" com a propriedade "id" 
 *            Mostrar um formulario dentro de uma modal
 *              https://getbootstrap.com/docs/3.3/javascript/#modals
 *         Caso ocorra algum erro na requisição chamar .fail(postError);
 *   Adicionar no javascript uma função para cancelar a edição 
 *     Adicionar um evento no botão cancelar da modal 
 *       Fechar a modal.
 *   
 *
 *  Definir layout
 *  Implementações no lado do servidor (repense a implementação atual)
 *
 */