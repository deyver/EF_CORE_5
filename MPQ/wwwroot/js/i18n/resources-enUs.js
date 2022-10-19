var tables = {};

tables.usuario = {
    loginRede: 'Usuario de red',
    dominio: 'Dominio',
    nome: 'Nombre',
    idioma: 'Idioma',
    status: 'Status'
}

tables.grupo = {    
    nome: 'Nombre',
    descricao: 'Descricion'
}

tables.menu = {
    level: "Nível",
    sequece: "Sequência",
    parentid: "Pai",
    name: 'Nombre',
    title: 'Título',
    url: 'Url',
    iconurl: 'Icone Url'
}

tables.site = {
    name: 'Nombre',
    empresa: 'empresa'
}

tables.empresa = {
    id: 'Id',
    nome: 'Name'
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
    name: 'Name',
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
    nome: 'Nombre',
    descricao: 'Descripción'
}

tables.dominio = {
    id: 'Id',
    descricao: 'Descripción',
    url: 'URL'
}

tables.grupoPermissao = {
    descricao: 'Descripción'
}

tables.idioma = {
    nome: 'Nombre',
    icone: 'Icono'
}

tables.planta = {
    id: 'Id',
    nome: 'Nombre',
    descricao: 'Descripción',
    nomeEmpresa: 'Nombre Empresa',
    descricaoEmpresa: 'Descripción Empresa'
}

tables.permissoes = {
    posX: 'Posición X',
    posY: 'Posición Y',
    PermissaoPai: 'Permiso arriba',
    PermissaoGrupo: 'Grupo de permisos',
    Sistema: 'Sistema',
    Icone: 'Icono'
}

tables.permissaoIdioma = {
    nome: 'Nombre',
    caminho: 'Ruta',
    descricaoPermissao: 'Descripción del permiso',
    descricaoIdioma: 'Descripción del idioma'
}

tables.rota = {
    id: 'Id',
    url: 'Url',
    sistema: 'Sistema'
}

tables.sistema = {
    id: 'Id',
    nome: 'Nombre',
    urlBase: 'Url Base'
}

tables.solicitacao = {
    verbo: 'Verbo Http',
    dados: 'Datos',
    observ: 'Observación',
    descRota: 'Descripción de ruta',
    status: 'Estado'
}

tables.logSolicitacao = {
    dataHora: 'Fecha y hora',
    dadosTrafegados: 'Datos traficados',
    descricaoErro: 'Error de descripción'
}

tables.status = {
    id: 'Id',
    nome: 'Nombre',
    codStatus: 'Código de estado'
}

tables.unidadNegocio = {
    id: 'Id',
    nome: 'Nombre',
    descricao: 'Descripción',
    codUnSap: 'Código de unidad de SAP'
}

tables.exclusao = {
    title: 'confirmar la eliminación del registro?',
    descricao: '¡Esto no se puede deshacer!',
    confirm: 'Si, borrar',
    cancel: 'Cancelar',
    erroSelec: 'Seleccione una fila para editar'
}

tables.comum = {
    editar: 'Edit',
    excluir: 'Delete',
    imprimir: 'Print',
    sim: 'Yes',
    nao: 'No',
    adicionar: 'Add',
    remover: 'Remove',
}

tables.checklistItem = {
    id: 'Id',
    descricao: 'Descripción',
    descricaoPadrao: 'Descripción estándar',
    idChecklistItem: 'ChecklistItem',
    idIdioma: 'Idioma',
    dataAlteracao: 'Data Alteração',
    idDominio: 'Dominio',
    idUsuarioAlteracao: 'Usuario Alteração',
    hostName: 'Nombre de host',
    descricaoIdioma: 'Idioma',
    padraoMax: 'Max',
    padraoMin: 'Min',
    tipoPadrao: 'Tipo estándar',
    tipo: 'Tipo'
}

tables.checklistMetodo = {
    id: 'Id',
    dataAlteracao: 'Data Alteração',
    idDominio: 'Dominio',
    idUsuarioAlteracao: 'Usuario Alteração',
    idStatusGeral: 'Status Geral',
    hostName: 'Host Name',
    usuario: 'User',
    descricaoDominio: 'Descripción Dominio',
    descricaoMetodoIdiomaPadrao: 'Método',
    descricaoStatusGeral: 'Estado Geral'
}

