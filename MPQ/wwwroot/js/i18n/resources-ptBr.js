var tables = {};

tables.usuario = {
    loginRede: 'Login',
    nome: 'Nome',
    idioma: 'Idioma Padrão',
    status: 'Status'
}

tables.grupo = {
    nome: 'Nome',
    descricao: 'Descrição'
}

tables.menu = {
    level: "Nível",
    sequece: "Sequência",
    parentid: "Pai",
    name: 'Nome',
    title: 'Título',
    url: 'Url',
    iconurl: 'Icone Url'
}

tables.site = {
    name: 'Nome',
    empresa: 'Empresa'
}

tables.empresa = {
    id: 'Id',
    nome: 'Nome'
}

tables.indicador = {
    id: 'Id',
    nome: 'Name'
}

tables.meta = {
    id: 'Id',
    nome: 'Name'
}

tables.projeto = {
    id: 'Id',
    name: 'Nome',
    customer: 'Cliente',
    site: 'Planta'
}

tables.productionline = {
    id: 'Id',
    name: 'Name',
    customer: 'Cliente',
    site: 'Planta',
    area: 'Area',
    project: 'Projeto'
}

tables.goalstf = {
    id: 'Id',
    site: 'Planta',
    indicator: 'Indicador',
    businessunit: 'Unidade de Negócio',
    goal: 'Meta',
    stfnumber: 'STF',
    generaltarget: 'Valor Geral'
}

tables.goalcustomer = {
    id: 'Id',
    name: 'Nome',
    site: 'Planta',
    customer: 'Cliente',
    unitofmeasurement: 'Unidade de Medida',
    stfnumber: 'STF'
}

tables.goalcustomerrange = {
    id: 'Id',
    goalcustomer: 'Meta do Cliente',
    name: 'Nome',
    value: 'Valor',
    color: 'Cor',
    operator: 'Operador'
}

tables.goalproductionline = {
    id: 'Id',
    productionline: 'Linha',
    site: 'Planta',
    customer: 'Cliente',
    project: 'Projeto',
    stfnumber: 'STF',
    target: 'Meta',
    productionarea: 'Area'
}

tables.goalproductionlinerange = {
    id: 'Id',
    productionline: 'Linha de Produção',
    name: 'Nome',
    value: 'Valor',
    color: 'Cor',
    operator: 'Operador'
}

tables.complaint = {
    id: 'Id',
    indicator: 'Indicador',
    complaintDate: 'Data',
    customer: 'Cliente',
    quantity: 'Quantidade',
    issue: 'Problema',
    containmentAction: 'Ação de contenção',
    actionDate: 'Data da Ação',
    actionResponsible: 'Responsável pela ação',
    gqrsNumber: 'Num GQRS',
    summarySent: 'Sumário enviado',
    site: 'Planta',
    businessUnit: 'Unidade de Negócio'
}

tables.complaintwarrantypart = {
    id: 'Id',
    site: 'Planta',    
    customer: 'Cliente',
    receiptDate: 'Data',
    partQuantity: 'Quantidade',
    issue: 'Problema',
    Legitimate: 'Legítimo',    
    status: 'Status',    
    businessUnit: 'Unidade de Negócio'
}

tables.customerview = {
    id: 'Id',
    site: 'Planta',
    businessUnit: 'Unidade de Negócio',
    indicator: 'Indicador',
    customer: 'Cliente',
    year: 'Ano',
    weekNumber: 'Semana',
    result: 'Resultado'    
}

tables.customer = {
    id: 'Id',
    name: 'Nome'
}

tables.producedpart = {
    id: 'Id',
    site: 'Site',
    productionarea: 'ProductionArea',
    year: 'Year',
    weeknumber: 'WeekNumber',
    quantity: 'Quantity'
}

tables.companhia = {
    id: 'Id',
    nome: 'Nome',
    descricao: 'Descrição'
}

