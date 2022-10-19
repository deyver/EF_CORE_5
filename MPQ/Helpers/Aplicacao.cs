using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MPQ.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MPQ.Helpers
{
    public class Aplicacao : IAplicacao
    {
        private readonly IHttpContextAccessor _contextAcessor;
        private readonly IConfiguration _config;

        public Aplicacao(IHttpContextAccessor contextAcessor, IConfiguration config)
        {
            this._contextAcessor = contextAcessor;
            this._config = config;
        }

        public Guid GetDominio()
        {
            string[] cookieValue = GetCookie();

            if (cookieValue == null || cookieValue.Length == 0)
            {
                return Guid.Empty;
            }

            Guid idDominio = Guid.Parse(cookieValue[0]);

            return idDominio;
        }

        public Guid GetPlanta()
        {
            string[] cookieValue = GetCookie();

            if (cookieValue == null || cookieValue.Length == 0)
            {
                return Guid.Empty;
            }

            Guid idPlanta = Guid.Parse(cookieValue[2]);

            return idPlanta;
        }

        public Guid GetUsuario()
        {
            string[] cookieValue = GetCookie();

            if (cookieValue == null || cookieValue.Length == 0)
            {
                return Guid.Empty;
            }

            Guid idUsuario = Guid.Parse(cookieValue[6]);

            return idUsuario;
        }

        public Guid GetIdioma()
        {
            string[] cookieValue = GetCookie();

            if (cookieValue == null || cookieValue.Length == 0)
            {
                return Guid.Empty;
            }

            Guid id = Guid.Parse(cookieValue[5]);

            return id;
        }

        public string NomeUsuarioDisplay()
        {
            string[] cookieValue = GetCookie();

            if (cookieValue == null || cookieValue.Length == 0)
            {
                return string.Empty;
            }

            string display = $"{cookieValue[1]} - {cookieValue[3]}";

            return display;
        }

        public bool CookieValido()
        {
            var cookieLogin = this._contextAcessor.HttpContext.Request.Cookies["mpq_login_session"];
            return !string.IsNullOrEmpty(cookieLogin);
        }

        string[] GetCookie()
        {
            string cookie = this._contextAcessor.HttpContext.Request.Cookies["mpq_login_session"];

            string[] cookieValue = StringUtils.Base64Decode(cookie).Split('\\');

            return cookieValue;
        }

        public string GetLogin()
        {
            string[] cookieValue = GetCookie();
            string login = cookieValue[1];
            return login;
        }

        public string GetHostName()
        {
            try
            {
                var hostEntry = Dns.GetHostEntry(_contextAcessor.HttpContext.Connection.RemoteIpAddress);
                return hostEntry.HostName.Split(".").FirstOrDefault();
            }
            catch
            {
                return string.Empty;
            }
        }

        public Guid GetStatusAtivo()
        {
            var v = this._config[key: "CadastrosPadrao:StatusAtivo"];

            if (Guid.TryParse(input: v, result: out Guid r))
                return r;

            return Guid.Empty;
        }

        public Guid GetStatusInativo()
        {
            var v = this._config[key: "CadastrosPadrao:StatusInativo"];

            if (Guid.TryParse(input: v, result: out Guid r))
                return r;

            return Guid.Empty;
        }

        public string GetAppToken()
        {
            return $"{_config["AcessoMPQ:Nome"]}:{_config["AcessoMPQ:Token"]}";
        }
    }
}