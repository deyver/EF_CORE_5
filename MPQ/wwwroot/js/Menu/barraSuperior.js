$(document).ready(function () {
    ListarTodos();
    Carregar();
});

function ListarTodos() {
    $.ajax({
        type: "GET",
        url: "/Sistema/GetAllSistema",
        dataType: "json"

    }).done(function (data) {
        var source =
                {
                    datatype: "json",
                    datafields: [
                        { name: 'nome' },
                        { name: 'urlBase' },
                        { name: 'id' }
                    ],
                    localdata: data
                };

        var dataAdapter = new $.jqx.dataAdapter(source);

        $("#jqxGrid").jqxGrid({
            source: dataAdapter,
            width: '100%',
            pageable: true,
            autoheight: true,
            pagesize: 10,
            pagermode: 'simple',
            showcolumnlines: true,
            showcolumnheaderlines: true,
            rowsheight: 50,
            columns: [
                {
                    text: ' ', cellsAlign: 'center', align: 'center', columnType: 'none', editable: false, sortable: false, dataField: null, width: 50,
                    createwidget: function (row, column, value, htmlElement) {
                        var button = $(`<div data-toggle="modal" data-row='${row.boundindex}' data-target="#myModal" title="Editar" data-modal="" class="btn btn-primary botaoEditar jqxButtonAligns" data-row='${row.boundindex}'><i class="fas fa-pencil-alt" style="color: aliceblue;" data-row='${row.boundindex}'></i></div>`);
                        $(htmlElement).append(button);
                        button.jqxButton({ template: "primary", height: '100%', width: '100%' });
                        button.click(function (event) {
                            var IndexGrid = parseInt(event.target.getAttribute('data-row'));
                            if (IndexGrid != undefined && IndexGrid != null) {

                                var data = $('#jqxGrid').jqxGrid('getrowdata', IndexGrid);

                                $.ajax({
                                    type: 'GET',
                                    url: '/Sistema/Update?id=' + data.id,
                                    contentType: 'html',
                                    beforeSend: function () {
                                        $('.preloader').fadeIn();
                                    }
                                }).done(function (result) {
                                    $(' #myModal #myModalContent').html(result);
                                    $('.preloader').fadeOut(600, function () {
                                        $('#mymodal').modal('show');
                                    });
                                }).fail(function () {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'Oops...',
                                        text: 'Erro ao abrir a janela, contate o administrador.'
                                    });

                                    $('.preloader').fadeOut();
                                });
                            }
                            else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Oops...',
                                    text: 'Necessário Selecionar uma linha para editar'
                                });
                            }
                        });
                    },
                    initwidget: function (row, column, value, htmlElement) {
                    }
                },
                {
                    text: ' ', cellsAlign: 'center', align: 'center', columnType: 'none', editable: false, sortable: false, dataField: null, width: 50,
                    createwidget: function (row, column, value, htmlElement) {
                        var button = $('<div class="btn btn-danger botaoApagar jqxButtonAligns" id="botaoApagar" data-row="' + row.boundindex + '"><i class="fas fa-trash " data-row="' + row.boundindex + '"></i> </div>');
                        $(htmlElement).append(button);
                        button.jqxButton({ template: "danger", height: '100%', width: '100%' });
                        button.click(function (event) {

                            var IndexGridDelete = parseInt(event.target.getAttribute('data-row'));

                            if (IndexGridDelete != undefined && IndexGridDelete > -1) {
                                Swal.fire({
                                    title: 'Confirma a exclusão do registro?',
                                    text: "Isso não poderá ser desfeito!",
                                    icon: 'warning',
                                    showCancelButton: true,
                                    confirmButtonColor: '#ff4141',
                                    cancelButtonColor: '#929292',
                                    confirmButtonText: 'Sim, excluir!',
                                    cancelButtonText: 'Cancelar'
                                }).then((result) => {
                                    if (result.value) {
                                        Deletar(IndexGridDelete);
                                    }
                                });
                            }
                            else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Oops...',
                                    text: 'Necessário Selecionar uma linha para editar'
                                });
                            }
                        });
                    },
                    initwidget: function (row, column, value, htmlElement) {
                    }
                },
                { text: 'Id', datafield: 'id', width: 'auto', editable: false },
                { text: 'Nome', datafield: 'nome', width: 'auto' },
                { text: 'URL Base', datafield: 'urlBase', width: 'auto' }
            ],
            localization: getLocalization()

        });
    });

}

function Deletar(indexGrid) {

    var dataDel = $('#jqxGrid').jqxGrid('getrowdata', indexGrid);
    $.ajax({
        type: 'POST',
        url: '/Sistema/Delete',
        dataType: 'json',
        data: {
            id: dataDel.id
        }
    }).done(function (data) {
        if (data.Sucesso) {

            Swal.fire({
                icon: 'success',
                title: 'Sucesso',
                text: data.Mensagem,
                timer: 2000,
                timerProgressBar: true,
                onBeforeOpen: () => {
                    Swal.showLoading();
                    timerInterval = setInterval(() => {
                        const content = Swal.getContent();
                        if (content) {
                            const b = content.querySelector('b');
                            if (b) {
                                b.textContent = Swal.getTimerLeft();
                            }
                        }
                    }, 100);
                },
                onClose: () => {
                    clearInterval(timerInterval);
                }
            }).then((result) => {
                ListarTodos();
            });
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Erro',
                text: data.Mensagem
            });
        }
    }).fail(function () {
        Swal.fire({
            icon: 'error',
            title: 'Erro',
            text: 'Erro ao processar a solicitação.'
        });
    });
}

function Carregar() {
    $('#botaoCriar').on('click', function () {

        $.ajax({
            type: 'GET',
            url: '/Login/Logout',
            contentType: 'html',
            beforeSend: function () {
                $('.preloader').fadeIn();
            }
        }).done(function (data) {
            $(' #myModal #myModalContent').html(data);
            $('.preloader').fadeOut(600, function () {
                $('#mymodal').modal('show');
            });
        }).fail(function () {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Erro ao abrir a janela, contate o administrador.'
            });

            $('.preloader').fadeOut();
        });
    });

    $('#btnPesquisar').on('click', function () {
        ListarTodos();
    });

    //Botao Exportar
    $("#btnExportar").click(function () {
        $("#jqxGrid").jqxGrid(
            'exportdata',
            'xls',
            'Sistemas',
            true,
            null,
            false);
    });
}