tables.dominio = {
    id: 'Id',
    descricao: 'Descrição',
    url: 'URL'
}

tables.grupoPermissao = {
    descricao: 'Descrição'
}

tables.idioma = {
    nome: 'Nome',
    icone: 'Ícone'
}

tables.permissoes = {
    posX: 'Posição X',
    posY: 'Posição Y',
    PermissaoPai: 'Permissão Pai',
    PermissaoGrupo: 'Grupo Permissão',
    Sistema: 'Sistema',
    Icone: 'Ícone'
}

tables.permissaoIdioma = {
    nome: 'Nome',
    caminho: 'Caminho',
    descricaoPermissao: 'Descrição Permissão',
    descricaoIdioma: 'Descrição Idioma'
}

tables.rota = {
    id: 'Id',
    url: 'Url',
    sistema: 'Sistema'
}

tables.sistema = {
    id: 'Id',
    nome: 'Nome',
    urlBase: 'Url Base',
    posicao: 'Posição'

}

tables.solicitacao = {
    verbo: 'Verbo Http',
    dados: 'Dados',
    observ: 'Observação',
    descRota: 'Descrição Rota',
    status: 'Status'
}

tables.status = {
    id: 'Id',
    nome: 'Nome',
    codStatus: 'Código Status'
}

tables.unidadNegocio = {
    id: 'Id',
    nome: 'Nome',
    descricao: 'Descrição',
    codUnSap: 'Código unidade SAP'
}

tables.exclusao = {
    title: 'Confirma a exclusão do registro?',
    descricao: 'Isso não poderá ser desfeito!',
    confirm: 'Sim, excluir!',
    cancel: 'Cancelar',
    erroSelec: 'Necessário Selecionar uma linha para editar'
}

tables.comum = {
    editar: 'Editar',
    excluir: 'Excluir',
    imprimir: 'Imprimir',
    sim: 'Sim',
    nao: 'Não',
    adicionar: 'Adicionar',
    remover: 'Remover',
}

tables.checklistItem = {
    id: 'Id',
    descricao: 'Descrição',
    descricaoPadrao: 'Descrição Padrão',
    idChecklistItem: 'ChecklistItem',
    idIdioma: 'Idioma',
    dataAlteracao: 'Data Alteração',
    idDominio: 'Domínio',
    idUsuarioAlteracao: 'Usuário Alteração',
    hostName: 'Host Name',
    descricaoIdioma: 'Idioma',
    padraoMax: 'Max',
    padraoMin: 'Min',
    tipoPadrao: 'Tipo Padrão',
    tipo: 'Tipo'
}

tables.checklistMetodo = {
    id: 'Id',
    dataAlteracao: 'Data Alteração',
    idDominio: 'Domínio',
    idUsuarioAlteracao: 'Usuário Alteração',
    hostName: 'Host Name',
    usuario: 'Usuário',
    descricaoDominio: 'Domínio',
    descricaoMetodoIdiomaPadrao: 'Método',
    descricaoStatusGeral: 'Status Geral'
}

tables.categoria = {
    desc: 'Descrição',
    afetaPosto: 'Afeta posto de trabalho',
    afetaFunc: 'Afeta funcionário',
    aplicaDestaquePosto: 'Aplica destaque posto',
    corDestaq: 'Cor destaque'

}

tables.area = {
    area: 'Área',
    tipoArea: 'Tipo Área',
    processo: 'Processo',
    status: 'Status',
    aplicaCor: 'Aplica Cor'
}

tables.evento = {
    categoriaDescricao: 'Categoria',
    eventoDescricao: 'Evento',
    informarHoraDescricao: 'Informar Hora',
    geraPendencia: 'Gera pendênica'
}

tables.checklistEquip = {
    planta: 'Planta',
    cliente: 'Cliente',
    descEquipamento: 'Desc. Equipamento',
    codEquipamento: 'Cód. Equipamento',
    tipoEquipamento: 'Tipo Equipamento',
    numSerie: 'Nr Série',
    modelo: 'Modelo'
}

