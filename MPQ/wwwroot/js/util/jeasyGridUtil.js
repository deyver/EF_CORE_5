/**
 * Habilita ou Desabilita o filtro do grid incluindo a mudança no ícone do botâo
 * @param {string} gridSelector Seletor do grid
 * @param {string} filterButtonISelector Seletor do botão de filtro
 * @param {boolean} enable Flag indicando se é para habilitar ou desabilitar o filtro
 * @param {Function} enableFunction Função para que seja possível customizar o grid quando for habilitado 
 *                                  o filtro, em alguns casos é preciso desabilitar uma coluna do filtro, por exemplo.
 */
function enableFilters(gridSelector, filterButtonISelector, enable, enableFunction) {
    if (enable) {
        if (enableFunction !== undefined && enableFunction != null)
            enableFunction();
        else
            $(gridSelector).datagrid('enableFilter');

        icon = 'icon-clear';
    }
    else {
        $(gridSelector).datagrid('disableFilter');
        icon = 'icon-filter';
    }

    $(filterButtonISelector).linkbutton({
        iconCls: icon
    });

    // Remove o filtro das colunas
    $(gridSelector).datagrid('options').columns[0].map(o => {
        $(gridSelector).datagrid('removeFilterRule', o.field);
    });
}

