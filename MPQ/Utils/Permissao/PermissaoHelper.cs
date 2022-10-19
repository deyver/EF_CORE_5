using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPQ.Models;
using MPQ.Utils.Cache;

namespace MPQ.Utils.Permissao
{
    public class PermissaoHelper : IPermissaoHelper
    {
        const string _keySession = "permissaoUsuario";

        private readonly ISessionHelper _sessionHelper;

        public PermissaoHelper(ISessionHelper sessionHelper)
        {
            this._sessionHelper = sessionHelper;
        }

        public void ArmazenarPermissoes(IEnumerable<MenuViewModel> permissoes)
        {
            this._sessionHelper.Adicionar(key: _keySession, valor: permissoes);
        }

        public bool PossuiPermissao(string login, string planta, string dominio, string pagina)
        {
            var permissoesSession = this._sessionHelper.RecuperarValor<IEnumerable<MenuViewModel>>(_keySession);
            string _pagina = pagina.ToLower();

            return permissoesSession != null && permissoesSession.Count(
                i => i.Url.Replace(@"\", "").ToLower() == _pagina) > 0;
        }
    }
}
