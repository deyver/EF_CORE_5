const GridBasico = function (gridSelector, parametros) {
    this.selector = gridSelector;
    this.pagesize = parametros.itensPorPagina !== undefined ? parametros.itensPorPagina : 10;
    this.height = 0;
    this.dataFields = [];
    this.colunas = [];
    this.consultar = function () { };

    this.temPaginacao = parametros.temPaginacao;
    this.temFiltro = parametros.temFiltro;
    this.temLinhaFiltro = parametros.temLinhaFiltro;
    this.temOrdenacao = parametros.temOrdenacao;
    this.editavel = parametros.editavel;
};

GridBasico.prototype.atualizar = function (data) {
    var source =
    {
        datatype: "json",
        datafields: this.dataFields.map(function (item) {
            return { name: item.nome };
        }),
        localdata: data
    };

    const dataAdapter = new $.jqx.dataAdapter(source);

    $(this.selector).jqxGrid({
        source: dataAdapter
    });
}

GridBasico.prototype.montar = function () {
    const gridId = this.selector;

    $(gridId).jqxGrid({
        width: '100%',
        height: this.height,
        autoheight: !this.height,
        pageable: this.temPaginacao !== undefined ? this.temPaginacao : true,
        pagesize: this.pagesize,
        filterable: this.temFiltro !== undefined ? this.temFiltro : false,
        sortable: this.temOrdenacao !== undefined ? this.temOrdenacao : true,
        showfilterrow: this.temLinhaFiltro !== undefined ? this.temLinhaFiltro : false,
        editable: this.editavel !== undefined ? this.editavel : false,
        showcolumnlines: true,
        showcolumnheaderlines: true,
        columnsresize: true,
        rowsheight: 50,
        columns: this.colunas,
        localization: getLocalization()
    });

    var gridWidth = $(gridId).width();

    // Distribui as colunas uniformemente pelo grid
    this.dataFields.forEach(function (item) {
        if (!item.tamanhoProporcional)
            return;

        $(gridId).jqxGrid('setcolumnproperty', item.nome, 'width', gridWidth * item.tamanhoProporcional / 100);
    });
}