tables.categoria = {
    desc: 'Descripción',
    afetaPosto: 'Afecta el trabajo',
    afetaFunc: 'Afecta al empleado',
    aplicaDestaquePosto: 'Aplica resaltado',
    corDestaq: 'Resaltado de color'

}

tables.area = {
    area: 'Zona',
    tipoArea: 'Tipo de área',
    processo: 'Proceso',
    status: 'Situación',
    aplicaCor: 'Aplicar color'
}

tables.evento = {
    categoriaDescricao: 'Categoría',
    eventoDescricao: 'Evento',
    informarHoraDescricao: 'Informar el tiempo',
    geraPendencia: 'Genera pendiente'
}

tables.checklistEquip = {
    planta: 'Planta',
    cliente: 'Cliente',
    descEquipamento: 'Desc. del equipo',
    codEquipamento: 'Cód. del equipo',
    tipoEquipamento: 'Tipo de equipo',
    numSerie: 'No de serie',
    modelo: 'Modelo'
}

tables.relacChecklist = {
    codigo: 'Cód. Lista de verific',
    revisao: 'Revisión',
    descricao: 'Descripción'
}

tables.checklistLinhas = {
    planta: 'Plano',
    codigo: 'Cód. Línea',
    descricao: 'Descr. Línea',
    area: 'Espacio de trabajo'
}

tables.equipamentoCliente = {
    plantaDescricao: 'Planta',
    clienteDescricao: 'Cliente',
    equipamentoFabricante: 'Fabricante',
    equipamentoCodigo: 'Cod. Equipo',
    equipamentoModelo: 'Modelo',
    equipamentoDescricao: 'Nombre',
    equipamentoSerie: 'Serie',
    equipamentoPatrimonio: 'Patrimonio'
}

tables.default = {
    desc: 'Descripción',
}

tables.checklistModelo = {
    codigoGrupo: 'Codigo del grupo',
    descricaoModeloRevisao: 'Descripción del Grupo',
    descricaoChecklistTipo: 'Tipo de lista de verificación',
    codigoSGI: 'Código SGI'
}

tables.checklistModeloItem = {
    sequencia: 'Secuencia',
    descricaoChecklistItem: 'Elemento de lista de verificación',
    descricaoChecklistMetodo: 'Método de lista de verificación',
}

tables.parada = {
    parada: 'Detener',
    status: 'Situación',
}

tables.matrizDefeito = {
    id: "Id",
    codigoMatriz: "Headquarters Code",
    descricao: "Description",
    descricaoDetalhada: "Detailed Description",
    aplicacao: "Application",
    status: "Active",

    detalhe: {
        processo: 'Proccess',
        aplica: 'Apply',
        obrigatorioObservacao: 'Observation Required',
        obrigatorioCavidade1: 'Cavity 1 Required',
        obrigatorioCavidade2: 'Cavity 2 Required',
    }
}

tables.paradaLinhaTurno = {
    parada: 'Detener',
    turno: 'Cambio',
    linha: 'Línea',
    planta: 'Planta',
    horaInicial: 'Hora Inicio',
    horaFinal: 'Hora Final'
}

tables.processo = {
    descricao: 'Descripción',
    unidadeNegocio: 'Unidad negocio',
    statusDescricao: 'Situación'
}

tables.checklistItemIdioma = {
    id: 'Id',
    idioma: 'Idioma',
    descricao: 'Descripción',
    descricaoPadrao: 'Descripción estándar',
    arquivoOperador: 'Archivo Operador',
    arquivoManutencao: 'Archivo Mantenimiento'
}

tables.cliente = {
    id: 'Id',
    nome: 'Nombre'
}

tables.checkListTipo = {
    desc: 'Descripción',
    descDepto: 'Departamento'
}

tables.checklistModeloPlanta = {
    DescricaoPlanta: 'Ubicación de la planta',
    Frequencia: 'Frecuencia'
}

tables.henkaten = {
    data: 'Fecha',
    tact: 'Takt Time',
    turno: 'Cambio',
    linha: 'Linea',
    planta: 'Planta'
}

tables.henkatenEvento = {
    categoria: 'Categoria',
    evento: 'Evento',
    observacao: 'Observación',
    hora: 'Hora',
    funcionario: 'Empleado',
    matricula: 'Registro',
    posto: 'Posto'
}

tables.turno = {
    id: 'Id',
    numero: 'Número',
    descricao: 'Descripción de turno',
}

