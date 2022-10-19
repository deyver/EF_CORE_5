using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Models.WebServices
{
    public class RetornoWS
    {
        public int Status { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public dynamic Dados { get; set; }
        public Guid Id { get; set; }

        public RetornoWS()
        {
        }

        public RetornoWS(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }
}
