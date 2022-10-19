using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Helpers
{
    public class AutenticacaoCookieHelper : IAutenticacaoHelper
    {
        private readonly IHttpContextAccessor _contextAcessor;
        const string cookieKey = "mpq_login_session";

        public AutenticacaoCookieHelper(IHttpContextAccessor contextAcessor)
        {
            this._contextAcessor = contextAcessor;
        }

        public string Get()
        {
            var cookie = _contextAcessor.HttpContext.Request.Cookies[cookieKey];

            if(cookie != null)
            {
                this.GravaSessao(valor: cookie);
            }

            return cookie;
        }

        public void GravaSessao(string valor)
        {
            var tsMinute = new TimeSpan(0, 30, 0);

            var option = new CookieOptions
            {
                Expires = DateTime.Now + tsMinute
            };

            this._contextAcessor.HttpContext.Response.Cookies.Append("mpq_login_session", valor, option);
        }
    }
}
