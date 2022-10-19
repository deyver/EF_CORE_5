var tables = {};

tables.usuario = {
    loginRede: 'Usuario de red',
    dominio: 'Dominio',
    nome: 'Nombre',
    email: 'Email',
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
    nome: 'Nombre'
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
    name: 'Nombre',
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
    urlBase: 'Url Base',
    posicao: 'Posicion'
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
    editar: 'Editar',
    excluir: 'Eliminar',
    imprimir: 'Imprimir',
    sim: 'Sí',
    nao: 'No',
    adicionar: 'Añadir',
    remover: 'Retirar',
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

tables.permissaoSistema = {
    id: "Id",
    nome: "Nombre",
    descricao: "Descripción",
    nomeExibicao: "Nombre de exibición",
    icone: "Icono",
    nomeSistema: "Nombre del sistema",
    urlBase: "URL Base",
    codSistema: "Código Sistema"
}

tables.perfilAcesso = {
    id: "Id",
    idGrupoPermissao: "Grupo de permisos",
    descricao: "Descripción",
    icone: "Icono",
    posicaoX: "Posición X",
    posicaoY: "Posición Y",
    idStatusGeral: "Status",
    dataCriacao: "Fecha de creación",
    dataAlteracao: "Cambiar fecha",
    idDominio: "Dominio",
    idUsuarioAlteracao: "Usuario",
    hostName: "HostName",
    grupoPermissao: "Grupo de permisos",
    status: "Status",
    dominio: "Dominio",
    usuarioAlteracao: "Usuario"
}

tables.matrizDefeito = {
    id: "Id",
    codigoMatriz: "Código de Matriz",
    descricao: "Descripción",
    descricaoDetalhada: "Descripción detallada",
    aplicacao: "Aplicación",
    status: "Activo",

    detalhe: {
        processo: 'Proceso',
        aplica: 'Aplicas',
        obrigatorioObservacao: 'Observación Requerida',
        obrigatorioCavidade1: 'Cavidad 1 Requerida',
        obrigatorioCavidade2: 'Cavidad 2 Requerida',
    }
}

tables.combinacao = {
    codigoYms: "Código Yms",
    codigoBoleta: "Código Boleta",
    descricao: "Descripción"
}

tables.modeloMaquina = {
    id: "Id",
    modelo: "Modelo"
}

tables.tempoSwt = {
    modeloMaq: 'Modelo Máquina',
    codigo: 'Cód Yms Comb.',
    combinacao: 'Combinación',
    fixo: 'Fijo',
    variavel: 'Variable',
    dataHoraModif: 'Fecha de modificación',
    usuario: 'Usuario'
}

tables.tipoEquipamento = {
    id: "Id",
    descricao: "Descripción",
}

tables.tipoIntegracao = {
    id: "Id",
    descricao: "Descripción",
    status: "Status"
}

tables.item = {
    codigo: 'Código',
    descricao: 'Descripción',
    tipoItem: 'Código Tipo'
}

tables.aplicador = {
    identificador: 'Identificador',
    modeloAplicador: 'Modelo',
    terminal: 'Terminal',
    cravacaoIsolante: 'Aislante',
    tipoFerramental: 'Herramental',

    codigoPecaAAN: 'Código Pieza A/AN',
    ferriteCondutorAAN: 'Ferrito Conductor A/AN',
    ferriteIsolanteAAN: 'Ferrito Aislante A/AN',
    codigoPecaW: 'Código Pieza W',
    ferriteCondutorW: 'Ferrito Conductor W',
    ferriteIsolanteW: 'Ferrito Aislante W',
    codigoPecaI: 'Código Pieza I',
    ferriteCondutorI: 'Ferrito Conductor I',
    ferriteIsolanteI: 'Ferrito Aislante I',
    codigoPecaQAP: 'Código Pieza Q/AP',
    ferriteCondutorQAP: 'Ferrito Conductor Q/AP',
    ferriteIsolanteQAP: 'Ferrito Aislante Q/AP',

    imobilizado: {
        imobilizado: 'Inmovilizado',
        planta: 'Planta',
    }
}

tables.tipoIntegracao = {
    id: "Id",
    processoDescricao: "Proceso",
    tipoIntegracaoDescricao: "Tipo Integración",
    descricao: "Descripción",
    status: "Status"
}

tables.tipoEtiqueta = {
    descricao: "Descripción",
    status: "Status"
}

tables.layoutEtiqueta = {
    id: "Id",
    idTipoEtiqueta: "Tipo de Etiqueta",
    urlLayout: "Deseño",
    descricao: "Descripcion",
    tipoEtiqueta: "Tipo de Etiqueta",
    status: "Estado"
}

tables.tipoEtiquetaIntegracao = {
    id: "Id",
    integracao: "Integracion",
    tipoEtiqueta: "Tipo de Etiqueta",
    status: "Estado"
}

tables.acompanhamentoSolicitacoes = {
    id: 'Id',
    idRota: 'Id Ruta',
    dados: 'Datos',
    rota: 'Ruta',
    descricaoStatus: 'Situación',
    planta: 'Planta',
    dataHora: 'Data/Hora',
    usuario: 'Usuario',
    verboHttp: 'Verbo HTTP',
    tentativasIntegracao: 'Intentos de integración'
}

tables.acompanhamentoSolicitacoesDetalhe = {
    descricaoErro: 'Descripción del error',
    dataHora: 'Fecha/Hora',
}

tables.acompanhamentoImportacoes = {
    id: 'Id',
    dataCriacao: 'Fecha/Hora Importación',
    arquivo: 'Archivo',
    descricaoStatus: 'Status',
    nomeUsuario: 'Nombre de Usuario',
    detalhes: 'Mensajes',
    mensagensLabel: 'Visualizar',
}

tables.atividade = {
    id: "Id",
    atividade: 'Actividad realizada',
};

tables.operacaoFamiliaTipoItemProcesso = {
    id: "Id",
    idOperacao: 'Id de Operación',
    codigoOperacaoEtiqueta: 'Codigo de Operación',
    descricaoOperacao: 'Descripción',
    idFamiliaTipoItemProcesso: 'Familia / Tipo de Artículo / Proceso / Tipo SA',
    dataAlteracao: 'Fecha de Actualización',
    linhasRepetidas: 'Líneas repetidas con mismo [Operación y Familia / Tipo de Artículo / Proceso / Tipo SA]'
};

tables.modeloPaco = {
    codigoPaco: 'Código Paco',
    descricao: 'Descripción',
    qtdeLinha: 'Can. Línea',
    qtdeColuna: 'Can. Columna',
    ativo: 'Ativo'
};

tables.codigosOperacao = {
    codigo: 'Código Operación',
    codigoOperacaoEtiqueta: 'Código Operación OES',
    descricao: 'Descripción',
    validaCondicaoCorte: 'Valida la Condición de Corte',
};

tables.tolerancia = {
    titulo: 'Tolerancia',
    condutor: "Tolerancia C/H - Conductor Crimp/Height ±",
    isolante: "Tolerancia C/H - Aislante Crimp/Height ±"
}


tables.tipoAlimentacao = {
    descricao: 'Descripción',
};

tables.tipoFerramental = {
    descricao: 'Descripción',

    mensagens: {
        descricaoJaExiste: 'La descripción [descricao] ya ha sido registrada',
    }
};

tables.modeloAplicador = {
    descricao: 'Modelo',
    isolante: 'Engarzado aislante',
    ferramental: 'Estampación'
};

tables.provePin = {
    tamanho: 'Talla',
    cor: 'Color',
    altura: 'Altura',
    comprimento: 'Largo'
};

tables.cravacaoIsolante = {
    descricao: 'Descripción',
    imagem: "Imagen"
};

tables.aplicadorHistorico = {
    descricao: 'Descripción',
    codigoItem: 'Terminal',
    descricaoModeloAplicador: 'Modelo Aplicador',
    descricaoCravacaoIsolante: 'Engarzado Aislante',
    descricaoTipoFerramental: 'Tipo Herramienta',
    descricaoTipoAlimentacao: 'Tipo Alimentación',
    critico: 'Crítico',
    codigoPecaAAN: 'Código Pieza A/AN',
    ferriteCondutorAAN: 'Ferr. Conductor(mm) A/AN',
    ferriteIsolanteAAN: 'Ferr. Aislante(mm) A/AN',
    codigoPecaW: 'Código Pieza W',
    ferriteCondutorW: 'Ferr. Conductor(mm) W',
    ferriteIsolanteW: 'Ferr. Aislante(mm) W',
    codigoPecaI: 'Código Pieza I',
    ferriteCondutorI: 'Ferr. Conductor(mm) I',
    ferriteIsolanteI: 'Ferr. Aislante(mm) I',
    codigoPecaQAP: 'Código Pieza Q/AP',
    ferriteCondutorQAP: 'Ferr. Conductor(mm) Q/AP',
    ferriteIsolanteQAP: 'Ferr. Aislante(mm) Q/AP',
    identificador: 'Identificador',
    usuarioData: 'Usuario - Fecha'

};

tables.padraoCravacao = {
    codigoControle: "Código de Control",
    terminal: "Terminal",
    selo: "Sello",
    aplicador: "Aplicador",
    cravacaoIsolante: "Aislante",
    tipoFerramental: "Herramental",
    usuarioDataHora: "Usuario e Fecha de última modificación",
    status: "Status"
};

tables.padraoCravacaoDadosCabo = {
    idCabo: "Id Cable",
    desc: "Desc",
    tipoCabo: "Tipo Cable",
    bitola: "Calibre",
    revisao: "Revisión",
    chCondutor: "C/H Conductor",
    chIsolante: "C/H Aislante",
    tracao: "Tensión",
    wa: "WA"
};

tables.padraoCravacaoDadosCaboPlantaCliente = {
    descricaoPlanta: 'Planta',
    descricaoCliente: 'Cliente',
    dataAlteracao: 'Cambiar Fecha'
};

tables.Terminal = {
    material: 'Material',
    desponte: 'Desponte',
    dataAlteracao: 'Data Alteração'
};

tables.padraoCravacaoDadosTerminalHistorico = {
    valorTorcao: 'Giro',
    valorCima: 'Hasta',
    valorBaixo: 'Bajo',
    valorLateral: 'Lado',
    toleranciaCWCondutorConcatenado: 'Tolerancia',
    cWCondutor: 'CW Conductor',
    toleranciaCWIsolanteConcatenado: 'Tolerancia',
    cWIsolante: 'CW Aislante',
    toleranciaCWEstabilizadorConcatenado: 'Tolerancia',
    cWEstabilizador: 'CW Estabilizador',
    toleranciaBellMouthFrontConcatenado: 'Tolerancia',
    bellMouthFront: 'Bell Front',
    toleranciaBellMouthRearConcatenado: 'Tolerancia',
    bellMouthRear: 'Bell Hear',
    toleranciaCuttingOFFConcatenado: 'Tolerancia',
    cuttingOFF: 'Cutting Off',
    toleranciaBrushConcatenado: 'Tolerancia',
    brush: 'Brush',
    imagem: 'Dibujo Terminal',
    usuarioData: 'Usuario - Fecha'
};

tables.util = {
    selecione_uma_opcao: "Seleccione una opción",
    erro_ao_carregar: "Error al intentar cargar datos",
    integracao: "Integracion",
    anexo: "Adjunto archivo",
    excluir: "Borrar",
    arquivo_enviado_com_sucesso: "Documento cargado exitosamente.",
    salvo_com_sucesso: 'Guardado exitosamente',
    sucesso: 'Éxito',
    erro: 'Error',
    verifique_as_celulas_destacadas: "Verifique las celdas resaltadas.",
    etiqueta: "Etiqueta",
    excluido_com_sucesso: "Eliminado con éxito.",
    resposta: "Respuesta",
    erro_ao_processar_solicitacao: "Error al procesar la solicitud",
    nao_pode_ser_vazio: "Es obligatorio",
    item_nao_encontrado: "Objeto no encontrado",
    somente_status_pendente: 'La edición solo está permitida para el estado pendiente',
    status_nao_foi_alterado: 'El estado no ha cambiado.',
    informe_um_motivo: 'Ingrese un motivo',
    cancelado: 'Cancelado',
    concluido: 'Completado',
    pendente: 'Pendiente',
    atencao: "Atención",
    nenhumItemFoiSelecionado: 'No hay elementos seleccionados',
    todos: 'Todo',
    sim: 'Si',
    nao: 'No',
    asLinhasDetacadasSeraoRemovidas: "Las líneas resaltadas en naranja están incompletas y se eliminarán antes de continuar. Desea continuar?",
    removerLinhasIncompletas: "eliminar líneas incompletas",
    cancelarEVoltarParaCorrigir: "Cancelar y volver a arreglar",
    errosNoGrid: "Inconsistencias encontradas",
    foramDetectados: "fueron detectados",
    correcaoNecessaria: "Se necesita una corrección para guardar los datos de sistema.",
    nenhum: "Ningún",
    cadastroEstaSendoUtilizado: "El catastro está siendo utilizado.",
    preenchidoParcialmente: "Artículos parcialmente llenos",
    verifiqueLinhasDestacadas: "Compruebe las líneas resaltadas.",
    jaExisteUmRegistroNoSistema: "Ya existe un registro en el sistema"

};

tables.JEasy = {
    atencao: "Aviso",
    asLinhasDetacadasSeraoRemovidas: "Las líneas resaltadas en naranja están incompletas y se eliminarán antes de continuar. Desea continuar?",
    removerLinhasIncompletas: "Eliminar líneas incompletas",
    cancelarEVoltarParaCorrigir: "Cancelar y volver a arreglar"
}