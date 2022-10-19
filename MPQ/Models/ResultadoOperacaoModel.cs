using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Models
{
    public class ResultadoOperacaoModel
    {
        public bool Sucesso { get; private set; }
        public string Auxiliar { get; private set; }

        public ResultadoOperacaoModel SetErro(string msg)
        {
            this.Auxiliar = msg;
            this.Sucesso = false;
            return this;
        }

        public ResultadoOperacaoModel SetErro(Exception ex)
        {
            this.Auxiliar = $"{ex.Message}. Detalhes: {ex.InnerException?.Message}";
            this.Sucesso = false;
            return this;
        }

        public ResultadoOperacaoModel SetSucesso(string msg = "")
        {
            this.Auxiliar = msg;
            this.Sucesso = true;
            return this;
        }
    }
}
