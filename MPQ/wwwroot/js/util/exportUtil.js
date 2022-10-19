function ExportarXlsx(campos = '*', nomeArquivo = 'arquivo', dados) {
    alasql(`SELECT ${campos} INTO XLSX("${nomeArquivo}.xlsx", {headers: true}) FROM ?`, [dados]);
}

function exportarExcel(dados, titulos, nomeArquivo) {
    let arrayDados = [];

    let titulosTratados = [];
    titulos.map(obj => {
        if (obj instanceof Date) {
            // Ajuste por conta de bug no componente xlsx.js (getTimeoffset não é preciso), está decrementando 28 segundos na hora de converter para Excel
            const campoData = fixPrecisionLoss(obj);

            titulosTratados.push(campoData);
            return;
        }

        titulosTratados.push(obj);
    });

    arrayDados.push(titulosTratados);

    dados.map(obj => {
        let linha = [];

        const campos = Object.keys(obj);

        campos.map(nomeCampo => {
            const campo = obj[nomeCampo];

            if (campo instanceof Date) {
                // Ajuste por conta de bug no componente xlsx.js (getTimeoffset não é preciso), está decrementando 28 segundos na hora de converter para Excel
                const campoData = fixPrecisionLoss(campo);

                linha.push(campoData);
                return;
            }

            linha.push(campo);
        });

        arrayDados.push(linha);
    });

    const dadosExportar = {
        data: arrayDados,
        fileName: nomeArquivo,
    };

    exportarXlsx(dadosExportar);
}

//function exportarExcel(dados, titulos, nomeArquivo) {
//    let arrayDados = [];

//    arrayDados.push(titulos);

//    dados.map(obj => {
//        let linha = [];

//        const campos = Object.keys(obj);

//        campos.map(nomeCampo => {
//            const campo = obj[nomeCampo];

//            if (campo instanceof Date) {
//                // Ajuste por conta de bug no componente xlsx.js (getTimeoffset não é preciso), está decrementando 28 segundos na hora de converter para Excel
//                const campoData = fixPrecisionLoss(campo);

//                linha.push(campoData);
//                return;
//            }

//            linha.push(campo);
//        });

//        arrayDados.push(linha);
//    });

//    const dadosExportar = {
//        data: arrayDados,
//        fileName: nomeArquivo,
//    };

//    exportarXlsx(dadosExportar);
//}

function exportarExcelGrid(grid, campos, nomeArquivo, titulos, formatarValorCampo) {
    let dados = [];

    if (titulos === undefined || titulos === null) {
        titulos = $(grid).datagrid('options').columns[0]
            .filter(obj => campos.find(q => obj.field === q))
            .map((obj, i) => (obj.title || '').trim());
    }

    dados.push(titulos);

    $(grid).datagrid('getRows').map(obj => {
        let linha = [];

        campos.map(nomeCampo => {
            let campo = obj[nomeCampo];

            if (typeof campo === 'string' && /\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}/g.test(campo)) {
                campoData = fixPrecisionLoss(new Date(campo)); // Ajuste por conta de bug no componente xlsx.js (getTimeoffset não é preciso), está decrementando 28 segundos na hora de converter para Excel

                linha.push(campoData);
                return;
            }

            if (formatarValorCampo !== undefined && formatarValorCampo !== null)
                campo = formatarValorCampo(campo);

            linha.push(campo);
        });

        dados.push(linha);
    });

    const dadosExportar = {
        data: dados,
        fileName: nomeArquivo
    };

    exportarXlsx(dadosExportar);
}

function exportarXlsx({ data, fileName = 'Workbook', sheetName = 'Sheet 1' }) {
    var workSheet = XLSX.utils.aoa_to_sheet(data);

    workSheet['!cols'] = fitToColumn(data);

    // A workbook is the name given to an Excel file
    let wb = XLSX.utils.book_new(); // make Workbook of Excel

    // add Worksheet to Workbook
    // Workbook contains one or more worksheets
    XLSX.utils.book_append_sheet(wb, workSheet, sheetName);

    // export Excel file
    XLSX.writeFile(wb, fileName + '.xlsx');
}

// Funções para ajustar as datas para serem exportadas para xlsx por conta de bug no getTimeoffset do javascript
const exportBugHotfixDiff = (function () {
    const basedate = new Date(1899, 11, 30, 0, 0, 0);
    const dnthreshAsIs = (new Date().getTimezoneOffset() - basedate.getTimezoneOffset()) * 60000;
    const dnthreshToBe = getTimezoneOffsetMS(new Date()) - getTimezoneOffsetMS(basedate);
    return dnthreshAsIs - dnthreshToBe;
}());

function getTimezoneOffsetMS(date) {
    var time = date.getTime();
    var utcTime = Date.UTC(date.getFullYear(),
        date.getMonth(),
        date.getDate(),
        date.getHours(),
        date.getMinutes(),
        date.getSeconds(),
        date.getMilliseconds());
    return time - utcTime;
}

function fixPrecisionLoss(date) {
    return (new Date(date.getTime() + exportBugHotfixDiff)); // Acrescenta os segundos perdidos, por conta do bug, na conversão de datas para exportação
}

function fitToColumn(arrayOfArray) {
    // get maximum character of each column
    return arrayOfArray[0].map((a, i) => ({
        wch: Math.max(...arrayOfArray.map(a2 => {
            if (!a2[i])
                return 0;

            if (a2[i] instanceof Date)
                return a2[i].toLocaleDateString().length + 2;

            return a2[i].toString().length + 2;
        }))
    }));
}
