using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MPQ.Models;

namespace MPQ.Helpers
{
    public class SalvarArquivoLocalHelper : IUploadArquivo
    {
        private readonly IConfiguration _configuration;

        public SalvarArquivoLocalHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public ResultadoOperacaoModel Salvar(IFormFile arquivo)
        {

            ResultadoOperacaoModel retorno = new ResultadoOperacaoModel() { };
            string _vDir = this._configuration[key: "Paths:Uploads"];
            if (string.IsNullOrEmpty(_vDir))
            {
                return retorno.SetErro(msg: "Não foi configurado diretório para upload (Paths:Uploads");
            }

            long tamanhoArquivos = arquivo.Length;
            var caminhoArquivo = Path.GetTempFileName();

            if (arquivo != null && arquivo.Length >= 0)
            {
                string nomeArquivo = arquivo.FileName;
                string caminhoDestinoArquivoOriginal = $"{_vDir}\\{DateTime.Now.ToString("ddMMyy_hhmmss")}_{nomeArquivo}";


                try
                {
                    if (!Directory.Exists(_vDir))
                    {
                        Directory.CreateDirectory(_vDir);
                    }
                    using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                    {
                        arquivo.CopyTo(stream);
                    }

                    retorno.SetSucesso(msg: caminhoDestinoArquivoOriginal);
                }
                catch (Exception ex)
                {
                    retorno.SetErro(ex: ex);
                }

            }

            return retorno;
        }

    }
}