tables.funcionario = {
    id: 'Id',
    nome: 'Nombre',
    matricula: 'Registro',
    idStatusGeral: 'Estado General',
    dataCriacao: 'Fecha de Creación',
    dataAlteracao: 'Cambiar fecha',
    idDominio: 'Dominio',
    idUsuarioAlteracao: 'Cambio de usuario',
    hostName: 'HostName',
    idEmpresa: 'Empresa',
    idPlanta: 'Planta',
    descricaoStatusGeral: 'Estado general',
    descricaoUsuarioAlteracao: 'Usuario',
    descricaoEmpresa: 'Empresa',
    descricaoPlanta: 'Planta',
    descricaoProcesso: 'Processo',
    descricaoDominio: 'Dominio',
    observacao: 'Nota'

}

tables.linhaPosto = {
    codigo: 'Código',
    postoDescricao: 'Puesto',
    statusDescricao: 'Situación',
    areaDescricao: 'Zona',
    tipoAreaDescricao: 'Área de tipo'
}

tables.FuncionarioPostoLinha = {
    nome: 'Nombre',
    matricula: 'Registro',
    observacao: 'Nota',
    posto: 'Puesto'
}

tables.consultaCheck = {
    dataCriacao: 'Creación de fecha y hora',
    operador: 'Operador',
    cliente: 'Cliente',
    turno: 'Turno',
    equipamento: 'Equipo',
    status: 'Estado',
    duracao: 'Duración',
    planta: 'Planta'
}

tables.catalogoDefeito = {
    id: "Id",
    codigo: "Código",
    descricao: "Descripción",
    obrigatorioObservacao: "Nota Requerida",
    idStatusGeral: "Id Status",
    idIdioma: "Id Idioma",
    descricaoStatusGeral: "Status",
    descricaoDominio: "Dominio",
    descricaoUsuario: "Usuario",
    descricaoIdioma: "Idioma"
}

tables.catalogoDefeitoComponente = {
    id: "Id",
    codigo: "Código",
    descricaoComponente: "Descripción",
    status: "Status"
}

tables.classificacaoRetrabalho = {
    id: "Id",
    descricaoCurta: "Breve descripción",
    descricaoLonga: "Descripción larga",
    cor: "Color",
    geraEtiqueta: "Genera etiqueta de retrabajo",
    descricaoStatus: "Situación",
    ativo: "Activo"
}

tables.modeloMaquina = {
    id: "Id",
    modelo: "Modelo"
}

tables.tempoSwt = {
    modeloMaq: 'Machine model',
    codigo: 'Cód Yms Comb.',
    combinacao: 'Combination',
    fixo: 'Fixed',
    variavel: 'Variable',
    dataHoraModif: 'Modification date',
    usuario: 'User'
}

tables.tipoEquipamento = {
    id: "Id",
    descricao: "Description",
}

tables.tipoIntegracao = {
    id: "Id",
    descricao: "Description",
    status: "Status"
}

tables.item = {
    codigo: 'Code',
    descricao: 'Description',
    tipoItem: 'Código Tipo'
}

tables.aplicador = {
    identificador: 'Identifier',
    modeloAplicador: 'Model',
    terminal: 'Terminal',
    cravacaoIsolante: 'Insulation Crimp',
    tipoFerramental: 'Crimping Die',

    codigoPecaAAN: 'A/AN Piece Code',
    ferriteCondutorAAN: 'A/AN Conductor Ferrite',
    ferriteIsolanteAAN: 'A/AN Insulation Ferrite',
    codigoPecaW: 'W Piece Code',
    ferriteCondutorW: 'W Conductor Ferrite',
    ferriteIsolanteW: 'W Insulation Ferrite',
    codigoPecaI: 'I Piece Code',
    ferriteCondutorI: 'I Conductor Ferrite',
    ferriteIsolanteI: 'I Insulation Ferrite',
    codigoPecaQAP: 'Q/AP Piece Code',
    ferriteCondutorQAP: 'Q/AP Conductor Ferrite',
    ferriteIsolanteQAP: 'Q/AP Insulation Ferrite',

    imobilizado: {
        imobilizado: 'Property',
        planta: 'Plant',
    }
}

tables.tipoIntegracao = {
    id: "Id",
    processoDescricao: "Process",
    tipoIntegracaoDescricao: "Integration Type",
    descricao: "Description",
    status: "Status"
}

tables.tipoEtiqueta = {
    descricao: "Description",
    status: "Status"
}

