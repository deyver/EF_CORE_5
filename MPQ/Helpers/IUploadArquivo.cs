using Microsoft.AspNetCore.Http;
using MPQ.Models;

namespace MPQ.Helpers
{
    public interface IUploadArquivo
    {
        ResultadoOperacaoModel Salvar(IFormFile arquivo);
    }
}