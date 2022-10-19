function RetornaRegistro(grid, row) {

    var page = $(grid).jqxGrid('getpaginginformation');

    if (page.pagenum > 0) {

        var data = $(grid).jqxGrid('getrows');

        var iSkip = page.pagenum * page.pagesize;

        return data[row.boundindex + iSkip];

    }
    else {
        return $(grid).jqxGrid('getrows')[row.boundindex];
    }

}

function retornaIndiceAbsoluto(gridId, numeroLinhaRelativa) {
    var pagina = $(gridId).jqxGrid('getpaginginformation');

    if (pagina.pagenum === 0)
        return numeroLinhaRelativa    

    var numeroPagina = pagina.pagenum * pagina.pagesize;

    return numeroLinhaRelativa + numeroPagina;
}