tables.layoutEtiqueta = {
    id: "Id",
    idTipoEtiqueta: "Label Type",
    urlLayout: "Layout",
    descricao: "Description",
    tipoEtiqueta: "Label Type",
    status: "Status"
}

tables.tipoEtiquetaIntegracao = {
    id: "Id",
    integracao: "Integration",
    tipoEtiqueta: "Label Type",
    status: "Status"

}

tables.acompanhamentoSolicitacoes = {
    id: 'Id',
    idRota: 'Route Id',
    dados: 'Data',
    rota: 'Route',
    descricaoStatus: 'Status',
    planta: 'Plant',
    dataHora: 'Date/Time',
    usuario: 'User',
    verboHttp: 'HTTP Verb',
    tentativasIntegracao: 'Attempts at Integration'
}

tables.acompanhamentoSolicitacoesDetalhe = {
    descricaoErro: 'Error description',
    dataHora: 'Date/Time',
}

tables.acompanhamentoImportacoes = {
    id: 'Id',
    dataCriacao: 'Import Date/Time',
    arquivo: 'File',
    descricaoStatus: 'Status',
    nomeUsuario: 'User Name',
    detalhes: 'Messages',
    mensagensLabel: 'View',
}

tables.atividade = {
    id: "Id",
    atividade: 'Activity performed',
};

tables.operacaoFamiliaTipoItemProcesso = {
    id: "Id",
    idOperacao: 'Operation Id',
    codigoOperacaoEtiqueta: 'Operation Code',
    descricaoOperacao: 'Description',
    idFamiliaTipoItemProcesso: 'Family / Item Type / Process / SA Type',
    dataAlteracao: 'Update Date',
    linhasRepetidas: 'Repeated Lines with same [Operation and Family / Item Type / Process / SA Type]'
};

tables.modeloPaco = {
    codigoPaco: 'Code Paco',
    descricao: 'Description',
    qtdeLinha: 'Qua. Line',
    qtdeColuna: 'Qua. Column',
    ativo: 'Active'
};

tables.codigosOperacao = {
    codigo: 'Operation Code',
    codigoOperacaoEtiqueta: 'OES Operation Code',
    descricao: 'Description',
    validaCondicaoCorte: 'Validates Cut Condition',
};

tables.tolerancia = {
    titulo: 'Tolerance',
    condutor: "C/H - Conductor Tolerance Crimp/Height ±",
    isolante: "C/H - Insulation Tolerance Crimp/Height ±"
}

tables.tipoAlimentacao = {
    descricao: 'Description',
};

tables.tipoFerramental = {
    descricao: 'Description',

    mensagens: {
        descricaoJaExiste: 'The description [descricao] has already been registered',
    }
};

tables.modeloAplicador = {
    descricao: 'Model',
    isolante: 'Insulating crimping',
    ferramental: 'Tooling'
};

tables.provePin = {
    tamanho: 'Size',
    cor: 'Color',
    altura: 'Height',
    comprimento: 'Length'
};

tables.cravacaoIsolante = {
    descricao: 'Description',
    imagem: "Image"
};

tables.aplicadorHistorico = {
    descricao: 'Description',
    codigoItem: 'Terminal',
    descricaoModeloAplicador: 'Applicator Model',
    descricaoCravacaoIsolante: 'Insulating Crimping',
    descricaoTipoFerramental: 'Tooling type',
    descricaoTipoAlimentacao: 'Food Type',
    critico: 'Critical',
    codigoPecaAAN: 'Part Code A/AN',
    ferriteCondutorAAN: 'Ferr. Conductor(mm) A/AN',
    ferriteIsolanteAAN: 'Ferr. Insulating(mm) A/AN',
    codigoPecaW: 'Part Code W',
    ferriteCondutorW: 'Ferr. Conductor(mm) W',
    ferriteIsolanteW: 'Ferr. Insulating(mm) W',
    codigoPecaI: 'Part Code I',
    ferriteCondutorI: 'Ferr. Conductor(mm) I',
    ferriteIsolanteI: 'Ferr. Insulating(mm) I',
    codigoPecaQAP: 'Part Code Q/AP',
    ferriteCondutorQAP: 'Ferr. Conductor(mm) Q/AP',
    ferriteIsolanteQAP: 'Ferr. Insulating(mm) Q/AP',
    identificador: 'Identifier',
    usuarioData: 'User - Date'

};