tables.relacChecklist = {
    codigo: 'Cód. Checklist',
    revisao: 'Revisão',
    descricao: 'Desc. Checklist'
}

tables.checklistLinhas = {
    planta: 'Planta',
    codigo: 'Cód. Linha',
    descricao: 'Descr. Linha',
    area: 'Área de trabalho'
}

tables.equipamentoCliente = {
    plantaDescricao: 'Planta',
    clienteDescricao: 'Cliente',
    equipamentoFabricante: 'Fabricante',
    equipamentoCodigo: 'Cod. Equipamento',
    equipamentoModelo: 'Modelo',
    equipamentoDescricao: 'Nome',
    equipamentoSerie: 'Série',
    equipamentoPatrimonio: 'Patrimônio'
}

tables.default = {
    desc: 'Descrição'
}

tables.parada = {
    parada: 'Parada',
    status: 'Status'
}

tables.paradaLinhaTurno = {
    parada: 'Parada',
    turno: 'Turno',
    linha: 'Linha',
    planta: 'Planta',
    horaInicial: 'Hora Inicio',
    horaFinal: 'Hora Final'
}

tables.processo = {
    descricao: 'Descrição',
    unidadeNegocio: 'Unidade de Negócio',
    statusDescricao: 'Situação'
}

tables.checklistModelo = {
    codigoGrupo: 'Código Grupo',
    descricaoModeloRevisao: 'Descrição do Grupo',
    descricaoChecklistTipo: 'Tipo de Checklist',
    codigoSGI: 'Código SGI'
}

tables.checklistModeloItem = {
    sequencia: 'Sequência',
    descricaoChecklistItem: 'Item de Checklist',
    descricaoChecklistMetodo: 'Método do Checklist'
}

tables.checklistItemIdioma = {
    id: 'Id',
    idioma: 'Idioma',
    descricao: 'Descrição',
    descricaoPadrao: 'Descrição Padrão',
    arquivoOperador: 'Arquivo Operador',
    arquivoManutencao: 'Arquivo Manutenção'
}

tables.cliente = {
    id: 'Id',
    nome: 'Nome'
}

tables.checkListTipo = {
    desc: 'Descrição',
    descDepto: 'Departamento'
}

tables.checklistModeloPlanta = {
    DescricaoPlanta: 'Localidade da Planta',
    Frequencia: 'Frequência'
}

tables.henkaten = {
    data: 'Data',
    tact: 'Takt Time',
    turno: 'Turno',
    linha: 'Linha',
    planta: 'Planta'
}

tables.henkatenEvento = {
    categoria: 'Categoria',
    evento: 'Evento',
    observacao: 'Observação',
    hora: 'Hora',
    funcionario: 'Funcionário',
    matricula: 'Matricula',
    posto: 'Posto'
}

tables.turno = {
    id: 'Id',
    numero: 'Número',
    descricao: 'Descrição do Turno',
}

tables.funcionario = {
    id: 'Id',
    nome: 'Nome',
    matricula: 'Matrícula',
    idStatusGeral: 'Status Geral',
    dataCriacao: 'Data Criacao',
    dataAlteracao: 'Data Alteracao',
    idDominio: 'Domínio',
    idUsuarioAlteracao: 'Usuário Alteração',
    hostName: 'Host Name',
    idEmpresa: 'Empresa',
    idPlanta: 'Planta',
    descricaoStatusGeral: 'Status Geral',
    descricaoUsuarioAlteracao: 'Usuário',
    descricaoEmpresa: 'Empresa',
    descricaoPlanta: 'Planta',
    descricaoProcesso: 'Processo',
    descricaoDominio: 'Domínio',
    observacao: 'Observação'
}

