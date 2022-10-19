using Microsoft.AspNetCore.Http;
using MPQ.Data.Repositories;
using MPQ.Domain;
using MPQ.Models;
using MPQ.Utils;
using MPQ.Utils.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Helpers
{
    public class UsuarioAplicacaoSession : IUsuarioAplicacao
    {
        private readonly ISessionHelper _sessionHelper;
        //private readonly IUsuarioService _usuarioService;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IUserRepository _userRepository;

        const string keySession = "usuarioLogado";

        public UsuarioAplicacaoSession(
            ISessionHelper sessionHelper,
            /*IUsuarioService usuarioService,*/
            IUserRepository userRepository,
            IHttpContextAccessor httpAccessor)
        {
            this._sessionHelper = sessionHelper;
            //this._usuarioService = usuarioService;
            this._httpAccessor = httpAccessor;
            this._userRepository = userRepository;
        }

        public void Armazenar(UsuarioViewModel user)
        {
            this._sessionHelper.Adicionar(key: keySession, valor: user);
        }

        public UsuarioViewModel Recuperar()
        {
            var usuario = this._sessionHelper.RecuperarValor<UsuarioViewModel>(keySession);
            var user = new User();

            if (usuario == null)
            {
                var cookie = this._httpAccessor.HttpContext.Request.Cookies["mpq_login_session"];
                if (cookie == null) { return null; }

                var cookieValue = StringUtils.Base64Decode(cookie).Split('\\');
                user = _userRepository.GetByLoginAsync(cookieValue[1]).Result;                
            }

            return new UsuarioViewModel() { Id = user.Id, Login = user.Login, Name = user.Name, DefaultSiteId = user.DefaultSiteId, DefaultLanguage = user.DefaultLanguage, Status = user.Status }; ;
        }
    }
}
