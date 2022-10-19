using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Utils.Cache
{
    public interface ISessionHelper
    {

        void Adicionar(string key, object valor);
        T RecuperarValor<T>(string key) where T : class;

    }
}