tables.linhaPosto = {
    codigo: 'Código',
    postoDescricao: 'Posto',
    statusDescricao: 'Status',
    areaDescricao: 'Área',
    tipoAreaDescricao: 'Tipo Área'
}

tables.FuncionarioPostoLinha = {
    nome: 'Nome',
    matricula: 'Matrícula',
    observacao: 'Observação',
    posto: 'Posto'
}

tables.consultaCheck = {
    dataCriacao: 'Data e Hora Criação',
    operador: 'Operador',
    cliente: 'Cliente',
    turno: 'Turno',
    equipamento: 'Equipamento',
    status: 'Status',
    duracao: 'H. Duração',
    planta: 'Planta'
}

tables.catalogoDefeito = {
    id: "Id",
    codigo: "Código",
    descricao: "Descrição",
    obrigatorioObservacao: "Obrigatório Observação",
    idStatusGeral: "Id Status",
    idIdioma: "Id Idioma",
    descricaoStatusGeral: "Status",
    descricaoDominio: "Domínio",
    descricaoUsuario: "Usuário",
    descricaoIdioma: "Idioma"
}

tables.classificacaoRetrabalho = {
    id: "Id",
    descricaoCurta: "Descrição Curta",
    descricaoLonga: "Descrição Longa",
    cor: "Cor",
    geraEtiqueta: "Gera etiqueta de retrabalho",
    descricaoStatus: "Situação",
    ativo: "Ativo"
}

tables.catalogoDefeitoComponente = {
    id: "Id",
    codigo: "Código",
    descricaoComponente: "Descrição",
    status: "Status"
}

tables.permissaoSistema = {
    id:"Id",
    nome:"Nome",
    descricao:"Descrição",
    nomeExibicao:"Nome de Exibição",
    icone:"Ícone",
    nomeSistema:"Nome do Sistema",
    urlBase:"URL Base",
    codSistema:"Código Sistema"
}

tables.perfilAcesso = {
    id: "Id",
    idGrupoPermissao: "Grupo Permissão",
    descricao: "Descrição",
    icone: "Ícone",
    posicaoX: "Posição X",
    posicaoY: "Posição Y",
    idStatusGeral: "Status",
    dataCriacao: "Data Criação",
    dataAlteracao: "Data Alterção",
    idDominio: "Domínio",
    idUsuarioAlteracao: "Usuário",
    hostName: "HostName",
    grupoPermissao: "Grupo Permissão",
    status: "Status",
    dominio:"Domínio",
    usuarioAlteracao: "Usuário"
}

tables.matrizDefeito = {
    id: "Id",
    codigoMatriz: "Código Matriz",
    descricao: "Descrição",
    descricaoDetalhada: "Descrição Detalhada",
    aplicacao: "Aplicação",
    status: "Ativo",

    detalhe: {
        processo: 'Processo',
        aplica: 'Aplica',
        obrigatorioObservacao: 'Obrigatório observação',
        obrigatorioCavidade1: 'Obrigatório cavidade 1',
        obrigatorioCavidade2: 'Obrigatório cavidade 2',
        geraEtiqueta: 'Gera Etiqueta',
    }
}

tables.combinacao = {
    codigoYms: "Código Yms",
    codigoBoleta: "Código Boleta",
    descricao: "Descrição"
}

tables.modeloMaquina = {
    id: "Id",
    modelo: "Modelo"
}

tables.tempoSwt = {
    modeloMaq: 'Modelo Máquina',
    codigo: 'Cód Yms Comb.',
    combinacao: 'Combinação',
    fixo: 'Fixo',
    variavel: 'Variável',
    dataHoraModif: 'Data Hora Modificação',
    usuario: 'Usuário'
}

tables.tipoEquipamento = {
    id: "Id",
    descricao: "Descrição",
}

tables.tipoIntegracao = {
    id: "Id",
    descricao: "Descrição",
    status: "Status"
}

tables.item = {
    codigo: 'Código',
    descricao: 'Descrição',
    tipoItem: 'Código Tipo'
}

