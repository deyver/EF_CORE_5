using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Helpers
{
    public interface IAutenticacaoHelper
    {

        void GravaSessao(string valor);
        string Get();

    }
}
