$(document).ready(function () { ListarTodos(); Carregar(); });

var _pgNumber = 0;
var _pgSize = 10;
var _registros = null;
var _jqxGrid = null;

function ListarTodos() {

    $.ajax({ type: "GET", url: "/GoalProductionLine/GetAllGoalProductionLine", dataType: "json" })
        .done(function (data) {

            _registros = data;

            var source = { datatype: "json", datafields: [{ name: 'id' }, { name: 'productionLine' }, { name: 'site' }, { name: 'customer' }, { name: 'project' }, { name: 'stfNumber' }, { name: 'target' }, { name: 'productionArea' }], localdata: _registros };
            var dataAdapter = new $.jqx.dataAdapter(source);
            var _c = tables.goalproductionline;

            _jqxGrid = $("#jqxGrid").jqxGrid({
                source: dataAdapter,
                width: '100%',
                pageable: true,
                autoheight: true,
                filterable: true,
                sortable: true,
                showfilterrow: true,
                pagesize: 10,
                pagermode: 'simple',
                showcolumnlines: true,
                showcolumnheaderlines: true,
                rowsheight: 50,
                columns: [
                    {
                        text: ' ', cellsAlign: 'center', align: 'center', columnType: 'none', editable: false, sortable: false, dataField: null, width: 50, filterable: false,
                        createwidget: function (row, column, value, htmlElement) {
                            var button = $(`<div data-toggle="modal" data-row='${row.boundindex}' data-target="#myModal" title="${tables.comum.editar}" data-modal="" class="btn btn-primary botaoEditar jqxButtonAligns" data-row='${row.boundindex}'><i class="fas fa-pencil-alt" style="color: aliceblue;" data-row='${row.boundindex}'></i></div>`);
                            $(htmlElement).append(button);
                            button.jqxButton({ template: "primary", height: '100%', width: '100%' });
                            button.click(function (event) {

                                var IndexGrid = parseInt(event.target.getAttribute('data-row'));

                                if (IndexGrid != undefined && IndexGrid > -1) {
                                    var reg = RetornaRegistro(_jqxGrid, row);

                                    if (reg != undefined && reg != null) {
                                        window.location.href = `/GoalProductionLine/Edit/${reg.id}`;

                                        //$.ajax({ type: 'GET', url: `/GoalProductionLine/Update?registro=${reg.id}`, contentType: 'html', beforeSend: function () { $('.preloader').fadeIn(); } })
                                        //    .done(function (result) { $(' #myModal #myModalContent').html(result); $('.preloader').fadeOut(600, function () { $('#myModal').modal('show'); }); })
                                        //    .fail(function () { Swal.fire({ icon: 'error', title: 'Oops...', text: 'Erro ao abrir a janela, contate o administrador.' }); $('.preloader').fadeOut(); });
                                    }
                                }
                                else {
                                    Swal.fire({ icon: 'error', title: 'Oops...', text: 'Necessário Selecionar uma linha para editar' });
                                }
                            });
                        },
                        initwidget: function (row, column, value, htmlElement) {
                        }
                    },
                    {
                        text: ' ', cellsAlign: 'center', align: 'center', columnType: 'none', editable: false, sortable: false, dataField: null, width: 50, filterable: false,
                        createwidget: function (row, column, value, htmlElement) {
                            var button = $(`<div title='${tables.comum.excluir}' class="btn btn-danger botaoApagar jqxButtonAligns" id="botaoApagar" data-row-delete="${row.boundindex}"><i class="fas fa-trash " data-row-delete='${row.boundindex}'></i> </div>`);
                            $(htmlElement).append(button);
                            button.jqxButton({ template: "danger", height: '100%', width: '100%' });
                            button.click(function (event) {

                                var IndexGridDelete = parseInt(event.target.getAttribute('data-row-delete'));

                                if (IndexGridDelete != undefined && IndexGridDelete > -1) {

                                    var reg = RetornaRegistro(_jqxGrid, row);

                                    Swal
                                        .fire({
                                            title: tables.exclusao.title, text: tables.exclusao.descricao, icon: 'warning', showCancelButton: true, confirmButtonColor: '#ff4141', cancelButtonColor: '#929292', confirmButtonText: tables.exclusao.confirm, cancelButtonText: tables.exclusao.cancel
                                        })
                                        .then((result) => {
                                            if (result.value) { Deletar(reg); }
                                        });
                                }
                                else {
                                    Swal.fire({ icon: 'error', title: 'Oops...', text: tables.exclusao.erroSelec });
                                }
                            });
                        },
                        initwidget: function (row, column, value, htmlElement) {
                        }
                    },
                    {
                        text: _c.site, datafield: 'site', width: '200px', cellsalign: 'left'
                    },
                    {
                        text: _c.productionarea, datafield: 'productionArea', width: '200px', cellsalign: 'left'
                    },
                    {
                        text: _c.customer, datafield: 'customer', width: '200px', cellsalign: 'left'
                    },
                    {
                        text: _c.project, datafield: 'project', width: '200px', cellsalign: 'left'
                    },
                    {
                        text: _c.productionline, datafield: 'productionLine', width: 'auto', cellsalign: 'left'
                    },
                    {
                        text: _c.stfnumber, datafield: 'stfNumber', width: '50px', cellsalign: 'right'
                    },
                    {
                        text: _c.target, datafield: 'target', width: '150px', cellsalign: 'right'
                    }
                ],
                localization: getLocalization()

            });



            $("#jqxGrid").bind("pagechanged", function (event) {
                var args = event.args;
                _pgNumber = (args.pagenum);
                _pgSize = args.pagesize;
            });

        });

}

function Carregar() {
    $('#botaoCriar').on('click', function () {
        $.ajax({ type: 'GET', url: '/GoalProductionLine/Create', contentType: 'html', beforeSend: function () { $('.preloader').fadeIn(); } })
            .done(function (data) { $('#myModal #myModalContent').html(data); $('.preloader').fadeOut(600, function () { $('#myModal').modal('show'); }); })
            .fail(function () { Swal.fire({ icon: 'error', title: 'Oops...', text: 'Erro ao abrir a janela, contate o administrador.' }); $('.preloader').fadeOut(); });
    });

    $('#btnPesquisar').on('click', function () { ListarTodos(); });

    //Botao Exportar
    $("#btnExportar").click(function () { $("#jqxGrid").jqxGrid('exportdata', 'xls', 'Projetos', true, null, false); });
}

function Deletar(goalproductionline) {

    $.ajax({ type: 'POST', url: '/GoalProductionLine/Delete', dataType: 'json', data: { registro: goalproductionline.id, } })
        .done(function (data) {
            if (!data.Sucesso) { Swal.fire({ icon: 'error', title: 'Erro', text: data.Mensagem }); return; }
            if (data.Sucesso) {

                Swal
                    .fire({
                        icon: 'success', title: 'Sucesso', text: data.Mensagem, timer: 2000, timerProgressBar: true,
                        onBeforeOpen: () => { Swal.showLoading(); timerInterval = setInterval(() => { const content = Swal.getContent(); if (content) { const b = content.querySelector('b'); if (b) { b.textContent = Swal.getTimerLeft(); } } }, 100); },
                        onClose: () => { clearInterval(timerInterval); }
                    })
                    .then((result) => { ListarTodos(); });
            }
        }).fail(function () {
            Swal.fire({ icon: 'error', title: 'Erro', text: 'Erro ao processar a solicitação.' });
        });
}