tables.aplicador = {
    identificador: 'Identificador',
    modeloAplicador: 'Modelo',
    terminal: 'Terminal',
    cravacaoIsolante: 'Cravação Isolante',
    tipoFerramental: 'Tipo Ferramental',

    codigoPecaAAN: 'Código Peça A/AN',
    ferriteCondutorAAN: 'Ferrite Condutor A/AN',
    ferriteIsolanteAAN: 'Ferrite Isolante A/AN',
    codigoPecaW: 'Código Peça W',
    ferriteCondutorW: 'Ferrite Condutor W',
    ferriteIsolanteW: 'Ferrite Isolante W',
    codigoPecaI: 'Código Peça I',
    ferriteCondutorI: 'Ferrite Condutor I',
    ferriteIsolanteI: 'Ferrite Isolante I',
    codigoPecaQAP: 'Código Peça Q/AP',
    ferriteCondutorQAP: 'Ferrite Condutor Q/AP',
    ferriteIsolanteQAP: 'Ferrite Isolante Q/AP',

    imobilizado: {
        imobilizado: 'Imobilizado',
        planta: 'Planta',
    }
}

tables.tipoIntegracao = {
    id: "Id",
    processoDescricao: "Processo",
    tipoIntegracaoDescricao: "Tipo Integração",
    descricao: "Descrição",
    status: "Status"
}

tables.tipoEtiqueta = {
    descricao: "Descrição",
    status: "Status"
}

tables.layoutEtiqueta = {
    id: "Id",
    idTipoEtiqueta: "Tipo de Etiqueta",
    urlLayout: "Layout",
    descricao: "Descricao",
    tipoEtiqueta: "Tipo de Etiqueta",
    status: "Status"
}

tables.tipoEtiquetaIntegracao = {
    id: "Id",
    integracao: "Integração",
    tipoEtiqueta: "Tipo de Etiqueta",
    status: "Status"
}

tables.acompanhamentoSolicitacoes = {
    id: 'Id',
    idRota: 'Id Rota',
    dados: 'Dados',
    rota: 'Rota',
    descricaoStatus: 'Status',
    planta: 'Planta',
    dataHora: 'Data/Hora',
    usuario: 'Usuário',
    verboHttp: 'Verbo HTTP',
    tentativasIntegracao: 'Tentativas de Integração'
}

tables.acompanhamentoSolicitacoesDetalhe = {
    descricaoErro: 'Descrição do Erro',
    dataHora: 'Data/Hora',
}

tables.acompanhamentoImportacoes = {
    id: 'Id',
    dataCriacao: 'Data/Hora Importação',
    arquivo: 'Arquivo',
    descricaoStatus: 'Status',
    nomeUsuario: 'Nome do Usuário',
    detalhes: 'Mensagens',
    mensagensLabel: 'Visualizar',
};

tables.atividade = {
    id: "Id",
    atividade: 'Atividade Realizada',
};

tables.operacaoFamiliaTipoItemProcesso = {
    id: "Id",
    idOperacao: 'Id Operação',
    codigoOperacaoEtiqueta: 'Código Operação',
    descricaoOperacao: 'Descricao',
    idFamiliaTipoItemProcesso: 'Família / Tipo Item / Processo / Tipo SA',
    dataAlteracao: 'Data Alteração',
    linhasRepetidas: 'Linhas Repetidas com mesma [Operação e Família / Tipo Item / Processo / Tipo SA]'
};

tables.modeloPaco = {
    codigoPaco: 'Código Paco',
    descricao: 'Descricao',
    qtdeLinha: 'Qtd. Linha',
    qtdeColuna: 'Qtd. Coluna',
    ativo: 'Ativo'
};

tables.codigosOperacao = {
    codigo: 'Código Operação',
    codigoOperacaoEtiqueta: 'Código Operação OES',
    descricao: 'Descrição',
    validaCondicaoCorte: 'Valida Condição de Corte',
};