tables.padraoCravacao = {
    codigoControle: "Code Control",
    terminal: "Terminal",
    selo: "Seal",
    aplicador: "Applicator",
    cravacaoIsolante: "Insulation Crimp",
    tipoFerramental: "Crimping Die",
    usuarioDataHora: "User e last modified date",
    status: "Status"
};

tables.padraoCravacaoDadosCabo = {
    idCabo: "Id Cable",
    desc: "Desc",
    tipoCabo: "Type Size",
    bitola: "Wire",
    revisao: "Revision",
    chCondutor: "C/H Condutor",
    chIsolante: "C/H Insulation",
    tracao: "Pull Force",
    wa: "WA"
};

tables.padraoCravacaoDadosCaboPlantaCliente = {
    descricaoPlanta: 'Planta',
    descricaoCliente: 'Cliente',
    dataAlteracao: 'Change Date'
};

tables.Terminal = {
    material: 'Material',
    desponte: 'Desponte',
    dataAlteracao: 'Data Alteração'
};

tables.padraoCravacaoDadosTerminalHistorico = {
    valorTorcao: 'Twist',
    valorCima: 'Up',
    valorBaixo: 'Low',
    valorLateral: 'Side',
    toleranciaCWCondutorConcatenado: 'Tolerance',
    cWCondutor: 'CW Conductor',
    toleranciaCWIsolanteConcatenado: 'Tolerance',
    cWIsolante: 'CW Insulating',
    toleranciaCWEstabilizadorConcatenado: 'Tolerance',
    cWEstabilizador: 'CW Stabilizer',
    toleranciaBellMouthFrontConcatenado: 'Tolerance',
    bellMouthFront: 'Bell Front',
    toleranciaBellMouthRearConcatenado: 'Tolerance',
    bellMouthRear: 'Bell Hear',
    toleranciaCuttingOFFConcatenado: 'Tolerance',
    cuttingOFF: 'Cutting Off',
    toleranciaBrushConcatenado: 'Tolerance',
    brush: 'Brush',
    imagem: 'Terminal Drawing',
    usuarioData: 'User - Date'
};

tables.util = {
    selecione_uma_opcao: "select an option",
    erro_ao_carregar: "Error trying to load data",
    integracao: "Integration",
    anexo: "Attachment",
    excluir: "Delete",
    arquivo_enviado_com_sucesso: "File uploaded successfully.",
    salvo_com_sucesso: 'Saved successfully',
    sucesso: 'Success',
    erro: 'Error',
    verifique_as_celulas_destacadas: "Check the highlighted cells.",
    etiqueta: "Tag",
    excluido_com_sucesso: "Successfully deleted",
    resposta: "Return",
    erro_ao_processar_solicitacao: "Error processing request",
    nao_pode_ser_vazio: "Must not be empty",
    item_nao_encontrado: "Item not found",
    somente_status_pendente: 'Editing is only allowed for Pending Status',
    status_nao_foi_alterado: 'Status has not changed.',
    informe_um_motivo: 'Enter a reason',
    cancelado: 'Canceled',
    concluido: 'Completed',
    pendente: 'Pending',
    atencao: "Warning",
    nenhumItemFoiSelecionado: 'No Items Selected',
    todos: 'All',
    sim: 'Yes',
    nao: 'No',
    asLinhasDetacadasSeraoRemovidas: "The lines highlighted in orange are incomplete and will be removed before proceeding. Do you want to continue?",
    removerLinhasIncompletas: "Remove incomplete lines",
    cancelarEVoltarParaCorrigir: "Cancel and go back to fix",
    errosNoGrid: "Inconsistencies Found",
    foramDetectados: "were detected",
    correcaoNecessaria: "Fix is ​​needed to save system data",
    nenhum: "None",
    cadastroEstaSendoUtilizado: "The register is being used.",
    preenchidoParcialmente: "Partially filled items",
    verifiqueLinhasDestacadas: "Check the highlighted lines.",
    jaExisteUmRegistroNoSistema: "A record already exists in the system"

};

tables.JEasy = {
    atencao: "Warning",
    asLinhasDetacadasSeraoRemovidas: "The lines highlighted in orange are incomplete and will be removed before proceeding. Do you want to continue?",
    removerLinhasIncompletas: "Remove incomplete lines",
    cancelarEVoltarParaCorrigir: "Cancel and go back to fix"
}