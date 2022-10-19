using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPQ.Models;

namespace MPQ.Utils.Permissao
{
    public interface IPermissaoHelper
    {
        void ArmazenarPermissoes(IEnumerable<MenuViewModel> permissoes);
        bool PossuiPermissao(string login, string planta, string dominio, string pagina);
    }
}
