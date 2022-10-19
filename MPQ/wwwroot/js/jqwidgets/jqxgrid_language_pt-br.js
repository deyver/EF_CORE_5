var getLocalization = function () {
    var localizationobj = {};
    localizationobj.firstDay = 0;
    localizationobj.days = {
        names: ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sabado"],
        namesAbbr: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"],
        namesShort: ["Do", "Se", "Te", "Qa", "Qi", "Se", "Sa"]
    };
    localizationobj.months = {
        names: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro", ""],
        namesAbbr: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez", ""]
    };
    localizationobj.groupsheaderstring = "Arraste aqui o cabeçalho da coluna para agrupar";
    localizationobj.pagergotopagestring = "Pagina:";
    localizationobj.pagershowrowsstring = "Linhas:";
    localizationobj.pagerrangestring = " de ";
    localizationobj.pagernextbuttonstring = "Próximo";
    localizationobj.pagerpreviousbuttonstring = "Anterior";
    localizationobj.sortascendingstring = "Ascendente";
    localizationobj.sortdescendingstring = "Descendente";
    localizationobj.sortremovestring = "Remover Ordem";
    localizationobj.sortremovestring = "Remover Ordenamento";
    localizationobj.groupbystring = "Agrupar por esta coluna";
    localizationobj.groupremovestring = "Remover do Agrupamento";
    localizationobj.filterchoosestring = "Todos";
    localizationobj.filterselectstring = "Selecione";
    localizationobj.filterselectallstring = "Todos";
    localizationobj.filterclearstring = "Limpar Filtros";
    localizationobj.filterstring = "Filtrar";
    localizationobj.filtershowrowstring = "Mostrar Linhas Que:";
    localizationobj.filterorconditionstring = "Ou";
    localizationobj.filterandconditionstring = "E";
    localizationobj.filterstringcomparisonoperators = [
        "Vazio", "Não vazio", "Contém", "Contém(Maiúsculo/Minúsculo)", "Não contém",
        "Não contém(Maiúsculo/Minúsculo)", "Inicia com", "Inicia com(Maiúsculo/Minúsculo)",
        "Termina com", "Termina com(Maiúsculo/Minúsculo)", "Igual", "Igual(Maiúsculo/Minúsculo)", "Nulo", "Não nulo"
    ];
    localizationobj.filternumericcomparisonoperators = [
        "Igual", "Não igual", "Menor que", "Menor ou igual que",
        "Maior que", "Maior ou igual que", "nulo", "não nulo"
    ];
    localizationobj.filterdatecomparisonoperators = [
        "Igual", "Não igual", "Menor que", "Menor ou igual que",
        "Maior que", "Maior ou igual que", "nulo", "não nulo"
    ];
    localizationobj.emptydatastring = "Nenhuma Informação Encontrada";
    localizationobj.loadtext = "Aguarde";
    var days = {
        names: ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado"],
        namesAbbr: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"],
        namesShort: ["Do", "Se", "Te", "Qa", "Qi", "Se", "Sa"]
    };
    localizationobj.days = days;
    var months = {
        names: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        namesAbbr: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"]
    };
    localizationobj.months = months;
    localizationobj.todayString = "Hoje";
    localizationobj.clearString = "Limpar";

    return localizationobj;
}