tables.tolerancia = {
    titulo: 'Tolerância',
    condutor: "Tolerância C/H - Condutor Crimp/Height ±",
    isolante: "Tolerância C/H - Isolante Crimp/Height ±"
}

tables.tipoAlimentacao = {
    descricao: 'Descrição',
};

tables.tipoFerramental = {
    descricao: 'Descrição',

    mensagens: {
        descricaoJaExiste: 'A descrição [descricao] já foi cadastrada',
    }
};

tables.modeloAplicador = {
    descricao: 'Modelo',
    isolante: 'Cravação Isolante',
    ferramental: 'Ferramental'
};

tables.provePin = {
    tamanho: 'Tamanho',
    cor: 'Cor',
    altura: 'Altura',
    comprimento: 'Comprimento'
};

tables.cravacaoIsolante = {
    descricao: 'Descrição',
    imagem: "Imagem"
};

tables.aplicadorHistorico = {
    descricao: 'Descrição',
    codigoItem: 'Terminal',
    descricaoModeloAplicador: 'Modelo Aplicador',
    descricaoCravacaoIsolante: 'Cravação Isolante',
    descricaoTipoFerramental: 'Tipo Ferramental',
    descricaoTipoAlimentacao: 'Tipo Alimentação',
    critico: 'Crítico',
    codigoPecaAAN: 'Código Peça A/AN',
    ferriteCondutorAAN: 'Ferr. Condutor(mm) A/AN',
    ferriteIsolanteAAN: 'Ferr. Isolante(mm) A/AN',
    codigoPecaW: 'Código Peça W',
    ferriteCondutorW: 'Ferr. Condutor(mm) W',
    ferriteIsolanteW: 'Ferr. Isolante(mm) W',
    codigoPecaI: 'Código Peça I',
    ferriteCondutorI: 'Ferr. Condutor(mm) I',
    ferriteIsolanteI: 'Ferr. Isolante(mm) I',
    codigoPecaQAP: 'Código Peça Q/AP',
    ferriteCondutorQAP: 'Ferr. Condutor(mm) Q/AP',
    ferriteIsolanteQAP: 'Ferr. Isolante(mm) Q/AP',
    identificador: 'Identificador',
    usuarioData: 'Usuário - Data'

};

tables.padraoCravacao = {
    codigoControle: "Cód. Controle",
    terminal: "Terminal",
    selo: "Selo",
    aplicador: "Aplicador",
    cravacaoIsolante: "Cravação Isolante",
    tipoFerramental: "Tipo Ferramental",
    usuarioDataHora: "Usuário e Data Última Modificação",
    status: "Status"
};

tables.padraoCravacaoDadosCabo = {
    idCabo: "Id Cabo",
    desc: "Desc",
    tipoCabo: "Tipo Cabo",
    bitola: "Bitola",
    revisao: "Revisão",
    chCondutor: "C/H Condutor",
    chIsolante: "C/H Isolante",
    tracao: "Tração",
    wa: "WA"
};

tables.padraoCravacaoDadosCaboHistorico = {
    chCondutor: "C/H Condutor",
    chIsolante: "C/H Isolante",
    chEstabilizador: "C/H Estabilizador",
    tracao: "Tração",
    wa: "WA",
    posicaoFios: 'Posição Fios',
    tolerancia: '~',
    usuarioData: 'Usuário e Data',
};

tables.padraoCravacaoDadosCaboPlantaCliente = {
    descricaoPlanta: 'Planta',
    descricaoCliente: 'Cliente',
    dataAlteracao: 'Data Alteração'
};

tables.Terminal = {
    material: 'Material',
    desponte: 'Desponte',
    dataAlteracao: 'Data Alteração'
};

tables.padraoCravacaoDadosTerminalHistorico = {
    valorTorcao: 'Torção',
    valorCima: 'Cima',
    valorBaixo: 'Baixo',
    valorLateral: 'Lateral',
    toleranciaCWCondutorConcatenado: 'Tolerância',
    cWCondutor: 'CW Condutor',
    toleranciaCWIsolanteConcatenado: 'Tolerância',
    cWIsolante: 'CW Isolante',
    toleranciaCWEstabilizadorConcatenado: 'Tolerância',
    cWEstabilizador: 'CW Estabilizador',
    toleranciaBellMouthFrontConcatenado: 'Tolerância',
    bellMouthFront: 'Bell Front',
    toleranciaBellMouthRearConcatenado: 'Tolerância',
    bellMouthRear: 'Bell Hear',
    toleranciaCuttingOFFConcatenado: 'Tolerância',
    cuttingOFF: 'Cutting Off',
    toleranciaBrushConcatenado: 'Tolerância',
    brush: 'Brush',
    imagem: 'Desenho Terminal',
    usuarioData: 'Usuário - Data'
};

tables.util = {
    selecione_uma_opcao: "Selecione uma opção",
    erro_ao_carregar: "Erro ao tentar carregar os dados",
    integracao: "Integração",
    anexo: "Anexo",
    excluir: "Excluir",
    arquivo_enviado_com_sucesso: "Arquivo enviado com sucesso.",
    salvo_com_sucesso: 'Salvo com sucesso',
    sucesso: Sucesso = 'Sucesso',
    erro: Erro = 'Erro',
    verifique_as_celulas_destacadas: "Verifique as células destacadas.",
    etiqueta: "Etiqueta",
    excluido_com_sucesso: "Excluído com Sucesso.",
    resposta: "Resposta",
    erro_ao_processar_solicitacao: "Erro ao processar a solicitação",
    nao_pode_ser_vazio: "É obrigatório",
    item_nao_encontrado: "Item não encontrado",
    somente_status_pendente: 'A edição só é permitida para o Status Pendente',
    status_nao_foi_alterado: 'O Status não foi alterado.',
    informe_um_motivo: 'Informe um motivo',
    impossivel: 'Impossível',
    nenhum_arquivo_selecionado_para_upload: 'Nenhum arquivo selecionado para upload.',
    importacao_iniciada_com_sucesso: 'Importação iniciada com sucesso!',
    falha_ao_realizar_importação: 'Falha ao realizar importação.',
    cancelado: 'Cancelado',
    concluido: 'Concluído',
    pendente: 'Pendente',
    atencao: "Atenção",
    nenhumItemFoiSelecionado: 'Nenhum Item Foi Selecionado',
    todos: 'Todos',
    sim: 'Sim',
    nao: 'Não',
    asLinhasDetacadasSeraoRemovidas: "As linhas destacadas em laranja estão incompletas e serão removidas antes de prosseguir.Deseja continuar?",
    removerLinhasIncompletas: "Remover linhas incompletas",
    cancelarEVoltarParaCorrigir: "Cancelar e voltar para corrigir",
    errosNoGrid: "Inconsistências Encontradas",
    foramDetectados: "Foram detectados",
    correcaoNecessaria: "A correção é necessária para salvar os dados no sistema",
    nenhum: "Nenhum",
    cadastroEstaSendoUtilizado: "O cadastro está sendo utilizado.",
    preenchidoParcialmente: "Itens preenchidos parcialmente",
    verifiqueLinhasDestacadas: "Verifique as linhas Destacadas.",
    jaExisteUmRegistroNoSistema: "Já existe um registro no sistema"
};

tables.JEasy = {
    atencao: "Atenção",
    asLinhasDetacadasSeraoRemovidas: "As linhas destacadas em laranja estão incompletas e serão removidas antes de prosseguir. Deseja continuar?",
    removerLinhasIncompletas: "Remover linhas incompletas",
    cancelarEVoltarParaCorrigir: "Cancelar e voltar para corrigir"
}